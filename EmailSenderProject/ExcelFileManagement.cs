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
        public static void packageExcelFiles(string filePath)
        {
            //load the chosen excel file
            XLWorkbook wb = new XLWorkbook(filePath);

            //query the employee table
            List<Dictionary<string, string>> employees = SQLEngineManagement.RunSelectQuery();
            List<string> errorList = [];

            //loop through each employee and take their name and put them as arguments in the method
            foreach (Dictionary<string, string> employee in employees)
            {
                XLWorkbook newBook = new XLWorkbook();
                IXLWorksheet wsSource;
                string employeeNameInSheet = $"{employee["Last_Name"]}_{employee["First_Name"]}";
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
            }
            //var wsSource = wb.Worksheet();
        }
    }
}
