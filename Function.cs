using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MapGIS.UI.Controls;
using MapGIS.GeoMap;
using MapGIS.GeoDataBase;
using MapGIS.GeoObjects.Geometry;
using MapGIS.GeoObjects.Info;
using MapGIS.GeoObjects.Att;
using MapGIS.Analysis.SpatialAnalysis;
using MapGIS.GeoObjects;

namespace PrecisionControlApp
{
    class Function
    {

        static public VectorLayer addPointLayer(DataBase db, string name)
        {
            //修改于——2022.8.31 XL
            //添加简单要素类(点)到图层,返回图层
            IVectorCls vectorClsPoint = null;
            vectorClsPoint = db.GetXClass(XClsType.SFCls) as IVectorCls;
            vectorClsPoint.Open(name, 0);

            //for (int i = 1; i <= vectorClsPoint.Count; i++)
            //{
            //    //设置点的图形信息
            //    PntInfo pntInfo = new PntInfo();
            //    int[] outcl = new int[3] { 10, 10, 10 };
            //    float[] outpen = new float[3] { 5, 5, 5 };
            //    pntInfo.SymID = 1;
            //    pntInfo.OutClr = outcl;
            //    pntInfo.Width = 5;
            //    pntInfo.Height = 5;
            //    //pntInfo.Height = 0.001F;
            //    pntInfo.OutPenW = outpen;
            //    vectorClsPoint.UpdateInfo(i, pntInfo);
            //}

            VectorLayer vectorLayerPoint = null;
            vectorLayerPoint = new VectorLayer(VectorLayerType.SFclsLayer);
            vectorLayerPoint.AttachData(vectorClsPoint);
            vectorLayerPoint.FollowZoom = false;

            //关闭类
            vectorClsPoint.Close();
            return vectorLayerPoint;
        }

        static public VectorLayer addLineLayer(DataBase db, string name)
        {
            //修改于——2022.8.31 XL
            //添加简单要素类(线)到图层,返回图层
            IVectorCls vectorClsLine = null;
            vectorClsLine = db.GetXClass(XClsType.SFCls) as IVectorCls;
            bool rtn = vectorClsLine.Open(name, 0);

            //for (int i = 1; i <= vectorClsLine.Count; i++)
            //{
            //    LinInfo linInfo = new LinInfo();
            //    int[] outcl = new int[3];
            //    outcl[0] = 1463;
            //    float[] outpen = new float[3] { 1, 5, 5 };
            //    linInfo.OutClr = outcl;
            //    linInfo.OutPenW = outpen;
            //    vectorClsLine.UpdateInfo(i, linInfo);
            //}

            VectorLayer vectorLayerLine = null;
            vectorLayerLine = new VectorLayer(VectorLayerType.SFclsLayer);
            vectorLayerLine.AttachData(vectorClsLine);

            //关闭类
            vectorClsLine.Close();
            return vectorLayerLine;
        }

        static public VectorLayer addAreaLayer(DataBase db, string name)
        {
            //修改于——2022.8.31 XL
            //添加简单要素类(面)到图层,返回图层
            IVectorCls vectorClsArea = null;
            vectorClsArea = db.GetXClass(XClsType.SFCls) as IVectorCls;
            vectorClsArea.Open(name, 0);

            //for (int i = 1; i <= vectorClsArea.Count; i++)
            //{
            //    RegInfo regInfo = new RegInfo();
            //    regInfo.FillClr = 545;
            //    regInfo.Transparency = 50;
            //    vectorClsArea.UpdateInfo(i, regInfo);
            //}

            VectorLayer vectorLayerArea = null;
            vectorLayerArea = new VectorLayer(VectorLayerType.SFclsLayer);
            vectorLayerArea.AttachData(vectorClsArea);

            //关闭类
            vectorClsArea.Close();
            return vectorLayerArea;
        }

