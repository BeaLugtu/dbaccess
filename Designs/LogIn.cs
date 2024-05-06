using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseSQLConnectedSignUpXample.Designs
{
    public partial class LogIn : Form
    {

        DBAccess objDABAccess = new DBAccess();
        DataTable dtUsers = new DataTable();

        public LogIn()
        {
            InitializeComponent();
        }

        private void logInBtn_Click(object sender, EventArgs e)
        {
            string UserEmail = usernameTB.Text;
            string Password = passwordTB.Text;

            if (UserEmail.Equals(""))
            {
                MessageBox.Show("Please Enter Your Email");
            }

            else if (UserEmail.Equals(""))
            {
                MessageBox.Show("Please Enter Your Password");
            }
            else
            {
                string query = "Select * from Users Where Email= '" + UserEmail + "' AND Password = '" + Password + "'";

                objDABAccess.readDatathroughAdapter(query, dtUsers);
                
                if(dtUsers.Rows.Count > 0 )
                {
                    MessageBox.Show("Successfully Log in.");
                    objDABAccess.closeConn();
                    this.Hide();
                    
                    HomePage homePage = new HomePage();
                    homePage.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Input. Please Try Again.");
                }
            }

        }

        private void SignUpFormBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUp signUp = new SignUp();
            signUp.Show();
        }

    }
}
