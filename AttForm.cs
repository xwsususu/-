using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrecisionControlApp
{
    public partial class AttForm : Form
    {
        string type;
        List<Lockpoint> pointlist = null;
        List<Streetarea> arealist = null;

        public AttForm(string type)
        {
            InitializeComponent();
            this.type = type;
        }

        private void AttForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(1000, 350);
            if (type == "point")
            {
                this.Text = "封控点信息表";
                this.Size = new Size(782, 500);
                this.listView1.Columns.Add("序号", 60, HorizontalAlignment.Center);
                this.listView1.Columns.Add("名称", 200, HorizontalAlignment.Center);
                this.listView1.Columns.Add("封控类型", 100, HorizontalAlignment.Center);
                this.listView1.Columns.Add("所属街道", 100, HorizontalAlignment.Center);
                this.listView1.Columns.Add("工作人员人数", 120, HorizontalAlignment.Center);
                this.listView1.Columns.Add("工作状态", 100, HorizontalAlignment.Center);
                this.listView1.Columns.Add("流动人数", 100, HorizontalAlignment.Center);
            }
            if (type == "area")
            {
                this.Text = "街道防控信息表";
                this.Size = new Size(904, 500);
                this.listView1.Columns.Add("序号", 60, HorizontalAlignment.Center);
                this.listView1.Columns.Add("名称", 200, HorizontalAlignment.Center);
                this.listView1.Columns.Add("每日新增病例数", 134, HorizontalAlignment.Center);
                this.listView1.Columns.Add("三区分划等级", 120, HorizontalAlignment.Center);
                this.listView1.Columns.Add("最后一例出现日期", 148, HorizontalAlignment.Center);
                this.listView1.Columns.Add("预计解除日期", 120, HorizontalAlignment.Center);
                this.listView1.Columns.Add("当前日期", 120, HorizontalAlignment.Center);
            }
            showInfoData();
        }

        private void showInfoData()
        {
            if (type == "point")
            {
                this.listView1.Items.Clear();
                this.listView1.BeginUpdate();
                for (int i = 0; i < pointlist.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = pointlist[i].OID.ToString();
                    lvi.SubItems.Add(pointlist[i].PNAME);
                    string pointranktotext= pointlist[i].POINTRANK.ToString();
                    switch (pointranktotext)
                    {
                        case "0": pointranktotext = "采样点";break;
                        case "1":pointranktotext = "管控点";break;
                        case "2": pointranktotext = "封闭点";break;
                        case "3":pointranktotext = "曾用点";break;
                        default:break;
                    }
                    lvi.SubItems.Add(pointranktotext);
                    lvi.SubItems.Add(pointlist[i].PSCOPE.ToString());
                    lvi.SubItems.Add(pointlist[i].WORKNUM.ToString());
                    lvi.SubItems.Add(pointlist[i].WORKSTATE);
                    lvi.SubItems.Add(pointlist[i].PERSONNUM.ToString());
                    this.listView1.Items.Add(lvi);
                }
                this.listView1.EndUpdate();
            }
            if (type == "area")
            {
                this.listView1.Items.Clear();
                this.listView1.BeginUpdate();
                for (int i = 0; i < arealist.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = arealist[i].OID.ToString();
                    lvi.SubItems.Add(arealist[i].NAME);
                    lvi.SubItems.Add(arealist[i].SUM.ToString());
                    string pointranktotext = arealist[i].POLYGONRANK.ToString();
                    switch (pointranktotext)
                    {
                        case "0": pointranktotext = "防范区"; break;
                        case "1": pointranktotext = "管控区"; break;
                        case "2": pointranktotext = "隔离区"; break;
                        default: break;
                    }
                    lvi.SubItems.Add(pointranktotext);
                    lvi.SubItems.Add(arealist[i].LASTTIME.ToString());
                    lvi.SubItems.Add(arealist[i].RELEASETIME.ToString());
                    lvi.SubItems.Add(arealist[i].CURRENTTIME.ToString());
                    this.listView1.Items.Add(lvi);
                }
                this.listView1.EndUpdate();
            }
        }
        public void addPointInfo(List<Lockpoint> list)
        {
            this.pointlist = list;
            showInfoData();
        }
        public void addAreaInfo(List<Streetarea> list)
        {
            this.arealist = list;
            showInfoData();
        }

    }
}
