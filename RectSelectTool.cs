using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapGIS.GISControl;
using MapGIS.GeoMap;
using MapGIS.GeoDataBase;
using MapGIS.GeoObjects.Att;
using MapGIS.GeoObjects.Geometry;

namespace PrecisionControlApp
{
    class RectSelectTool
    {
        Rect rect = new Rect();//框选矩形
        static public List<IVectorCls> selClsList = null;
        static public List<RecordSet> rcdSetList = null;
        static public List<List<string>> querylist = null;
        static SelectSet set = null; //选择集对象
        //初始化
        static MainForm MainForm = new MainForm();

        //打开数据库获取oid列表
        static public void Getoid(string sfClsname, out List<long> oidlist)
        {
            oidlist = new List<long>();
            string url = "gdbp://MapGISLocalPlus/JZFK_GDB/" + sfClsname;
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            sFeatureCls1.Open(url);//固定数据库
            int k = 1;
            while (k < 200)
            {
                if (sFeatureCls1.GetAtt(k).GetValue(0) != null)
                {
                    oidlist.Add((long)k);
                }
                k++;
            }
            sFeatureCls1.Close();
        }

        static public void Getoid(out List<long> oidlist)
        {
            oidlist = new List<long>();
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/controlpoints");//固定数据库
            int k = 1;
            while (k < 100)
            {
                if (sFeatureCls1.GetAtt(k).GetValue(1) != null)
                {
                    oidlist.Add((long)k);
                }
                k++;
            }
            sFeatureCls1.Close();
        }

        //获取查询要素oid
        static public void GetSelectOid(Rect rect, out List<long> oidList)
        {
            QueryDef def = null; //查询条件
            SelectOption option = null;
            Transformation trans = null;

            ObjectID oid = new ObjectID();
            selClsList = new List<IVectorCls>();
            rcdSetList = new List<RecordSet>();
            trans = MainForm.mapCtrl.Transformation;
            //查询对象
            def = new QueryDef();
            //矩形查询
            def.SetRect(rect, SpaQueryMode.Contain);

            option = new SelectOption();
            //类型是点的图层均属于查询范围
            option.DataType = SelectDataType.Pnt;
            //当前地图中所有图层
            option.LayerCtrl = SelectLayerControl.All;
            //多选
            option.SelMode = SelectMode.Multiply;
            //结果数据累加
            option.UnMode = UnionMode.Add;
            if (set != null)
            { set.Clear(); }//去除之前不能消除的查询值
            //查询
            set = MainForm.map.Select(def, true, trans, option);
            //获取查询结果集
            //获取选择集列表
            List<SelectSetItem> selSetList = set.Get();
            //if (selSetList == null || selSetList.Count == 0)
            //    return;
            selSetList.Distinct();//去除重复元素
            oidList = new List<long>();
            if (selSetList.Count == 0)
                return;
            for (int i = 0; i < selSetList[0].IDList.Count; i++)
            {
                //获取处于编辑状态第一个图层的要素ID列表
                oidList.Add(selSetList[0].IDList[i]);
            }
        }

        //根据oid获得面属性
        static public void GetQueryFromOid2(long oid, out List<string> querylist2)
        {
            querylist2 = new List<string>();
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/multipolygons");//固定数据库
            string name = sFeatureCls1.GetAtt(oid).GetValue("name").ToString();
            string prank = sFeatureCls1.GetAtt(oid).GetValue(1).ToString();
            string pscope = sFeatureCls1.GetAtt(oid).GetValue(2).ToString();
            string worknum = sFeatureCls1.GetAtt(oid).GetValue(3).ToString();
            string workstate = sFeatureCls1.GetAtt(oid).GetValue(4).ToString();
            string personnum = sFeatureCls1.GetAtt(oid).GetValue(5).ToString();
            querylist2.Add(oid.ToString());
            querylist2.Add(name);
            querylist2.Add(prank);
            querylist2.Add(pscope);
            querylist2.Add(worknum);
            querylist2.Add(workstate);
            querylist2.Add(personnum);
            sFeatureCls1.Close();
        }

