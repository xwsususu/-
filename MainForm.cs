using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapGIS.GISControl;
using MapGIS.GeoMap;
using MapGIS.GeoDataBase;
using MapGIS.GeoObjects.Geometry;
using MapGIS.GeoObjects.Info;
using MapGIS.GeoObjects.Att;
using MapGIS.GeoDataBase.Net;
using System.IO;
using System.Drawing.Drawing2D;

namespace PrecisionControlApp
{
    public partial class MainForm : Form
    {
        //定义全局变量
        public static MapControl mapCtrl = new MapControl();
        public static Map map = new Map();
        VectorLayer vectorLayerPoint = new VectorLayer(VectorLayerType.SFclsLayer);
        VectorLayer vectorLayerLine = new VectorLayer(VectorLayerType.SFclsLayer);
        VectorLayer vectorLayerArea = new VectorLayer(VectorLayerType.SFclsLayer);
        VectorLayer vectorLayerArea2 = new VectorLayer(VectorLayerType.SFclsLayer);
        VectorLayer startEndPointLayer = new VectorLayer(VectorLayerType.SFclsLayer);
        VectorLayer rankpointArea = new VectorLayer(VectorLayerType.SFclsLayer);

        DataBase db = null;
        AttForm pointForm = new AttForm("point");
        AttForm areaForm = new AttForm("area");
        bool QueryFormState = false;

        //查询相关变量
        Rect rect = new Rect();//框选矩形
        Display disp = null;
        Rect selectrect = new Rect();//框选矩形对象
        Point point = new Point();//记录鼠标按下点
        double mx, my;//起始点
        double mx2, my2;//终止点
        int startoid = -1, endoid = -1;   //起点和终点附近的拓扑点OID

        //统计相关变量
        ControlPointsQueryCount controlpointsQueryCount = new ControlPointsQueryCount();

        //面编辑相关变量
        string polygon_rank;
        string last_time;

        //记录各功能状态
        bool IsAddFKD = false;  //添加封控点按钮是否按下
        int IsClickTimes = 0;  //记录鼠标点击的次数

        //鼠标左键是否处于按下状态
        bool mousestate;//鼠标按下状态--true按下--false松开

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //初始化地图控件
            //mapCtrl = new MapControl();
            mapCtrl.Dock = DockStyle.Fill;
            mapCtrl.ShowScrollBar = false;
            mapCtrl.ShowRuler = false;
            mapCtrl.ShowContextMenu = false;
            this.Map1.Controls.Add(mapCtrl);

            //随时间自更新
            AutoChangeFromDate();

            //map.IsFixedScalesDisplay = true;

            disp = mapCtrl.Display;

            ////设置固定大小、笔宽
            //disp.FixedPntSize = true;
            //disp.FixedPntPenWidth = true;

            //注册mapCtrl鼠标
            mapCtrl.MouseDown += new MouseEventHandler(mapCtrl_MouseDown);
            mapCtrl.MouseMove += new MouseEventHandler(mapCtrl_MouseMove);
            mapCtrl.MouseUp += new MouseEventHandler(mapCtrl_MouseUp);

            //定义变量
            Server srv = null;

            //连接数据源，打开数据库
            srv = new Server();
            srv.Connect("MapGISLocalPlus", "", "");
            if (srv.GetDBID("JZFK_GDB1") == 0) srv.AttachGDB("JZFK_GDB1", @"JZFK_GDB.hdb", "");
            db = srv.OpenGDB("JZFK_GDB");
            if (db == null)
            {
                MessageBox.Show("打开数据库失败");
                return;
            }

            //点、线、面三个基本图层
            vectorLayerPoint = Function.addPointLayer(db, "controlpoints");
            vectorLayerLine = Function.addLineLayer(db, "lines");
            vectorLayerArea = Function.addAreaLayer(db, "multipolygons");
            rankpointArea = Function.addAreaLayer(db, "rankpoints_buffer");

