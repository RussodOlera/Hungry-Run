using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HunrgyRun
{
    public partial class Cart : Form
    {
        string[] temp;
        string user_logged;
        public Cart()
        {
            InitializeComponent();
            hide_objects();
            string path_user_log = Application.StartupPath + "Users/User_Logged.txt";
            StreamReader f = new StreamReader(path_user_log);
            temp = f.ReadLine().Split(";");
            f.Close();
            user_logged = temp[0];
            Load_Cart();
            label7.Visible = false;
        }
        private void hide_objects()
        {
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            listBox1.Visible = false;
            listBox2.Visible = false;
            listBox3.Visible = false;
            button1.Visible = false;
        }
        private void show_objects()
        {
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = false;
            listBox1.Visible = true;
            listBox2.Visible = true;
            listBox3.Visible = true;
            button1.Visible = true;
        }
        private void Load_Cart()
        {
            listBox3.RightToLeft.ToString();
            float tot = 0;
            string path = Application.StartupPath + "Users/" + user_logged + "/Cart.txt";
            if (File.Exists(path))
            {
                show_objects();
                StreamReader f = new StreamReader(path);
                while (!f.EndOfStream)
                {
                    string[] line = f.ReadLine().Split(";");
                    listBox1.Items.Add(line[0]);
                    listBox2.Items.Add(line[1]);
                    float prezzo = Convert.ToSingle(Convert.ToSingle(line[1]) * Convert.ToSingle(line[2]));
                    listBox3.Items.Add(prezzo+"€");
                    tot += prezzo;
                }
                f.Close();
            }
            else
                label7.Visible = true;
            label6.Text = tot.ToString()+"€";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form f = new Buy();
            f.Show();
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            
        }
    }
}
