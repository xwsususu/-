using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MapGIS.GISControl;
using MapGIS.GeoMap;
using MapGIS.GeoDataBase;
using MapGIS.GeoObjects.Att;

namespace PrecisionControlApp
{
    class ControlPointsQueryCount
    {
        public long id = 0;//街道索引
        List<List<string>> list1 = new List<List<string>>();//记录需要显示字段的二维列表

        //增派成功提示
        public void Countpersonnum(MapControl mapControl, VectorLayer LayerPoint, long oid)
        {
            if (id == oid)
            {
                mapControl.EndFlash();
                MessageBox.Show("序号为" + id + "的检测点已增派医护人员", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //单个监测点状态变更为休息时，表示当天检测结束，先统计流动人员和工作人员人数，再清零
        public void SetToZero(string state, long oid)
        {
            if (state == "休息")
            {
                CountPolygonDay(oid);
                CountPolygonCount2();
                CalPercentage(id);
            }
        }

        //统计数据(统计检测点数目),将所有正常状态的检测点数据统计
        public void CountData()
        {
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/controlpoints");
            //获取点要素OID列表
            List<long> oidli = new List<long>();
            RectSelectTool.Getoid(out oidli);
            //逐个统计检测点数据
            for (int k = 0; k < oidli.Count; k++)
            {
                Record record1 = sFeatureCls1.GetAtt(oidli[k]);
                long pscope = long.Parse(record1.GetValue(2).ToString());
                if (int.Parse(record1.GetValue(1).ToString()) == 0)
                {
                    CountPolygonDay(oidli[k]);
                    //CalPercentage2(pscope);
                    PredictNum(pscope);
                }
            }
            sFeatureCls1.Close();
        }

        //上班了则统一调为正常状态,前一天检测人数清零
        public void SetStartCount()
        {
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            SFeatureCls sFeatureCls2 = new SFeatureCls();
            sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/controlpoints");
            sFeatureCls2.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/countvalue");
            //前一天检测人数清零
            for (int m = 1; m < 27; m++)
            {
                Record record = sFeatureCls2.GetAtt(m);
                record.SetFldFromStr(1, "0");
                sFeatureCls2.UpdateAtt(m, record);
            }
            sFeatureCls2.Close();
            //获取点要素OID列表
            List<long> oidli = new List<long>();
            RectSelectTool.Getoid(out oidli);
            //逐个统计状态为正常的检测点数据
            for (int k = 0; k < oidli.Count; k++)
            {
                Record record1 = sFeatureCls1.GetAtt(oidli[k]);
                if (int.Parse(record1.GetValue(1).ToString()) == 0
                    && record1.GetValue(4).ToString() == "休息")
                {
                    //逐个变更状态
                    record1.SetFldFromStr(4, "正常");
                    record1.SetFldFromStr(5, "0");
                    sFeatureCls1.UpdateAtt(oidli[k], record1);
                }
            }
            sFeatureCls1.Close();
        }

        //当检测点检出阳性，发出警告，请求工作人员处理
        public void warning(MapControl mapControl, VectorLayer vectorLayer, string state, long oid)
        {
            if (state == "检出阳性")
            {
                mapControl.StartFlash(200, 2, 6, 1);
                mapControl.PushFocusData(vectorLayer, oid);
                MessageBox.Show("序号为 " + oid + " 的核酸点检出阳性，请尽快前往处理！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                mapControl.EndFlash();
        }

        //统计医护人员和流动人数，超限则提醒增派医护人员
        public void Countpersonnum(MapControl mapControl, VectorLayer LayerPoint, int num, long oid)
        {
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            SFeatureCls sFeatureCls2 = new SFeatureCls();
            sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/controlpoints");//固定数据库
            sFeatureCls2.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/setvalue");//阈值集
            int worknum, setworknum, setpersonnum;
            Record record = sFeatureCls1.GetAtt(oid);
            worknum = int.Parse(record.GetValue("worknum").ToString());
            for (long i = 1; i < 6; i++)
            {
                Record record1 = sFeatureCls2.GetAtt(i);
                setworknum = int.Parse(record1.GetValue(0).ToString());
                setpersonnum = int.Parse(record1.GetValue(1).ToString());
                if (worknum <= setworknum && num >= setpersonnum)
                {
                    id = oid;
                    mapControl.StartFlash(800, 3, 4, 0);
                    mapControl.PushFocusData(LayerPoint, oid);
                    MessageBox.Show("序号为 " + oid + " 的核酸点检测人数超出预计，需要增派医护人员！", "提示", MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            sFeatureCls1.Close();
            sFeatureCls2.Close();
        }

        //将需要显示的字段添加到二维列表中（实时）
        public void SelectQuery(out List<List<string>> list1)
        {
            list1 = new List<List<string>>();
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            SFeatureCls sFeatureCls2 = new SFeatureCls();
            sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/multipolygons");
            sFeatureCls2.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/countvalue");
            for (int k = 0; k < 26; k++)
            {
                Record record1 = sFeatureCls1.GetAtt(k + 1);
                Record record2 = sFeatureCls2.GetAtt(k + 1);
                List<string> list = new List<string>();
                list.Add(record1.GetValue(0).ToString());
                list.Add(record1.GetValue(1).ToString());
                list.Add(record1.GetValue(2).ToString());
                list.Add(record1.GetValue(3).ToString());
                list.Add(record1.GetValue(4).ToString());
                list.Add(record1.GetValue(5).ToString());
                list.Add(record2.GetValue(1).ToString());
                list.Add(record2.GetValue(0).ToString());
                list1.Add(list);
            }
            sFeatureCls1.Close();
            sFeatureCls2.Close();
        }
        //将需要显示的字段添加到二维列表中（当天）
        public void SelectAllQuery(out List<List<string>> list2)
        {
            list2 = new List<List<string>>();
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            SFeatureCls sFeatureCls2 = new SFeatureCls();
            sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/multipolygons");
            sFeatureCls2.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/countvalue");
            for (int k = 0; k < 26; k++)
            {
                Record record1 = sFeatureCls1.GetAtt(k + 1);
                Record record2 = sFeatureCls2.GetAtt(k + 1);
                List<string> list = new List<string>();
                list.Add(record1.GetValue(0).ToString());
                list.Add(record1.GetValue(1).ToString());
                list.Add(record1.GetValue(2).ToString());
                list.Add(record1.GetValue(3).ToString());
                list.Add(record1.GetValue(4).ToString());
                list.Add(record1.GetValue(5).ToString());
                list.Add(record2.GetValue(0).ToString());
                list.Add(record2.GetValue(1).ToString());
                list.Add(record2.GetValue(2).ToString());
                list.Add(record2.GetValue(3).ToString());
                list.Add(record2.GetValue(4).ToString());
                list.Add(record2.GetValue(5).ToString());
                list.Add(record2.GetValue(6).ToString());
                list2.Add(list);
            }
            sFeatureCls1.Close();
            sFeatureCls2.Close();
        }

        #region 数据统计
        //统计各街道检测点数目
        public void CountMatchPointBelongNum()
        {
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            SFeatureCls sFeatureCls2 = new SFeatureCls();
            List<long> oidlist = new List<long>();
            List<long> oidl = new List<long>();
            List<int> recordlist = new List<int>();
            sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/countvalue");
            sFeatureCls2.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/controlpoints");
            RectSelectTool.Getoid(out oidlist);
            RectSelectTool.Getoid("sfcls/multipolygons",out oidl);
            //统计需要的封控点属性值
            for (int l = 0; l < oidlist.Count; l++)
            {
                Record record2 = sFeatureCls2.GetAtt(oidlist[l]);
                if (record2.GetValue(1).ToString() == "0")
                {
                    for (int k = 0; k < oidl.Count + 1; k++)
                    {
                        recordlist.Add(0);
                        Record record1 = sFeatureCls1.GetAtt(k);
                        if (record2.GetValue(2).ToString() == k.ToString())
                        {
                            recordlist[k] += 1;
                            record1.SetFldFromStr(5, recordlist[k].ToString());
                        }
                        sFeatureCls1.UpdateAtt(k, record1);
                    }
                }
            }
            sFeatureCls1.Close();
            sFeatureCls2.Close();
        }

        //每日检测人数,流动人数统计与更新(单个状态更新)
        public void CountPolygonDay(long oid)
        {
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            SFeatureCls sFeatureCls2 = new SFeatureCls();
            int actualpersonnum;
            sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/countvalue");
            sFeatureCls2.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/controlpoints");
            //统计需要的封控点属性值
            Record record1 = sFeatureCls2.GetAtt(oid);
            id = long.Parse(record1.GetValue(2).ToString());
            Record record2 = sFeatureCls1.GetAtt(id);
            actualpersonnum = int.Parse(record2.GetValue(1).ToString());
            actualpersonnum += int.Parse(record1.GetValue(5).ToString());
            record2.SetFldFromStr(1, actualpersonnum.ToString());
            //int actualnum = int.Parse(record2.GetValue(5).ToString()) + 1;
            //record2.SetFldFromStr(5, actualnum.ToString()) ;
            sFeatureCls1.UpdateAtt(id, record2);
            //record1.SetFldFromStr(5, "0");//统计完后清空流动人数
            sFeatureCls2.UpdateAtt(oid, record1);
            sFeatureCls1.Close();
            sFeatureCls2.Close();
        }

        //累计检测人数统计与更新(单个)
        public void CountPolygonCount2()
        {
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            SFeatureCls sFeatureCls2 = new SFeatureCls();
            sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/countvalue");
            sFeatureCls2.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/multipolygons");
            for (int k = 1; k < 27; k++)
            {
                Record record1 = sFeatureCls1.GetAtt(k);
                if (record1.GetValue(1).ToString() != "0")
                {
                    int countperson;
                    //当天的检测人数加上之前的
                    countperson = int.Parse(record1.GetValue(1).ToString()) + int.Parse(record1.GetValue(3).ToString());
                    record1.SetFldFromStr(3, countperson.ToString());
                    sFeatureCls1.UpdateAtt(k, record1);
                }
            }
            sFeatureCls1.Close();
            sFeatureCls2.Close();
        }

        //累计新增病例统计与更新
        public void CountNewSumNum()
        {
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            SFeatureCls sFeatureCls2 = new SFeatureCls();
            sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/countvalue");
            sFeatureCls2.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/multipolygons");
            for (int k = 1; k < 27; k++)
            {
                Record record2 = sFeatureCls2.GetAtt(k);
                Record record1 = sFeatureCls1.GetAtt(k);
                if (record2.GetValue(4).ToString() != "0")
                {
                    int countnewnum;
                    //之前的累计新增加上每天的SUM    
                    countnewnum = int.Parse(record1.GetValue(2).ToString()) + int.Parse(record2.GetValue(4).ToString());
                    record1.SetFldFromStr(2, countnewnum.ToString());
                    sFeatureCls1.UpdateAtt(k, record1);
                }
            }
            sFeatureCls1.Close();
            sFeatureCls2.Close();
        }

        //预计检测人数
        public void PredictNum(long iid)
        {
            SFeatureCls sFeatureCls1 = new SFeatureCls();
            sFeatureCls1.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/countvalue");
            Record record1 = sFeatureCls1.GetAtt(iid);
            int predictperson = (int.Parse(record1.GetValue(1).ToString()) + int.Parse(record1.GetValue(0).ToString())) / 2;
            record1.SetFldFromStr(0, predictperson.ToString());//预计人数
            sFeatureCls1.UpdateAtt(iid, record1);
            sFeatureCls1.Close();
        }

        //计算检出比例重载（在累计检测人数与累计新增病例统计与更新之后）
        public void CalPercentage(long oid)
        {
            SFeatureCls sFeatureCls = new SFeatureCls();
            sFeatureCls.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/countvalue");
            Record record = sFeatureCls.GetAtt(oid);
            int suggestnum = (int.Parse(record.GetValue(0).ToString())) / 5000 + 1;
            double result = double.Parse(record.GetValue(2).ToString()) / double.Parse(record.GetValue(3).ToString());
            record.SetFldFromStr(4, result.ToString());
            record.SetFldFromStr(6, suggestnum.ToString());
            sFeatureCls.UpdateAtt(oid, record);
            sFeatureCls.Close();
        }

        //计算检出比例（在累计检测人数与累计新增病例统计与更新之后）
        public void CalPercentage()
        {
            SFeatureCls sFeatureCls = new SFeatureCls();
            sFeatureCls.Open("gdbp://MapGISLocalPlus/JZFK_GDB/sfcls/countvalue");
            for (int k = 1; k < 27; k++)
            {
                Record record = sFeatureCls.GetAtt(k);
                int suggestnum = (int.Parse(record.GetValue(1).ToString())) / 5000 + 1;
                double result = double.Parse(record.GetValue(2).ToString()) / double.Parse(record.GetValue(3).ToString());
                record.SetFldFromStr(4, result.ToString());
                record.SetFldFromStr(6, suggestnum.ToString());
                sFeatureCls.UpdateAtt(k, record);
            }
            sFeatureCls.Close();
        }

        #endregion
    }
}
