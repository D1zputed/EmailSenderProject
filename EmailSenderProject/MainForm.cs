using ClosedXML.Excel;
using System.Diagnostics;
using System.Security.Principal;

namespace EmailSenderProject
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (SQLEngineManagement.IsNowConnected() == true)
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
                    ExcelFileManagement.packageExcelFiles(selectedFile, emailProgressBar, recepientEmailLabel);
                }
                else
                {
                    Console.WriteLine("No folder selected.");
                }
            }
        }

        private void employeeUploadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new())
            {
                fileDialog.Filter = "CSV Files|*.csv";
                DialogResult result = fileDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fileDialog.FileName))
                {
                    string selectedFile = fileDialog.FileName;

                    // Get all files in the folder
                    EmployeeCsv employeeList = new(selectedFile);
                    List<string> errorList = SQLEngineManagement.InsertEmployees(employeeList);
                    if (errorList.Count() > 0)
                    {
                        string errorMessages = string.Join(Environment.NewLine, errorList);
                        MessageBox.Show(errorMessages, "Insert Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Console.WriteLine("No folder selected.");
                }
            }
        }
    }
}
