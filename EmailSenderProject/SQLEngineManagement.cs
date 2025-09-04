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
            /*
             * This function only test the connection to the database
             */
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
            /*
             * This function does a sql query and puts the result on a list of dictionaries
             */
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
        public static void InsertEmployees(EmployeeCsv EmployeeCsv)
        {
            /*
             * This deletes the database then inserts a new csv file. 
             */
            //Resets the database first
            using (var sqlitecon = new SqliteConnection("Data Source=EmployeeDB.db"))
            {
                sqlitecon.Open();
                string deleteCmd = "DELETE FROM Employee;";

                using (var delCommand = new SqliteCommand(deleteCmd, sqlitecon))
                {
                    delCommand.ExecuteNonQuery();
                }
            }


            //Insert into the database

            using (sqlitecon)
            {
                string sqlQuery = "INSERT INTO Employee (First_Name, Last_Name, Email) VALUES (@firstName, @lastName, @email)";
                SqliteCommand command = new SqliteCommand(sqlQuery, sqlitecon);
                sqlitecon.Open();
                var firstNameParam = command.Parameters.Add("@firstName", SqliteType.Text);
                var lastNameParam = command.Parameters.Add("@lastName", SqliteType.Text);
                var emailParam = command.Parameters.Add("@email", SqliteType.Text);
                try
                {
                    foreach (var emp in EmployeeCsv.records)
                    {
                        firstNameParam.Value = emp.First_Name;
                        lastNameParam.Value = emp.Last_Name;
                        emailParam.Value = emp.Email;
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        public static void InitializeDatabase()
        {
            /*
             * Checks if a database exist and if not, then it creates it
             */
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