            //string fileName = "D:\\Desktop\\linestyle.lyrsty";
            string fileName = "linestyle.lyrsty";
            //List<LayerStyleInfo> styleInfos = new List<LayerStyleInfo>();
            //string mapName = "";
            //MapStyleTool.ParseMapStyle(File.ReadAllText(fileName, Encoding.UTF8), out styleInfos, out mapName);
            long styleFlag = 0;
            styleFlag |= (MapStyleTool.AttributeFlag & ~MapStyleTool.LyrSrsFlag & ~MapStyleTool.LyrSysLibFlag
                          & ~MapStyleTool.LyrStatFlag ) | MapStyleTool.DisAttFlag;
            long rtn = MapStyleTool.ImportLayerStyle(vectorLayerLine, File.ReadAllText(fileName, Encoding.UTF8), styleFlag, 0);

            //设置地图初始显示范围为江汉区
            Rect rect = new Rect(12715565, 3575983, 12724557, 3586295);
            map.IsCustomEntireRange = true;
            map.SetEntireRange(rect);

            //服务图层
            ImageLayer serverLayer = new ImageLayer();
            serverLayer.AccessMode = MapServerAccessMode.ServerAndCache;

            MapServerInfo[] serverInfos = MapServer.GetServerInfos();
            MapServerInfo serverInfo = null;

            string type = "AMapMercatorEMap";
            foreach (MapServerInfo info in serverInfos)
            {
                if (info.ServerType == type || info.ServerType.Equals(type))
                {
                    serverInfo = info;
                    break;
                }
            }

            MapServer mapserver = MapServer.CreateInstance(serverInfo);
            serverLayer.MapServer = mapserver;

            //添加图层
            map.Append(serverLayer);
            map.Append(vectorLayerArea);
            map.Append(rankpointArea);
            map.Append(vectorLayerLine);
            map.Append(vectorLayerPoint);

            //激活地图
            mapCtrl.ActiveMap = map;
            mapCtrl.Restore();

            //数据库，断开数据源连接
            db.Close();
            srv.DisConnect();
        }



        //工具栏按钮——复位窗口
        private void resetMap_Click(object sender, EventArgs e)
        {
            mapCtrl.Restore();
        }


