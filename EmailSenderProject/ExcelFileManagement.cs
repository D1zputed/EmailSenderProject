using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProject
{
    internal class ExcelFileManagement
    {
        public static void packageExcelFiles(string filePath, ProgressBar progressBar, Label label)
        {
            progressBar.Visible = true;
            label.Visible = true;


            //load the chosen excel file
            XLWorkbook wb = new XLWorkbook(filePath);


            //query the employee table
            List<Dictionary<string, string>> employees = SQLEngineManagement.RunSelectQuery();
            List<string> errorList = [];


            // Initialize progress bar
            progressBar.Minimum = 0;
            progressBar.Maximum = employees.Count;
            progressBar.Value = 0;


            //loop through each employee and take their name and put them as arguments in the method
            foreach (Dictionary<string, string> employee in employees)
            {
                XLWorkbook newBook = new XLWorkbook();
                IXLWorksheet wsSource;
                string employeeNameInSheet = $"{employee["Last_Name"]}_{employee["First_Name"]}";
                label.Text = $"Sending Email To: {employeeNameInSheet}";
                try
                {
                    wsSource = wb.Worksheet(employeeNameInSheet);
                }
                catch (ArgumentException)
                {
                    errorList.Add(employeeNameInSheet);
                    continue;
                }


                wsSource.CopyTo(newBook, employeeNameInSheet);
                newBook.SaveAs($"{employeeNameInSheet}.xlsx");
                EmailManagement.sendEmail(employee["Email"], $"{employeeNameInSheet}.xlsx");


                // Update progress bar safely on UI thread
                if (progressBar.InvokeRequired)
                {
                    progressBar.Invoke(() => progressBar.Value++);
                }
                else
                {
                    progressBar.Value++;
                }
            }
            if (errorList.Count() > 0)
            {
                string errorMessages = string.Join(Environment.NewLine, errorList);
                MessageBox.Show(errorMessages, "Sending Unsuccessful for these", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressBar.Visible = false;
            label.Visible = false;
        }
    }
}
