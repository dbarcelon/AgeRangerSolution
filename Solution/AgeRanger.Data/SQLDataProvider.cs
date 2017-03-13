using AgeRanger.Utils;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace AgeRanger.Data
{
    [HandleException(applicationName: "AgeRanger.SQLDataProvider")]
    public class SQLDataProvider
    {
        #region Methods
        public static DataSet ExecuteStoresProcedure(string commandText, CommandType commandType=CommandType.Text)
        {
            var result = default(DataSet);
            DataTable dt = new DataTable(); 
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            var connection = new SQLiteConnection(connectionString);


            using (SQLiteCommand command = new SQLiteCommand())
            {
                connection.Open();  
                command.CommandType = commandType;
                command.Connection = connection;
                command.CommandText = commandText;
                command.CommandTimeout = 180;

                dt.Load(command.ExecuteReader());
                result = new DataSet();
                result.Tables.Add(dt);
                connection.Close();       
                //using (SQLiteDataAdapter sqlDataAdapter = new SQLiteDataAdapter(command))
                //{
                //    result = new DataSet();
                //    sqlDataAdapter.Fill(result);
                //    command.Parameters.Clear();
                //    connection.Close();
                //}
            }
            return result;
        }

        public static bool ExecuteNonQuery(string commandText, CommandType commandType = CommandType.Text)
        {
            bool bResponse = false;

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            var connection = new SQLiteConnection(connectionString);


            using (SQLiteCommand command = new SQLiteCommand())
            {
                connection.Open();
                command.CommandType = commandType;
                command.Connection = connection;
                command.CommandText = commandText;
                command.CommandTimeout = 180;

                command.ExecuteNonQuery();  
                command.Parameters.Clear();
                connection.Close();
                bResponse = true; 
            }
            return bResponse;
        }

        public static object ExecuteScalar(string commandText, CommandType commandType = CommandType.Text)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            var connection = new SQLiteConnection(connectionString);
            object oScalar = null; 
            using (SQLiteCommand command = new SQLiteCommand())
            {
                connection.Open();
                command.CommandType = commandType;
                command.Connection = connection;
                command.CommandText = commandText;
                command.CommandTimeout = 180;

                oScalar = command.ExecuteScalar();
                command.Parameters.Clear();
                connection.Close();
            }
            return oScalar;
        }

        #endregion

    }
}
