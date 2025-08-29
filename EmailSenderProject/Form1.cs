using ClosedXML.Excel;
using System.Diagnostics;
using System.Security.Principal;

namespace EmailSenderProject
{
    public partial class Form1 : Form
    {
        SQLEngineManagement SQLEngManage = new();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (SQLEngManage.CheckConnection() == true)
            {
                MessageBox.Show("You are now connected!", "NOTICE!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Connection Failed!", "NOTICE!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void uploadFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new())
            {
                fileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                DialogResult result = fileDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fileDialog.FileName))
                {
                    string selectedFile = fileDialog.FileName;

                    // Get all files in the folder
                    Debug.WriteLine(selectedFile);
                    ExcelFileManagement.packageExcelFiles(selectedFile);
                }
                else
                {
                    Console.WriteLine("No folder selected.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var wb = new XLWorkbook();
            wb.Worksheets.Add("sheet");
            var wsSource = wb.Worksheet(1);
            var name = wsSource.ToString();

            Debug.WriteLine(name);

        }
    }
}
