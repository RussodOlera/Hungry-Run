using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HunrgyRun
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            set_menu();
        }

        private void set_menu()
        {
            panel10.Visible = false;
            panel8.Visible = false;
            panel6.Visible = false;
        }
        private void hide_menu()
        {
            if(panel10.Visible == true)
                panel10.Visible = false;
            if(panel8.Visible == true)
                panel8.Visible = false;
            if(panel6.Visible == true)
                panel6.Visible = false;
        }

        private void show_menu(Panel panel)
        {
            if(panel.Visible == false)
            {
                hide_menu();
                panel.Visible = true;
            }
            else
                panel.Visible = false;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;

            }
            else
            {
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            show_menu(panel10);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            show_menu(panel8);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            show_menu(panel6);
        }
    }
}
