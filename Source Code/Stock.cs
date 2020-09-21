using Microsoft.Reporting.WinForms;
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
    public partial class Stock : Form
    {
        ConnectionString obj = new ConnectionString();
        public Stock()
        {
            InitializeComponent();
            pnlStockMenu.Visible = true;
            pnlViewStock.Visible = false;
            pnlAddNewStock.Visible = false;
            pnlUpdateStock.Visible = false;
            pnlViewLowStock.Visible = false;
            pnlViewPurchaseHistory.Visible = false;
            pnlViewExpiryStock.Visible = false;
            pnlViewStockReports.Visible = false;
            pnlViewLowStocksReport.Visible = false;
            pnlDateExpiryStocksReport.Visible = false;
            pnlPurchaseHistoryReport.Visible = false;
            lblTitle.Text = "Stock Menu";
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
            pnlStockMenu.Visible = true;
            pnlViewStock.Visible = false;
            pnlAddNewStock.Visible = false;
            pnlUpdateStock.Visible = false;
            pnlViewLowStock.Visible = false;
            pnlViewPurchaseHistory.Visible = false;
            pnlViewExpiryStock.Visible = false;
            pnlViewStockReports.Visible = false;
            pnlViewLowStocksReport.Visible = false;
            pnlDateExpiryStocksReport.Visible = false;
            pnlPurchaseHistoryReport.Visible = false;
            lblTitle.Text = "Stock Menu";
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Main obj = new Main();
            this.Hide();
            obj.Show();
        }

        private void Stock_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnViewStock_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select *  from tblStocks", obj.con);
            adapt.Fill(dt);
            dataGridViewStock.DataSource = dt;
            pnlStockMenu.Visible = false;
            pnlViewStock.Visible = true;
            lblTitle.Text = "View Stock";
        }

        private void btnLowStock_Click(object sender, EventArgs e)
        {
            String limit = "10";
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select *  from tblStocks where Quantity <= '" + limit + "'", obj.con);
            adapt.Fill(dt);
            dataGridViewLowStock.DataSource = dt;

            pnlStockMenu.Visible = false;
            pnlViewLowStock.Visible = true;
            lblTitle.Text = "Low Stocks";
        }

        private void btnAddNewStock_Click(object sender, EventArgs e)
        {
            pnlStockMenu.Visible = false;
            pnlAddNewStock.Visible = true;
            lblTitle.Text = "Add New Stock";

            obj.cmd = new SqlCommand("select ProductName from tblStocks", obj.con);
            obj.con.Open();
            SqlDataReader dr = obj.cmd.ExecuteReader();
            AutoCompleteStringCollection mycollaction = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                mycollaction.Add(dr.GetString(0));
            }
            txtProductName.AutoCompleteCustomSource = mycollaction;
            obj.con.Close();
        }

        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            pnlStockMenu.Visible = false;
            pnlUpdateStock.Visible = true;
            lblTitle.Text = "Edit Stock";
            obj.cmd = new SqlCommand("select ProductName from tblStocks", obj.con);
            obj.con.Open();
            SqlDataReader dr = obj.cmd.ExecuteReader();
            AutoCompleteStringCollection mycollaction = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                mycollaction.Add(dr.GetString(0));
            }
            txtProductNameEdit.AutoCompleteCustomSource = mycollaction;
            obj.con.Close();
            cmbProductTypeEditStock.Text = "Product Type";
        }

        private void btnCancelAddStock_Click(object sender, EventArgs e)
        {
            pnlStockMenu.Visible = true;
            pnlViewStock.Visible = false;
            pnlAddNewStock.Visible = false;
            pnlUpdateStock.Visible = false;
            pnlViewLowStock.Visible = false;
            lblTitle.Text = "Stock Menu";
            resetAddNewStock();
        }

        private void btnResetAddStock_Click(object sender, EventArgs e)
        {
            resetAddNewStock();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String productName, productType, productSize, expDate;
            DateTime upDate;
            Double unitSalePrice, uniteWhoelSalePrice, newPurchaseQuantity, newPurchasePrice, curentQuantity, totalPurchaseProduct, totalPurchaseExpenses;
            productName = txtProductName.Text.Trim();
            productType = cmbProductType.Text.Trim();
            productSize = txtProductSize.Text;
            expDate = dateEpr.Text;
            upDate = DateTime.Now.Date;

            if (productName != "" && productType != "" && txtUnitWholeSalePrice.Text != "" && txtUniteSalePrice.Text != "" && txtNewPurchasePrice.Text != "" && txtNewPurchaseQuantity.Text != "")
            {
                unitSalePrice = Convert.ToDouble(txtUniteSalePrice.Text);
                uniteWhoelSalePrice = Convert.ToDouble(txtUnitWholeSalePrice.Text);
                newPurchaseQuantity = Convert.ToDouble(txtNewPurchaseQuantity.Text);
                newPurchasePrice = Convert.ToDouble(txtNewPurchasePrice.Text);
                
                string query = "select * from tblStocks where ProductName = '" + productName + "' and ProductType ='" + productType + "' and ProductSize ='" + productSize + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, obj.con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);
                if (dtbl.Rows.Count < 1)
                {
                    //Insertions Product
                    DialogResult confirm = MessageBox.Show("Do you want to Add New Stock With Product ? \n Product Name: " + productName + " \nType     :\t   " + productType + " \nSize     :\t   " + productSize + "\nTotal Stock   :" + newPurchaseQuantity, "Confirmation", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        obj.con.Open();
                        obj.cmd = new SqlCommand("INSERT INTO tblStocks (ProductName, ProductType, ProductSize, Quantity, UnitSalePrice, UnitWholeSalePrice, LastPurchaseQuantity, PurchasePrice,  ExpiryDate, LastUpdate, TotalPurchaseProduct, TotalPurchaseExpenses, LowStockLimit)  VALUES('" + productName + "', '" + productType + "', '" + productSize + "', '" + newPurchaseQuantity + "', '" + unitSalePrice + "', '" + uniteWhoelSalePrice + "', '" + newPurchaseQuantity + "', '" + newPurchasePrice + "', '" + expDate + "', '" + upDate + "', '" + newPurchaseQuantity + "', '" + newPurchasePrice + "', '10')", obj.con);
                        obj.cmd.ExecuteNonQuery();
                        resetAddNewStock();
                        obj.con.Close();
                        MessageBox.Show("Add Product and Stock", "Sucssesfully");
                        pnlStockMenu.Visible = true;
                        pnlAddNewStock.Visible = false;
                        return;
                    }
                    else if (confirm == DialogResult.No)
                    {
                        return;
                    }
                }

                try
                {
                    curentQuantity = Convert.ToDouble(txtCurentStock.Text);
                    totalPurchaseProduct = Convert.ToDouble(dtbl.Rows[0]["TotalPurchaseProduct"].ToString());
                    totalPurchaseExpenses = Convert.ToDouble(dtbl.Rows[0]["TotalPurchaseExpenses"].ToString());

                    totalPurchaseProduct += newPurchaseQuantity;
                    totalPurchaseExpenses += newPurchasePrice;
                    curentQuantity += newPurchaseQuantity;
                    DialogResult confirm = MessageBox.Show("Do you want to Update Stock? \n Product Name: " + productName + " \nType     :\t   " + productType + " \nSize     :\t   " + productSize + "\nTotal Stock   :" + curentQuantity, "Confirmation", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        obj.con.Open();
                        obj.cmd = new SqlCommand("UPDATE tblStocks SET Quantity = '" + curentQuantity + "', UnitSalePrice = '" + unitSalePrice + "', UnitWholeSalePrice = '" + uniteWhoelSalePrice + "', LastPurchaseQuantity = '" + newPurchaseQuantity + "', PurchasePrice = '" + newPurchasePrice + "', ExpiryDate = '" + expDate + "', LastUpdate = '" + upDate + "', TotalPurchaseProduct = '" + totalPurchaseProduct + "', TotalPurchaseExpenses = '" + totalPurchaseExpenses + "' WHERE ProductName = '" + productName + "' and ProductType = '" + productType + "'  and ProductSize ='" + productSize + "'", obj.con);
                        obj.cmd.ExecuteNonQuery();
                        resetAddNewStock();
                        obj.con.Close();
                        MessageBox.Show("Update Stock", "Sucssesfully");
                    }
                    else if (confirm == DialogResult.No)
                    {
                        return;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else 
            {
                MessageBox.Show("Product Name, Quantity, Sale Price, Expiry Date & Purchase Price Must be Fillup????");
            }
        }

        void resetAddNewStock()
        {
            txtNewPurchaseQuantity.Text = "";
            txtNewPurchasePrice.Text = "";
            txtProductName.Text = "Product Name";
            cmbProductType.Text = "";
            txtProductSize.Text = "";
            txtCurentStock.Text = "";
            txtLastPurchasePrice.Text = "";
            txtLastPurchaseQuantity.Text = "";
            txtUniteSalePrice.Text = "";
            txtUnitWholeSalePrice.Text = "";
            dateNewExpriyEditStock.Text = "";
        }

        private void btnHlp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("First Fillup Product Informations.\nThen fillup Like as:\n Last Purchase Quantity: 10 \n Purchase Price: 200 \n Expiry Date: 05-11-2020", "Help");
        }

        private void cmbProductName_MouseClick(object sender, MouseEventArgs e)
        {/*
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT ProductName FROM tblStocks", obj.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                //Assign DataTable as DataSource.
                cmbProductName.DataSource = dt;
                cmbProductName.DisplayMember = "ProductName";
                cmbProductName.ValueMember = "ProductName";
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            cmbProductType.Text = "Please Select Product Type ";
          * */
        }

        private void cmbProductType_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                String productName = txtProductName.Text.Trim();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT ProductType FROM tblStocks WHERE ProductName = '" + productName + "'", obj.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    //Assign DataTable as DataSource.
                    cmbProductType.DataSource = dt;
                    cmbProductType.DisplayMember = "ProductType";
                    cmbProductType.ValueMember = "ProductType";
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            txtProductSize.Text = "Product Size";
        }

        private void txtProductSize_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                String productName = txtProductName.Text;
                String productType = cmbProductType.Text.Trim();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT ProductSize FROM tblStocks WHERE ProductName = '" + productName + "' and ProductType = '" + productType + "'", obj.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                txtProductSize.DataSource = dt;
                txtProductSize.DisplayMember = "ProductSize";
                txtProductSize.ValueMember = "ProductSize";
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtProductSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String productName = txtProductName.Text;
                String productType = cmbProductType.Text.Trim();
                String productSize = txtProductSize.Text;
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblStocks WHERE ProductName = '" + productName + "' and ProductType = '" + productType + "' and ProductSize = '" + productSize + "'", obj.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    txtCurentStock.Text = dt.Rows[0]["Quantity"].ToString();
                    txtLastPurchaseQuantity.Text = dt.Rows[0]["LastPurchaseQuantity"].ToString();
                    txtLastPurchasePrice.Text = dt.Rows[0]["PurchasePrice"].ToString();
                    txtUniteSalePrice.Text = dt.Rows[0]["UnitSalePrice"].ToString();
                    txtUnitWholeSalePrice.Text = dt.Rows[0]["UnitWholeSalePrice"].ToString();
                }
                else
                {
                    return;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void cmbProductTypeEditStock_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                String productName = txtProductNameEdit.Text;
                SqlDataAdapter sda = new SqlDataAdapter("SELECT ProductType FROM tblStocks WHERE ProductName = '" + productName + "'", obj.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                //Assign DataTable as DataSource.
                cmbProductTypeEditStock.DataSource = dt;
                cmbProductTypeEditStock.DisplayMember = "ProductType";
                cmbProductTypeEditStock.ValueMember = "ProductType";
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            txtProductSizeEditStock.Text = "Product Size";
        }

        private void textProductSizeEditStock_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                String productName = txtProductNameEdit.Text;
                String productType = cmbProductTypeEditStock.Text.Trim();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT ProductSize FROM tblStocks WHERE ProductName = '" + productName + "' and ProductType = '" + productType + "'", obj.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                //Assign DataTable as DataSource.
                txtProductSizeEditStock.DataSource = dt;
                txtProductSizeEditStock.DisplayMember = "ProductSize";
                txtProductSizeEditStock.ValueMember = "ProductSize";
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            txtProductSize.Text = "Product Size";
        }

        private void textProductSizeEditStock_TextChanged(object sender, EventArgs e)
        {
            try
            {
                String productName = txtProductNameEdit.Text;
                String productType = cmbProductTypeEditStock.Text.Trim();
                String productSize = txtProductSizeEditStock.Text.Trim();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblStocks WHERE ProductName = '" + productName + "' and ProductType = '" + productType + "' and ProductSize = '" + productSize + "'", obj.con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                if (dtbl.Rows.Count >= 1)
                {
                    txtOldLastPurchaseQuantityEditStock.Text = dtbl.Rows[0]["LastPurchaseQuantity"].ToString();
                    txtOldPurchasePriceEditStock.Text = dtbl.Rows[0]["PurchasePrice"].ToString();
                    String datess = dtbl.Rows[0]["ExpiryDate"].ToString();
                    string[] h = datess.Split(' ');
                    txtOldExpriyEditStock.Text = h[0];
                    txtOldUnitSalePriceEditStock.Text = dtbl.Rows[0]["UnitSalePrice"].ToString();
                    txtOldUnitWholeSalePriceEditStock.Text = dtbl.Rows[0]["UnitWholeSalePrice"].ToString();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void resetEditStock()
        {
             txtOldLastPurchaseQuantityEditStock.Text = "";
             txtOldPurchasePriceEditStock.Text = "";
             txtOldExpriyEditStock.Text = "";
             txtNewLastPurchaseQuantityEditStock.Text = "";
             txtNewPurchasePriceEditStock.Text = "";
             txtProductNameEdit.Text = "Search Product";
             cmbProductTypeEditStock.Text = "";
             txtProductSizeEditStock.Text = "";
             txtOldUnitSalePriceEditStock.Text = "";
             txtOldUnitWholeSalePriceEditStock.Text = "";
             txtNewUnitSalePriceEditStock.Text = "";
             txtNewUnitWholeSalePriceEditStock.Text = "";
             dateNewExpriyEditStock.Text = "";
        }

        private void btnHelpEditStock_Click(object sender, EventArgs e)
        {
            MessageBox.Show("First Fillup Product Informations.\nThen fillup New Stocks Section Like as:\n Last Purchase Quantity: 10 \n Purchase Price: 200 \n Expiry Date: 05-11-2020", "Help");
        }

        private void btnCancelEditStock_Click(object sender, EventArgs e)
        {
            pnlUpdateStock.Visible = false;
            pnlStockMenu.Visible = true;
            lblTitle.Text = "Stock Menu";
            resetEditStock();
        }

        private void btnResetEditStock_Click(object sender, EventArgs e)
        {
            resetEditStock();
        }

        private void btnSaveEditStock_Click(object sender, EventArgs e)
        {
            String productName, productType, productSize, expDate;
            DateTime upDate;
            Double prsPrice, oldPrsPrice, OldLastPurchaseQuantity, lastPurchaseQuantity, unitSalePrice, unitWholeSalePrice, curentQuantity, totalPurchaseProduct, totalPurchaseExpenses;
            productName = txtProductNameEdit.Text;
            productType = cmbProductTypeEditStock.Text.Trim();
            productSize = txtProductSizeEditStock.Text.Trim();
            expDate = dateNewExpriyEditStock.Text;
            upDate = DateTime.Now.Date;

            if (productName != "" && productType != "" && txtNewPurchasePriceEditStock.Text != "" && txtNewLastPurchaseQuantityEditStock.Text != "" && txtNewUnitSalePriceEditStock.Text != "" && txtNewUnitWholeSalePriceEditStock.Text != "")
            {
                string query = "select * from tblStocks where ProductName = '" + productName + "' and ProductType ='" + productType + "' and ProductSize ='" + productSize + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, obj.con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);
                if (dtbl.Rows.Count < 1)
                {
                    MessageBox.Show("Product Name and Type is not Valid", "Error");
                    return;
                }
                oldPrsPrice = Convert.ToDouble(txtOldPurchasePriceEditStock.Text);
                OldLastPurchaseQuantity = Convert.ToDouble(txtOldLastPurchaseQuantityEditStock.Text);
                prsPrice = Convert.ToDouble(txtNewPurchasePriceEditStock.Text);
                lastPurchaseQuantity = Convert.ToDouble(txtNewLastPurchaseQuantityEditStock.Text);
                unitSalePrice = Convert.ToDouble(txtNewUnitSalePriceEditStock.Text);
                unitWholeSalePrice = Convert.ToDouble(txtNewUnitWholeSalePriceEditStock.Text);
                try
                {
                    curentQuantity = Convert.ToDouble(dtbl.Rows[0]["Quantity"].ToString());
                    totalPurchaseProduct = Convert.ToDouble(dtbl.Rows[0]["TotalPurchaseProduct"].ToString());
                    totalPurchaseExpenses = Convert.ToDouble(dtbl.Rows[0]["TotalPurchaseExpenses"].ToString());

                    totalPurchaseProduct -= OldLastPurchaseQuantity;
                    totalPurchaseProduct += lastPurchaseQuantity;
                    totalPurchaseExpenses -= oldPrsPrice;
                    totalPurchaseExpenses += prsPrice;
                    curentQuantity -= OldLastPurchaseQuantity;
                    curentQuantity += lastPurchaseQuantity;

                    if (curentQuantity < 0)
                    {
                        MessageBox.Show("Current Stock can't Less then zero.\n Old Last Purchase Quantity = " + OldLastPurchaseQuantity + "\n New Last Purchase Quantity = " + lastPurchaseQuantity, "Warning");
                    }
                    else
                    {
                        DialogResult confirm = MessageBox.Show("Do you want to Edit Stock? \n Product Name: " + productName + " \nType     :\t   " + productType + " \nSize     :\t   " + productSize + "\n Total Stock   :" + curentQuantity, "Confirmation", MessageBoxButtons.YesNo);
                        if (confirm == DialogResult.Yes)
                        {
                            obj.con.Open();
                            obj.cmd = new SqlCommand("UPDATE tblStocks SET Quantity = '" + curentQuantity + "', UnitSalePrice = '" + unitSalePrice + "', UnitWholeSalePrice = '" + unitWholeSalePrice + "', LastPurchaseQuantity = '" + lastPurchaseQuantity + "', PurchasePrice = '" + prsPrice + "', ExpiryDate = '" + expDate + "', LastUpdate = '" + upDate + "', TotalPurchaseProduct = '" + totalPurchaseProduct + "', TotalPurchaseExpenses = '" + totalPurchaseExpenses + "' WHERE ProductName = '" + productName + "' and ProductType = '" + productType + "'", obj.con);
                            obj.cmd.ExecuteNonQuery();
                            resetEditStock();
                            MessageBox.Show("Updated","Sucessfull");
                            obj.con.Close();
                        }
                        else if (confirm == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Product Name, Purchase Quantity, Expiry Date, Purchase Price & Unite Price Must be Fillup????", "Error");
            }
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            pnlAddNewStock.BackColor = Color.FromArgb(70, 0, 0, 0);
            pnlUpdateStock.BackColor = Color.FromArgb(70, 0, 0, 0);
            groupBox2.BackColor = Color.FromArgb(100, 50, 50, 50);
            groupBox1.BackColor = Color.FromArgb(100, 50, 50, 50);
            groupBox6.BackColor = Color.FromArgb(100, 50, 50, 50);
            groupBox4.BackColor = Color.FromArgb(100, 50, 50, 50);
            groupBox3.BackColor = Color.FromArgb(100, 50, 50, 50);
            groupBox5.BackColor = Color.FromArgb(100, 50, 50, 50);
            // TODO: This line of code loads data into the 'pMSDataSet.tblStocks' table. You can move, or remove it, as needed.
            //this.tblStocksTableAdapter.Fill(this.pMSDataSet.tblStocks);
            // TODO: This line of code loads data into the 'pMSDataSet.tblStocks' table. You can move, or remove it, as needed.
            //this.tblStocksTableAdapter.Fill(this.pMSDataSet.tblStocks);
            // TODO: This line of code loads data into the 'pMSDataSet.tblStocks' table. You can move, or remove it, as needed.
            //this.tblStocksTableAdapter.Fill(this.pMSDataSet.tblStocks);
            // TODO: This line of code loads data into the 'pMSDataSet.tblStocks' table. You can move, or remove it, as needed.
            //this.tblStocksTableAdapter.Fill(this.pMSDataSet.tblStocks);
            // TODO: This line of code loads data into the 'pMSDataSet.tblStocks' table. You can move, or remove it, as needed.
            //this.tblStocksTableAdapter.Fill(this.pMSDataSet.tblStocks);
            // TODO: This line of code loads data into the 'pMSDataSet.tblStocks' table. You can move, or remove it, as needed.
           // this.tblStocksTableAdapter.Fill(this.pMSDataSet.tblStocks);

            this.reportViewerViewStocks.RefreshReport();
        }

        private void btnViewStockHistory_Click(object sender, EventArgs e)
        {
            obj.con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select *  from tblStocks", obj.con);
            adapt.Fill(dt);
            dataGridViewStockHistory.DataSource = dt;
            obj.con.Close();
            pnlViewPurchaseHistory.Visible = true;
            pnlStockMenu.Visible = false;
            lblTitle.Text = "Purchase History";
        }


        private void txtNewLastPurchaseQuantityEditStock_TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtNewLastPurchaseQuantityEditStock.Text, out parsedValue))
            {
                txtNewLastPurchaseQuantityEditStock.Text = "";
                return;
            }
        }

        private void txtNewPurchasePriceEditStock_TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtNewPurchasePriceEditStock.Text, out parsedValue))
            {
                txtNewPurchasePriceEditStock.Text = "";
                return;
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtNewPurchaseQuantity.Text, out parsedValue))
            {
                txtNewPurchaseQuantity.Text = "";
                return;
            }
        }

        private void txtPurchasePrice_TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtNewPurchasePrice.Text, out parsedValue))
            {
                txtNewPurchasePrice.Text = "";
                return;
            }
        }

        private void btnBackViewStock_Click(object sender, EventArgs e)
        {
            pnlStockMenu.Visible = true;
            pnlViewStock.Visible = false;
            lblTitle.Text = "Stock Menu";
        }

        private void btnViewStockPrintReport_Click(object sender, EventArgs e)
        {
            pnlViewStock.Visible = false;
            pnlViewStockReports.Visible = true;
            lblTitle.Text = "Print View";
            try
            {
                obj.con.Open();
                obj.cmd = new SqlCommand("Select * from tblStocks", obj.con);

                SqlDataAdapter sda = new SqlDataAdapter(obj.cmd);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                ReportDataSource rds = new ReportDataSource("DataSetViewStocks", dtbl);
                reportViewerViewStocks.LocalReport.DataSources.Clear();
                reportViewerViewStocks.LocalReport.DataSources.Add(rds);
                reportViewerViewStocks.RefreshReport();

                obj.con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLowStockBack_Click(object sender, EventArgs e)
        {
            pnlStockMenu.Visible = true;
            pnlViewLowStock.Visible = false;
            lblTitle.Text = "Stocks Menu";
        }

        private void btnLowStockReport_Click(object sender, EventArgs e)
        {
            pnlViewLowStock.Visible = false;
            pnlViewLowStocksReport.Visible = true;
            lblTitle.Text = "Print View";
            try
            {
                String limit = "10";
                obj.con.Open();
                obj.cmd = new SqlCommand("Select * from tblStocks where Quantity <= '" + limit + "'", obj.con);

                SqlDataAdapter sda = new SqlDataAdapter(obj.cmd);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                ReportDataSource rds = new ReportDataSource("DataSetViewStocks", dtbl);
                reportViewerLowStocks.LocalReport.DataSources.Clear();
                reportViewerLowStocks.LocalReport.DataSources.Add(rds);
                reportViewerLowStocks.RefreshReport();

                obj.con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDateExpiryStockBack_Click(object sender, EventArgs e)
        {
            pnlStockMenu.Visible = true;
            pnlViewExpiryStock.Visible = false;
            lblTitle.Text = "Stocks Menu";
        }

        private void btnDateExpiryStockReport_Click(object sender, EventArgs e)
        {
            pnlViewExpiryStock.Visible = false;
            pnlDateExpiryStocksReport.Visible = true;
            lblTitle.Text = "Print View";
            try
            {
                DateTime today = DateTime.Now.Date;
                obj.con.Open();
                obj.cmd = new SqlCommand("Select * from tblStocks where ExpiryDate < '" + today + "'", obj.con);

                SqlDataAdapter sda = new SqlDataAdapter(obj.cmd);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                ReportDataSource rds = new ReportDataSource("DataSetViewStocks", dtbl);
                reportViewerDateExpiryStocksReport.LocalReport.DataSources.Clear();
                reportViewerDateExpiryStocksReport.LocalReport.DataSources.Add(rds);
                reportViewerDateExpiryStocksReport.RefreshReport();

                obj.con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPurchaseViewBack_Click(object sender, EventArgs e)
        {
            pnlViewPurchaseHistory.Visible = false;
            pnlStockMenu.Visible = true;
            lblTitle.Text = "Stocks Menu";
        }

        private void btnPurchaseViewReport_Click(object sender, EventArgs e)
        {
            pnlPurchaseHistoryReport.Visible = true;
            pnlViewPurchaseHistory.Visible = false;
            lblTitle.Text = "Print View";
            try
            {
                obj.con.Open();
                obj.cmd = new SqlCommand("Select * from tblStocks", obj.con);

                SqlDataAdapter sda = new SqlDataAdapter(obj.cmd);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                ReportDataSource rds = new ReportDataSource("DataSetViewStocks", dtbl);
                reportViewerPurchaseHistory.LocalReport.DataSources.Clear();
                reportViewerPurchaseHistory.LocalReport.DataSources.Add(rds);
                reportViewerPurchaseHistory.RefreshReport();

                obj.con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label100_Click(object sender, EventArgs e)
        {

        }

        private void txtUniteSalePrice_TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtUniteSalePrice.Text, out parsedValue))
            {
                txtUniteSalePrice.Text = "";
                return;
            }
        }

        private void txtUnitWholeSalePrice_TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtUnitWholeSalePrice.Text, out parsedValue))
            {
                txtUnitWholeSalePrice.Text = "";
                return;
            }
        }

        private void txtProductNameEdit_Enter(object sender, EventArgs e)
        {
            txtProductNameEdit.Text = "";
            cmbProductType.Text = "Product Type";
        }

        private void txtNewUnitSalePriceEditStock_TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtNewUnitSalePriceEditStock.Text, out parsedValue))
            {
                txtNewUnitSalePriceEditStock.Text = "";
                return;
            }
        }

        private void txtNewUnitWholeSalePriceEditStock_TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtNewUnitWholeSalePriceEditStock.Text, out parsedValue))
            {
                txtNewUnitWholeSalePriceEditStock.Text = "";
                return;
            }
        }

        private void txtProductName_Enter(object sender, EventArgs e)
        {
            resetAddNewStock();
            txtProductName.Text = "";
            cmbProductType.Text = "Product Type";
            
        }

       

    }
}
