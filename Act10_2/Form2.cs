using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Act10_2
{
    public partial class Form2 : Form
    {
        DataSet StoreSalesDataSet;
        public Form2()
        {
            InitializeComponent();
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            StoreSales storeSales = new StoreSales();
            StoreSalesDataSet = storeSales.GetData();
            dgvStores.DataSource = StoreSalesDataSet.Tables["stores"];
            dgvSales.DataSource = StoreSalesDataSet.Tables["sales"];
            dgvSales.DataMember = "StoreSales";
        }
    }
}
