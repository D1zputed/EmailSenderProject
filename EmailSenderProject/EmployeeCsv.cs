using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProject
{
    internal class EmployeeCsv
    {
        private readonly string _filePath;
        public List<Employee> records;

        public EmployeeCsv(string filePath)
        {
            this._filePath = filePath;
            readfile();
        }

        public void readfile()
        {
            using (var reader = new StreamReader(_filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<Employee>().ToList();
            }
        }
    }
}