        //获取查询集结果
        static public void GetRectSelectQuery(MapControl mapControl, Map map1, Rect rect, out List<IVectorCls> selClsList, out List<RecordSet> rcdSetList, out List<List<string>> querylist)
        {
            //mapCtrl.Dock = DockStyle.Fill;
            //this.Map.Controls.Add(mapCtrl); //把MapControl控件承载在Panel控件上面
            // //打开待查询的地图
            // //该地图包含多个图层
            //doc = new Document();
            //doc.Open(@"D:\MapGIS compete\MAPGIS文档\地图文档稿2.mapx");
            //maps = doc.GetMaps();
            //map = maps.GetMap(0);
            //mapCtrl.ActiveMap = map;
            //mapCtrl.Restore();

            //初始化
            //查询相关变量
            QueryDef def = null; //查询条件
            SelectOption option = null;
            Transformation trans = null;

            ObjectID oid = new ObjectID();
            selClsList = new List<IVectorCls>();
            rcdSetList = new List<RecordSet>();
            List<string> querylistlist = new List<string>();//单个属性的列表
            querylist = new List<List<string>>();//所有类型属性的列表
            trans = mapControl.Transformation;
            //查询对象
            def = new QueryDef();
            //矩形查询
            def.SetRect(rect, SpaQueryMode.Contain);

            option = new SelectOption();
            //类型是点的图层均属于查询范围
            option.DataType = SelectDataType.Pnt;
            //当前地图中所有图层
            option.LayerCtrl = SelectLayerControl.All;
            //多选
            option.SelMode = SelectMode.Multiply;
            //结果数据累加
            option.UnMode = UnionMode.Add;
            if (set != null)
            { set.Clear(); }//去除之前不能消除的查询值
            //查询
            set = map1.Select(def, true, trans, option);
            //获取查询结果集
            //获取选择集列表
            List<SelectSetItem> selSetList = set.Get();
            if (selSetList == null || selSetList.Count == 0)
                return;
            selSetList.Distinct();
            for (int i = 0; i < selSetList.Count; i++)
            {
                //获取图层
                MapLayer maplayer = selSetList[i].Layer;
                //获取处于编辑状态第一个图层的要素ID列表
                List<long> oidList = selSetList[i].IDList;
                //获取图层对应的类的信息
                string url = maplayer.URL;
                SFeatureCls sFeatureCls = new SFeatureCls();
                sFeatureCls.Open(url);
                IVectorCls selCls = maplayer.GetData() as IVectorCls;

                //获取oid及其对应的属性值
                ObjectIDs oids = new ObjectIDs();
                Fields fields = new Fields();
                Field field = new Field();
                for (int id = 0; id < oidList.Count; id++)
                {
                    oid.Int64Val = oidList[id];
                    oids.Append(oid);
                    Record record = sFeatureCls.GetAtt(oid.Int64Val);
                    //获取字段名
                    //fields = record.Fields;
                    //for(int k=0;k<6;k++)
                    //{
                    //    field = fields.GetItem(k);
                    //    string fie = field.FieldName;
                    //    querylistlist.Add(fie);
                    //}
                    string name = (string)record.GetValue("pname");
                    string type = (string)record.GetValue("geomtype");
                    string prank = record.GetValue("pointrank").ToString();
                    string pscope = record.GetValue("pscope").ToString();
                    querylistlist.Add(name);
                    querylistlist.Add(type);
                    querylistlist.Add(prank);
                    querylistlist.Add(pscope);
                }
                //获取结果集
                RecordSet rcdSet = new RecordSet(selCls);
                rcdSet.AddSet(oids);

                //记录图层和结果集
                selClsList.Add(selCls);
                rcdSetList.Add(rcdSet);
                querylist.Add(querylistlist);
                sFeatureCls.Close();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="clslist">选中的目的类列表</param>
        /// <param name="rcdsetlist">对应目的类中选中的结果集列表</param>
        //将查询结果（属性）显示在窗口中
        //最终需要调用的查询函数
        static public void RectSelectTool1(MapControl mapControl, Rect rect, Map map)
        {
            //List<IVectorCls> selClsList = null;//查询图层列表
            //List<RecordSet> rcdSetList = null; //对应图层的结果集列表
            //List<List<string>> querylist = null;
            GetRectSelectQuery(mapControl, map, rect, out selClsList, out rcdSetList, out querylist);//获取查询信息结果
            mapControl.FlashSelectSet();//闪烁查询结果
        }
    }
}
