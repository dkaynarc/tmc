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
        MainForm parent;

        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(MainForm mainForm) : this()
        {
            this.parent = mainForm;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            parent.Authenticate(this.userNameTextBox.Text.ToString(), this.passwordTextBox.Text.ToString());
            this.Close();
        }

    }
}
