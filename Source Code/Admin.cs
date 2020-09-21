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
    public partial class Admin : Form
    {
        ConnectionString obj = new ConnectionString();
        public Admin()
        {
            InitializeComponent();
            pnlAdminMenu.Visible = true;
            pnlUpdateProductSalePrice.Visible = false;
            pnlUpdateProductSalePrice.Visible = false;
            pnlViewMedicines.Visible = false;
            pnlChangePassword.Visible = false;
            pnlAddExpense.Visible = false;
            pnlViewExpense.Visible = false;
            lblTitle.Text = "Admin Menu";
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            pnlAdminMenu.BackColor = Color.FromArgb(70, 0, 0, 0);
            pnlUpdateProductSalePrice.BackColor = Color.FromArgb(70, 0, 0, 0);
            pnlAddExpense.BackColor = Color.FromArgb(100, 50, 50, 50);
            pnlChangePassword.BackColor = Color.FromArgb(100, 50, 50, 50);
            lblTitle.BackColor = Color.FromArgb(100, 50, 50, 50);
            // TODO: This line of code loads data into the 'pMSDataSet.tblExpense' table. You can move, or remove it, as needed.
            //this.tblExpenseTableAdapter.Fill(this.pMSDataSet.tblExpense);
            // TODO: This line of code loads data into the 'pMSDataSet.tblUser' table. You can move, or remove it, as needed.
            //this.tblUserTableAdapter.Fill(this.pMSDataSet.tblUser);
            // TODO: This line of code loads data into the 'pMSDataSet.tblStocks' table. You can move, or remove it, as needed.
            //this.tblStocksTableAdapter.Fill(this.pMSDataSet.tblStocks);
            Main main = new Main();
            main.Close();
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

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help obj = new Help();
            this.Hide();
            obj.Show();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            pnlAdminMenu.Visible = true;
            pnlUpdateProductSalePrice.Visible = false;
            pnlUpdateProductSalePrice.Visible = false;
            pnlViewMedicines.Visible = false;
            pnlChangePassword.Visible = false;
            pnlAddExpense.Visible = false;
            pnlViewExpense.Visible = false;
            lblTitle.Text = "Admin Menu";
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

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }   

        private void btnUpdateProductSalePrice_Click(object sender, EventArgs e)
        {
            pnlAdminMenu.Visible = false;
            pnlUpdateProductSalePrice.Visible = true;
            lblTitle.Text = "Update Low Stock Limit";

            obj.cmd = new SqlCommand("select ProductName from tblStocks", obj.con);
            obj.con.Open();
            SqlDataReader dr = obj.cmd.ExecuteReader();
            AutoCompleteStringCollection mycollaction = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                mycollaction.Add(dr.GetString(0));
            }
            txtProductNameStockLimit.AutoCompleteCustomSource = mycollaction;
            obj.con.Close();
        }

        private void txtProductNameStockLimit_Enter(object sender, EventArgs e)
        {
            txtProductNameStockLimit.Text = "";
            txtProductTypeStockLimit.Text = "Select Product type";
        }

        void resetUpdateSalePrice()
        {
            txtProductNameStockLimit.Text = "Search Product";
            txtProductTypeStockLimit.Text = "";
            txtProductNameStockLimit.Text = "";
            txtOldStockLimit.Text = "";
            txtNewStockLimit.Text = "";
        }
        private void btnUpdateUnitSalePriceCancel_Click(object sender, EventArgs e)
        {
            pnlAdminMenu.Visible = true;
            pnlUpdateProductSalePrice.Visible = false;
            lblTitle.Text = "Admin Menu";
            resetUpdateSalePrice();
        }

        private void btnUpdateUnitSalePriceReset_Click(object sender, EventArgs e)
        {
            resetUpdateSalePrice();
        }

        private void cmbProductType_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                String productName = txtProductNameStockLimit.Text.Trim();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT ProductType FROM tblStocks WHERE ProductName = '" + productName + "'", obj.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                txtProductTypeStockLimit.DataSource = dt;
                txtProductTypeStockLimit.DisplayMember = "ProductType";
                txtProductTypeStockLimit.ValueMember = "ProductType";

                txtProductSizeStockLimit.Text = "Product Size";
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtProductSizeStockLimit_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                String productName = txtProductNameStockLimit.Text.Trim();
                String productType = txtProductTypeStockLimit.Text.Trim();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT ProductSize FROM tblStocks WHERE ProductName = '" + productName + "' and ProductType = '" + productType + "'", obj.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                txtProductSizeStockLimit.DataSource = dt;
                txtProductSizeStockLimit.DisplayMember = "ProductSize";
                txtProductSizeStockLimit.ValueMember = "ProductSize";
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateUnitSalePriceSave_Click(object sender, EventArgs e)
        {
            String productName = txtProductNameStockLimit.Text;
            String productType = txtProductTypeStockLimit.Text.Trim();
            String productSize = txtProductSizeStockLimit.Text.Trim();
            String newStockLimit = txtNewStockLimit.Text.Trim();
            
            obj.cmd = new SqlCommand("UPDATE tblStocks SET LowStockLimit = '" + newStockLimit + "' WHERE ProductName = '" + productName + "' and ProductType = '" + productType + "' and ProductSize = '" + productSize + "'", obj.con);
            try
            {
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();
                MessageBox.Show("Update Successful");
                resetUpdateSalePrice();
                obj.con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnViewMedicine_Click(object sender, EventArgs e)
        {
            obj.con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select *  from tblStocks", obj.con);
            adapt.Fill(dt);
            dataGridViewProducts.DataSource = dt;
            obj.con.Close();

            pnlAdminMenu.Visible = false;
            pnlViewMedicines.Visible = true;
            lblTitle.Text = "View Product";
        }

        private void btnPasswordChange_Click(object sender, EventArgs e)
        {
            pnlAdminMenu.Visible = false;
            pnlChangePassword.Visible = true;
            lblTitle.Text = "Change Password";
        }

        private void cmbUserName_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblUser", obj.con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                cmbUserName.DataSource = dtbl;
                cmbUserName.DisplayMember = "UserName";
                cmbUserName.ValueMember = "UserName";
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelChangePassword_Click(object sender, EventArgs e)
        {
            pnlAdminMenu.Visible = true;
            pnlChangePassword.Visible = false;
            lblTitle.Text = "Admin Menu";
            resetChangePassword();
        }

        private void btnResetChangePassword_Click(object sender, EventArgs e)
        {
            resetChangePassword();
        }

        void resetChangePassword()
        {
            cmbUserName.Text = "Please Select a User";
            txtConfPass.Text = "";
            txtNewPass.Text = "";
            txtOldPass.Text = "";
        }

        private void btnUpdateChangePassword_Click(object sender, EventArgs e)
        {
            String userName = cmbUserName.Text.Trim();
            String oldPass = txtOldPass.Text;
            String newPass = txtNewPass.Text;
            if (oldPass == "" || newPass == "")
            {
                MessageBox.Show("Please enter old and new password??", "Warning");
                return;
            }
            if (newPass != txtConfPass.Text)
            {       
                MessageBox.Show("Please enter old and new password??", "Warning");
                return;
            }

            try
            {
                obj.con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblUser Where UserName = '" + userName + "' and Password = '" + oldPass + "' ", obj.con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);
                if (dtbl.Rows.Count >= 1)
                {
                    DialogResult confirm = MessageBox.Show("Do you want to change password", "Confirmation", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        obj.cmd = new SqlCommand("UPDATE tblUser SET Password = '" + newPass + "' WHERE UserName = '" + userName + "'", obj.con);
                        obj.cmd.ExecuteNonQuery();
                        MessageBox.Show("Changed Successfully", "Thank you");
                        pnlChangePassword.Visible = false;
                        pnlAdminMenu.Visible = true;
                        obj.con.Close();
                    }
                    else if (confirm == DialogResult.No)
                    {
                        obj.con.Close();
                        return;
                    }
                }
                else
                    MessageBox.Show("User Name and Password Not match", "Error!!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            obj.con.Close();    
        }

        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            pnlAddExpense.Visible = true;
            pnlAdminMenu.Visible = false;
            lblTitle.Text = "Add Expense";
        }

        private void btnViewExpese_Click(object sender, EventArgs e)
        {
            obj.con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select *  from tblExpense", obj.con);
            adapt.Fill(dt);
            dataGridViewExpense.DataSource = dt;
            obj.con.Close();
            pnlViewExpense.Visible = true;
            pnlAdminMenu.Visible = false;
            lblTitle.Text = "View Expense";
        }

        private void btnExpenseCancel_Click(object sender, EventArgs e)
        {
            resetExpense();
            pnlAddExpense.Visible = false;
            pnlAdminMenu.Visible = true;
            lblTitle.Text = "Admin Menu";
        }

        private void btnExpenseReset_Click(object sender, EventArgs e)
        {
            resetExpense();
        }

        void resetExpense()
        {
            txtConsumerName.Text = "";
            txtExDeatails.Text = "";
            txtCost.Text = "";
        }

        private void btnExpenseSave_Click(object sender, EventArgs e)
        {
            String consumerName, exDeatails;
            DateTime upDate;
            Double cost;
            consumerName = txtConsumerName.Text;
            exDeatails = txtExDeatails.Text;
            upDate = DateTime.Now.Date;

            if (consumerName != "" && exDeatails != "" && txtCost.Text != "")
            {
                cost = Convert.ToDouble(txtCost.Text);

                try
                {
                    obj.con.Open();
                    obj.cmd = new SqlCommand("INSERT INTO tblExpense (ConsumerName, ExpenseDetails, ExpenseDate, Cost)  VALUES('" + consumerName + "', '" + exDeatails + "', '" + upDate + "', '" + cost + "')", obj.con);
                    obj.cmd.ExecuteNonQuery();
                    MessageBox.Show("Add this expense", "Successfully");
                    resetExpense();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Name, Details and cost Must be Fillup????", "Wrong");
            }
            obj.con.Close();
        }

        private void dataGridViewProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            if (e.ColumnIndex == 7)
            {
                DialogResult result = MessageBox.Show("Are you sure to Delete?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        i = Convert.ToInt16(dataGridViewProducts.Rows[e.RowIndex].Cells[0].Value.ToString());
                        BindingSource a = new BindingSource();
                        obj.con.Open();
                        obj.cmd = new SqlCommand("DELETE FROM tblStocks WHERE Id = '" + i + "'", obj.con);
                        obj.cmd.ExecuteNonQuery();
                        DataTable dt = new DataTable();
                        SqlDataAdapter adapt = new SqlDataAdapter("select * from tblStocks", obj.con);
                        adapt.Fill(dt);
                        dataGridViewProducts.DataSource = dt;
                        obj.con.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
        }

        private void dataGridViewExpense_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            if (e.ColumnIndex == 4)
            {
                DialogResult result = MessageBox.Show("Are you sure to Delete?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        i = Convert.ToInt16(dataGridViewExpense.Rows[e.RowIndex].Cells[0].Value.ToString());
                        BindingSource a = new BindingSource();
                        obj.con.Open();
                        obj.cmd = new SqlCommand("DELETE FROM tblExpense WHERE Id = '" + i + "'", obj.con);
                        obj.cmd.ExecuteNonQuery();
                        DataTable dt = new DataTable();
                        SqlDataAdapter adapt = new SqlDataAdapter("select * from tblExpense", obj.con);
                        adapt.Fill(dt);
                        dataGridViewExpense.DataSource = dt;
                        obj.con.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
        }

        private void txtCost_TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtCost.Text, out parsedValue))
            {
                txtCost.Text = "";
                return;
            }
        }

        private void txtNewStockLimit_TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtOldStockLimit.Text, out parsedValue))
            {
                txtOldStockLimit.Text = "";
                return;
            }
        }

        private void txtProductSizeStockLimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String productName = txtProductNameStockLimit.Text;
                String productType = txtProductTypeStockLimit.Text.Trim();
                String productSize = txtProductSizeStockLimit.Text.Trim();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblStocks WHERE ProductName = '" + productName + "' and ProductType = '" + productType + "' and ProductSize = '" + productSize + "'", obj.con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                if (dtbl.Rows.Count >= 1)
                {
                    txtOldStockLimit.Text = dtbl.Rows[0]["LowStockLimit"].ToString();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }
}
