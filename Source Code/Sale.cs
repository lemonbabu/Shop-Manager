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
    public partial class Sale : Form
    {
        ConnectionString obj = new ConnectionString();
        public Sale()
        {
            InitializeComponent();
            tempGridViewRefash();
            pnlNewSale.Visible = true;
            pnlSaleCustomer.Visible = false;
            pnlSaleMemoPrint.Visible = false;
            lblSaleTitle.Text = "New Sale";
        }
        double totalCost, overallDiscount, payment, due;
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
            pnlNewSale.Visible = true;
            pnlSaleCustomer.Visible = false;
            pnlSaleMemoPrint.Visible = false;
            lblSaleTitle.Text = "Reguler Sale";
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

        private void Sale_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void cmbProductType_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                String productName = txtProductName.Text.Trim();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT ProductType FROM tblStocks WHERE ProductName = '" + productName + "'", obj.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                cmbProductType.DataSource = dt;
                cmbProductType.DisplayMember = "ProductType";
                cmbProductType.ValueMember = "ProductType";
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
                //Assign DataTable as DataSource.
                txtProductSize.DataSource = dt;
                txtProductSize.DisplayMember = "ProductSize";
                txtProductSize.ValueMember = "ProductSize";
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void resetSale()
        {
            txtUnitPrice.Text = "";
            txtStocks.Text = "";
            txtSaleQuantity.Text = "";
            txtSaleIndivisualDiscount.Text = "";
            cmbProductType.Text = "";
            txtProductName.Text = "Search Product";
            txtPurchaseQuantity.Text = "";
            txtPurchasePrice.Text = "";
            txtProductSize.Text = "";
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            resetSale();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            String productName, productType, productSize, warranty;
            Double quantity, indivisualDiscount, stock, unitPrice, price;
            productName = txtProductName.Text;
            productType = cmbProductType.Text.Trim();
            productSize = txtProductSize.Text;
            
            if (productName == "" || productType == "" || productSize == "" || txtSaleQuantity.Text == "" || txtStocks.Text == "")
            {
                MessageBox.Show("Select Product details and Quantity please.....", "Wrong");
            }
            else
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
                if (txtSaleIndivisualDiscount.Text == "" || txtSaleIndivisualDiscount.Text == " " || txtSaleIndivisualDiscount.Text == "    ")
                    txtSaleIndivisualDiscount.Text = "0";
                quantity = Convert.ToDouble(txtSaleQuantity.Text.Trim());
                indivisualDiscount = Convert.ToDouble(txtSaleIndivisualDiscount.Text.Trim());
                stock = Convert.ToDouble(txtStocks.Text.Trim());
                unitPrice = Convert.ToDouble(txtUnitPrice.Text.Trim());
                indivisualDiscount = (unitPrice * quantity) * indivisualDiscount / 100;
                price = (unitPrice * quantity) - indivisualDiscount;
                Double stockLimit = Convert.ToDouble(dtbl.Rows[0]["LowStockLimit"].ToString());
                warranty = dtbl.Rows[0]["ExpiryDate"].ToString();
                if (stockLimit >= (stock - quantity))
                    MessageBox.Show("This Product is Cross the Low Stock Limit", "Low Stock");
                try
                {
                    query = "select * from tblTemp where ProductName = '" + productName + "' and ProductType ='" + productType + "' and ProductSize ='" + productSize + "'";
                    sda = new SqlDataAdapter(query, obj.con);
                    dtbl = new DataTable();
                    sda.Fill(dtbl);
                    if (dtbl.Rows.Count >= 1)
                    {
                        Double oldQuantity = Convert.ToDouble(dtbl.Rows[0]["Quantity"].ToString());
                        quantity += oldQuantity;
                        price = (unitPrice * quantity) - indivisualDiscount;
                        if (quantity > stock)
                        {
                            MessageBox.Show("Product Name: " + productName + " Stock is not enought??", "Low Stock");
                        }
                        else
                        {
                            obj.con.Open();
                            obj.cmd = new SqlCommand("UPDATE tblTemp SET Quantity = '" + quantity + "', Discount = '" + indivisualDiscount + "', Price = '" + price + "' WHERE ProductName = '" + productName + "' and ProductType ='" + productType + "' and ProductSize ='" + productSize + "'", obj.con);
                            obj.cmd.ExecuteNonQuery();
                            obj.con.Close();
                        }
                    }
                    else
                    {
                        if (quantity > stock)
                        {
                            MessageBox.Show("Product Name: " + productName + " Stock is not enought??", "Low Stock");
                        }
                        else
                        {
                            obj.con.Open();
                            obj.cmd = new SqlCommand("INSERT INTO tblTemp (ProductName, ProductType, ProductSize, Warranty, Quantity, UnitPrice, Discount, Price)  VALUES('" + productName + "', '" + productType + "', '" + productSize + "', '"+ warranty +"', '" + quantity + "', '" + unitPrice + "', '" + indivisualDiscount + "', '" + price + "')", obj.con);
                            obj.cmd.ExecuteNonQuery();
                            obj.con.Close();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                resetSale();
                tempGridViewRefash();
            }
        }

        private void Sale_Load(object sender, EventArgs e)
        {
            pnlNewSale.BackColor = Color.FromArgb(70, 0, 0, 0);
            pnlSaleCustomer.BackColor = Color.FromArgb(70, 0, 0, 0);
            lblSaleTitle.BackColor = Color.FromArgb(100, 50, 50, 50);
            groupBox1.BackColor = Color.FromArgb(100, 50, 50, 50);
            groupBox3.BackColor = Color.FromArgb(100, 50, 50, 50);
            groupBox4.BackColor = Color.FromArgb(100, 50, 50, 50);
            groupBox5.BackColor = Color.FromArgb(100, 50, 50, 50);
            // TODO: This line of code loads data into the 'pMSDataSet.tblCustomers' table. You can move, or remove it, as needed.
            //this.tblCustomersTableAdapter.Fill(this.pMSDataSet.tblCustomers);
            // TODO: This line of code loads data into the 'pMSDataSet.tblTemp' table. You can move, or remove it, as needed.
           // this.tblTempTableAdapter.Fill(this.pMSDataSet.tblTemp);

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

        private void dataGridViewSale_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            if (e.ColumnIndex == 8)
            {
                DialogResult result = MessageBox.Show("Are you sure to Delete?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        i = Convert.ToInt16(dataGridViewSale.Rows[e.RowIndex].Cells[0].Value.ToString());
                        BindingSource a = new BindingSource();
                        deleteItem(a, i);
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

        //Delete 1 item from graidview in the tbleNewSells
        void deleteItem(BindingSource a, int i)
        {
            obj.con.Open();
            obj.cmd = new SqlCommand("DELETE FROM tblTemp WHERE Id = '" + i + "'", obj.con);
                
            try
            {
                obj.cmd.ExecuteNonQuery();
                obj.con.Close();
                tempGridViewRefash();
            }
            catch (Exception e)
            {
                MessageBox.Show("SQL error" + e);
            }
        }

        //this function work on delete all data from tblNewSells table
        void dltAllDataTblNewSells()
        {
            obj.cmd = new SqlCommand("DELETE FROM tblTemp", obj.con);

            try
            {
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();
                obj.con.Close();
                tempGridViewRefash();
            }
            catch (Exception e)
            {
                MessageBox.Show("SQL error" + e);
            }
        }

        private void btnGridViewClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to Delete All Collections?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                BindingSource a = new BindingSource();
                dltAllDataTblNewSells();
                resetSale();
            }
            else if (result == DialogResult.No)
            {
                return;
            }
        }

        void tempGridViewRefash()
        {
            obj.con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from tblTemp", obj.con);
            adapt.Fill(dt);
            dataGridViewSale.DataSource = dt;
            obj.con.Close();
        }

        private void txtSaleQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd_Click(sender, e);
            }
        }

        private void txtSaleIndivisualDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd_Click(sender, e);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                obj.cmd = new SqlCommand("select SUM(Price) from tblTemp", obj.con);
                obj.con.Open();
                var ob = obj.cmd.ExecuteScalar();
                if (ob.ToString() == "")
                {
                    totalCost = 0;
                    obj.con.Close();
                    MessageBox.Show("Select Product First!!", "Warning");
                    return;
                }
                else
                {
                    totalCost = Convert.ToDouble(ob);
                }
                obj.con.Close();
                txtTotalCost.Text = totalCost.ToString();
                due = totalCost;
                txtDue.Text = due.ToString();
                overallDiscount = 0;
                txtOverAllDiscount.Text = overallDiscount.ToString();
                payment = 0;
                txtPayment.Text = payment.ToString();
                pnlNewSale.Visible = false;
                pnlSaleCustomer.Visible = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaleBack_Click(object sender, EventArgs e)
        {
            pnlNewSale.Visible = true;
            pnlSaleCustomer.Visible = false;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Do you sure to confirm this sale?", "Sale Confirmation", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                confirmSale();
                resetPay();
                resetSale();
                dltAllDataTblNewSells();
                MessageBox.Show("Sale Successfully", "Thank you");
                pnlNewSale.Visible = true;
                pnlSaleCustomer.Visible = false;
            }
            else if (confirm == DialogResult.No)
            {
                return;
            }
        }

        private void btnConfirmPrint_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Do you sure to confirm this sale and Print Cashmemo?", "Sale Confirmation", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                confirmSale();
                resetPay();
                resetSale();
                dltAllDataTblNewSells();
                MessageBox.Show("Sale Successfully", "Thank you");

                try
                {
                    pnlSaleMemoPrint.Visible = true;
                    pnlSaleCustomer.Visible = false;
                    lblSaleTitle.Text = "Print View";

                    obj.cmd = new SqlCommand("Select * from tblCustomers where CustomerId = '" + cstId + "'", obj.con);
                    SqlDataAdapter sda = new SqlDataAdapter(obj.cmd);
                    DataTable dtbl = new DataTable();
                    sda.Fill(dtbl);

                    ReportDataSource rds = new ReportDataSource("DataSetCustomers", dtbl);
                    reportViewerSaleMemo.LocalReport.DataSources.Clear();
                    reportViewerSaleMemo.LocalReport.DataSources.Add(rds);
                    reportViewerSaleMemo.RefreshReport();

                    obj.cmd = new SqlCommand("Select * from tblSaleProducts where CustomerId = '" + cstId + "'", obj.con);
                    sda = new SqlDataAdapter(obj.cmd);
                    dtbl = new DataTable();
                    sda.Fill(dtbl);

                    rds = new ReportDataSource("DataSetProducts", dtbl);
                    reportViewerSaleMemo.LocalReport.DataSources.Add(rds);
                    reportViewerSaleMemo.RefreshReport();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (confirm == DialogResult.No)
            {
                return;
            }
        }

        String cstId;
        void confirmSale()
        {
            String customerName, customerPhone, customerAddress, payMethod, productName, productType, productSize, warranty, time;
            DateTime date;
            int customerId;
            Double quantity, unitPrice, indivisualDiscount, price;
            customerName = txtCustomerName.Text;
            customerPhone = txtCustomerPhone.Text;
            customerAddress = txtCustomerAddress.Text;
            payMethod = cmbPayMethod.Text.Trim();
            date = DateTime.Now.Date;
            time = DateTime.Now.ToString("HH:mm").Trim();

            if (customerName != "" && customerPhone != "")
            {
                try
                {
                    obj.cmd = new SqlCommand("INSERT INTO tblCustomers (CustomerName, CustomerPhone, CustomerAddress, TotalCost, Discount, Payment, Due, PayMethod, Date, Time, WholeSale)  VALUES('" + customerName + "', '" + customerPhone + "', '" + customerAddress + "', '" + totalCost + "', '" + overallDiscount + "', '" + payment + "', '" + due + "', '" + payMethod + "', '" + date + "', '" + time + "', 'NO')", obj.con);
                    obj.con.Open();
                    obj.cmd.ExecuteNonQuery();
                    //Customer table select query for customer id
                    string query = "Select * from tblCustomers Where CustomerName = '" + customerName + "' and CustomerPhone = '" + customerPhone + "' and Date = '" + date + "' and Time = '" + time + "' ";
                    SqlDataAdapter sda = new SqlDataAdapter(query, obj.con);
                    DataSet dataset = new DataSet();
                    sda.Fill(dataset);
                    customerId = Convert.ToInt32(dataset.Tables[0].Rows[0]["CustomerId"].ToString());
                    cstId = customerId.ToString();
                    // select query for temp product data 
                    query = "select * from tblTemp";
                    sda = new SqlDataAdapter(query, obj.con);
                    DataTable dtbl = new DataTable();
                    sda.Fill(dtbl);
                    int n = dtbl.Rows.Count;

                    for(int i = 0; i < n; i++)
                    {
                        productName = dtbl.Rows[i]["ProductName"].ToString();
                        productType = dtbl.Rows[i]["ProductType"].ToString();
                        productSize = dtbl.Rows[i]["ProductSize"].ToString();
                        warranty = dtbl.Rows[i]["Warranty"].ToString();
                        quantity = Convert.ToDouble(dtbl.Rows[i]["Quantity"].ToString());
                        unitPrice = Convert.ToDouble(dtbl.Rows[i]["UnitPrice"].ToString());
                        indivisualDiscount = Convert.ToDouble(dtbl.Rows[i]["Discount"].ToString());
                        price = Convert.ToDouble(dtbl.Rows[i]["Price"].ToString());
                        //Insertion of main saleproduct table
                        obj.cmd = new SqlCommand("INSERT INTO tblSaleProducts (CustomerId, ProductName, ProductType, ProductSize, Warranty, Quantity, UnitPrice, Discount, Price)  VALUES('" + customerId + "', '" + productName + "', '" + productType + "', '" + productSize + "', '" + warranty + "', '" + quantity + "', '" + unitPrice + "', '" + indivisualDiscount + "', '" + price + "')", obj.con);
                        obj.cmd.ExecuteNonQuery();

                        //Update query of stocks table 
                        query = "Select Quantity from tblStocks Where ProductName = '" + productName + "' and ProductType = '" + productType + "' and ProductSize = '" + productSize + "'";
                        SqlDataAdapter sdaStocks = new SqlDataAdapter(query, obj.con);
                        DataTable dtblStocks = new DataTable();
                        sdaStocks.Fill(dtblStocks);

                        Double oldQuantity = Convert.ToDouble(dtblStocks.Rows[0]["Quantity"].ToString());
                        oldQuantity -= quantity;
                        obj.cmd = new SqlCommand("UPDATE tblStocks SET Quantity = '" + oldQuantity + "' WHERE ProductName = '" + productName + "' and ProductType ='" + productType + "' and ProductSize = '" + productSize + "'", obj.con);
                        obj.cmd.ExecuteNonQuery();
                    }

                    obj.con.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Customer Name and Phone Should be fillup!!","Error");
        }

        private void chkPaid_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPaid.Checked)
            {
                txtPayment.ReadOnly = true;
                if (txtOverAllDiscount.Text != "")
                    overallDiscount = Convert.ToDouble(txtOverAllDiscount.Text);
                else
                    overallDiscount = 0;

                Double discount = totalCost * overallDiscount / 100;
                payment = totalCost - discount;
                due = 0;
                txtPayment.Text = payment.ToString();
                txtDue.Text = due.ToString();
                txtOverAllDiscount.Text = overallDiscount.ToString();
            }
            else
                txtPayment.ReadOnly = false;
        }

        private void btnPayReset_Click(object sender, EventArgs e)
        {
            resetPay();
        }

        void resetPay()
        {
            txtCustomerName.Text = "Mr";
            txtCustomerPhone.Text = "01";
            txtCustomerAddress.Text = "";
            overallDiscount = 0;
            due = totalCost;
            payment = 0;
            txtOverAllDiscount.Text = overallDiscount.ToString();
            txtPayment.Text = payment.ToString();
            txtDue.Text = due.ToString();
            cmbPayMethod.Text = "Cash";
            chkPaid.Checked = false;
        }

        private void txtPayment_TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtPayment.Text, out parsedValue))
            {
                txtPayment.Text = "0";
                return;
            }

            if (chkPaid.Checked)
                return;
            if (txtPayment.Text != "")
            {
                if (txtOverAllDiscount.Text != "")
                    overallDiscount = Convert.ToDouble(txtOverAllDiscount.Text);
                else
                    overallDiscount = 0;

                payment = Convert.ToDouble(txtPayment.Text);
            }
            else
                payment = 0;

            
            Double discount = totalCost * overallDiscount / 100;
            due = totalCost - (discount + payment);
            txtDue.Text = due.ToString();
            txtPayment.Text = payment.ToString();
            txtOverAllDiscount.Text = overallDiscount.ToString();
        }

        private void txtOverAllDiscount_TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtOverAllDiscount.Text, out parsedValue))
            {
                txtOverAllDiscount.Text = "0";
                return;
            }

            if (txtOverAllDiscount.Text != "")
                overallDiscount = Convert.ToDouble(txtOverAllDiscount.Text);
            else
                overallDiscount = 0;
            
            Double discount = totalCost * overallDiscount / 100;
            due = totalCost - discount - payment;
            txtDue.Text = due.ToString();
            txtOverAllDiscount.Text = overallDiscount.ToString();
        }

        private void txtSaleQuantity_TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtSaleQuantity.Text, out parsedValue))
            {
                txtSaleQuantity.Text = "";
                return;
            }
        }

        private void txtSaleIndivisualDiscount_TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtSaleIndivisualDiscount.Text, out parsedValue))
            {
                txtSaleIndivisualDiscount.Text = "";
                return;
            }
        }

        private void cmbProductType_SelectedIndexChanged(object sender, KeyPressEventArgs e)
        {
            cmbProductType_SelectedIndexChanged(sender, e);
        }

        private void txtProductName_Enter(object sender, EventArgs e)
        {
            txtProductName.Text = "";
            cmbProductType.Text = "Product Type";
        }

        private void txtProductSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String productName = txtProductName.Text;
                String productType = cmbProductType.Text.Trim();
                String productSize = txtProductSize.Text.Trim();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblStocks WHERE ProductName = '" + productName + "' and ProductType = '" + productType + "' and ProductSize = '" + productSize + "'", obj.con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                if (dtbl.Rows.Count >= 1)
                {
                    txtPurchasePrice.Text = dtbl.Rows[0]["PurchasePrice"].ToString();
                    txtPurchaseQuantity.Text = dtbl.Rows[0]["LastPurchaseQuantity"].ToString();
                    txtUnitPrice.Text = dtbl.Rows[0]["UnitSalePrice"].ToString();
                    txtStocks.Text = dtbl.Rows[0]["Quantity"].ToString();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
