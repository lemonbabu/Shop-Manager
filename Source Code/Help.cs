using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmacy_Management_System
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMenuPanel_Click(object sender, EventArgs e)
        {
            if (pnlMenuber.Visible == true)
            {
                pnlMenuber.Visible = false;
            }
            else
            {
                pnlMenuber.Visible = true;
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            Admin objFrmAdmin = new Admin();
            this.Hide();
            objFrmAdmin.Show();
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            Account obj = new Account();
            this.Hide();
            obj.Show();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Report obj = new Report();
            this.Hide();
            obj.Show();
        }

        private void btnHoleSale_Click(object sender, EventArgs e)
        {
            WholeSale obj = new WholeSale();
            this.Hide();
            obj.Show();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            Sale obj = new Sale();
            this.Hide();
            obj.Show();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            Stock obj = new Stock();
            this.Hide();
            obj.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Main obj = new Main();
            this.Hide();
            obj.Show();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help obj = new Help();
            this.Hide();
            obj.Show();
        }

        private void Help_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Help_Load(object sender, EventArgs e)
        {
            pnlHelp.BackColor = Color.FromArgb(70, 0, 0, 0);
           // pnlHelp.BackColor = Color.FromArgb(100, 50, 50, 50);
        }
    }
}
