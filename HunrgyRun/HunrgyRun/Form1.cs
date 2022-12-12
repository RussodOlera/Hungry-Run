using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HunrgyRun
{
    public partial class Form1 : Form
    {
        public static IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);
        Socket socket;
        public string User_logged;
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

        private Form activeForm = null;
        private void openChilFormMenu(Form childForm)
        {
            if(activeForm!=null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildFrom.Controls.Add(childForm);
            panelChildFrom.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
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

        private void button12_Click(object sender, EventArgs e)
        {
            openChilFormMenu(new Form2(User_logged));
        }

        private void button13_Click(object sender, EventArgs e)
        {
            openChilFormMenu(new Form3(User_logged));
        }

        private void button14_Click(object sender, EventArgs e)
        {
            openChilFormMenu(new Form4());
        }

        private void button15_Click(object sender, EventArgs e)
        {
            openChilFormMenu(new Form5());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form f = new Form_Register(User_logged);
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form f = new Form_LogIn();
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            string path = Application.StartupPath + "Users/User_Logged.txt";
            string[] line;
            StreamReader sr = new StreamReader(path);
            line = sr.ReadLine().Split(";");
            sr.Close();
            try
            {
                byte[] bytes = new byte[1024];
                byte[] toSend;
                string data = null;
                socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(remoteEP);
                try
                {
                    toSend = Encoding.ASCII.GetBytes("logout;"+line + " Disconnected...");
                    socket.Send(toSend);
                }
                catch (ArgumentNullException ane)
                {
                    MessageBox.Show("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    MessageBox.Show("SocketException : {0}", se.ToString());
                }
                catch (Exception Events)
                {
                    MessageBox.Show("Unexpected exception : {0}", Events.ToString());
                }
            }
            catch (Exception Events)
            {
                Console.WriteLine(Events.ToString());
            }
            File.Delete(path);
            MessageBox.Show("Ti sei disconnesso");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            openChilFormMenu(new Cart());
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text != "")
                textBox1.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                textBox1.Text = "Ristoranti,Fast Food,Supermercati";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string loc = textBox1.Text;
            switch(loc)
            {
                case "Limbo": openChilFormMenu(new Form2(User_logged));break;
                case "All You Can Eat": openChilFormMenu(new Form3(User_logged));break;
                case "Old Wild West": openChilFormMenu(new Form4());break;
                case "Beer Garden": openChilFormMenu(new Form5());break;
                default: 
                    MessageBox.Show("Non ho trovato nessun elemento...");
                    textBox1.Text = "Ristoranti,Fast Food,Supermercati";break;
            }
        }
    }
}
