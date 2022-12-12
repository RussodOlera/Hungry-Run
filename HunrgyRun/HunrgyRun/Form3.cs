using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Sockets;
using System.Net;

namespace HunrgyRun
{
    public partial class Form3 : Form
    {
        public static IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);
        Socket socket;
        public Form3(string User_logged)
        {
            InitializeComponent();
            add_tab_name();
            panel1.Visible = false;
            listBox3.Visible = false;
            label10.Visible = false;
        }
        private void add_tab_name()
        {
            tabPage1.Text = "Antipasti";
            tabPage2.Text = "Risotti";
            tabPage3.Text = "Pollo";
            tabPage4.Text = "Rotolini";
            tabPage5.Text = "Roll";
        }
        private void load_Name()
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*string path = Application.StartupPath + "Users/" + "Cart.txt";*/

            string path_log = Application.StartupPath + "Users/User_Logged.txt";
            if (File.Exists(path_log))
            {
                StreamReader r = new StreamReader(path_log);
                string line = r.ReadLine();
                string[] data = line.Split(";");
                string user = data[0];
                string paswd = data[1];
                r.Close();
                load_cart(user, paswd);
            }
            else
                MessageBox.Show("Devi prima accedere con il tuo account!");



        }

        private void load_cart(string user, string paswd)
        {


            try
            {
                string cart = null;
                string temp;
                string path = Application.StartupPath + "Users" + "/" + user + "/Cart.txt";
                int cont = listBox1.Items.Count;
                byte[] bytes = new byte[2048];
                byte[] toSend;
                string data = null;
                socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(remoteEP);
                string command = "load_cart;" + user;
                cart = command + ";";

                if (!File.Exists(path))
                {
                    StreamWriter f = new StreamWriter(path);
                    for (int i = 0; i < cont; i++)
                    {
                        temp = listBox1.Items[i].ToString() + ";" + listBox2.Items[i].ToString() + ";" + listBox3.Items[i].ToString();
                        //MessageBox.Show(listBox1.Items[i].ToString() + ";" + listBox2.Items[i].ToString());
                        f.WriteLine(temp);
                        if (i < cont - 1)
                            cart += temp + ";";
                        else
                            cart += temp;
                    }
                    f.Close();
                }
                else
                {
                    for (int i = 0; i < cont; i++)
                    {
                        temp = listBox1.Items[i].ToString() + ";" + listBox2.Items[i].ToString() + ";" + listBox3.Items[i].ToString();
                        File.AppendAllText(path, temp + "\n");
                        if (i < cont - 1)
                            cart += temp + ";";
                        else
                            cart += temp;
                        MessageBox.Show(cart.ToString());
                    }
                }
                toSend = Encoding.ASCII.GetBytes(cart);
                socket.Send(toSend);
            }
            catch
            {

            }

            MessageBox.Show("L'ordine è stato aggiunto al carrello!");
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

        }
        

        private void reset_choise()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            reset_choise();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(label9.Text);
            listBox2.Items.Add(numericUpDown1.Value.ToString());
            listBox3.Items.Add(label10.Text);
            panel1.Visible = false;
            reset_choise();
            numericUpDown1.Value = 1;
        }

        

     


        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                MessageBox.Show("");
                panel1.Visible = true;
                label9.Text = "Ravioli di Carne";
                label10.Text = "5,00";
            }
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Ravioli di Verdure";
                label10.Text = "5,00";
            }
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Involtini";
                label10.Text = "6,50";
            }
        }

        private void radioButton4_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Chele di granchio";
                label10.Text = "4,00";
            }
        }

        private void radioButton5_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Fiori di zucca";
                label10.Text = "4,50";
            }
        }

        private void radioButton6_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Pane";
                label10.Text = "4,00";
            }
        }

        private void radioButton12_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton12.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Riso Cantonese";
                label10.Text = "12,00";
            }
        }

        private void radioButton11_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton11.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Riso gamberi,...";
                label10.Text = "15,00";
            }
        }

        private void radioButton10_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton10.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Riso anguilla";
                label10.Text = "16,50";
            }
        }

        private void radioButton9_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Riso Curry";
                label10.Text = "18,00";
            }
        }

        private void radioButton8_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Riso Bianco";
                label10.Text = "14,50";
            }
        }

        private void radioButton18_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton18.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Pollo mandorle";
                label10.Text = "18,00";
            }
        }

        private void radioButton17_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton17.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Pollo,funghi,bambù";
                label10.Text = "16,50";
            }
        }

        private void radioButton16_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton16.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Pollo agrodolce";
                label10.Text = "13,00";
            }
        }

        private void radioButton15_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton15.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Pollo piccante";
                label10.Text = "14,00";
            }
        }

        private void radioButton14_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton14.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Pollo e limone";
                label10.Text = "12,50";
            }
        }

        private void radioButton24_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton24.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Tonno";
                label10.Text = "5,50";
            }
        }

        private void radioButton23_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton23.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Salmone";
                label10.Text = "6,50";
            }
        }

        private void radioButton22_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton22.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Gamberi";
                label10.Text = "9,50";
            }
        }

        private void radioButton21_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton21.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Granchio";
                label10.Text = "7,50";
            }
        }

        private void radioButton20_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton20.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Avocado";
                label10.Text = "7,00";
            }
        }

        private void radioButton19_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton19.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Cetrioli";
                label10.Text = "7,50";
            }
        }

        private void radioButton30_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton30.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Tiger";
                label10.Text = "1,50";
            }
        }

        private void radioButton29_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton29.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Rainbow";
                label10.Text = "4,00";
            }
        }

        private void radioButton28_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton28.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Green Dragon";
                label10.Text = "6,50";
            }
        }

        private void radioButton27_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton27.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Dragon";
                label10.Text = "7,00";
            }
        }

        private void radioButton26_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton26.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Tiger";
                label10.Text = "14,50";
            }
        }

        private void radioButton25_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton25.Checked)
            {
                panel1.Visible = true;
                label9.Text = "Tataki";
                label10.Text = "2,50";
            }
        }
    }
}
