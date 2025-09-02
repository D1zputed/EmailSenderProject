using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;
using Microsoft.Data.Sqlite;
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
        private static SqliteConnection sqlitecon = new SqliteConnection("DataSource=EmployeeDB.db");
        public Boolean IsNowConnected()
        {
            try
            {
                sqlitecon.Open();
                sqlitecon.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                sqlitecon.Close();
                return false;
            }
        }
        public static List<Dictionary<string, string>> RunSelectQuery()
        {
            List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();
            Dictionary<string, string> column;
            string sqlQuery = "SELECT Id, First_Name, Last_Name, Email FROM Employee";

            SqliteCommand command = new SqliteCommand(sqlQuery, sqlitecon);

            try
            {
                sqlitecon.Open();

                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    column = new Dictionary<string, string>();

                    column["Id"] = reader["Id"].ToString();
                    column["First_Name"] = reader["First_Name"].ToString();
                    column["Last_Name"] = reader["Last_Name"].ToString();
                    column["Email"] = reader["Email"].ToString();

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
                sqlitecon.Close();
            }

            return rows;
        }
        public static void InsertEmployees()
        {

        }
        public Dictionary<string, string> GetNameEmailDictionary()
        {
            Dictionary<string, string> nameEmailPair = new Dictionary<string, string>();
            List<Dictionary<string, string>> rowsFromDatabase = RunSelectQuery();
            foreach (Dictionary<string, string> column in rowsFromDatabase)
            {
                string nameKey = $"{column["first_name"]}_{column["last_name"]}".ToLower();
                nameEmailPair[nameKey] = column["email"];
            }

            return nameEmailPair;
        }
        public static void InitializeDatabase()
        {
            string databasePath = "Employee.db";

            if (!File.Exists(databasePath))
            {
                using (sqlitecon)
                {
                    sqlitecon.Open();
                    var createTableCmd = sqlitecon.CreateCommand();
                    createTableCmd.CommandText =
                    @"
                          CREATE TABLE Employees (
                              Id INTEGER PRIMARY KEY AUTOINCREMENT,
                              FirstName TEXT NOT NULL,
                              LastName TEXT NOT NULL,
                              Email TEXT NOT NULL
                          );
                      ";

                    createTableCmd.ExecuteNonQuery();
                    sqlitecon.Close();
                }
            }
        }
    }
}
