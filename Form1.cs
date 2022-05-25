using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SeaBattleV3
{
    public partial class Form1 : Form
    {
        public Form2 frm2;
        public Form3 frm3;
        public int hod;
        public bool compflag;
        Random rnd = new();
        //int btclick;


        public Form1()
        {
            InitializeComponent();
            hod = rnd.Next(2);

        }

        public void load()
        {
            frm2 = new Form2(this);
            frm2.Show();
            frm3 = new Form3(this);
            frm3.Show();
            if (compflag)
            {
                frm2.label6.Text = "Ваш ход";
                //frm3.label6.Text = "Ход противника";
                frm3.Hide();
            }
            else if (!compflag)
            {
                if (hod == 0)
                {
                    frm2.label6.Text = "Ваш ход";
                    frm3.label6.Text = "Ход противника";
                    //frm3.Hide();
                }
                else
                {
                    //hod = 0;
                    frm3.label6.Text = "Ваш ход";
                    frm2.label6.Text = "Ход противника";
                    //frm2.Hide();
                }
            }
            
        }

        public void button1_Click(object sender, EventArgs e)
        {
            //btclick = 1;
            compflag = false;
            load(); 
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //btclick = 2;
            compflag = true;
            
            load();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            MessageBox.Show(File.ReadAllText("Materials\\info.txt"), "Помощь");
        }

        public double[,] distribution(double [,] arr,int n,List<double> ships)
        {
            int dir, i0 = 0, j0 = 0, f = 0;
            int lim1, lim2, lim3, lim4;
            int i, j;
            bool flag;
            while (f < ships.Count)
            {
                flag = true;
                dir = rnd.Next(0,2);
                lim1 = lim2 = lim3 = lim4 = 1;
                //lim1 -- i u ; lim2 -- i d; lim3 -- j l; lim4 -- j r; 
               // try
                //{
                    if (dir == 0)
                    {
                        i0 = rnd.Next(0, n - (int)ships[f] + 1);
                        j0 = rnd.Next(0, n);

                        if (i0 == 0)
                        {
                            lim1 = 0; 
                        }
                        if (i0 + (int)ships[f] >=n)
                        {
                            lim2 = 0;
                        }
                        if (j0 == 0)
                        {
                            lim3 = 0;
                        }
                        if (j0 == n-1)
                        {
                            lim4 = 0; 
                        }


                        for ( i = i0 - lim1; i < i0 + (int)ships[f] + lim2; i++)
                        {
                            for ( j = j0 - lim3; j < j0 + lim4; j++)
                            {
                                if (arr[i, j] != 0)
                                    flag = false;
                                
                            }
                            
                        }

                        if (flag)
                        {
                            for ( i = i0; i < i0 + (int)ships[f]; i++)
                            {
                                arr[i, j0] = ships[f];
                            }
                            f++;
                        }
                    }

                    if (dir == 1)
                    {
                        j0 = rnd.Next(0, n - (int)ships[f] + 1);
                        i0 = rnd.Next(0, n);

                        if (j0 == 0)
                        {
                            lim3 = 0;
                        }
                        if (j0 + (int)ships[f] >= n)
                        {
                            lim4 = 0;
                        }
                        if (i0 == 0)
                        {
                            lim1 = 0;
                        }
                        if (i0 == n - 1)
                        {
                            lim2 = 0;
                        }
                        
                        for ( j = j0 - lim3; j < j0 + (int)ships[f] +lim4; j++)
                        {
                            for ( i = i0 - lim1; i < i0 + lim2; i++)
                            {
                                if (arr[i, j] != 0)
                                    flag = false;
                            }
                            
                        }

                        if (flag)
                        {
                            for ( j = j0; j < j0 + (int)ships[f]; j++)
                            {
                                arr[i0, j] = ships[f];
                            }
                            f++;
                        }
                    }


                //}
               // catch 
               // {
                //    continue;
                //}
            }
            return arr;
        }
        //public double[,] distribution(double[,] arr, int n, List<double> ships)
        //{
        //    int dir, i0, j0, f = 0, num1, num2;
        //    //double cur;
        //    bool flag;
        //    while (f < ships.Count)
        //    {
        //        flag = true;
        //        //cur = ships[f];
        //        dir = rnd.Next(2);
        //        i0 = rnd.Next(n);
        //        j0 = rnd.Next(n);


        //        if (dir == 0)
        //        {
        //            num1 = 1;
        //            num2 = 1;
        //            if (i0 + (int)ships[f] >= n)
        //                continue;
        //            if (i0 + (int)ships[f] + 1 == n)
        //                num1 = 0;
        //            if (i0 - 1 < 0)
        //                num2 = 0;

        //            for (int i = i0 - num2; i < i0 + (int)ships[f] + num1; i++)
        //            {
        //                for (int j = j0 - 1; j <= j0 + 1; j++)
        //                {
        //                    if (arr[i, j] != 0)
        //                        flag = false;

        //                }

        //            }

        //            if (flag)
        //            {
        //                for (int i = i0; i < i0 + (int)ships[f]; i++)
        //                {
        //                    arr[i, j0] = ships[f];
        //                }

        //            }
        //        }

        //        if (dir == 1)
        //        {
        //            num1 = 1;
        //            num2 = 1;
        //            if (j0 + (int)ships[f] >= n)
        //                continue;
        //            if (j0 + (int)ships[f] + 1 >= n)
        //                num1 = 0;
        //            for (int j = j0 - 1; j < j0 + (int)ships[f] + num1; j++)
        //            {
        //                for (int i = i0 - 1; i <= i0 + 1; i++)
        //                {
        //                    if (arr[i, j] != 0)
        //                        flag = false;
        //                }

        //            }

        //            if (flag)
        //            {
        //                for (int j = j0; j < j0 + (int)ships[f]; j++)
        //                {
        //                    arr[i0, j] = ships[f];
        //                }

        //            }
        //        }

        //        if (flag)
        //        {
        //            f++;
        //        }



        //    }



        //    return arr;
        //}

        public bool lose(double[,] arr, int n)
        {
            for (int i = 0; i<n; i++)
            {
                for (int j = 0; j<n ; j++)
                {
                    if (arr[i,j] > 0)
                        return false;
                }
            }
            return true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(File.ReadAllText("Materials\\match_results.txt"), "История матчей");
        }

        public void writeToMatchesFile(string name)
        {
            File.AppendAllText("Materials\\match_results.txt", $"№{File.ReadAllLines("Materials\\match_results.txt").Length + 1} -- {textBox1.Text}:{textBox2.Text} -> победил {name}\n");
        }

    }
}
