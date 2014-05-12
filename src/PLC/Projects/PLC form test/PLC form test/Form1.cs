using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PLC_form_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();



        }

        public string returnName(int i)
        {
            switch(i)
            {
                case 0:
                    return "Tray 4";
                case 1:
                    return "Tray 1";
                case 2:
                    return "Tray 2";
                case 3:
                    return "Tray 3";
                case 4:
                    return "Tray 5";
                case 5:
                    return "Tray 6";
                case 6:
                    return "PLC Emergency Stop";
                case 7:
                    return "SCADA Emergency Stop";


            }
            return null;
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            //axAsadtcp1.Function = ASADTCP_FUNC_READ ;
            richTextBox1.Clear();
             //   .Document.Blocks.Clear();
            axAsadtcp1.MemStart = "Y0";

            axAsadtcp1.MemQty = 1;
            axAsadtcp1.SyncRefresh();

            //axAsadtcp1.ShowAboutBox();
            // Asadtcp1.GetDataBitM(i) 
            

            for (short i = 0; i < 8; i++)
            {

                if (this.axAsadtcp1.GetDataBitM(i))
                {
                    richTextBox1.AppendText(returnName(i) + ": true, \n");
                }
                if (!this.axAsadtcp1.GetDataBitM(i))
                {
                    richTextBox1.AppendText(returnName(i) + ": false, \n");
                }

            }
            richTextBox1.AppendText("done");
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void axAsadtcp1_Complete(object sender, AxASADTCPLib._DAsadtcpEvents_CompleteEvent e)
        {

        }
    }
}
