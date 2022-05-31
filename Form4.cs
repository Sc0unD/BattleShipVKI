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
    public partial class Form4 : Form
    {
        public Form1 frm1;
        public Form4(Form1 fr1)
        {
            InitializeComponent();
            frm1 = fr1;
            this.Width = 812;
            this.Height = 514;
        }
    }
}