        //MapCtrl上鼠标单击事件
        //修改于——2022.9.2 XL
        private void mapCtrl_MouseDown(object sender, MouseEventArgs e)
        {
            mousestate = true;
            if (e.Button == MouseButtons.Left )
            {
                if (this.IsAddFKD == true)
                {
                    EditFkdInfoForm infoForm = new EditFkdInfoForm();

                    //将设备坐标转为标尺坐标
                    Transformation trans = mapCtrl.Transformation;
                    double mx = 0;
                    double my = 0;
                    trans.DpToMp(e.X, e.Y, ref mx, ref my);

                    long areaoid = Function.pointGetInfo("area", mx, my);
                    if (areaoid == -1) MessageBox.Show("不能添加所在区以外的封控点！");
                    else
                    {
                        Dot dot = new Dot(e.X, mapCtrl.Height - e.Y);
                        disp.Begin();
                        disp.Point(dot, 10, 10);
                        disp.End();

                        Function.addFKD(mx, my);
                    }
                    this.IsAddFKD = false;
                }
                if (rectQuery.Checked == true)
                {
                    point.X = e.X;
                    point.Y = e.Y;
                    mx = my = 0;
                    Transformation transformation = mapCtrl.Transformation;
                    transformation.DpToMp(e.X, Map1.Height - e.Y, ref mx, ref my);
                }
                if (queryFKD.Checked == true)
                {
                    Transformation trans = mapCtrl.Transformation;
                    double mx = 0;
                    double my = 0;
                    trans.DpToMp(e.X, Map1.Height - e.Y, ref mx, ref my);

                    long oid = Function.pointGetInfo("point", mx, my);
                    if (oid != -1)
                    {
                        mapCtrl.EndFlash();
                        mapCtrl.PushFocusData(vectorLayerPoint, oid);
                        mapCtrl.StartFlash();
                    }
                    object item = Function.getFieldValue("controlpoints", oid);
                    if (item.GetType() == typeof(Lockpoint))
                    {
                        List<Lockpoint> lockpoints = new List<Lockpoint>();
                        lockpoints.Add((Lockpoint)item);
                        if (pointForm.IsDisposed) pointForm = new AttForm("point");
                        pointForm.addPointInfo(lockpoints);
                        pointForm.Update();
                        pointForm.Show();
                    }
                    else MessageBox.Show("未查询到任何属性信息！");
                }
                if (queryJD.Checked == true)
                {
                    Transformation trans = mapCtrl.Transformation;
                    double mx = 0;
                    double my = 0;
                    trans.DpToMp(e.X, Map1.Height - e.Y, ref mx, ref my);

                    long oid = Function.pointGetInfo("area", mx, my);
                    if (oid != -1)
                    {
                        mapCtrl.EndFlash();
                        mapCtrl.PushFocusData(vectorLayerArea, oid);
                        mapCtrl.StartFlash();
                    }
                    object item = Function.getFieldValue("multipolygons", oid);
                    if (item.GetType() == typeof(Streetarea))
                    {
                        List<Streetarea> streetareas = new List<Streetarea>();
                        streetareas.Add((Streetarea)item);
                        if (areaForm.IsDisposed) areaForm = new AttForm("area");
                        areaForm.addAreaInfo(streetareas);
                        areaForm.Update();
                        areaForm.Show();
                    }
                    else MessageBox.Show("未查询到任何属性信息！");
                }
                if (pathGuide.Checked == true)
                {
                    IsClickTimes += 1;

                    Transformation trans = mapCtrl.Transformation;
                    double mx = 0;
                    double my = 0;
                    trans.DpToMp(e.X, Map1.Height - e.Y, ref mx, ref my);

                    Server srv = new Server();
                    srv.Connect("MapGISLocalPlus", "", "");
                    db = srv.OpenGDB("JZFK_GDB");
                    startEndPointLayer = Function.addPointLayer(db, "startendpoint");
                    map.Append(startEndPointLayer);
                    SFeatureCls sFeatureCls = new SFeatureCls();
                    sFeatureCls.Open("gdbp://MapGISLocalPlus/JZFK_GDB/ds/道路网/sfcls/startendpoint");

                    //几何信息
                    Dot3D dot = new Dot3D(mx, my, 0);
                    GeoPoints points = new GeoPoints();
                    points.Append(dot);

                    //图形信息
                    PntInfo pntInfo = new PntInfo();
                    float[] outpen = new float[3] { 0.05F, 0.05F, 0.05F };
                    pntInfo.OutPenW = outpen;
                    pntInfo.SymID = 363;
                    pntInfo.Width = 8;
                    pntInfo.Height = 8;

                    NetCls neyCls = new NetCls(db);
                    neyCls.Open("roadlines", 0);

                    if (IsClickTimes == 1)
                    {
                        int[] outcl = new int[3] { 7, 4, 3 };
                        pntInfo.OutClr = outcl;
                        sFeatureCls.Append(points, null, pntInfo);
                        mapCtrl.Refresh();

                        Dot pos = new Dot(mx, my);
                        startoid = neyCls.GetNearNode(pos, 300);
                    }
                    if (IsClickTimes == 2)
                    {
                        int[] outcl = new int[3] { 6, 4, 3 };
                        pntInfo.OutClr = outcl;
                        sFeatureCls.Append(points, null, pntInfo);
                        mapCtrl.Refresh();

                        Dot pos = new Dot(mx, my);
                        endoid = neyCls.GetNearNode(pos, 300);

                        List<long> oidlist = new List<long>();
                        List<long> fbdoidlist = Function.getFBDoidlist();
                        for (int i = 0; i < fbdoidlist.Count; i++)
                        {
                            List<long> topooidlist = Function.getTopoNod(fbdoidlist[i]);
                            for (int j = 0; j < topooidlist.Count; j++)
                            { oidlist.Add(topooidlist[j]); }
                        }

                        List<NetElement> lstElement = new List<NetElement>();
                        NetElement itemElement = new NetElement();
                        itemElement.ElemID = startoid;
                        itemElement.Type = ElemType.NodeElem;
                        itemElement.IsFlag = true;
                        lstElement.Add(itemElement);
                        for (int k = 0; k < oidlist.Count; k++)
                        {
                            itemElement = new NetElement();
                            itemElement.ElemID = (int)oidlist[k];
                            itemElement.Type = ElemType.NodeElem;
                            itemElement.IsFlag = false;
                            lstElement.Add(itemElement);
                        }
                        itemElement = new NetElement();
                        itemElement.ElemID = endoid;
                        itemElement.Type = ElemType.NodeElem;
                        itemElement.IsFlag = true;
                        lstElement.Add(itemElement);

                        NetAnalyse netAnalyse = new NetAnalyse();
                        List<PathInfo> lstPathInfo = new List<PathInfo>();
                        netAnalyse.Cls = neyCls;
                        netAnalyse.Element = lstElement;
                        PathAnalyRtn pathAnalyRtn = netAnalyse.PathAnalyse(false, false, out lstPathInfo);
                        List<int> edges = null;
                        for (int i = 0; i < lstPathInfo.Count; i++)
                        { edges = lstPathInfo[i].Edges; }

                        mapCtrl.EndFlash();
                        for (int i = 0; i < edges.Count; i++)
                        { mapCtrl.PushFocusData(vectorLayerLine, edges[i], FocusDataType.HighLight); }
                        mapCtrl.StartFlash();

                    }

                    neyCls.Close();
                    sFeatureCls.Close();
                    db.Close();
                    srv.DisConnect();
                }
            }

        }
        private void mapCtrl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (rectQuery.Checked == true && mx != 0 && mousestate == true)
                {
                    Rect drawrect1 = new Rect(point.X, Map1.Height - point.Y, e.X, Map1.Height - e.Y);//设备坐标与地图坐标系的转换
                    mapCtrl.Refresh();
                    disp.Begin();
                    disp.SetPen(10, 6);
                    //disp.SetBrush(6, 0, 0, 0, 0);
                    //disp.BackClr = 4;
                    //disp.Transparency = 50;
                    disp.Rect(drawrect1);//矩形
                    disp.End();
                }
            }
        }

        private void mapCtrl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && mousestate == true)
            {
                mousestate = false;
                if (rectQuery.Checked == true && mx != 0 && e.Button != MouseButtons.Middle)
                {
                    Transformation transformation = mapCtrl.Transformation;
                    transformation.DpToMp(e.X, Map1.Height - e.Y, ref mx2, ref my2);
                    if(mx>mx2)
                    {
                        double tx, ty;
                        tx = mx2;mx2 = mx;mx = tx;
                        ty = my2; my2 = my; my = ty;
                    }
                    selectrect = new Rect(mx, my2, mx2, my);

                    List<long> vs = new List<long>();//查询结果OID列表
                    List<Lockpoint> lockpoints = new List<Lockpoint>();
                    RectSelectTool.GetSelectOid(selectrect, out vs);
                    mapCtrl.FlashSelectSet();
                    if (vs.Count == 0)
                        return;
                    for (int m = 0; m < vs.Count; m++)
                    {
                        lockpoints.Add((Lockpoint)Function.getFieldValue("controlpoints", vs[m]));//获取一行记录并添加到列表中
                    }
                    if (pointForm.IsDisposed) pointForm = new AttForm("point");
                    pointForm.addPointInfo(lockpoints);
                    pointForm.Update();
                    pointForm.Show();
                }
            }
        }


        private void addFKD_Click(object sender, EventArgs e)
        {
            this.IsAddFKD = true;
        }

        private void rectQuery_Click(object sender, EventArgs e)
        {
            rectQuery.Checked = !rectQuery.Checked;
            mapCtrl.EndFlash();
        }


        private void queryFKD_Click(object sender, EventArgs e)
        {
            queryFKD.Checked = !queryFKD.Checked;
            mapCtrl.EndFlash();
            //bool t = queryFKD.Checked;
            //queryFKD.Checked = !t;
            //IsqueryFKD = !IsqueryFKD;
        }

        private void queryJD_Click(object sender, EventArgs e)
        {
            queryJD.Checked = !queryJD.Checked;
            mapCtrl.EndFlash();
        }

        private void pathGuide_Click(object sender, EventArgs e)
        {
            IsClickTimes = 0;
            pathGuide.Checked = !pathGuide.Checked;
            mapCtrl.EndFlash();

            SFeatureCls sFeatureCls = new SFeatureCls();
            sFeatureCls.Open("gdbp://MapGISLocalPlus/JZFK_GDB/ds/道路网/sfcls/startendpoint");
            RectSelectTool.Getoid("ds/道路网/sfcls/startendpoint", out List<long> oidlist);
            sFeatureCls.Delete(oidlist);

            //SFeatureCls sFeatureCls1 = new SFeatureCls();
            //sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/rankpoints_buffer");
            //if (map.IndexOf(rankpointArea) == -1)
            //{
            //    Function.RankBuffer(Function.getFBDoidlist());


            //    //for (int i = 1; i <= sFeatureCls1.Count; i++)
            //    //{
            //    //    RegInfo regInfo = new RegInfo();
            //    //    regInfo.PatID = 4;
            //    //    regInfo.FillClr = 48;
            //    //    regInfo.Transparency = 50;
            //    //    sFeatureCls1.UpdateInfo(2 * i - 1, regInfo);
            //    //}

            //    rankpointArea.AttachData(sFeatureCls1);
            //    map.Append(rankpointArea);

            //}
            //else map.DragOut(rankpointArea);

            mapCtrl.Refresh();
            sFeatureCls.Close();
            //sFeatureCls1.Close();
            db.Close();
        }

        //设置工具栏的样式
        private void title_Paint(object sender, PaintEventArgs e)
        {
            labeltitle.Location = new Point((title.Width - labeltitle.Width) / 2, (title.Height - labeltitle.Height) / 2);
            buttonbox.Location = new Point(title.Width - buttonbox.Width - 30, (title.Height - buttonbox.Height) / 2);
            toolbox.Location = new Point(30, (title.Height - toolbox.Height) / 2);

            Rectangle rectangle = new Rectangle(0, 0, title.Width, title.Height);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            LinearGradientBrush brush = new LinearGradientBrush(rectangle, System.Drawing.Color.FromArgb(210, 210, 210), System.Drawing.Color.FromArgb(242, 242, 242), LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(brush, rectangle);
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, rectangle, System.Drawing.Color.White, ButtonBorderStyle.Outset);
        }


        private void closebutton_Click(object sender, EventArgs e)
        { this.Close(); }

        private void minbutton_Click(object sender, EventArgs e)
        { this.WindowState = FormWindowState.Minimized; }

        private void normalbutton_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                title.Width = 1144;
            }
            else { this.WindowState = FormWindowState.Maximized; }
        }

        #region 属性编辑按钮点击事件
        private void editFKD(object sender, EventArgs e)
        {
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/controlpoints");//固定数据库

            EditFkdInfoForm infoForm = new EditFkdInfoForm();
            infoForm.ShowDialog();
            //指示编辑窗口输入值符合要求
            if (infoForm.lockpoint.OID != -1 && infoForm.IsFinsh == true)
            {
                List<Lockpoint> lockpoints1 = new List<Lockpoint>();
                lockpoints1.Add(infoForm.lockpoint);
                Function.editGDBdata(lockpoints1);
                if(lockpoints1[0].WORKSTATE!=null)
                {
                    controlpointsQueryCount.warning(mapCtrl, vectorLayerPoint, lockpoints1[0].WORKSTATE, lockpoints1[0].OID);
                    controlpointsQueryCount.SetToZero(lockpoints1[0].WORKSTATE, lockpoints1[0].OID);
                }
                if (lockpoints1[0].PERSONNUM != -1)
                    controlpointsQueryCount.Countpersonnum(mapCtrl, vectorLayerPoint, lockpoints1[0].PERSONNUM, lockpoints1[0].OID);
                if (lockpoints1[0].WORKNUM != -1)
                    controlpointsQueryCount.Countpersonnum(mapCtrl, vectorLayerPoint, lockpoints1[0].OID);
                Lockpoint lockpoint = new Lockpoint();
                List<Lockpoint> lockpoints = new List<Lockpoint>();
                lockpoints.Add((Lockpoint)Function.getFieldValue("controlpoints", infoForm.lockpoint.OID));
                if (pointForm.IsDisposed) pointForm = new AttForm("point");
                pointForm.addPointInfo(lockpoints);
                mapCtrl.Refresh();
                pointForm.Update();
                pointForm.Show();
            }
            sFeatureCls1.Close();
        }

        private void 实时街道检测信息统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SFeatureCls sFeatureCls2 = new SFeatureCls();
            sFeatureCls2.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/countvalue");
            //统计前清空当天检测人数
            for (int k = 1; k < 27; k++)
            {
                Record record2 = sFeatureCls2.GetAtt(k);
                record2.SetFldFromStr(1, "0");
                sFeatureCls2.UpdateAtt(k, record2);
            }
            sFeatureCls2.Close();
            controlpointsQueryCount.CountData();
            List<List<string>> list = new List<List<string>>();
            controlpointsQueryCount.SelectQuery(out list);
            CountForm countForm = new CountForm("实时统计");
            countForm.addQueryInfo(list);
            countForm.Update();
            countForm.Show();
        }

        private void 今日最终检测信息统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr=MessageBox.Show("该统计将会清空今日检测数据，是否确定？","提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr== DialogResult.Yes)
            {
                controlpointsQueryCount.CalPercentage();
                controlpointsQueryCount.CountPolygonCount2();
                controlpointsQueryCount.CountNewSumNum();
                SFeatureCls sFeatureCls1 = new SFeatureCls();
                sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/controlpoints");
                List<long> oidli = new List<long>();
                RectSelectTool.Getoid(out oidli);
                //逐个变更状态为正常的检测点状态
                for (int k = 0; k < oidli.Count; k++)
                {
                    Record record1 = sFeatureCls1.GetAtt(oidli[k]);
                    if (int.Parse(record1.GetValue(1).ToString()) == 0
                        && record1.GetValue(4).ToString() == "正常")
                    {
                        //逐个变更状态
                        record1.SetFldFromStr(4, "休息");
                        record1.SetFldFromStr(5, "0");
                        sFeatureCls1.UpdateAtt(oidli[k], record1);
                    }
                }
                sFeatureCls1.Close();
                List<List<string>> list = new List<List<string>>();
                controlpointsQueryCount.SelectAllQuery(out list);
                CountForm countForm = new CountForm("当天最终统计");
                countForm.addAllQueryInfo(list);
                countForm.Update();
                countForm.Show();
            }
        }

        private void toolStripDropDownButton3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (map.IndexOf(vectorLayerArea2) !=-1)
            {
                map.DragOut(vectorLayerArea2);
                map.Append(vectorLayerPoint);
            }
            else
            {
                map.DragOut(vectorLayerPoint);
                map.Append(vectorLayerArea2);
            }
            Function.PointBuffer();
            SFeatureCls sFeatureCls = new SFeatureCls();
            sFeatureCls.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/controlpoints_buffer");
            vectorLayerArea2.AttachData(sFeatureCls);
            mapCtrl.Refresh();
        }

        private void 街道防控信息修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool update = false;//sum是否发生更新
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/multipolygons");//固定数据库
            QueryAreaEditForm queryAreaEditForm = new QueryAreaEditForm();
            queryAreaEditForm.ShowDialog();
            if (queryAreaEditForm.oidvalue == null)
                return;
            long oid = long.Parse(queryAreaEditForm.oidvalue);//获取oid
            string currenttime = sFeatureCls1.GetAtt(1).GetValue(5).ToString();//获取当前时间
            Record record = sFeatureCls1.GetAtt(oid);
            polygon_rank = record.GetValue(1).ToString();
            last_time = record.GetValue("lasttime").ToString();//获取更新前的最后一例时间(用于非封闭区)
            record.SetFldFromStr("sum", queryAreaEditForm.sum);
            //如果sum值不为0，则最后一例日期更新；同时防控等级更新为封控区
            if (int.Parse(queryAreaEditForm.sum) > 0)
            {
                record.SetFldFromStr("lasttime", currenttime);
                record.SetFldFromStr(1, "2");
                sFeatureCls1.UpdateAtt(oid, record);
                polygon_rank = record.GetValue(1).ToString();
                last_time = record.GetValue("lasttime").ToString();//获取最后一例时间
                update = true;
            }
            //若区域为封闭区，预计解除时间是最后一例日期的14天后。
            if (int.Parse(polygon_rank) == 2 && update == true)
            {
                record.SetFldFromStr(3, DateTime.Parse(last_time).AddDays(14).ToString());
            }
            sFeatureCls1.UpdateAtt(oid, record);
            sFeatureCls1.Close();
            List<string> selectquerylist1 = new List<string>();
            List<Streetarea> streetareas = new List<Streetarea>();
            streetareas.Add((Streetarea)Function.getFieldValue("multipolygons", oid));//获取面属性
            if (areaForm.IsDisposed) areaForm = new AttForm("area");
            areaForm.addAreaInfo(streetareas);
            areaForm.Update();
            areaForm.Show();
            //SelectReturnQuery(selectquerylist1);
        }
        #endregion

        private void buttonTEST_Click(object sender, EventArgs e)
        {
            TEST.test2();
            //List<long> list = Function.getFBDoidlist();
            //int i = 0;
        }

        #region 街道表触发器
        public void AutoChangeRank(Record record,string time)
        {
            string rank= record.GetValue(1).ToString();
            string lasttime = record.GetValue(2).ToString();
            //若区域为管控区，预计解除时间是最后一例日期的21天后。
           if (rank=="1")
            {
                record.SetFldFromStr(3, DateTime.Parse(lasttime).AddDays(21).ToString());
            }
         //封闭区sum值保持为0的14天后，区域等级由封闭区降为管控区。
         //管控区sum值保持为0的21天后，区域等级由管控区降为防范区。
            if (record.GetValue(4).ToString() == "0")
            {
                if (DateTime.Parse(time).Date.Subtract(DateTime.Parse(lasttime).Date).TotalDays > 13 && rank == "2")
                    record.SetFldFromStr(1, "1");
                if (DateTime.Parse(time).Date.Subtract(DateTime.Parse(lasttime).Date).TotalDays > 20 && rank == "1")
                    record.SetFldFromStr(1, "0");
            }
        }
        #endregion

        #region 自定义随日期变化变更状态函数
        public void AutoChangeFromDate()
        {
            //上班了，更新当前时间同时sum值清零
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            SFeatureCls sFeatureCls2 = new SFeatureCls();
            sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/multipolygons");//固定数据库
            sFeatureCls2.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/countvalue");
            string currenttime = sFeatureCls1.GetAtt(1).GetValue(5).ToString();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            //只有当日期变化才会清空数据等操作
            if (DateTime.Now.Date.Subtract(DateTime.Parse(currenttime).Date).TotalDays > 0)
            {
                //上班了，将检测点状态全改为正常
                controlpointsQueryCount.SetStartCount();
                //计算检测点数目
                controlpointsQueryCount.CountMatchPointBelongNum();
                for (int k = 1; k < 27; k++)
                {
                    Record record2 = sFeatureCls2.GetAtt(k);
                    record2.SetFldFromStr(1, "0");
                    sFeatureCls2.UpdateAtt(k, record2);
                    Record record = sFeatureCls1.GetAtt(k);
                    record.SetFldFromStr(5, date);
                    record.SetFldFromStr(4, "0");
                    AutoChangeRank(record,currenttime);
                    sFeatureCls1.UpdateAtt(k, record);
                }
            }
            sFeatureCls1.Close();
            sFeatureCls2.Close();
        }
    #endregion
}
}
