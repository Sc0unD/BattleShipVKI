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
            if (!compflag)
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
            else if (compflag)
            {
                frm2.label6.Text = "Ваш ход";
                //frm3.label6.Text = "Ход противника";
                frm3.Hide();
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

        public void shipsAdd(ref double[,] arr, int n, List<double> ships)
        {
            int i0, j0, dir;
            bool shipPlaced;

            foreach(var ship in ships)
            {
                dir = rnd.Next(2); // 0 -> vertical  1 -> horizontal
                shipPlaced = false;
                while (!shipPlaced)
                {
                    if (dir == 0)
                    {
                        i0 = rnd.Next(n - (int)ship + 1);
                        j0 = rnd.Next(n);
                    }
                    else
                    {
                        i0 = rnd.Next(n);
                        j0 = rnd.Next(n - (int)ship + 1);
                    }
                    if (!canPlaceShip(arr,n,i0,j0,dir,ship))
                    {
                        continue;
                    }

                    placeShip(ref arr ,ship, i0, j0, dir);
                    shipPlaced = true;
                }
                
                
                
            }
        }

        bool canPlaceShip(double[,] arr, int n, int ib, int jb, int dir, double ship)
        {
            int lim1 = 1, lim2 = 1, lim3 = 1, lim4 = 1; /// lim1 -> i up, lim2 -> i down, lim3 -> j left, lim4 -> j right

            if (dir == 0)
            {
                if (ib - 1 < 0)
                    lim1 = 0;
                if (ib + (int)ship >= n)
                    lim2 = 0;
                if (jb - 1 < 0)
                    lim3 = 0;
                if (jb + 1 >= n)
                    lim4 = 0;

                for (int i = ib - lim1; i < ib + (int)ship + lim2; i++)
                {
                    for (int j = jb - lim3; j <= jb + lim4; j++)
                    {
                        if (arr[i, j] != 0)
                            return false;
                    }

                }
            }

            else
            {
                if (ib - 1 < 0)
                    lim1 = 0;
                if (ib + 1 >= n)
                    lim2 = 0;
                if (jb - 1 < 0)
                    lim3 = 0;
                if (jb + (int)ship >= n)
                    lim4 = 0;

                for (int i = ib - lim1; i <= ib + lim2 ; i++)
                {
                    for (int j = jb - lim3; j < jb + (int)ship + lim4; j++)
                    {
                        if (arr[i, j] != 0)
                            return false;
                    }

                }
            }

            return true;
        }

        void placeShip(ref double[,] arr, double ship, int i0, int j0, int dir)
        {
            if (dir == 0)
            {
                for (int i = i0; i < i0 + (int)ship; i++)
                {
                    arr[i, j0] = ship;
                }
            }
            else 
                for (int j = j0; j < j0 + (int)ship; j++)
                {
                    arr[i0, j] = ship;
                }
        }

        //public double[,] distribution(double [,] arr,int n,List<double> ships)
        //{
        //    int dir, i0 = 0, j0 = 0, f = 0;
        //    bool flag;
        //    while (f < ships.Count)
        //    {
        //        flag = true;
        //        dir = rnd.Next(0,2);


        //        try
        //        {
        //            if (dir == 0)
        //            {
        //                i0 = rnd.Next(0, n - (int)ships[f] + 1);
        //                j0 = rnd.Next(0, n);
        //                for (int i = i0 - 1; i <= i0 + (int)ships[f]; i++)
        //                {
        //                    for (int j = j0 - 1; j <= j0+1; j++)
        //                    {
        //                        if (arr[i, j] != 0)
        //                            flag = false;
                                
        //                    }
                            
        //                }

        //                if (flag)
        //                {
        //                    for (int i = i0; i < i0 + (int)ships[f]; i++)
        //                    {
        //                        arr[i, j0] = ships[f];
        //                    }
                            
        //                }
        //            }

        //            if (dir == 1)
        //            {
        //                j0 = rnd.Next(0,n - (int)ships[f] + 1);
        //                i0 = rnd.Next(0,n);
        //                for (int j = j0 - 1; j <= j0 + (int)ships[f]; j++)
        //                {
        //                    for (int i = i0 - 1; i <= i0 + 1; i++)
        //                    {
        //                        if (arr[i, j] != 0)
        //                            flag = false;
        //                    }
                            
        //                }

        //                if (flag)
        //                {
        //                    for (int j = j0; j < j0 + (int)ships[f]; j++)
        //                    {
        //                        arr[i0, j] = ships[f];
        //                    }
                            
        //                }
        //            }

        //            if (flag)
        //            {
        //                f++;
        //            }
                
        //        }
        //        catch (IndexOutOfRangeException) 
        //        {
        //            continue;
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
