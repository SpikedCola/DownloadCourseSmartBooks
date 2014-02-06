namespace CourseSmart_Stitcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.inputTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.outputTB = new System.Windows.Forms.TextBox();
            this.inputFolderBtn = new System.Windows.Forms.Button();
            this.outputFileBtn = new System.Windows.Forms.Button();
            this.loadBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.topBtn = new System.Windows.Forms.Button();
            this.bottomBtn = new System.Windows.Forms.Button();
            this.removeBtn = new System.Windows.Forms.Button();
            this.pageList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.upBtn = new System.Windows.Forms.Button();
            this.downBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.previewFlash = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.outputFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewFlash)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Folder:";
            // 
            // inputTB
            // 
            this.inputTB.Location = new System.Drawing.Point(97, 24);
            this.inputTB.Name = "inputTB";
            this.inputTB.Size = new System.Drawing.Size(308, 20);
            this.inputTB.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Output PDF File:";
            // 
            // outputTB
            // 
            this.outputTB.Location = new System.Drawing.Point(97, 53);
            this.outputTB.Name = "outputTB";
            this.outputTB.Size = new System.Drawing.Size(308, 20);
            this.outputTB.TabIndex = 3;
            // 
            // inputFolderBtn
            // 
            this.inputFolderBtn.Location = new System.Drawing.Point(411, 22);
            this.inputFolderBtn.Name = "inputFolderBtn";
            this.inputFolderBtn.Size = new System.Drawing.Size(26, 23);
            this.inputFolderBtn.TabIndex = 4;
            this.inputFolderBtn.Text = "...";
            this.inputFolderBtn.UseVisualStyleBackColor = true;
            this.inputFolderBtn.Click += new System.EventHandler(this.inputFolderBtn_Click);
            // 
            // outputFileBtn
            // 
            this.outputFileBtn.Location = new System.Drawing.Point(411, 51);
            this.outputFileBtn.Name = "outputFileBtn";
            this.outputFileBtn.Size = new System.Drawing.Size(26, 23);
            this.outputFileBtn.TabIndex = 5;
            this.outputFileBtn.Text = "...";
            this.outputFileBtn.UseVisualStyleBackColor = true;
            this.outputFileBtn.Click += new System.EventHandler(this.outputFileBtn_Click);
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(116, 110);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(117, 23);
            this.loadBtn.TabIndex = 7;
            this.loadBtn.Text = "Load SWF Files";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Enabled = false;
            this.saveBtn.Location = new System.Drawing.Point(239, 110);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(92, 23);
            this.saveBtn.TabIndex = 8;
            this.saveBtn.Text = "Save PDF";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // topBtn
            // 
            this.topBtn.Location = new System.Drawing.Point(342, 61);
            this.topBtn.Name = "topBtn";
            this.topBtn.Size = new System.Drawing.Size(95, 23);
            this.topBtn.TabIndex = 10;
            this.topBtn.Text = "Move To Top";
            this.topBtn.UseVisualStyleBackColor = true;
            this.topBtn.Click += new System.EventHandler(this.topBtn_Click);
            // 
            // bottomBtn
            // 
            this.bottomBtn.Location = new System.Drawing.Point(342, 148);
            this.bottomBtn.Name = "bottomBtn";
            this.bottomBtn.Size = new System.Drawing.Size(95, 23);
            this.bottomBtn.TabIndex = 11;
            this.bottomBtn.Text = "Move To Bottom";
            this.bottomBtn.UseVisualStyleBackColor = true;
            this.bottomBtn.Click += new System.EventHandler(this.bottomBtn_Click);
            // 
            // removeBtn
            // 
            this.removeBtn.Location = new System.Drawing.Point(342, 328);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(95, 23);
            this.removeBtn.TabIndex = 12;
            this.removeBtn.Text = "Remove Page";
            this.removeBtn.UseVisualStyleBackColor = true;
            this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
            // 
            // pageList
            // 
            this.pageList.FormattingEnabled = true;
            this.pageList.Location = new System.Drawing.Point(6, 61);
            this.pageList.Name = "pageList";
            this.pageList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.pageList.Size = new System.Drawing.Size(311, 290);
            this.pageList.TabIndex = 13;
            this.pageList.SelectedIndexChanged += new System.EventHandler(this.pageList_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.upBtn);
            this.groupBox1.Controls.Add(this.downBtn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pageList);
            this.groupBox1.Controls.Add(this.topBtn);
            this.groupBox1.Controls.Add(this.removeBtn);
            this.groupBox1.Controls.Add(this.bottomBtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 143);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 361);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Organize Pages";
            // 
            // upBtn
            // 
            this.upBtn.Location = new System.Drawing.Point(342, 90);
            this.upBtn.Name = "upBtn";
            this.upBtn.Size = new System.Drawing.Size(95, 23);
            this.upBtn.TabIndex = 17;
            this.upBtn.Text = "Move Up";
            this.upBtn.UseVisualStyleBackColor = true;
            this.upBtn.Click += new System.EventHandler(this.upBtn_Click);
            // 
            // downBtn
            // 
            this.downBtn.Location = new System.Drawing.Point(342, 119);
            this.downBtn.Name = "downBtn";
            this.downBtn.Size = new System.Drawing.Size(95, 23);
            this.downBtn.TabIndex = 16;
            this.downBtn.Text = "Move Down";
            this.downBtn.UseVisualStyleBackColor = true;
            this.downBtn.Click += new System.EventHandler(this.downBtn_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(311, 32);
            this.label3.TabIndex = 15;
            this.label3.Text = "Listed below are all the SWF files (pages) we found. Please order them to your li" +
                "king, and then press Save PDF.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.inputTB);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.outputTB);
            this.groupBox2.Controls.Add(this.inputFolderBtn);
            this.groupBox2.Controls.Add(this.outputFileBtn);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(456, 86);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Configuration";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.previewFlash);
            this.groupBox3.Location = new System.Drawing.Point(474, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(438, 492);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Page Preview";
            // 
            // previewFlash
            // 
            this.previewFlash.Enabled = true;
            this.previewFlash.Location = new System.Drawing.Point(6, 19);
            this.previewFlash.Name = "previewFlash";
            this.previewFlash.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("previewFlash.OcxState")));
            this.previewFlash.Size = new System.Drawing.Size(426, 467);
            this.previewFlash.TabIndex = 0;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 510);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(900, 23);
            this.progressBar.TabIndex = 17;
            // 
            // outputFileDialog
            // 
            this.outputFileDialog.DefaultExt = "pdf";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 541);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.loadBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "CourseSmart Stitcher";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.previewFlash)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox outputTB;
        private System.Windows.Forms.Button inputFolderBtn;
        private System.Windows.Forms.Button outputFileBtn;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button topBtn;
        private System.Windows.Forms.Button bottomBtn;
        private System.Windows.Forms.Button removeBtn;
        private System.Windows.Forms.ListBox pageList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button upBtn;
        private System.Windows.Forms.Button downBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private AxShockwaveFlashObjects.AxShockwaveFlash previewFlash;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.SaveFileDialog outputFileDialog;
    }
}

