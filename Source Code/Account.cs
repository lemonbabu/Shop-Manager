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
    public partial class Account : Form
    {
        ConnectionString obj = new ConnectionString();
        public Account()
        {
            InitializeComponent();

            pnlAccountMenu.Visible = true;
            pnlViewDueCustomer.Visible = false;
            pnlDuyPayment.Visible = false;
            pnlAccounts.Visible = false;
            pnlAccountsReport.Visible = false;
            pnlDueReport.Visible = false;
            lblTitle.Text = "Accounts Menu";
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
            pnlAccountMenu.Visible = true;
            pnlViewDueCustomer.Visible = false;
            pnlDuyPayment.Visible = false;
            pnlAccounts.Visible = false;
            pnlAccountsReport.Visible = false;
            pnlViewDueCustomer.Visible = false;
            pnlDueReport.Visible = false;
            lblTitle.Text = "Accounts Menu";
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

        private void Account_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnViewDueCustomer_Click(object sender, EventArgs e)
        {
            pnlAccountMenu.Visible = false;
            pnlViewDueCustomer.Visible = true;
            lblTitle.Text = "View Due";
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select *  from tblCustomers where Due > '0' ", obj.con);
            adapt.Fill(dt);
            dataGridViewDueCustomer.DataSource = dt;
        }

        private void Account_Load(object sender, EventArgs e)
        {
            pnlAccountMenu.BackColor = Color.FromArgb(70, 0, 0, 0);
            lblTitle.BackColor = Color.FromArgb(100, 50, 50, 50);
            groupBox3.BackColor = Color.FromArgb(100, 50, 50, 50);
            groupBox1.BackColor = Color.FromArgb(100, 50, 50, 50);
            groupBox5.BackColor = Color.FromArgb(100, 50, 50, 50);
            // TODO: This line of code loads data into the 'pMSDataSet.tblCustomers' table. You can move, or remove it, as needed.
            //this.tblCustomersTableAdapter.Fill(this.pMSDataSet.tblCustomers);
            // TODO: This line of code loads data into the 'pMSDataSet.tblCustomers' table. You can move, or remove it, as needed.
            //this.tblCustomersTableAdapter.Fill(this.pMSDataSet.tblCustomers);

            this.reportViewerAccounts.RefreshReport();
        }

        private void btnDuePayment_Click(object sender, EventArgs e)
        {
            pnlAccountMenu.Visible = false;
            pnlDuyPayment.Visible = true;
            lblTitle.Text = "Due Payment";
        }

        private void btnDuePayCancel_Click(object sender, EventArgs e)
        {
            resetDuePay();
            pnlAccountMenu.Visible = true;
            pnlDuyPayment.Visible = false;
            lblTitle.Text = "Accounts Menu";
        }

        private void btnDuePaySubmit_Click(object sender, EventArgs e)
        {
            String cstId = txtDueCustomerSearch.Text;
            String duePay = txtDuePayment.Text;
            String preDue = txtPreDue.Text;
            String cstName = cmbCustomerName.Text.Trim();
            String cstPhone = cmbCustomerPhone.Text.Trim();
            if (cstId == "")
            {
                MessageBox.Show("Enter valid Customer Id first");
                return;
            }
            else
            {
                if (duePay == "")
                {
                    MessageBox.Show("Enter Curent Paymet !!!!");
                    return;
                }
                else
                {
                    double pay = Convert.ToDouble(duePay);
                    double due = Convert.ToDouble(preDue);
                    if (pay > due)
                    {
                        MessageBox.Show("Payment is gater then due!!!!");
                        return;
                    }
                    else
                    {
                        try
                        {
                            obj.con.Open();
                            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblCustomers Where CustomerId = '" + cstId + "' AND CustomerName = '" + cstName + "' AND CustomerPhone = '" + cstPhone + "'", obj.con);
                            DataTable dtbl = new DataTable();
                            sda.Fill(dtbl);
                            if (dtbl.Rows.Count >= 1)
                            {
                                double totalPay = Convert.ToDouble(txtPrePayment.Text);
                                totalPay += pay;
                                due -= pay;
                                DialogResult confirm = MessageBox.Show("Do you sure to confirm Payment this Due?", "Due Payment", MessageBoxButtons.YesNo);
                                if (confirm == DialogResult.Yes)
                                {
                                    obj.cmd = new SqlCommand("UPDATE tblCustomers SET Payment = '" + totalPay + "', Due = '" + due + "' WHERE CustomerId = '" + cstId + "'", obj.con);
                                    obj.cmd.ExecuteNonQuery();
                                    resetDuePay();
                                    MessageBox.Show("Due Payment Successfully", "Thank you");
                                }
                                else if (confirm == DialogResult.No)
                                {
                                    obj.con.Close();
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Customer Id, Name and Phone is not match???");
                            }
                            obj.con.Close();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        void resetDuePay()
        {
            txtDueCustomerSearch.Text = "";
            cmbCustomerName.Text = "Select Customer Name";
            cmbCustomerPhone.Text = "";
            txtDuePayment.Text = "";
            txtPreDue.Text = "";
            txtPrePayment.Text = "";
            txtTotalCost.Text = "";

        }

        private void cmbCustomerName_MouseClick(object sender, MouseEventArgs e)
        {
            resetDuePay();
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT CustomerName FROM tblCustomers", obj.con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                cmbCustomerName.DataSource = dtbl;
                cmbCustomerName.DisplayMember = "CustomerName";
                cmbCustomerName.ValueMember = "CustomerName";
                cmbCustomerPhone.Text = "Select Customer Phone";
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbCustomerPhone_MouseClick(object sender, MouseEventArgs e)
        {
            String cstName = cmbCustomerName.Text.Trim();
            if (cstName == "")
                return;
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT CustomerPhone FROM tblCustomers Where CustomerName = '" + cstName + "'", obj.con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                cmbCustomerPhone.DataSource = dtbl;
                cmbCustomerPhone.DisplayMember = "CustomerPhone";
                cmbCustomerPhone.ValueMember = "CustomerPhone";
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbCustomerPhone_SelectedIndexChanged(object sender, EventArgs e)
        {
            String cstName = cmbCustomerName.Text.Trim();
            String cstPhone = cmbCustomerPhone.Text.Trim();
            String cstId = txtDueCustomerSearch.Text;
            if (cstId != "")
                return;
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT CustomerId FROM tblCustomers Where CustomerName = '" + cstName + "' AND CustomerPhone = '" + cstPhone + "'", obj.con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);
                if (dtbl.Rows.Count >= 1)
                {
                    txtDueCustomerSearch.Text = dtbl.Rows[0]["CustomerId"].ToString();
                    searchCustomer();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void searchCustomer()
        {
            try
            {
                String cstId = txtDueCustomerSearch.Text;
                if (cstId == "")
                {
                    MessageBox.Show("Enter Customer Id first");
                    return;
                }
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblCustomers Where CustomerId = '" + cstId + "' ", obj.con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);
                if (dtbl.Rows.Count >= 1)
                {
                    cmbCustomerName.DataSource = dtbl;
                    cmbCustomerName.DisplayMember = "CustomerName";
                    cmbCustomerName.ValueMember = "CustomerName";

                    cmbCustomerPhone.DataSource = dtbl;
                    cmbCustomerPhone.DisplayMember = "CustomerPhone";
                    cmbCustomerPhone.ValueMember = "CustomerPhone";

                    txtTotalCost.Text = dtbl.Rows[0]["TotalCost"].ToString();
                    txtPrePayment.Text = dtbl.Rows[0]["Payment"].ToString();
                    txtPreDue.Text = dtbl.Rows[0]["Due"].ToString();
                }
                else
                {
                    MessageBox.Show("Customer Id is not valid", "Wrong");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtReturnCustomerSearch_Click(object sender, EventArgs e)
        {
            resetDuePay();
        }

        private void txtReturnCustomerSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnReturnCustomerSearch_Click(sender, e);
            }
        }

        private void btnReturnCustomerSearch_Click(object sender, EventArgs e)
        {
            searchCustomer();
        }

        Double totalPay, totalDue, totalExp, totalInc;
        private void btnAccounts_Click(object sender, EventArgs e)
        {
            try
            {
                obj.con.Open();
                obj.cmd = new SqlCommand("select SUM(Payment) from tblCustomers", obj.con);
                var ob = obj.cmd.ExecuteScalar();
                if (ob.ToString() == "")
                {
                    MessageBox.Show("No Accounts Available?!","Sorry");
                    obj.con.Close();
                    return;
                }
                else
                    totalPay = Convert.ToDouble(ob.ToString());
                
                obj.cmd = new SqlCommand("select SUM(Cost) from tblExpense", obj.con);
                ob = obj.cmd.ExecuteScalar();
                if (ob.ToString() != "")
                    totalExp = Convert.ToDouble(ob.ToString());
                else
                    totalExp = 0;

                obj.cmd = new SqlCommand("select SUM(Due) from tblCustomers", obj.con);
                ob = obj.cmd.ExecuteScalar();
                if (ob.ToString() != "")
                    totalDue = Convert.ToDouble(ob.ToString());
                else
                    totalDue = 0;

                totalInc = (totalPay + totalDue) - totalExp;

                txtNetInc.Text = totalInc.ToString();
                txtTotalPayment.Text = totalPay.ToString();
                txtTotalExp.Text = totalExp.ToString();
                txtTotalDue.Text = totalDue.ToString();
                obj.con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            pnlAccounts.Visible = true;
            pnlAccountMenu.Visible = false;
            lblTitle.Text = "Accounts";
        }

        private void btnAccountsCancel_Click(object sender, EventArgs e)
        {
            pnlAccounts.Visible = false;
            pnlAccountMenu.Visible = true;
            lblTitle.Text = "Accounts Menu";
        }

        private void btnAccountsPrint_Click(object sender, EventArgs e)
        {
            pnlAccountsReport.Visible = true;
            pnlAccounts.Visible = false;
        }

        private void txtDuePayment_TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtDuePayment.Text, out parsedValue))
            {
                txtDuePayment.Text = "0";
                return;
            }
        }

        private void btnAccountsReport_Click(object sender, EventArgs e)
        {
            pnlAccountsReport.Visible = true;
            pnlAccountMenu.Visible = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            String date1 = dateTimePicker1.Text.Trim();
            String date2 = dateTimePicker2.Text.Trim();
            if (date1 != "" || date1 != "")
            {
                obj.cmd = new SqlCommand("Select * from tblCustomers Where Date >= '" + date1 + "' and Date <= '" + date2 + "' ", obj.con);
                SqlDataAdapter sda = new SqlDataAdapter(obj.cmd);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                ReportDataSource rds = new ReportDataSource("DataSetCustomers", dtbl);
                reportViewerAccounts.LocalReport.DataSources.Clear();
                reportViewerAccounts.LocalReport.DataSources.Add(rds);
                reportViewerAccounts.RefreshReport();

                obj.cmd = new SqlCommand("Select * from tblExpense Where ExpenseDate >= '" + date1 + "' and ExpenseDate <= '" + date2 + "' ", obj.con);
                sda = new SqlDataAdapter(obj.cmd);
                dtbl = new DataTable();
                sda.Fill(dtbl);

                rds = new ReportDataSource("DataSetExpense", dtbl);
                reportViewerAccounts.LocalReport.DataSources.Add(rds);
                reportViewerAccounts.RefreshReport();
            }
            else
            {
                MessageBox.Show("Please Select Date??");
            }
        }

        private void btnDueViewBack_Click(object sender, EventArgs e)
        {
            pnlAccountMenu.Visible = true;
            pnlViewDueCustomer.Visible = false;
            lblTitle.Text = "Accounts Menu";
        }

        private void btnDueViewReport_Click(object sender, EventArgs e)
        {
            pnlDueReport.Visible = true;
            pnlViewDueCustomer.Visible = false;
            lblTitle.Text = "Print View";
            try
            {
                obj.cmd = new SqlCommand("Select * from tblCustomers where Due > '0'", obj.con);

                SqlDataAdapter sda = new SqlDataAdapter(obj.cmd);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                ReportDataSource rds = new ReportDataSource("DataSetCustomers", dtbl);
                reportViewerDue.LocalReport.DataSources.Clear();
                reportViewerDue.LocalReport.DataSources.Add(rds);
                reportViewerDue.RefreshReport();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
