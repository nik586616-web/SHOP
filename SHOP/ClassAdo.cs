using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHOP
{
    public class ClassAdo
    {
        public string connectionString { get; set; }
        public SqlCommand cmd;
        public ClassAdo()
        {
            connectionString = "Data Source=localhost\\SQLEXPRES; Initial Catalog = shop;Integrated Security=True;Connect Timeout=30;" +
                "Encrypt=True;TrustServerCertificate = True ;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        public DataSet GetDataSet(string sqlQuerry)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sqlQuerry, connectionString);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Table");
            return ds;
        }
        public void DataGridBind(string sqlQuerry, DataGridView dataGridView)
        {
            DataSet ds = GetDataSet(sqlQuerry);
            dataGridView.DataSource = ds.Tables[0].DefaultView;
        }
        public SqlCommand stProcExec(string commandText)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = commandText;
            return cmd;
        }
        public void Proc(string commandText)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = commandText;
            cmd.ExecuteNonQuery();
        }
        public void comboBoxBind(string sqlQuery, ComboBox comboBox, string displayMember, string valueMember)
        {
            DataSet ds = GetDataSet(sqlQuery);
            comboBox.DataSource = ds.Tables[0];
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
            comboBox.SelectedValue = ds.Tables[0].Rows[0][valueMember].ToString();
        }
    }
}