        /// <summary>
        /// 根据点的位置（地图坐标），返回点附近的点or线or面的oid，未查到则返回-1
        /// </summary>
        /// <param name="type">查询的图层类型</param>
        /// <param name="X">地图坐标 X</param>
        /// <param name="Y">地图坐标 Y</param>
        /// <returns></returns>
        static public long pointGetInfo(string type, double X, double Y)
        {
            Dot dot = new Dot(X, Y);
            QueryDef querydef = new QueryDef();
            querydef.SetNear(dot, 0.2, 0.2);

            SelectOption option = new SelectOption();
            if (type == "point") option.DataType = SelectDataType.Pnt;
            else if (type == "line") option.DataType = SelectDataType.Line;
            else if (type == "area") option.DataType = SelectDataType.Polygon;
            option.LayerCtrl = SelectLayerControl.All;
            option.SelMode = SelectMode.Single;
            option.UnMode = UnionMode.Copy;

            Transformation trans = MainForm.mapCtrl.Transformation;
            SelectSet set = MainForm.map.Select(querydef, true, trans, option);
            List<SelectSetItem> list = new List<SelectSetItem>();
            list = set.Get();
            set.Clear();
            set.Dispose();
            querydef.Dispose();
            trans.Dispose();
            if (list.Count != 0) { return list[0].IDList[0]; }
            else return -1;
        }

        /// <summary>
        /// 通过点的位置，在地图上添加点（即在点的简单要素类中新增一行记录）
        /// 手动输入点的名称、封控等级，所属街道为默认填充
        /// </summary>
        /// <param name="mapCtrl">地图视图控件</param>
        /// <param name="map">地图对象</param>
        /// <param name="X">地图坐标 X</param>
        /// <param name="Y">地图坐标 Y</param>
        static public void addFKD(double X, double Y)
        {
            long areaoid = pointGetInfo("area", X, Y);
            EditFkdInfoForm infoForm = new EditFkdInfoForm("add");
            infoForm.setPscope(areaoid);
            infoForm.StartPosition = FormStartPosition.Manual;
            infoForm.Location = new Point(Cursor.Position.X + 10, Cursor.Position.Y + 10);
            infoForm.ShowDialog();

            if (infoForm.IsFinsh == false) MainForm.mapCtrl.Refresh();
            if (infoForm.IsFinsh == true)
            {
                List<Lockpoint> lockpoints = new List<Lockpoint>();
                infoForm.lockpoint.X = X;
                infoForm.lockpoint.Y = Y;
                lockpoints.Add(infoForm.lockpoint);
                editGDBdata(lockpoints);

            }
        }

        /// <summary>
        /// 通过oid查询，返回给定简单要素类中属性字段的Datatype对象，查询失败返回-1
        /// </summary>
        /// <param name="sfClsname">简单要素类的名称</param>
        /// <param name="oid">记录的oid</param>
        /// <returns></returns>
        static public object getFieldValue(string sfClsname, long oid)
        {
            string url = "gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/" + sfClsname;
            SFeatureCls sFeatureCls = new SFeatureCls();
            sFeatureCls.Open(url);
            Record record = sFeatureCls.GetAtt(oid);
            sFeatureCls.Close();
            if (ISoidExist("sfcls/"+sfClsname, oid) == true)
            {
                if (sfClsname == "controlpoints")
                {
                    Lockpoint lockpoint = new Lockpoint();
                    lockpoint.OID = oid;
                    lockpoint.PNAME = (string)record.GetValue("PNAME");
                    lockpoint.POINTRANK = (int)(double)record.GetValue("POINTRANK");
                    lockpoint.PSCOPE = (int)(double)record.GetValue("PSCOPE");
                    lockpoint.WORKNUM = (int)(double)record.GetValue("WORKNUM");
                    lockpoint.WORKSTATE = (string)record.GetValue("WORKSTATE");
                    lockpoint.PERSONNUM = (int)(double)record.GetValue("PERSONNUM");
                    return lockpoint;
                }
                else if (sfClsname == "multipolygons")
                {
                    Streetarea streetarea = new Streetarea();
                    streetarea.OID = oid;
                    streetarea.NAME = (string)record.GetValue("NAME");
                    streetarea.SUM = (int)(double)record.GetValue("SUM");
                    streetarea.POLYGONRANK = (int)record.GetValue("POLYGONRANK");
                    streetarea.LASTTIME = (DateTime)record.GetValue("LASTTIME");
                    streetarea.RELEASETIME = (DateTime)record.GetValue("RELEASETIME");
                    streetarea.CURRENTTIME = (DateTime)record.GetValue("CURRENTTIME");
                    return streetarea;
                }
                else return -1;

            }
            else return -1;
        }

