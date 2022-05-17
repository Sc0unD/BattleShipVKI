using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattleV3
{
    public partial class Form1 : Form
    {
        public Form2 frm2;
        public Form3 frm3;

        public int flag;
        public bool compflag;
        //int btclick;


        public Form1()
        {
            InitializeComponent();
            flag = new Random().Next(2);
            

        }

        public void load()
        {
            frm2 = new Form2(this);
            frm2.Show();
            frm3 = new Form3(this);
            frm3.Show();
            if (!compflag)
            {
                if (flag == 0)
                {
                    frm2.label6.Text = "Ваш ход";
                    frm3.label6.Text = "Ход противника";
                    //frm3.Hide();
                }
                else
                {
                    //flag = 0;
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
    }
}
