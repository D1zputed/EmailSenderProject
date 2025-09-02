using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProject
{
    internal class CSVHandler(string filePath)
    {
        private readonly string _filePath = filePath;
        public void readfile()
        {
            using (var reader = new StreamReader(_filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Employee>();
            }
        }
    }
    internal class Employee
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
    }


}