        /// <summary>
        /// 传入点数据类型的List，对数据库中的数据进行修改（对面的操作暂时未完成）
        /// 若OID已存在，则修改值；若OID不存在，则新增一个点
        /// </summary>
        /// <param name="lockpoints">点数据类型List（可选参数）</param>
        /// <param name="streetareas">面数据类型List（可选参数）</param>
        static public void editGDBdata(List<Lockpoint> lockpoints = null, List<Streetarea> streetareas = null)
        {
            if (lockpoints != null)
            {
                SFeatureCls sFeatureCls = new SFeatureCls();
                sFeatureCls.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/controlpoints");

                for (int i = 0; i < lockpoints.Count; i++)
                {
                    Record record = new Record();
                    if (ISoidExist("sfcls/controlpoints", lockpoints[i].OID)) record = sFeatureCls.GetAtt(lockpoints[i].OID);
                    else
                    {
                        Fields fields = sFeatureCls.Fields;
                        record.Fields = fields;
                    }

                    if (lockpoints[i].PNAME != null) record.SetFldFromStr("PNAME", lockpoints[i].PNAME);
                    if (lockpoints[i].POINTRANK != -1) record.SetFldFromStr("POINTRANK", lockpoints[i].POINTRANK.ToString());
                    //record.SetValue("PSCOPE", lockpoints[i].PSCOPE);
                    if (lockpoints[i].WORKNUM != -1) record.SetFldFromStr("WORKNUM", lockpoints[i].WORKNUM.ToString());
                    if (lockpoints[i].WORKSTATE != null)  record.SetFldFromStr("WORKSTATE", lockpoints[i].WORKSTATE); 
                    if (lockpoints[i].PERSONNUM != -1) record.SetFldFromStr("PERSONNUM", lockpoints[i].PERSONNUM.ToString());

                    if (ISoidExist("sfcls/controlpoints", lockpoints[i].OID))
                    {
                        //图形信息
                        PntInfo pntInfo = new PntInfo();
                        int[] outcl = new int[3] { 10, 4, 3 };
                        pntInfo.OutClr = outcl;
                        float[] outpen = new float[3] { 0.05F, 0.05F, 0.05F };
                        pntInfo.OutPenW = outpen;
                        if (lockpoints[i].POINTRANK == 0) pntInfo.SymID = 530;
                            else if (lockpoints[i].POINTRANK == 1) pntInfo.SymID = 526;
                            else if (lockpoints[i].POINTRANK == 2) pntInfo.SymID = 528;
                            else pntInfo.SymID = 520;
                        pntInfo.Width = 8;
                        pntInfo.Height = 8;
                    if (lockpoints[i].POINTRANK != -1)
                        sFeatureCls.UpdateInfo(lockpoints[i].OID, pntInfo);
                        bool rtn = sFeatureCls.UpdateAtt(lockpoints[i].OID, record);
                        if (rtn) MessageBox.Show("修改属性信息成功！");
                    }
                    else
                    {
                        //图形信息
                        PntInfo pntInfo = new PntInfo();
                        int[] outcl = new int[3] { 10, 4, 3 };
                        pntInfo.OutClr = outcl;
                        float[] outpen = new float[3] { 0.05F, 0.05F, 0.05F };
                        pntInfo.OutPenW = outpen;
                        if (lockpoints[i].POINTRANK == 0) pntInfo.SymID = 530;
                        else if (lockpoints[i].POINTRANK == 1) pntInfo.SymID = 526;
                        else if (lockpoints[i].POINTRANK == 2) pntInfo.SymID = 528;
                        else  pntInfo.SymID = 520;
                        pntInfo.Width = 8;
                        pntInfo.Height = 8;

                        //几何信息
                        Dot3D dot = new Dot3D(lockpoints[i].X, lockpoints[i].Y, 0);
                        GeoPoints points = new GeoPoints();
                        points.Append(dot);

                        record.SetValue("PSCOPE", lockpoints[i].PSCOPE);
                        long oid = sFeatureCls.Append(points, record, pntInfo);
                        MainForm.mapCtrl.Refresh();
                        if (oid > 0) MessageBox.Show("添加封控点成功！");
                    }
                }
                sFeatureCls.Close();
            }
            else if(streetareas!=null)
            {

            }
        }

