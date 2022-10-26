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
    public partial class EditFkdInfoForm : Form
    {
        //字段属性值
        string type = null;
        public Lockpoint lockpoint = new Lockpoint();
        //记录是否点了确认按钮
        public bool IsFinsh = false;

        public EditFkdInfoForm()
        {
            InitializeComponent();
        }
        public EditFkdInfoForm(string type)
        {
            this.type = type;
            InitializeComponent();
        }


        private void EditFkdInfoForm_Load(object sender, EventArgs e)
        {
            if(type=="add")
            {
                textTank1.Text = "不可自定义OID!";
                textTank1.ForeColor = Color.Red;
                textTank1.Enabled = false;
                buttonDel.Visible = false;
                tableLayoutPanel2.ColumnStyles[1].Width = 0;
            }
        }

        private void textTank1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;//handle为已处理
            }
        }

        private void textTank7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;//handle为已处理
            }
        }

        private void textTank1_TextChanged(object sender, EventArgs e)
        {
            if(type!="add")
            {
                try
                {
                    long oid = long.Parse(textTank1.Text);
                    object item = Function.getFieldValue("controlpoints", oid);
                    if (item.GetType() == typeof(Lockpoint))
                    {
                        Lockpoint lockpoint = (Lockpoint)item;
                        setPscope(lockpoint.PSCOPE);
                    }
                }
                catch (FormatException)
                { MessageBox.Show("OID请输入正确的数值！"); }
            }
        }

        public void setPscope(long oid)
        {
            lockpoint.PSCOPE = (int)oid;
            Streetarea streetarea = (Streetarea)Function.getFieldValue("multipolygons", oid);
            textTank4.Text = streetarea.NAME;
        }
        private void no_Click(object sender, EventArgs e)
        {
            IsFinsh = false;
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            bool IsEdit = true;//判断是否输入完必填项
            if (type == "add")
            {
                if (textTank2.Text == "") { MessageBox.Show("请输入封控点的名称！"); IsEdit = false; }
                else if (textTank3.Text == "") { MessageBox.Show("请输入封控点的等级！"); IsEdit = false; }
                else if (textTank5.Text == "") { MessageBox.Show("请输入工作人员的人数！"); IsEdit = false; }
                else if (textTank6.Text == "") { MessageBox.Show("请输入工作状态！"); IsEdit = false; }
                else if (textTank7.Text == "") { MessageBox.Show("请输入流动人员的数目！"); IsEdit = false; }
            }
            if (type != "add")
            {
                if (textTank1.Text == "") { MessageBox.Show("请输入要修改要素的OID！"); IsEdit = false; }
                else lockpoint.OID = long.Parse(textTank1.Text);
                if (Function.ISoidExist("sfcls/controlpoints", lockpoint.OID) == false) { MessageBox.Show("输入的OID不存在！"); IsEdit = false; }
            }
            if (textTank2.Text != "") lockpoint.PNAME = textTank2.Text;
            if (textTank3.Text != "") lockpoint.POINTRANK = int.Parse(textTank3.Text);
            if (textTank5.Text != "") lockpoint.WORKNUM = int.Parse(textTank5.Text);
            if (textTank6.Text != "") lockpoint.WORKSTATE = textTank6.Text;
            if (textTank7.Text != "") lockpoint.PERSONNUM = int.Parse(textTank7.Text);

            if (IsEdit == true) { IsFinsh = true; this.Close(); }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (textTank1.Text != "")
            {
                long oid = long.Parse(textTank1.Text);
                Function.delOnePoint(oid);
                MessageBox.Show("删除成功!");
                this.Close();
            }
            else MessageBox.Show("未输入oid，删除失败!");
        }
    }
}
