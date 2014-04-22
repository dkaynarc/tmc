using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.GPU;

namespace Tmc.Vision
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void pictureBox1_draw(Image<Gray, Byte> src)
        {
            pictureBox1.Image = src.ToBitmap();
        }

        public void pictureBox2_draw(Image<Bgr, Byte> src)
        {
            pictureBox2.Image = src.ToBitmap();
        }

        public void getValue(out int min, out int max, out double par1, out double par2)
        {
            min = (int)Nmin.Value;
            max = (int)Nmax.Value;
            par1 = (double)Npar1.Value;
            par2 = (double)Npar2.Value;
        }
    }
}
