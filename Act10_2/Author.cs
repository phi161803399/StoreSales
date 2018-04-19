using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Act10_2
{
    public class Author
    {
        SqlConnection _pubConnection;
        string _connString;
        SqlDataAdapter _pubDataAdapter;
        DataSet authorDataSet;

        public Author()
        {
            _connString = "Data Source=localhost;" + 
                "Initial Catalog=pubs;Integrated Security=True";
            _pubConnection = new SqlConnection();
            _pubConnection.ConnectionString = _connString;
            SqlCommand selectCommand =
            new SqlCommand("select au_id, au_lname,au_fname from authors",
            _pubConnection);
            _pubDataAdapter = new SqlDataAdapter();
            _pubDataAdapter.SelectCommand = selectCommand;

            SqlCommand updateCommand = new SqlCommand("Update authors set au_lname = @au_lname," +
                "au_fname = @au_fname where au_id = @au_id", _pubConnection);
            updateCommand.Parameters.Add("@au_id", SqlDbType.VarChar, 11, "au_id");
            updateCommand.Parameters.Add("@au_lname", SqlDbType.VarChar, 40, "au_lname");
            updateCommand.Parameters.Add("@au_fname", SqlDbType.VarChar, 40, "au_fname");
            _pubDataAdapter.UpdateCommand = updateCommand;
        }

        public DataSet GetData()
        {
            try
            {
                authorDataSet = new DataSet();
                _pubDataAdapter.Fill(authorDataSet, "authors");
                return authorDataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateData(DataSet changedData)
        {
            try
            {
                _pubDataAdapter.Update(changedData, "authors");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }    
}
