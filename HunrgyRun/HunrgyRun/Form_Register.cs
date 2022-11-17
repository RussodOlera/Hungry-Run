using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HunrgyRun
{
    public partial class Form_Register : Form
    {
        public Form_Register()
        {
            InitializeComponent();
            tabPage1.Text = "Dati Personali";
            tabPage2.Text = "Pagamento";
            tabPage3.Text = "User & Password";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;
            pictureBox1.Image=Image.FromFile(path);
        }

        private bool control_full()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "" || textBox11.Text == "" || textBox12.Text == "" || textBox13.Text == "")
            {
                if (comboBox1.Text == "" || comboBox2.Text == "GG" || comboBox3.Text == "MM" || comboBox4.Text == "MM")
                {
                    if (!radioButton1.Checked)
                    {
                        return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }  
            else
                return true;
        }
        private bool control_data()
        {
            bool temp = true;
            if (Convert.ToInt32(comboBox2.Text) <= 29 && comboBox3.Text == "Feb" && Convert.ToInt32(textBox6.Text) % 4 == 0)
                temp = true;
            else if (Convert.ToInt32(comboBox2.Text) <= 28 && comboBox3.Text == "Feb" && Convert.ToInt32(textBox6.Text) % 4 != 0)
                temp = true;
            else if (comboBox3.Text != "Feb")
                temp = true;
            else
                temp = false;
            return temp;
                
        }
        private bool control_passwd()
        {
            if (textBox12.Text == textBox13.Text)
                return true;
            else
                return false;
        }
        private void save_user()
        {
            string dir=Application.StartupPath+"Users";
            Directory.CreateDirectory(dir);

            string path1 = dir +"/"+ tabPage1.Text + ".txt";
            string path2 = dir +"/"+ tabPage2.Text + ".txt";
            

            StreamWriter file1= new StreamWriter(path1);
            file1.WriteLine(textBox1.Text + ";" + textBox2.Text + "\n" + comboBox2.Text + "/" + comboBox3.Text + "/" + textBox6.Text + "\n" + textBox3.Text + "\n" + textBox4.Text + "\n" + comboBox1.Text + textBox5.Text + pictureBox1.Image.ToString() );
            file1.Close();

            StreamWriter file2 = new StreamWriter(path2);
            file2.WriteLine(radioButton1.Text + "\n" + textBox8.Text + "\n" + textBox9.Text + "\n" + textBox10.Text + "\n" + comboBox4.Text + "/" + textBox7.Text);
            file2.Close();                                   
        }
        private void remember_user()
        {
            string dir = Application.StartupPath + "Users"; 
            string path = dir + "/" + tabPage3.Text + ".txt";
            StreamWriter file = new StreamWriter(path);
            file.WriteLine(textBox11.Text + ";" + textBox12.Text);
            file.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            bool contrl_full=control_full();
            if (contrl_full)
            {
                bool contrl_data = control_data();
                if (contrl_data)
                {
                    bool pwd = control_passwd();
                    if (pwd)
                    {
                        save_user();
                        if(radioButton2.Checked)
                        {
                            remember_user();
                        }
                        MessageBox.Show("La registrazione è avvenuta con successo!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Le due password non corrispondono\n Riprova");
                        textBox12.Text = "";
                        textBox13.Text = "";
                    }
                        
                }
                else
                {
                    MessageBox.Show("La data di nascita è errata\n Riprova");
                    comboBox2.Text = "GG";
                    comboBox3.Text = "MM";
                    textBox6.Text = "Anno";
                }
                    

            }
            else
                MessageBox.Show("Completa prima tutti i campi!");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
