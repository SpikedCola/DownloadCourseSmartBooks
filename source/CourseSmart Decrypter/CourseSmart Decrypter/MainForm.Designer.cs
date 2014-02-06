namespace CourseSmart_Decrypter
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.decryptBtn = new System.Windows.Forms.Button();
            this.inputFolderTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.outputFolderTB = new System.Windows.Forms.TextBox();
            this.inputSelectBtn = new System.Windows.Forms.Button();
            this.outputSelectBtn = new System.Windows.Forms.Button();
            this.inputFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.outputFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Directory:";
            // 
            // decryptBtn
            // 
            this.decryptBtn.Location = new System.Drawing.Point(230, 86);
            this.decryptBtn.Name = "decryptBtn";
            this.decryptBtn.Size = new System.Drawing.Size(75, 23);
            this.decryptBtn.TabIndex = 1;
            this.decryptBtn.Text = "Decrypt";
            this.decryptBtn.UseVisualStyleBackColor = true;
            this.decryptBtn.Click += new System.EventHandler(this.decryptBtn_Click);
            // 
            // inputFolderTB
            // 
            this.inputFolderTB.Location = new System.Drawing.Point(105, 16);
            this.inputFolderTB.Name = "inputFolderTB";
            this.inputFolderTB.Size = new System.Drawing.Size(386, 20);
            this.inputFolderTB.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output Directory:";
            // 
            // outputFolderTB
            // 
            this.outputFolderTB.Location = new System.Drawing.Point(105, 44);
            this.outputFolderTB.Name = "outputFolderTB";
            this.outputFolderTB.Size = new System.Drawing.Size(386, 20);
            this.outputFolderTB.TabIndex = 4;
            // 
            // inputSelectBtn
            // 
            this.inputSelectBtn.Location = new System.Drawing.Point(497, 13);
            this.inputSelectBtn.Name = "inputSelectBtn";
            this.inputSelectBtn.Size = new System.Drawing.Size(23, 23);
            this.inputSelectBtn.TabIndex = 5;
            this.inputSelectBtn.Text = "...";
            this.inputSelectBtn.UseVisualStyleBackColor = true;
            this.inputSelectBtn.Click += new System.EventHandler(this.inputSelectBtn_Click);
            // 
            // outputSelectBtn
            // 
            this.outputSelectBtn.Location = new System.Drawing.Point(497, 42);
            this.outputSelectBtn.Name = "outputSelectBtn";
            this.outputSelectBtn.Size = new System.Drawing.Size(23, 23);
            this.outputSelectBtn.TabIndex = 6;
            this.outputSelectBtn.Text = "...";
            this.outputSelectBtn.UseVisualStyleBackColor = true;
            this.outputSelectBtn.Click += new System.EventHandler(this.outputSelectBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 125);
            this.Controls.Add(this.outputSelectBtn);
            this.Controls.Add(this.inputSelectBtn);
            this.Controls.Add(this.outputFolderTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.inputFolderTB);
            this.Controls.Add(this.decryptBtn);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CourseSmart Decrypter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button decryptBtn;
        private System.Windows.Forms.TextBox inputFolderTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox outputFolderTB;
        private System.Windows.Forms.Button inputSelectBtn;
        private System.Windows.Forms.Button outputSelectBtn;
        private System.Windows.Forms.FolderBrowserDialog inputFolderBrowser;
        private System.Windows.Forms.FolderBrowserDialog outputFolderBrowser;
    }
}

