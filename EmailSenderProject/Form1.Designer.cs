namespace EmailSenderProject
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            uploadFileButton = new Button();
            SuspendLayout();
            // 
            // uploadFileButton
            // 
            uploadFileButton.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            uploadFileButton.Location = new Point(291, 271);
            uploadFileButton.Name = "uploadFileButton";
            uploadFileButton.Size = new Size(212, 41);
            uploadFileButton.TabIndex = 0;
            uploadFileButton.Text = "Upload File";
            uploadFileButton.UseVisualStyleBackColor = true;
            uploadFileButton.Click += uploadFileButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(uploadFileButton);
            Name = "Form1";
            Text = "Main Page";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion
        private Button uploadFileButton;
    }
}
