using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tmc.Scada.App
{
    public partial class LoginForm : Form
    {
        MainForm mainform;

        /// <summary>
        /// Initialises form components
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Overloaded constructor accepts parent mainform
        /// </summary>
        /// <param name="mainForm"></param>
        public LoginForm(MainForm mainForm) : this()
        {
            this.mainform = mainForm;
        }

        /// <summary>
        /// Calls parent's Authenticate method based on user input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitButton_Click(object sender, EventArgs e)
        {
            mainform.Authenticate(this.userNameTextBox.ToString(), this.passwordTextBox.ToString());
            this.Close();
        }

    }
}
