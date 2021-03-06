﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using connect = System.Data.SqlClient.SqlConnection;
using command = System.Data.SqlClient.SqlCommand;
using reader = System.Data.SqlClient.SqlDataReader;



namespace frameworks
{
    public partial class UpdateResident :MetroFramework.Forms.MetroForm
    {
        VideoCapture capture;
        public UpdateResident()
        {
            InitializeComponent();
            Run();
        }
        private void Run()
        {
            try
            {
                capture = new VideoCapture();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }
            Application.Idle += ProcessFrame;
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            imageBox1.Image = capture.QuerySmallFrame();
            imageBox3.Image = capture.QuerySmallFrame();

        }
        private void UpdateResident_Load(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            imageBox1.Visible = false;
            imageBox2.Visible = false;
            imageBox3.Visible = false;
            imageBox4.Visible = false;
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            main ma = new main();
            ma.ShowDialog();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            main ma = new main();
            ma.ShowDialog();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Enabled == true)
            {
                panel1.Enabled = true;
            }
        }
        public void ClearTextbox(params TextBox[] textBoxes)
        {
            for (int i = 0; i < textBoxes.Length; i++)
            {
                textBoxes[i].Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearTextbox(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ClearTextbox(textBox7, textBox8, textBox9, textBox10, textBox11);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            imageBox1.Visible = true;
            imageBox2.Visible = false;
        }
        string imagelocation = "";
        private void button5_Click(object sender, EventArgs e)
        {
            imageBox2.Image = imageBox1.Image;
            imageBox2.Image.Save(@"C:\Users\User\Desktop\New folder\" + textBox1.Text + ".png");
            imagelocation = @"C:\Users\User\Desktop\New folder\" + textBox1.Text + ".png";
            imageBox1.Visible = false;
            imageBox2.Visible = true;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            imageBox3.Visible = true;
            imageBox4.Visible = false;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            imageBox4.Image = imageBox3.Image;
            imageBox4.Image.Save(@"C:\Users\User\Desktop\New folder\Tenant" + textBox11.Text + ".png");
            imagelocation = @"C:\Users\User\Desktop\New folder\Tenant" + textBox11.Text + ".png";
            imageBox3.Visible = false;
            imageBox4.Visible = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            connect con = new connect("Data Source=(local);Initial Catalog=appartment;Integrated Security=True");
            string query = "update resident set Members=@m,Name=@n,Pho=@p,Nic=@ni,Room=@r,Img=@i where Fno=@f";
            command com = new command(query, con);
            com.Parameters.AddWithValue("@f", textBox1.Text);
            com.Parameters.AddWithValue("@m", textBox2.Text);
            com.Parameters.AddWithValue("@n", textBox3.Text);
            com.Parameters.AddWithValue("@p", textBox4.Text);
            com.Parameters.AddWithValue("@ni", textBox5.Text);
            com.Parameters.AddWithValue("@r", textBox6.Text);
            com.Parameters.AddWithValue("@i", imagelocation);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            MessageBox.Show("Successfully updated");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string path = "";
            connect con = new connect("Data Source=(local);Initial Catalog=appartment;Integrated Security=True");
            command cmd = new command("select * from resident where Fno=@fn ", con);
            cmd.Parameters.AddWithValue("@fn", textBox1.Text);
            con.Open();
            reader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {

                textBox2.Text = sdr[1].ToString();
                textBox3.Text = sdr[2].ToString();
                textBox4.Text = sdr[3].ToString();
                textBox5.Text = sdr[4].ToString();
                textBox6.Text = sdr[5].ToString();
                path = sdr["Img"].ToString();
            }
            con.Close();
            imageBox2.Visible = true;
            imageBox2.ImageLocation = path;
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            string path = "";
            connect con = new connect("Data Source=(local);Initial Catalog=appartment;Integrated Security=True");
            command cmd = new command("select * from tenant where Flatno = @f", con);
            cmd.Parameters.AddWithValue("@f", textBox11.Text);
            con.Open();
            reader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                path = sdr["Img1"].ToString();
                textBox7.Text = sdr[1].ToString();
                textBox8.Text = sdr[2].ToString();
                textBox9.Text = sdr[3].ToString();
                textBox10.Text = sdr[4].ToString();
            }
            con.Close();
            imageBox4.Visible = true;
            imageBox4.ImageLocation = path;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            connect con = new connect("Data Source=(local);Initial Catalog=appartment;Integrated Security=True");
            string query = "update tenant set Name=@n,fammem=@m,Nic=@ni,Phno=@p,Img1=@i where Flatno=@f";
            command com = new command(query, con);
            com.Parameters.AddWithValue("@f", textBox11.Text);
            com.Parameters.AddWithValue("@n", textBox7.Text);
            com.Parameters.AddWithValue("@m", textBox8.Text);
            com.Parameters.AddWithValue("@ni", textBox9.Text);
            com.Parameters.AddWithValue("@p", textBox10.Text);
            com.Parameters.AddWithValue("@i", imagelocation);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            textBox11.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            MessageBox.Show("Successfully updated");
        }
    }
}
