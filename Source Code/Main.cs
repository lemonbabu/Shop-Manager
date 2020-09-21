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
    public partial class Main : Form
    {
        ConnectionString obj = new ConnectionString();
        public Main()
        {
            InitializeComponent(); 
            pnlSearchResult.Visible = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            pnlHome.BackColor = Color.FromArgb(100, 0, 0, 0);
            label1.BackColor = Color.FromArgb(100, 0, 0, 0);
            // TODO: This line of code loads data into the 'pMSDataSet.tblStocks' table. You can move, or remove it, as needed.
            //this.tblStocksTableAdapter.Fill(this.pMSDataSet.tblStocks);
            Login login = new Login();
            login.Close();

            obj.cmd = new SqlCommand("select ProductName from tblStocks", obj.con);
            obj.con.Open();
            SqlDataReader dr = obj.cmd.ExecuteReader();
            AutoCompleteStringCollection mycollaction = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                mycollaction.Add(dr.GetString(0));
            }
            txtSearchMedicine.AutoCompleteCustomSource = mycollaction;
            obj.con.Close();
        }

        private void pnlHome_Paint(object sender, PaintEventArgs e)
        {
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm tt");
            lblSec.Text = DateTime.Now.ToString("ss");
            lblDate.Text = DateTime.Now.ToString("dd MM yyyy");
            lblDay.Text = DateTime.Now.ToString("dddd");
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help obj = new Help();
            this.Hide();
            obj.Show();
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
            pnlSearchResult.Visible = false;
        }
        
        private void tblStocksBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tblStocksBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.pMSDataSet);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            String prdName = txtSearchMedicine.Text.Trim();
            SqlDataAdapter adapt = new SqlDataAdapter("select *  from tblStocks where ProductName = '" + prdName + "'", obj.con);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                tblStocksDataGridView.DataSource = dt;
                obj.con.Close();
                pnlSearchResult.Visible = true;
            }
            else
                MessageBox.Show("This Product is not found", "Sorry");
        }


        private void txtSearchMedicine_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearchMedicine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void txtSearchMedicine_Enter(object sender, EventArgs e)
        {
            txtSearchMedicine.Text = "";
        }

   
    }
}
