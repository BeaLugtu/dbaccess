using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataBaseSQLConnectedSignUpXample
{
    class DBAccess
    {
        private static SqlConnection connection = new SqlConnection();
        private static SqlCommand command = new SqlCommand();
        private static SqlDataReader DbReader;
        private static SqlDataAdapter adapter = new SqlDataAdapter();
        public SqlTransaction DbTran;


        private readonly string MySqlCon = "server=26.96.197.206; user=root; database=exam.io; password=admin";
        private readonly MySqlConnection mySqlConnection = new MySqlConnection();

        // Creates a database connectionm
        public void createConn()
        {
            try
            {
                if (mySqlConnection.State != ConnectionState.Open)
                {
                    mySqlConnection.ConnectionString = MySqlCon;
                    mySqlConnection.Open();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw;
            }
        }


        // Closes the existing database connection
        public void closeConn()
        {
            connection.Close();
        }

        // Executes SQL commands (insert, update, delete) on a DataTable using a DataAdapter
        public int executeDataAdapter(DataTable tblName, string strSelectSql)
        {
            try
            {
                if (connection.State == 0)
                {
                    createConn();
                }

                adapter.SelectCommand.CommandText = strSelectSql;
                adapter.SelectCommand.CommandType = CommandType.Text;
                SqlCommandBuilder DbCommandBuilder = new SqlCommandBuilder(adapter);

                string insert = DbCommandBuilder.GetInsertCommand().CommandText.ToString();
                string update = DbCommandBuilder.GetUpdateCommand().CommandText.ToString();
                string delete = DbCommandBuilder.GetDeleteCommand().CommandText.ToString();

                return adapter.Update(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Reads data from the database into a DataTable using a DataAdapter and a provided SQL query
        public void readDatathroughAdapter(string query, DataTable tblName)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }

                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                adapter = new SqlDataAdapter(command);
                adapter.Fill(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Reads data from the database using a DataReader and a provided SQL query
        public SqlDataReader readDatathroughReader(string query)
        {
            // DataReader used to sequentially read data from a data source
            SqlDataReader reader;

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }

                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                reader = command.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Executes a SQL command (insert, update, delete) using a provided SqlCommand object
        // Executes a SQL command (insert, update, delete) using a provided MySqlCommand object
        public int executeQuery(MySqlCommand dbCommand)
        {
            try
            {
                if (mySqlConnection.State != ConnectionState.Open)
                {
                    createConn(); // Ensure connection is open
                }

                dbCommand.Connection = mySqlConnection; // Assign mySqlConnection
                dbCommand.CommandType = CommandType.Text;

                return dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw;
            }
        }

        internal int executeQuery(SqlCommand insertCommand)
        {
            throw new NotImplementedException();
        }
    }

}