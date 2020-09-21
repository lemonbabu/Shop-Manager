using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Pharmacy_Management_System
{
    class ConnectionString
    {
        public SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\PMS.mdf;Integrated Security=True");
        //public SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Programing\Visual Studio\New Editions\Pharmacy Management System\Pharmacy Management System\PMS.mdf;Integrated Security=True");
        public SqlCommand cmd = new SqlCommand();
        public void appClose(FormClosingEventArgs e)
        {
            DialogResult confirm = MessageBox.Show("      Are you sure to close this Application?", "Exit", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (confirm == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

    }
}
