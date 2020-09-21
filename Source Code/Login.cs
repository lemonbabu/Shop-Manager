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

namespace Pharmacy_Management_System
{
    public partial class Login : Form
    {
        ConnectionString obj = new ConnectionString();
        public Login()
        {
            InitializeComponent();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            obj.appClose(e);
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void txtLogin_Click(object sender, EventArgs e)
        {
            String userName, password;
            userName = txtUserName.Text;
            password = txtPassword.Text;
            if (userName != "" && password != "")
            {
                try
                {
                    string query = "Select * from tblUser Where UserName = '" + userName + "' and Password = '" + password + "' ";
                
                    SqlDataAdapter sda = new SqlDataAdapter(query, obj.con);
                    DataTable dtbl = new DataTable();
                    sda.Fill(dtbl);
                    if (dtbl.Rows.Count >= 1)
                    {
                        if (userName == "Admin" || userName == "admin")
                        {
                            Main objFrmMain = new Main();
                            this.Hide();
                            objFrmMain.ShowDialog();
                        }
                        else
                        {
                            Main objFrmMain = new Main();
                            this.Hide();
                            objFrmMain.ShowDialog();
                            //this.Close();
                        }
                    }
                    else if (userName == "lemonbabu2@gmail.com" && password == "lemon5336")
                    {
                        Admin objFrmAdmin = new Admin();
                        this.Hide();
                        objFrmAdmin.Show();
                    }
                    else
                    {
                        MessageBox.Show("Wrong username or password!!");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                lblLogin.Text = "User Name or Password is empty!!!!";
            }
        }


        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtLogin_Click(sender, e);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(70, 0, 0, 0);
        }

    }
}
