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
        StreamReader file;
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

        private void button1_Click(object sender, EventArgs e)
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
            //file = new(Directory.GetCurrentDirectory() + "\\Materials\\info.txt", Encoding.UTF8);
            file = new("Materials\\info.txt", Encoding.UTF8);
            MessageBox.Show(file.ReadToEnd(), "Помощь");
            file.Close();
        }

        public double[,] distribution(double [,] arr,int n,List<double> ships)
        {
            int dir, i0, j0, f = 0;
            //double cur;
            bool flag;
            while (f < ships.Count)
            {
                flag = true;
                //cur = ships[f];
                dir = rnd.Next(2);
                i0 = rnd.Next(n);
                j0 = rnd.Next(n);
                try
                {
                    if (dir == 0)
                    {
                        for (int i = i0 - 1; i <= i0 + (int)ships[f]; i++)
                        {
                            for (int j = j0 - 1; j <= j0+1; j++)
                            {
                                if (arr[i, j] != 0)
                                    flag = false;
                                
                            }
                            
                        }

                        if (flag)
                        {
                            for (int i = i0; i < i0 + (int)ships[f]; i++)
                            {
                                arr[i, j0] = ships[f];
                            }
                            
                        }
                    }

                    if (dir == 1)
                    {
                        for (int j = j0 - 1; j <= j0 + (int)ships[f]; j++)
                        {
                            for (int i = i0 - 1; i <= i0 + 1; i++)
                            {
                                if (arr[i, j] != 0)
                                    flag = false;
                            }
                            
                        }

                        if (flag)
                        {
                            for (int j = j0; j < j0 + (int)ships[f]; j++)
                            {
                                arr[i0, j] = ships[f];
                            }
                            
                        }
                    }

                    if (flag)
                    {
                        f++;
                    }
                
                }
                catch (IndexOutOfRangeException) 
                {
                    continue;
                }
              
                
            }



            return arr;
        }


    }
}
