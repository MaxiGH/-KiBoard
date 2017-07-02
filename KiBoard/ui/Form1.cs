using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiBoard.ui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
            this.Text = "KiBoard";
            this.Size = new Size(1000, 800);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
