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
    public partial class KiForm : Form
    {
        private bool closing;

        public KiForm()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
            this.Text = "KiBoard";
            this.Size = new Size(1000, 800);
            closing = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            closing = true;
            e.Cancel = true;
        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {

        }

        public bool shouldClose()
        {
            return closing;
        }
    }
}
