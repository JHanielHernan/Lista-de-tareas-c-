using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Aplication_Tasks
{
    public partial class Form1 : Form
    {
        Stack<list_task> _list_tasks = new Stack<list_task>();
        public Form1()
        {
            InitializeComponent();
            txt_status.SelectedIndex = 0;
            drawing_listbox();
            txt_status.Text = "starting";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    var new_list = new list_task(txt_title.Text, txt_content.Text, Convert.ToDateTime(txt_time.Text), txt_status.Text);
                    this._list_tasks.Push(new_list);
                    drawing_listbox();
                    txt_content.Clear();
                    txt_title.Clear();
                    txt_time.Clear();
                    txt_status.Text = "starting";
                }
                else MessageBox.Show("Please fill in all the fields","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
        }

        void drawing_listbox()
        {
            box_list.Controls.Clear();
            foreach(var n in _list_tasks)
            {
                box_list.Controls.Add(n);
            }
        }

        bool Validation()
        {
            if (txt_title.Text == String.Empty || txt_content.Text == String.Empty) return false;
            return true;
        }

        private void txt_title_Enter(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.Show("Title",txt_title);
        }

        private void txt_time_Enter(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip tt = new ToolTip();
            tt.Show("Date and Time, format is MM/dd/aaaa hh:mm:ss tt",txt_time);
        }

        private void txt_content_Enter(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.Show("Content",txt_content);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program developed by Joseph Hernandez\r\ngmail Haniel06Hernandez@gmail.com\r\nIf you have any questions or problems, please send an email.\r\nThis program will be receiving updates based on the needs it addresses.", "HELP",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
