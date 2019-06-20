using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 修改文件名
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadXls_Click(object sender, EventArgs e)
        {
            string sql = "select * from userInfo where 班级编号 = " + textBox2.Text;
            dataGridView2.DataSource = DBHelper.GetDataTable(sql);

            sql = "select count(*) from userInfo where 班级编号 = " + textBox2.Text;
            label2.Text = DBHelper.GetScalar(sql).ToString();

            ChangeState();
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            string dirPath = txtDirPath.Text;
            var fileNameArr = Directory.GetFiles(dirPath);

            if (radioButton2.Checked)
            {
                fileNameArr = Directory.GetDirectories(dirPath);
            }
            List<object> list = new List<object>();
            for (int i = 0; i < fileNameArr.Length; i++)
            {
                list.Add(
                        new { fileName = fileNameArr[i].ToString() }
                    );
            }
            dataGridView1.DataSource = list;



            string sql = "select * from userInfo where 班级编号 = " + textBox2.Text;
            DataTable dt = DBHelper.GetDataTable(sql);

            ChangeState();
        }

        public void ChangeState()
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView2.Rows.Count > 0)
            {
                btnProcess.Enabled = true;
            }
            else
            {
                btnProcess.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Directory.Move(@"e:\aaa", @"e:\bbb");
            //File.Move(@"e:\aaa",@"e:\bbb");
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            string temp = dataGridView1.Rows[0].Cells[0].Value.ToString();
            string fixedPath = temp.Substring(0, temp.LastIndexOf(@"\"));
            string endOf = temp.Substring(temp.LastIndexOf("."));
            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView2.Rows.Count; j++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString().Contains(dataGridView2.Rows[j].Cells["考生姓名"].Value.ToString()))
                    {
                        if (radioButton2.Checked)
                        {
                            Directory.Move(dataGridView1.Rows[i].Cells[0].Value.ToString(), fixedPath + @"\" + dataGridView2.Rows[j].Cells["准考证号"].Value + "+" + dataGridView2.Rows[j].Cells["考生姓名"].Value);
                        }
                        else
                        {
                            //处理文件，添加后缀
                            File.Move(dataGridView1.Rows[i].Cells[0].Value.ToString(), fixedPath + @"\" + dataGridView2.Rows[j].Cells["准考证号"].Value + "+" + dataGridView2.Rows[j].Cells["考生姓名"].Value + endOf);
                        }
                        break;
                    }
                }
            }

        }
    }
}
