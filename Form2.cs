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
        int x0 = 30, y0 = 45, h = 32, xd = 396;
        public double[,] arr1 = new double[n, n];
        public double[,] arr2 = new double[n, n];      
        public Button [,] bArr1 = new Button[n, n];
        public Button[,] bArr2 = new Button[n, n];

        Random rnd = new Random();

        List<double> ships = new()
        {
            4.1, 3.1, 3.2, 2.1, 2.2, 2.3, 1.1, 1.2, 1.3, 1.4
        };

        // 1.xx - кол-во палуб;
        // x.1x - номер;


        public Form2(Form1 frm1)
        {
            InitializeComponent();
            this.frm1 = frm1;
            
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
                    arr1[i, j] = 0;
                    bArr1[i, j] = new Button();
                    bArr1[i, j].Text = arr1[i, j].ToString();
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
            int i0, j0;
            i0 = (bt.Top - y0) / h;
            j0 = (bt.Left - x0) / h;
            

            if (!frm1.compflag)
            {
                if (frm1.hod == 0)
                {
                    arr1[i0, j0] = frm1.frm3.arr2[i0, j0];
                    bArr1[i0, j0].Text = frm1.frm3.bArr2[i0, j0].Text;
                    if (arr1[i0, j0] == 0)
                    {
                        bArr1[i0, j0].BackColor = Color.Aqua;
                        frm1.frm3.bArr2[i0, j0].BackColor = Color.Aqua;
                    }
                    else
                    {
                        bArr1[i0, j0].BackColor = Color.Red;
                        frm1.frm3.bArr2[i0, j0].BackColor = Color.Red;
                    }
                    frm1.hod = 1;
                    label6.Text = "Ход противника";
                    frm1.frm3.label6.Text = "Ваш ход";
                    //this.Hide();
                    //Thread.Sleep(2000);
                    //frm1.frm3.Show();
                }

            
               

            //else
            //{
            //    frm1.flag = 1;
            //    Thread.Sleep(5000);
            //    int ic = rnd.Next(n), jc = rnd.Next(n);
            //    while (frm1.frm3.arr1[ic,jc] != 0)
            //    {
            //        ic = rnd.Next(n);
            //        jc = rnd.Next(n);
            //    }

            //    frm1.frm3.bt_Click(frm1.frm3.arr1[ic, jc],new EventArgs());
            //    frm1.flag = 0;
            //}
                
            }

            //this.Text = System.IO.Directory.GetCurrentDirectory() + @"\Resourses\3117918.png";
            //int i0 = (bt.Top - y0) / h, j0 = (bt.Left - x0) / h;
            //bArr1[i0, j0].Text = "";
            //bArr1[i0, j0].Image = Properties.Resources._3117918;
        }

        //bool potopil(int ia, int ja)
        //{
        //    if (arr2[ia,ja] % 10 == 1)
        //    {
    
        //    }

        //    else if (arr2[ia, ja] % 10 == 2)
        //    {

        //    }

        //    return false;
        //}

    }


}
