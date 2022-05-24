using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace SeaBattleV3

{
    public partial class Form2 : Form
    {
        public Form1 frm1;
        const int n = 10;
        int x0 = 30, y0 = 45, h = 34, xd = 396;
        public double[,] arr1 = new double[n, n];
        public double[,] arr2 = new double[n, n];      
        public Button [,] bArr1 = new Button[n, n];
        public Button[,] bArr2 = new Button[n, n];

        Random rnd = new Random();

        List<double> ships = new()
        {
            4.1, 3.1, 3.2, 2.1, 2.2, 2.3, 1.1, 1.2, 1.3, 1.4
        };

        // 1.x - кол-во палуб;
        // x.1 - номер;


        public Form2(Form1 frm1)
        {
            InitializeComponent();
            this.frm1 = frm1;
            this.Width = 812;
            this.Height = 514;
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            arr2 = frm1.distribution(arr2, n, ships);

            if (frm1.textBox1.Text == "")
                frm1.textBox1.Text = "Player1";
            this.Text = frm1.textBox1.Text;

            label4.Top = 15;
            label4.Left = x0;
            label5.Top = 15;
            label5.Left = x0 + xd;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    arr1[i, j] = -1;
                    bArr1[i, j] = new Button();
                    //bArr1[i, j].Text = arr1[i, j].ToString();
                    bArr1[i, j].Width = h;
                    bArr1[i, j].Height = h;
                    bArr1[i, j].Left = x0 + j * h;
                    bArr1[i, j].Top = y0 + i * h;
                    bArr1[i, j].Click += bt_Click;
                    Controls.Add(bArr1[i, j]);
                }
            }
            

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //arr2[i, j] = 0;
                    bArr2[i, j] = new Button();
                    //bArr2[i, j].Text = arr2[i, j].ToString();
                    if (arr2[i,j] == 0)
                    {
                        bArr2[i, j].BackColor = Color.Aqua;
                    }
                    else
                    {
                        bArr2[i, j].BackColor = Color.GhostWhite;

                    }
                    bArr2[i, j].Width = h;
                    bArr2[i, j].Height = h;
                    bArr2[i, j].Left = x0 + j * h + xd;
                    bArr2[i, j].Top = y0 + i * h;
                    //bArr2[i, j].Click += bt_Click;
                    Controls.Add(bArr2[i, j]);
                }
            }
        }

        public void bt_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            DialogResult dg;
            int i0, j0;
            i0 = (bt.Top - y0) / h;
            j0 = (bt.Left - x0) / h;


            if (!frm1.compflag)
            {
                if (frm1.hod == 0 && arr1[i0, j0] == -1)
                {
                    //arr1[i0, j0] = frm1.frm3.arr2[i0, j0];
                    //bArr1[i0, j0].Text = frm1.frm3.bArr2[i0, j0].Text;
                    if (frm1.frm3.arr2[i0, j0] == 0)
                    {
                        bArr1[i0, j0].BackColor = Color.Aqua;
                        frm1.frm3.bArr2[i0, j0].BackColor = Color.SkyBlue;
                        arr1[i0, j0] = 0;
                        frm1.hod = 1;
                        label6.Text = "Ход противника";
                        frm1.frm3.label6.Text = "Ваш ход";

                    }
                    else
                    {
                        bArr1[i0, j0].BackColor = Color.Crimson;
                        frm1.frm3.bArr2[i0, j0].BackColor = Color.Crimson;
                        arr1[i0, j0] = 1;
                    }

                    frm1.frm3.arr2[i0, j0] = -1;



                    //this.Hide();
                    //Thread.Sleep(2000);
                    //frm1.frm3.Show();
                }

            }


            if (frm1.lose(frm1.frm3.arr2, n))
            {
                dg = MessageBox.Show($"Победил {this.Text}, хотите сыграть еще раз?", "Победа", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                frm1.writeToMatchesFile(this.Text);
                this.Close();
                frm1.frm3.Close();

                if (dg == DialogResult.Yes)
                {
                    frm1.hod = new Random().Next(2);
                    frm1.button1_Click(sender, new EventArgs());
                }
            }



        }

        

    }


}
