using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Act10_2
{
    class StoreSales
    {
        SqlConnection _pubConnection;
        string _connString;
        SqlDataAdapter _storeDataAdapter = new SqlDataAdapter();
        SqlDataAdapter _salesDataAdapter = new SqlDataAdapter();
        DataSet storeSalesDataSet;

        public StoreSales()
        {
            _connString = "Data Source=localhost;Initial Catalog=pubs;Integrated Security=True";
            _pubConnection = new SqlConnection();
            _pubConnection.ConnectionString = _connString;
        }

        public DataSet GetData()
        {
            try
            {
                //Get Store Info
                string selectStoresSQL = "SELECT [stor_id] ,[stor_name]," +
                    "[city],[state] FROM [stores]";
                SqlCommand selectStoresCommand =
                new SqlCommand(selectStoresSQL, _pubConnection);
                selectStoresCommand.CommandType = CommandType.Text;
                _storeDataAdapter.SelectCommand = selectStoresCommand;
                //Get Sales Info
                string selectSalesSQL = "SELECT [stor_id],[ord_num]," +
                "[ord_date],[qty] FROM [sales]";
                SqlCommand selectSalesCommand =
                new SqlCommand(selectSalesSQL, _pubConnection);
                selectSalesCommand.CommandType = CommandType.Text;
                _salesDataAdapter.SelectCommand = selectSalesCommand;
                //Get data and fill DataSet
                storeSalesDataSet = new DataSet();
                _storeDataAdapter.Fill(storeSalesDataSet, "stores");
                _salesDataAdapter.Fill(storeSalesDataSet, "sales");
                //Create relationahip between tables
                DataColumn Store_StoreIdColumn =
                storeSalesDataSet.Tables["stores"].Columns["stor_id"];
                DataColumn Sales_StoreIdColumn =
                storeSalesDataSet.Tables["sales"].Columns["stor_id"];
                DataRelation StoreSalesRelation =
                new DataRelation("StoresToSales", Store_StoreIdColumn, Sales_StoreIdColumn);
                storeSalesDataSet.Relations.Add(StoreSalesRelation);
                return storeSalesDataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