        /// <summary>
        /// 判断输入的点OID是否存在，存在返回true，不存在返回false
        /// </summary>
        /// <param name="oid">点要素的OID</param>
        /// <returns></returns> 
        static public bool ISoidExist(string name, long oid)
        {
            //List<long> oidlist = new List<long>();
            //Rect rect = new Rect(12715565, 3575983, 12724557, 3586295);
            //RectSelectTool.GetSelectOid(rect, out oidlist);
            RectSelectTool.Getoid(name, out List<long> oidlist);
            for (int i = 0; i < oidlist.Count; i++)
            {
                if (oid == oidlist[i]) return true;
            }
            return false;
        }

        /// <summary>
        /// 传入OID数值，删除一个点要素
        /// </summary>
        /// <param name="oid">OID</param>
        static public void delOnePoint(long oid)
        {
            SFeatureCls sFeatureCls = new SFeatureCls();
            sFeatureCls.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/controlpoints");
            sFeatureCls.Delete(oid);
            sFeatureCls.Close();
        }

        /// <summary>
        /// 根据点要素的OID，返回点附近的拓扑点的OID
        /// </summary>
        /// <param name="oid">controlpoints中点的OID</param>
        /// <returns></returns>
        static public List<long> getTopoNod(long oid)
        {
            List<long> oidlist = new List<long>();

            //通过OID 获取点的几何信息（即坐标）
            SFeatureCls sFeatureCls = new SFeatureCls();
            sFeatureCls.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/controlpoints");
            IGeometry geom = sFeatureCls.GetGeometry(oid);
            GeoPoints gemopoint = geom as GeoPoints;
            Dot3D dot = gemopoint.GetItem(0);
            sFeatureCls.Close();

            //根据坐标查询附近的拓扑点的OID
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/ds/道路网/sfcls/roadlines_TopoNod");
            QueryDef queryDef = new QueryDef();
            double range = 300; //点附近上下左右各300米
            Rect rect = new Rect(dot.X - range, dot.Y - range, dot.X + range, dot.Y + range);
            queryDef.SetRect(rect, SpaQueryMode.Intersect);
            RecordSet recordSet = sFeatureCls1.Select(queryDef);
            for (int i = 0; i < recordSet.Count; i++)
            {
                recordSet.MoveNext();
                oidlist.Add(recordSet.CurrentID);
            }
            sFeatureCls1.Close();

            return oidlist;
        }

        /// <summary>
        /// 获取封闭点的OID集合(List)
        /// </summary>
        /// <returns></returns>
        static public List<long> getFBDoidlist()
        {
            List<long> fbdoidlist = new List<long>();

            RectSelectTool.Getoid("sfcls/controlpoints", out List<long> oidlist);
            Lockpoint lockpoint = new Lockpoint();
            for (int i = 0; i < oidlist.Count; i++)
            {
                lockpoint = (Lockpoint)getFieldValue("controlpoints", oidlist[i]);
                if (lockpoint.POINTRANK == 2) { fbdoidlist.Add(oidlist[i]); }
            }
            return fbdoidlist;
        }

