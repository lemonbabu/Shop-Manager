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
    public partial class Report : Form
    {
        ConnectionString obj = new ConnectionString();
        public Report()
        {
            InitializeComponent();
            pnlReportMenu.Visible = true;
            pnlSalesHistory.Visible = false;
            pnlSalesReport.Visible = false;
            pnlSaleMemo.Visible = false;
            pnlSaleMemoPrint.Visible = false;
            lblTitle.Text = "Report Menu";
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

        private void Report_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
       
        private void btnSalesHistory_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select *  from tblCustomers", obj.con);
            adapt.Fill(dt);
            dataGridViewSalesHistory.DataSource = dt;

            pnlReportMenu.Visible = false;
            pnlSalesHistory.Visible = true;
            lblTitle.Text = "Sale History";
        }

       
        private void Report_Load(object sender, EventArgs e)
        {
            pnlReportMenu.BackColor = Color.FromArgb(70, 0, 0, 0);
            lblTitle.BackColor = Color.FromArgb(100, 50, 50, 50);
            groupBox6.BackColor = Color.FromArgb(100, 50, 50, 50);
            
            this.reportViewerSaleMemo.RefreshReport();
        }

        private void btnSaleReport_Click(object sender, EventArgs e)
        {
            pnlSalesReport.Visible = true;
            pnlReportMenu.Visible = false;
            lblTitle.Text = "Sales Report";
            dateTimePickerSale1.Text = "";
            dateTimePickerSale1.Text = "";
        }

      
        private void btnSaleOk_Click(object sender, EventArgs e)
        {
            String date1 = dateTimePickerSale1.Text.Trim();
            String date2= dateTimePickerSale2.Text.Trim();
            String wholeSale;
            if (chkWholeSale.Checked)
                wholeSale = "YES";
            else
                wholeSale = "NO";
            if (date1 != "" && date2 != "")
            {
                try{
                    obj.con.Open();
                    obj.cmd = new SqlCommand("Select * from tblCustomers Where Date >= '" + date1 + "' and  Date <= '" + date1 + "' and WholeSale = '" + wholeSale + "'", obj.con);
                    SqlDataAdapter sda = new SqlDataAdapter(obj.cmd);
                    DataTable dtbl = new DataTable();
                    sda.Fill(dtbl);

                    ReportDataSource rds = new ReportDataSource("DataSetCustomers", dtbl);
                    reportViewerSales.LocalReport.DataSources.Clear();
                    reportViewerSales.LocalReport.DataSources.Add(rds);
                    reportViewerSales.RefreshReport();
                    obj.con.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please Select Date??");
            }
        }

        
        private void btnSaleMemo_Click(object sender, EventArgs e)
        {
            pnlSaleMemo.Visible = true;
            pnlReportMenu.Visible = false;
            lblTitle.Text = "Print Memo";
        }

        private void btnSaleCustomerSearch_Click(object sender, EventArgs e)
        {
            obj.con.Open();
            searchCustomer();
            obj.con.Close();
        }

        void searchCustomer()
        {
            try
            {
                String cstId = txtReturnCustomerSearch.Text;
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

        private void btnSaleMemoCancel_Click(object sender, EventArgs e)
        {
            pnlReportMenu.Visible = true;
            pnlSaleMemo.Visible = false;
            lblTitle.Text = "Report Menu";
        }

        private void btnSaleMomoPrint_Click(object sender, EventArgs e)
        {
            String cstId = txtReturnCustomerSearch.Text;
            String chkRetrn;
            try
            {
                obj.con.Open();
                if (chkReturn.Checked)
                {
                    chkRetrn = "YES";
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblCustomers Where CustomerId = '" + cstId + "' and Retrn = '" + chkRetrn + "'", obj.con);
                    DataTable dtbl = new DataTable();
                    sda.Fill(dtbl);
                    if (dtbl.Rows.Count >= 1)
                    {
                        try
                        {
                            pnlSaleMemo.Visible = false;
                            lblTitle.Text = "Print View";

                            obj.cmd = new SqlCommand("Select * from tblCustomers where CustomerId = '" + cstId + "'", obj.con);
                            sda = new SqlDataAdapter(obj.cmd);
                            dtbl = new DataTable();
                            sda.Fill(dtbl);

                            ReportDataSource rds = new ReportDataSource("DataSetCustomers", dtbl);

                            obj.cmd = new SqlCommand("Select * from tblReturnProducts where CustomerId = '" + cstId + "'", obj.con);
                            sda = new SqlDataAdapter(obj.cmd);
                            dtbl = new DataTable();
                            sda.Fill(dtbl);

                            rds = new ReportDataSource("DataSetReturnProducts", dtbl);
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                        MessageBox.Show("This Customers have no return bill?","Sorry");
                }
                else
                {
                    try
                    {
                        pnlSaleMemoPrint.Visible = true;
                        pnlSaleMemo.Visible = false;
                        lblTitle.Text = "Print View";

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
                obj.con.Close();
                resetReturnCustomer();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtReturnCustomerSearch_Click(object sender, EventArgs e)
        {
            resetReturnCustomer();
        }

        void resetReturnCustomer()
        {
            txtReturnCustomerSearch.Text = "";
            cmbCustomerPhone.Text = "";
            cmbCustomerName.Text = "Select Customer Name";
        }

        private void txtReturnCustomerSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSaleCustomerSearch_Click(sender, e);
            }
        }

        private void cmbCustomerName_MouseClick(object sender, MouseEventArgs e)
        {
            resetReturnCustomer();
            try
            {
                obj.con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT CustomerName FROM tblCustomers", obj.con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                cmbCustomerName.DataSource = dtbl;
                cmbCustomerName.DisplayMember = "CustomerName";
                cmbCustomerName.ValueMember = "CustomerName";
                cmbCustomerPhone.Text = "Select Customer Phone";
                obj.con.Close();
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
                obj.con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT CustomerPhone FROM tblCustomers Where CustomerName = '" + cstName + "'", obj.con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                cmbCustomerPhone.DataSource = dtbl;
                cmbCustomerPhone.DisplayMember = "CustomerPhone";
                cmbCustomerPhone.ValueMember = "CustomerPhone";
                obj.con.Close();
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
            String cstId = txtReturnCustomerSearch.Text;
            if (cstId != "")
                return;
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT CustomerId FROM tblCustomers Where CustomerName = '" + cstName + "' AND CustomerPhone = '" + cstPhone + "'", obj.con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);
                if (dtbl.Rows.Count >= 1)
                {
                    txtReturnCustomerSearch.Text = dtbl.Rows[0]["CustomerId"].ToString();
                    searchCustomer();
                }
                obj.con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void dataGridViewSalesHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            if (e.ColumnIndex == 12)
            {
                DialogResult result = MessageBox.Show("Are you sure to Delete?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        i = Convert.ToInt16(dataGridViewSalesHistory.Rows[e.RowIndex].Cells[0].Value.ToString());
                        BindingSource a = new BindingSource();
                        obj.con.Open();
                        obj.cmd = new SqlCommand("DELETE FROM tblCustomers WHERE CustomerId = '" + i + "'", obj.con);
                        obj.cmd.ExecuteNonQuery();
                        obj.cmd = new SqlCommand("DELETE FROM tblSaleProducts WHERE CustomerId = '" + i + "'", obj.con);
                        obj.cmd.ExecuteNonQuery();
                        DataTable dt = new DataTable();
                        SqlDataAdapter adapt = new SqlDataAdapter("select * from tblCustomers", obj.con);
                        adapt.Fill(dt);
                        dataGridViewSalesHistory.DataSource = dt;
                        obj.con.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                        obj.con.Close();
                    }
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            pnlReportMenu.Visible = true;
            pnlSaleMemo.Visible = false;
            pnlSaleMemoPrint.Visible = false;
            pnlSalesHistory.Visible = false;
            pnlSalesReport.Visible = false;
            lblTitle.Text = "Report Me";
        }

        
    }
}
