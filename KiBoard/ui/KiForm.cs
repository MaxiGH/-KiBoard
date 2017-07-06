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
        private bool keyPressed;

        public KiForm()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
            this.Text = "KiBoard";
            this.Size = new Size(800, 600);
            closing = false;
            keyPressed = false;
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

        public bool ShouldClose
        {
            get { return closing; }
            set { closing = true; }
        }

        private void KiForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressed = true;
        }

        public bool isKeyPressed()
        {
            bool result = keyPressed;
            keyPressed = false;
            return result;
        }
    }
}
