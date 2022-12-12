using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HunrgyRun
{
    public partial class Form_LogIn : Form
    {
        public static IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);
        Socket socket;
        string[] answer;
        public Form_LogIn()
        {
            InitializeComponent();
            control_Remember();
            if (textBox2.Text == "Password")
                textBox2.UseSystemPasswordChar = false;
            else
                textBox2.UseSystemPasswordChar = true;
        }
        private void control_Remember()
        {
            string path=Application.StartupPath + "Users\\User & Password.txt";
            if (File.Exists(path))
            {
                textBox2.UseSystemPasswordChar = true;
                StreamReader file = new StreamReader(path);
                string line;
                line = file.ReadLine();
                textBox1.Text = line;
                line = file.ReadLine();
                textBox2.Text=line;
                file.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox2.UseSystemPasswordChar = false;
            else if (!checkBox1.Checked)
                textBox2.UseSystemPasswordChar = true;
        }
        private void log_in()
        {
            /*string path = Application.StartupPath + "Users\\User & Password.txt";
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                string line;
                line = file.ReadLine();
                if(line==textBox1.Text)
                {
                    line = file.ReadLine();
                    if(line==textBox2.Text)
                    {
                        MessageBox.Show("Accesso effettuato con successo!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Nome o password non corretto");
                        textBox2.Text = "Password";
                    }
                }
                else
                {
                    MessageBox.Show("Nome o password non corretto");
                    textBox2.Text = "Password";
                }
                file.Close();
            }*/


            try
            {
                byte[] bytes = new byte[1024];
                byte[] toSend;
                string data = null;
                
                socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(remoteEP);
                toSend = Encoding.ASCII.GetBytes("login;" + textBox1.Text + ";" + textBox2.Text);
                socket.Send(toSend);
                int bytesRec = socket.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                answer = data.Split(";");
                switch(answer[0])
                {
                    case "true":
                        MessageBox.Show("Accesso Effettuato");
                        Save_logged();
                        this.Close();
                        break;
                    case "false":
                        MessageBox.Show("Nome o password non corretto");
                        textBox2.Text = "Password"; 
                        break;
                }
            }
            catch
            { 

            }
        }
        private void Save_logged()
        {
            string path = Application.StartupPath + "Users/User_Logged.txt";
            StreamWriter f = new StreamWriter(path);
            if(File.Exists(path))
            {
                f.WriteLine(answer[1] + ";" + answer[2]);
                f.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            log_in();
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "Password")
                textBox2.Text = "";
        }
    }
    
}

