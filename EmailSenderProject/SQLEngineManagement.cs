using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmailSenderProject
{
    internal class SQLEngineManagement
    {
        private static SqlConnection sqlCon = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=SqlConnectionTest;User Id=default1;Password=12345678;TrustServerCertificate=True;");
        public Boolean IsNowConnected()
        {
            try
            {
                sqlCon.Open();
                sqlCon.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                sqlCon.Close();
                return false;
            }
        }
        public static List<Dictionary<string,string>> RunSelectQuery()
        {
            List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();
            Dictionary<string, string> column;
            string sqlQuery = "SELECT id, first_name, last_name, email FROM SqlConnectionTest.dbo.receipientListTable";

            SqlCommand command = new SqlCommand(sqlQuery, sqlCon);

            try
            {
                sqlCon.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    column = new Dictionary<string, string>();

                    column["id"] = reader["id"].ToString();
                    column["first_name"] = reader["first_name"].ToString();
                    column["last_name"] = reader["last_name"].ToString();
                    column["email"] = reader["email"].ToString();

                    rows.Add(column);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }

            return rows;
        }
        public Dictionary<string, string> GetNameEmailDictionary()
        {
            Dictionary<string, string> nameEmailPair = new Dictionary<string, string>();
            List<Dictionary<string, string>> rowsFromDatabase = RunSelectQuery();
            foreach(Dictionary<string, string> column in rowsFromDatabase)
            {
                string nameKey = $"{column["first_name"]}_{column["last_name"]}".ToLower();
                nameEmailPair[nameKey] = column["email"];
            }

            return nameEmailPair;
        }
    }
}
