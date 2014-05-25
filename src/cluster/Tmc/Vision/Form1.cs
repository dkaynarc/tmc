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

using Tmc.Common;

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
           // pictureBox1.Image = src.ToBitmap();
        }

        public void pictureBox2_draw(Image<Bgr, Byte> src)
        {
            pictureBox2.Image = src.ToBitmap();
        }

        public void getValue(ref int min, ref int max, ref double par3, ref double par4, ref int cannyThresh, ref int cannyAccumThresh)
        {
            min =               Convert.ToInt32(Nmin.Value);
            max =               Convert.ToInt32(Nmax.Value);
            cannyThresh =       Convert.ToInt32(Npar1.Value);
            cannyAccumThresh =  Convert.ToInt32(Npar2.Value);
            par3 =              Convert.ToDouble(Npar3.Value);
            par4 =              Convert.ToDouble(Npar4.Value);
            //(int)Nmax.Value;
            //par1 = (double)Npar1.Value;
            //par2 = (double)Npar2.Value;
        }

        public void trayFill(Tray<Tablet> trayList)
        {
            cell0.Text = colToString(trayList.Cells[0]);
            cell1.Text = colToString(trayList.Cells[1]);
            cell2.Text = colToString(trayList.Cells[2]);
            cell3.Text = colToString(trayList.Cells[3]);
            cell4.Text = colToString(trayList.Cells[4]);
            cell5.Text = colToString(trayList.Cells[5]);
            cell6.Text = colToString(trayList.Cells[6]);
            cell7.Text = colToString(trayList.Cells[7]);
            cell8.Text = colToString(trayList.Cells[8]);
            //textBox1.Text = b;
        }
        public string colToString(Tablet tablet)
        {
            if (tablet == null) return "none";
            else return tablet.Color.ToString();
            

        }
    }
}
