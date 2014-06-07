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
    public partial class CreateUserForm : Form
    {
        WebApiClient _webApiClient;

        public CreateUserForm()
        {
            InitializeComponent();
        }

        public CreateUserForm(WebApiClient _webApiClient) : this()
        {
            this._webApiClient = _webApiClient;       
        }

        private void createNewUserButton_Click(object sender, EventArgs e)
        {
            String name = this.nameTextBox.Text.ToString();
            String password = this.passwordTextBox.Text.ToString();
            String roleName = this.userTypeTextBox.Text.ToString();
            _webApiClient.AddUser(name,password,roleName);
            // test new user
            testUserCreation(name, password);
            clearTextBoxes();
        }

        private void clearTextBoxes()
        {
            this.nameTextBox.Clear();
            this.passwordTextBox.Clear();
            this.userTypeTextBox.Clear();
        }

        private void testUserCreation(String name, String password)
        {
            var userInfo = _webApiClient.Authenticate(name, password);
            if (userInfo.Result == "success")
            {
                MessageBox.Show("User created successfully");
            }
            else
            {
                MessageBox.Show("User could not be created.\nPlease try again.");
            }
        }
    }
}
