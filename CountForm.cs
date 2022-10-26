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
    public partial class CountForm : Form
    {
        
        string type;
        List<List<string>> CountQuery = new List<List<string>>();
        List<List<string>> CountAllQuery = new List<List<string>>();

        public CountForm(string type)
        {
            this.type = type;
            InitializeComponent();
        }

        private void CountForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(1000, 350);
            if (type == "实时统计")
            {
                this.Text = "实时统计信息表";
                //this.Size = new Size(782, 500);
                this.listView1.Columns.Add("序号", 60, HorizontalAlignment.Center);
                this.listView1.Columns.Add("名称", 150, HorizontalAlignment.Center);
                this.listView1.Columns.Add("三区分划等级", 130, HorizontalAlignment.Center);
                this.listView1.Columns.Add("最后一例出现日期", 150, HorizontalAlignment.Center);
                this.listView1.Columns.Add("预计解除日期", 150, HorizontalAlignment.Center);
                this.listView1.Columns.Add("今日新增病例数", 130, HorizontalAlignment.Center);
                this.listView1.Columns.Add("当前日期", 150, HorizontalAlignment.Center);
                this.listView1.Columns.Add("今日检测人数", 130, HorizontalAlignment.Center);
                this.listView1.Columns.Add("预计今日检测人数", 150, HorizontalAlignment.Center);
                this.BackColor = Color.DarkSeaGreen;
                this.listView1.BackColor = Color.DarkSeaGreen;
            }
            if (type == "当天最终统计")
            {
                this.Text = "今日最终统计信息表";
                //this.Size = new Size(904, 500);
                this.listView1.BackColor = Color.AliceBlue;
                this.listView1.Columns.Add("序号", 60, HorizontalAlignment.Center);
                this.listView1.Columns.Add("名称", 150, HorizontalAlignment.Center);
                this.listView1.Columns.Add("三区分划等级", 130, HorizontalAlignment.Center);
                this.listView1.Columns.Add("最后一例出现日期", 150, HorizontalAlignment.Center);
                this.listView1.Columns.Add("预计解除日期", 150, HorizontalAlignment.Center);
                this.listView1.Columns.Add("今日新增病例数", 130, HorizontalAlignment.Center);
                this.listView1.Columns.Add("当前日期", 150, HorizontalAlignment.Center);
                this.listView1.Columns.Add("预计明日检测人数", 150, HorizontalAlignment.Center);
                this.listView1.Columns.Add("今日检测人数", 130, HorizontalAlignment.Center);
                this.listView1.Columns.Add("累计新增病例", 130, HorizontalAlignment.Center);
                this.listView1.Columns.Add("累计检测人数", 130, HorizontalAlignment.Center);
                this.listView1.Columns.Add("检出比例", 150, HorizontalAlignment.Center);
                this.listView1.Columns.Add("采样点数目", 100, HorizontalAlignment.Center);
                this.listView1.Columns.Add("建议采样点数目", 150, HorizontalAlignment.Center);
            }
            showInfoData();
        }

        private void showInfoData()
        {
            if (type == "实时统计")
            {
                this.listView1.Items.Clear();
                this.listView1.BeginUpdate();
                for (int i = 0; i < CountQuery.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = (i + 1).ToString();
                    lvi.SubItems.Add(CountQuery[i][0]);
                    string pointranktotext = CountQuery[i][1];
                    switch (pointranktotext)
                    {
                        case "0": pointranktotext = "防范区"; break;
                        case "1": pointranktotext = "管控区"; break;
                        case "2": pointranktotext = "隔离区"; break;
                        default:break;
                    }
                    lvi.SubItems.Add(pointranktotext);
                    lvi.SubItems.Add(CountQuery[i][2]);
                    lvi.SubItems.Add(CountQuery[i][3]);
                    lvi.SubItems.Add(CountQuery[i][4]);
                    lvi.SubItems.Add(CountQuery[i][5]);
                    lvi.SubItems.Add(CountQuery[i][6]);
                    lvi.SubItems.Add(CountQuery[i][7]);
                    this.listView1.Items.Add(lvi);
                }
                this.listView1.EndUpdate();
            }
            if (type == "当天最终统计")
            {
                this.listView1.Items.Clear();
                this.listView1.BeginUpdate();
                for (int i = 0; i < CountAllQuery.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = (i + 1).ToString();
                    lvi.SubItems.Add(CountAllQuery[i][0]);
                    string pointranktotext = CountAllQuery[i][1];
                    switch (pointranktotext)
                    {
                        case "0": pointranktotext = "防范区"; break;
                        case "1": pointranktotext = "管控区"; break;
                        case "2": pointranktotext = "封闭区"; break;
                        default: break;
                    }
                    lvi.SubItems.Add(pointranktotext);
                    lvi.SubItems.Add(CountAllQuery[i][2]);
                    lvi.SubItems.Add(CountAllQuery[i][3]);
                    lvi.SubItems.Add(CountAllQuery[i][4]);
                    lvi.SubItems.Add(CountAllQuery[i][5]);
                    lvi.SubItems.Add(CountAllQuery[i][6]);
                    lvi.SubItems.Add(CountAllQuery[i][7]);
                    lvi.SubItems.Add(CountAllQuery[i][8]);
                    lvi.SubItems.Add(CountAllQuery[i][9]);
                    lvi.SubItems.Add(CountAllQuery[i][10]);
                    lvi.SubItems.Add(CountAllQuery[i][11]);
                    lvi.SubItems.Add(CountAllQuery[i][12]);
                    this.listView1.Items.Add(lvi);
                }
                this.listView1.EndUpdate();
            }
        }
        public void addQueryInfo(List<List<string>> list)
        {
            this.CountQuery = list;
            showInfoData();
        }
        public void addAllQueryInfo(List<List<string>> list)
        {
            this.CountAllQuery = list;
            showInfoData();
        }
    }
}
