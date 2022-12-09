using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HunrgyRun
{
    public partial class Buy : Form
    {
        public static IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);
        Socket socket;


        string[] temp;
        string user_logged;
        string request = "order;";
        public Buy()
        {
            InitializeComponent();

            string path_user_log = Application.StartupPath + "Users/User_Logged.txt";
            StreamReader f = new StreamReader(path_user_log);
            temp = f.ReadLine().Split(";");
            f.Close();
            user_logged = temp[0];
            request += user_logged + ";";
            load_Order();
            load_User();
        }
        private void load_Order()
        {
            float tot = 0;
            string path_dir_cart = Application.StartupPath + "Users/" + user_logged + "/Cart.txt";
            StreamReader sr = new StreamReader(path_dir_cart);
            while(!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] value = line.Split(";");
                float prezzo = Convert.ToSingle(Convert.ToSingle(value[1]) * Convert.ToSingle(value[2]));
                listBox1.Items.Add("n° " + value[1] + " " + value[0]);
                request += value[1] + ";" + value[0] + ";" + value[2] + ";";
                listBox3.Items.Add(prezzo + "€");
                tot += prezzo;
            }
            sr.Close();
            label4.Text = tot + "€";
        }

        private void load_User()
        {
            string path_dir_user = Application.StartupPath + "Users/" + user_logged + "/Dati Personali.txt";
            StreamReader sr=new StreamReader(path_dir_user);
            temp = sr.ReadLine().Split(";");
            listBox2.Items.Add("Nome: " + temp[0]+" " + temp[1]);
            request += temp[0] + ";" + temp[1] + ";";
            temp = sr.ReadLine().Split("\n");
            listBox2.Items.Add("Nato il: " + temp[0]);
            temp = sr.ReadLine().Split("\n");
            listBox2.Items.Add("Indirizzo Spedizione: " + temp[0]);
            request += temp[0] + ";";
            temp = sr.ReadLine().Split("\n");
            listBox2.Items.Add("Email: " + temp[0]);
            request += temp[0] + ";";
            temp = sr.ReadLine().Split("\n");
            listBox2.Items.Add("N° cel: " + temp[0]);
            request += temp[0];
            sr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = new byte[1024];
                byte[] toSend;
                string data = null;
                socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(remoteEP);
                toSend = Encoding.ASCII.GetBytes(request);
                socket.Send(toSend);
            }
            catch
            {

            }
            File.Delete(Application.StartupPath + "Users/" + user_logged + "/Cart.txt");
            MessageBox.Show("Ordine Confermato!");
            this.Close();
        }
    }
}
