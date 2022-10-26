using MapGIS.GeoDataBase;
using MapGIS.GeoObjects.Att;
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
    public partial class QueryAreaEditForm : Form
    {

        //定义用户修改对应的变量
        public string sum = null;
        public string oidvalue = null;
        //定义时间字段

        public QueryAreaEditForm()
        {
            InitializeComponent();
        }

        //窗口加载
        private void QueryAreaEditForm_Load(object sender, EventArgs e)
        {
        }

        #region 按钮点击事件
        private void button1_Click(object sender, EventArgs e)
        {
            if (sum != null && oidvalue != null)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            oidvalue = null;
            this.Close();
        }
        #endregion

        #region 文本输入框焦点事件
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox1.Text != "" && textBox1.Text != "oid不能为空!" && int.TryParse(textBox1.Text, out int re))
                if (int.Parse(textBox1.Text) > 0 && int.Parse(textBox1.Text) < 27)
                {
                    oidvalue = textBox1.Text;
                }
                else
                { }
            else
            {
                textBox1.ForeColor = Color.Red;
                textBox1.Text = "oid不能为空!";
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text != null && textBox2.Text != "" && textBox2.Text != "请输入0~10000之间的整数" && int.TryParse(textBox2.Text, out int result))
            {
                if (int.Parse(textBox2.Text) >= 0 && int.Parse(textBox2.Text) < 10000)
                { sum = textBox2.Text; }
                else
                {
                    textBox2.ForeColor = Color.Red;
                    textBox2.Text = "请输入0~10000之间的整数";
                }
            }
            else
            {
                textBox2.ForeColor = Color.Red;
                textBox2.Text = "请输入0~10000之间的整数";
            }
        }
        #endregion

    }
}
