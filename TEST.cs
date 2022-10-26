using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapGIS.GeoDataBase.Net;
using MapGIS.GeoDataBase;
using MapGIS.GeoObjects.Geometry;
using MapGIS.GISControl;
using MapGIS.GeoMap;
using System.IO;

namespace PrecisionControlApp
{
    class TEST
    {
        static public void test1()
        {
            //定义变量
            DataBase gdb = null;
            Server gdbServer = new Server();
            NetAnalyse netAnalyse = new NetAnalyse();
            NetCls neyCls = null;
            List<NetElement> lstElement = new List<NetElement>();
            List<PathInfo> lstPathInfo = new List<PathInfo>();

            //连接数据源，打开数据库 
            bool rtn = gdbServer.Connect("MapGISLocalPlus", "", "");
            if (rtn)
            {
                gdb = gdbServer.OpenGDB("JZFK_GDB");
                if (gdb != null)
                {
                    //打开网络类
                    neyCls = new NetCls(gdb);
                    rtn = neyCls.Open("roadlines", 0);
                    if (rtn)
                    {
                        //变量初始化
                        bool ifCantour = false;
                        bool ifCanRound = false;

                        //将网标加入网标列表
                        Dot pos = new Dot(12720046.29, 3581256.34);
                        NetElement itemElement = null;
                        int elementID = 0;
                        //根据坐标点获取网络结点ID号
                        elementID = neyCls.GetNearNode(pos, 1);
                        itemElement = new NetElement();
                        itemElement.ElemID = elementID;
                        //网标类型为点上网标
                        itemElement.Type = ElemType.NodeElem;
                        //网标类型为路径点
                        itemElement.IsFlag = true;
                        lstElement.Add(itemElement);

                        pos = new Dot(12720172.12, 3581419.06);
                        //根据坐标点获取网络结点ID号
                        elementID = neyCls.GetNearNode(pos, 1);
                        itemElement = new NetElement();
                        itemElement.ElemID = elementID;
                        itemElement.Type = ElemType.NodeElem;
                        //网标类型为路径点
                        itemElement.IsFlag = false;
                        lstElement.Add(itemElement);

                        pos = new Dot(12719763.71, 3581930.87);
                        //根据坐标点获取网络结点ID号
                        elementID = neyCls.GetNearNode(pos, 1);
                        itemElement = new NetElement();
                        itemElement.ElemID = elementID;
                        itemElement.Type = ElemType.NodeElem;
                        //网标类型为路径点
                        itemElement.IsFlag = true;
                        lstElement.Add(itemElement);

                        //设置网络类
                        netAnalyse.Cls = neyCls;
                        //设置网标列表
                        netAnalyse.Element = lstElement;
                        //路径分析
                        PathAnalyRtn pathAnalyRtn = netAnalyse.PathAnalyse(ifCantour, ifCanRound, out lstPathInfo);
                        List<int> nodes = null;
                        for (int i = 0; i < lstPathInfo.Count; i++)
                        {
                            nodes = lstPathInfo[i].Nodes;
                        }
                        MessageBox.Show(nodes.ToString());
                        //关闭类
                        neyCls.Close();
                    }
                    else
                    {
                        MessageBox.Show("打开网络类失败！");
                    }
                    //关闭数据库
                    gdb.Close();
                }
                else
                {
                    MessageBox.Show("打开地理数据库失败！");
                }
                //断开数据源连接
                gdbServer.DisConnect();
            }
            else
            {
                MessageBox.Show("连接数据源不成功！");
            }
        }

        static public void test2()
        {
            //地图视图控件
            MapControl mapControl = new MapControl();
            //地图文档对象
            Document doc = new Document();

            mapControl.Dock = DockStyle.Fill;
            //this.splitContainer1.Panel2.Controls.Add(mapControl);
            //doc.Open(@"C:\MapGIS 10\Sample\地图文档.mapx");
            //mapControl.ActiveMap = doc.GetMaps().GetMap(0);

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "图层样式(*.lyrsty)|*.lyrsty";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofd.FileName;
                if (!File.Exists(fileName))
                {
                    MessageBox.Show("所选图层样式文件不存在，请正确选择样式文件！", "提示");
                    return;
                }

                //图层样式信息列表
                List<LayerStyleInfo> styleInfos = new List<LayerStyleInfo>();
                string mapName = "";
                MapStyleTool.ParseMapStyle(File.ReadAllText(fileName, Encoding.UTF8), out styleInfos, out mapName);

                MapLayer layer = mapControl.ActiveMap.get_Layer(0);
                long styleFlag = 0;
                styleFlag |= (MapStyleTool.AttributeFlag & ~MapStyleTool.LyrSrsFlag & ~MapStyleTool.LyrSysLibFlag
                              & ~MapStyleTool.LyrStatFlag) | MapStyleTool.DisAttFlag;

                bool bContinue = true;
                long rtn = MapStyleTool.ImportLayerStyle(layer, File.ReadAllText(fileName, System.Text.Encoding.UTF8), styleFlag, 1);
                if (rtn == MapStyleTool.MatchErrorVersion)
                {
                    if (MessageBox.Show("样式版本不匹配，您是否要继续应用样式？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                        bContinue = false;
                }
                else if (rtn == MapStyleTool.MatchErrorLyrType)
                {
                    if (MessageBox.Show("图层类型不匹配，您是否要继续应用样式？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                        bContinue = false;
                }
                else if (rtn == MapStyleTool.MatchErrorNotFindSystemLib)
                {
                    if (MessageBox.Show("找不到系统库，您是否要继续应用样式？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                        bContinue = false;
                }
                if (bContinue)
                {
                    //this.Close();
                }
            }
        }
    }
}
