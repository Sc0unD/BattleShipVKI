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
    public partial class Form3 : Form
    {
        public Form1 frm1;
        const int n = 10;
        int x0 = 30, y0 = 35, h = 34, xd = 396, ic, jc;
        public double[,] arr1 = new double[n, n], arr2 = new double[n, n];
        public Button[,] bArr1 = new Button[n, n], bArr2 = new Button[n, n];

        Random rnd = new Random();

        List<double> ships = new()
        {
            4.1, 3.1, 3.2, 2.1, 2.2, 2.3, 1.1, 1.2, 1.3, 1.4
        };

        int dir = 0; 
        bool flag_dir = false;

        public Form3(Form1 fr1)
        {
            InitializeComponent();
            this.frm1 = fr1;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(1920 - 100 - this.Width, 1080 / 2 - (int)(this.Height / 1.5));
            this.Width = 812;
            this.Height = 514;
            timer1.Interval = 10;
            label6.Location = new Point(12, this.Height - 56 - label6.Height);
            label3.Location = new Point(this.Width - 30 - label3.Width, this.Height - 56 - label3.Height);
            label1.Location = new Point(this.Width - 30 - label1.Width, label3.Location.Y - 10 - label1.Height);
            label2.Location = new Point(this.Width - 30 - label2.Width, label1.Location.Y - 10 - label2.Height);

        }
        private void Form3_Load(object sender, EventArgs e)
        {
            frm1.shipsAdd(ref arr2, n, ships);

            if (frm1.textBox2.Text == "")
                frm1.textBox2.Text = "Player2";
            this.Text = frm1.textBox2.Text;

            label4.Top = 15;
            label4.Left = x0;
            label5.Top = 15;
            label5.Left = x0 + xd;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
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
                    if (arr2[i, j] == 0)
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
            

            if (frm1.hod == 1 && arr1[i0,j0] == 0)
            {

                if (frm1.frm2.arr2[i0, j0] == 0)
                {
                    bArr1[i0, j0].BackColor = Color.Aqua;
                    frm1.frm2.bArr2[i0, j0].BackColor = Color.SkyBlue;
                    //frm1.hod = 0;
                    arr1[i0, j0] = -6.0;
                    frm1.frm2.arr2[i0, j0] = -6.0;
                    label6.Text = "Ход противника";
                    frm1.frm2.label6.Text = "Ваш ход";
                    dir = 0;
                    flag_dir = false;
                    if (frm1.compflag)
                    {
                        //timer1.Enabled = false;
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
                    if (frm1.killOrNot(frm1.frm2.arr2, i0, j0, n))
                    {
                        frm1.paintIfKill(ref arr1, ref frm1.frm2.arr2, ref bArr1, ref frm1.frm2.bArr2, i0, j0, n);
                        dir = 0;
                        flag_dir = false;
                    }
                    else
                    {
                        frm1.frm2.arr2[i0, j0] *= -1;
                        arr1[i0, j0] = frm1.frm2.arr2[i0, j0];
                        bArr1[i0, j0].BackColor = Color.DarkOrange;
                        frm1.frm2.bArr2[i0, j0].BackColor = Color.DarkOrange;
                    }
                }
            }

            if (frm1.lose(frm1.frm2.arr2, n))
            {
                if (frm1.compflag)
                    timer1.Enabled = false;
                frm1.writeToMatchesFile(this.Text);
                dg = MessageBox.Show($"Победил {this.Text}, хотите сыграть еще раз?", "Победа", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                this.Close();
                frm1.frm2.Close();

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

        public void hodForComp()
        {
            Thread.Sleep(1000);
            bool flag = false, dir_flag = false, flag_hod = false;
            int dir = 0; //1 - left, 2 - right, 3 - up, 4 - down
            ic = jc = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (arr1[i, j] < 0 && arr1[i, j] > -5.0)
                    {
                        ic = i;
                        jc = j;
                        flag = true;
                        break;
                    }
                }
                if (flag)
                    break;
            }

            if (flag)
            {
                if (jc-1 >= 0 && jc + 1 < n)
                {
                    if (arr1[ic,jc-1] == arr1[ic, jc] || arr1[ic, jc + 1] == arr1[ic, jc])
                    {
                        dir = rnd.Next(1, 3);
                        dir_flag = true;
                    }
                }
                else if (jc + 1 >= n)
                {
                    if (arr1[ic, jc - 1] == arr1[ic, jc])
                    {
                        dir = 1;
                        dir_flag = true;
                    }
                }

                else if (jc - 1 < 0)
                {
                    if (arr1[ic, jc + 1] == arr1[ic, jc])
                    {
                        dir = 2;
                        dir_flag = true;
                    }
                }

                else if (ic - 1 >= 0 && ic + 1 < n)
                {
                    if (arr1[ic - 1, jc] == arr1[ic, jc] || arr1[ic + 1, jc] == arr1[ic, jc])
                    {
                        dir = rnd.Next(3, 5);
                        dir_flag = true;
                    }
                }
                else if (ic + 1 >= n)
                {
                    if (arr1[ic - 1, jc] == arr1[ic, jc])
                    {
                        dir = 3;
                        dir_flag = true;
                    }
                }

                else if (jc - 1 < 0)
                {
                    if (arr1[ic + 1, jc] == arr1[ic, jc])
                    {
                        dir = 4;
                        dir_flag = true;
                    }
                }
                if (!dir_flag)
                {
                    while (!dir_flag)
                    {
                        dir = rnd.Next(1,5);
                        dir_flag = true;
                        if (dir == 1)
                        {
                            if (jc - 1 < 0)
                                dir_flag = false;
                        }
                        else if (dir == 2)
                        {
                            if (jc + 1 >= n)
                                dir_flag = false;
                        }
                        else if (dir == 3)
                        {
                            if (ic - 1 < 0)
                                dir_flag = false;
                        }
                        else if (dir == 4)
                        {
                            if (ic + 1 >= n)
                                dir_flag = false;
                        }
                    }
                }
            }

            if (flag && dir_flag)
            {
                while (!flag_hod)
                {
                    if (dir == 1)
                    {
                        if (arr1[ic,jc - 1] == 0 || arr1[ic, jc - 1] == -6.0)
                        {
                            flag_hod = true;
                        }
                        jc--;
                    }
                    else if (dir == 2)
                    {
                        if (arr1[ic, jc + 1] >= 0 || arr1[ic, jc + 1] == -6.0)
                        {
                            flag_hod = true;
                        }
                        jc++;
                    }
                    else if (dir == 3)
                    {
                        if (arr1[ic - 1, jc] >= 0 || arr1[ic - 1, jc] == -6.0)
                        {
                            flag_hod = true;
                        }
                        ic--;
                    }
                    else if (dir == 4)
                    {
                        if (arr1[ic + 1, jc] >= 0 || arr1[ic + 1, jc] == -6.0)
                        {
                            flag_hod = true;
                        }
                        ic++;
                    }
                }
            }

            if (!flag && !flag_dir)
            {
                while (arr1[ic = rnd.Next(n), jc = rnd.Next(n)] != 0) {; }
            }
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            if (frm1.hod == 1 && frm1.compflag)
            {
                hodForComp();
                bt_Click(bArr1[ic, jc], new EventArgs());
            }
        }

    }
}
