using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseSQLConnectedSignUpXample
{
    public partial class SignUp : Form
    {

        DBAccess objDBAccess = new DBAccess();

        public SignUp()
        {
            InitializeComponent();
        }
        private void signUpBtn_Click(object sender, EventArgs e)
        {
            string Username = nameTB.Text;
            string UserEmail = usernameTB.Text;
            string Password = passwordTB.Text;
            string Country = countryCB.Text;

            if (Username.Equals(""))
            {
                MessageBox.Show("Please Enter your User Name");
            }
            else if (UserEmail.Equals(""))
            {
                MessageBox.Show("Please Enter your Email");
            }
            else if (Password.Equals(""))
            {
                MessageBox.Show("Please Enter your Password");
            }
            else if (Country.Equals(""))
            {
                MessageBox.Show("Please Choose your Country");
            }
            else
            {
                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO Users(Name, Email, Password, Country) VALUES(@Username, @UserEmail, @Password, @Country)");

                insertCommand.Parameters.AddWithValue("@Username", Username);
                insertCommand.Parameters.AddWithValue("@UserEmail", UserEmail);
                insertCommand.Parameters.AddWithValue("@Password", Password);
                insertCommand.Parameters.AddWithValue("@Country", Country);

                int row = objDBAccess.executeQuery(insertCommand);

                if (row == 1)
                {
                    MessageBox.Show("Account Created Successfully");
                    this.Hide();
                    HomePage homePage = new HomePage();
                    homePage.Show();
                }
                else
                {
                    MessageBox.Show("Error Occurred. Try again");
                }
            }
        }

        private void LogInFormBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Designs.LogIn login = new Designs.LogIn();
            login.Show();
        }
    }
}