        //核酸检测能力缓冲区
        static public void PointBuffer()
        {
            //地理数据库服务器对象
            Server srv = new Server();
            //连接数据源
            srv.Connect("mapgislocalplus", "", "");
            //地理数据库对象
            //打开数据库
            DataBase db = srv.OpenGDB("JZFK_GDB");
            //删除之前存在的缓冲区数据
            DeleteBuffer("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/controlpoints_buffer");
            //打开分析对象
            SFeatureCls sfcls = db.GetXClass(XClsType.SFCls) as SFeatureCls;
            sfcls.Open("controlpoints", 0);
            //结果简单要素类
            //创建结果要素类
            SFeatureCls destSfcls = db.GetXClass(XClsType.SFCls) as SFeatureCls;
            destSfcls.Create("controlpoints_buffer", GeomType.Reg, 0, 0, null);
            //创建缓冲区分析分析对象
            SpatialAnalysis sa = new SpatialAnalysis();
            //设置缓冲区分析分析参数
            SPBufferOption option = new SPBufferOption();
            //设置半径
            SFeatureCls sFeatureCls = new SFeatureCls();
            List<long> oli = new List<long>();
            sFeatureCls.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/controlpoints");
            RectSelectTool.Getoid(out oli);
            for (int k = 0; k < oli.Count; k++)
            {
                Record record = sFeatureCls.GetAtt(oli[k]);
                if (record.GetValue(1).ToString() == "0")
                {
                    switch (record.GetValue(3).ToString())
                    {
                        case "0": option.LeftRad = 0; break;
                        case "2": option.LeftRad = 100; break;
                        case "3": option.LeftRad = 150; break;
                        case "4": option.LeftRad = 200; break;
                        case "5": option.LeftRad = 250; break;
                        case "6": option.LeftRad = 300; break;
                        case "7": option.LeftRad = 350; break;
                        case "8": option.LeftRad = 400; break;
                        default: break;
                    }
                    //设置待缓冲区分析对象
                    ObjectID objId = new ObjectID();
                    objId.Int64Val = oli[k];
                    ObjectIDs objIds = new ObjectIDs();
                    objIds.Append(objId);
                    //执行分析
                    option.IsDissolve = true;
                    sa.SP_Buffer(sfcls, destSfcls, option, objIds);
                }
            }
            updateReginfo(destSfcls);
            //关闭地理要素及数据库，断开服务连接
            sfcls.Close();
            destSfcls.Close();
            db.Close();
            srv.DisConnect();
        }

        //删除缓冲区
        static public void DeleteBuffer(string url)
        {
            GDBURLHelp gDBURLHelp = new GDBURLHelp();
            GDBURLHelp.DeleteXCls(url);
        }

        //生成封闭点的缓冲区
        static public void RankBuffer(List<long> oidli)
        {
            //地理数据库服务器对象
            Server srv = new Server();
            //连接数据源
            srv.Connect("mapgislocalplus", "", "");
            //地理数据库对象
            //打开数据库
            DataBase db = srv.OpenGDB("JZFK_GDB");
            //删除之前存在的缓冲区数据
            DeleteBuffer("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/rankpoints_buffer");
            //打开分析对象
            SFeatureCls sfcls = db.GetXClass(XClsType.SFCls) as SFeatureCls;
            sfcls.Open("controlpoints", 0);
            //结果简单要素类
            //创建结果要素类
            SFeatureCls destSfcls = db.GetXClass(XClsType.SFCls) as SFeatureCls;
            destSfcls.Create("rankpoints_buffer", GeomType.Reg, 0, 0, null);
            //创建缓冲区分析分析对象
            SpatialAnalysis sa = new SpatialAnalysis();
            //设置缓冲区分析分析参数
            SPBufferOption option = new SPBufferOption();
            //设置半径
            SFeatureCls sFeatureCls = new SFeatureCls();
            sFeatureCls.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/controlpoints");
            for (int k = 0; k < oidli.Count; k++)
            {
                Record record = sFeatureCls.GetAtt(oidli[k]);
                option.LeftRad = 300;
                //设置待缓冲区分析对象
                ObjectID objId = new ObjectID();
                objId.Int64Val = oidli[k];
                ObjectIDs objIds = new ObjectIDs();
                objIds.Append(objId);
                //执行分析
                option.IsDissolve = true;
                sa.SP_Buffer(sfcls, destSfcls, option, objIds);
            }

            //关闭地理要素及数据库，断开服务连接
            sfcls.Close();
            destSfcls.Close();
            db.Close();
            srv.DisConnect();
        }

        //设置缓冲区样式
        static public void updateReginfo(SFeatureCls sFeatureCls)
        {
            for (int i = 1; i <= sFeatureCls.Count; i++)
            {
                RegInfo regInfo = new RegInfo();
                regInfo.FillClr = 48;
                regInfo.Transparency = 50;
                sFeatureCls.UpdateInfo(2 * i - 1, regInfo);
            }
        }
    }
}
