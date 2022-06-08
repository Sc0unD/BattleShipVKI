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
        int x0 = 30, y0 = 35, h = 34, xd = 396;
        public double[,] arr1 = new double[n, n], arr2 = new double[n, n];      
        public Button [,] bArr1 = new Button[n, n], bArr2 = new Button[n, n];

        Random rnd = new Random();

        List<double> ships = new()
        {
            4.1, 3.1, 3.2, 2.1, 2.2, 2.3, 1.1, 1.2, 1.3, 1.4
            // 1.x - кол-во палуб;
            // x.1 - номер;
        };

        public Form2(Form1 fr1)
        {
            InitializeComponent();
            this.frm1 = fr1;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 1080 / 2 - (int)(this.Height / 1.5)); 
            this.Width = 812;
            this.Height = 514;
            label6.Location = new Point(12,this.Height - 56 - label6.Height);
            label3.Location = new Point(this.Width - 30 - label3.Width, this.Height - 56 - label3.Height);
            label1.Location = new Point(this.Width - 30 - label1.Width, label3.Location.Y - 10 - label1.Height);
            label2.Location = new Point(this.Width - 30 - label2.Width, label1.Location.Y - 10 - label2.Height);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //frm1.distribution(arr2, n, ships);
            frm1.shipsAdd(ref arr2, n, ships);

            if (frm1.textBox1.Text == "")
                frm1.textBox1.Text = "Player1";
            this.Text = frm1.textBox1.Text;

            label4.Top = 10;
            label4.Left = x0;
            label5.Top = 10;
            label5.Left = x0 + xd;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++) //0 -> unknown; [-1;-4] -> ship; -5 -> shot area 
                {
                    arr1[i, j] = 0;
                    bArr1[i, j] = new Button();
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
                    bArr2[i, j] = new Button();
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


            if (frm1.hod == 0 && arr1[i0, j0] == 0)
            {
                if (frm1.frm3.arr2[i0, j0] == 0)
                {
                    bArr1[i0, j0].BackColor = Color.Aqua;
                    frm1.frm3.bArr2[i0, j0].BackColor = Color.SkyBlue;
                    arr1[i0, j0] = -6.0;
                    frm1.frm3.arr2[i0, j0] = -6.0;
                    frm1.hod = 1;
                    label6.Text = "Ход противника";
                    frm1.frm3.label6.Text = "Ваш ход";
                    if (frm1.compflag)
                    {
                        frm1.frm3.timer1.Enabled = true;
                    }
                    else
                    {
                        //this.Hide();
                        //Thread.Sleep(2000);
                        //frm1.frm3.Show();
                    }

                }
                else
                {
                    if (frm1.killOrNot(frm1.frm3.arr2, i0, j0, n))
                    {
                        //frm1.frm3.arr2[i0, j0] = -5.0;
                        //bArr1[i0, j0].BackColor = Color.Crimson;
                        //frm1.frm3.bArr2[i0, j0].BackColor = Color.Crimson;
                        frm1.paintIfKill(ref arr1, ref frm1.frm3.arr2, ref bArr1, ref frm1.frm3.bArr2, i0, j0,n);
                    }
                    else
                    {
                        frm1.frm3.arr2[i0, j0] *= -1;
                        arr1[i0, j0] = frm1.frm3.arr2[i0, j0];
                        bArr1[i0, j0].BackColor = Color.DarkOrange;
                        frm1.frm3.bArr2[i0, j0].BackColor = Color.DarkOrange;
                    }
                    //arr1[i0, j0] = frm1.frm3.arr2[i0, j0];
                    //arr2[i0, j0] = frm1.frm3.arr2[i0, j0];

                }
                //frm1.frm3.arr2[i0, j0] = -1;
                
            }



            if (frm1.lose(frm1.frm3.arr2, n))
            {
                frm1.writeToMatchesFile(this.Text);
                dg = MessageBox.Show($"Победил {this.Text}, хотите сыграть еще раз?", "Победа", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                this.Close();
                frm1.frm3.Close();

                if (dg == DialogResult.Yes)
                {
                    frm1.hod = new Random().Next(2);
                    if (!frm1.compflag)
                        frm1.button1_Click(sender, new EventArgs());
                    else
                        frm1.button2_Click(sender, new EventArgs());
                }
            }
        }
    }
}
