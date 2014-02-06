using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CourseSmart_Decrypter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void inputSelectBtn_Click(object sender, EventArgs e)
        {
            if (inputFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                inputFolderTB.Text = inputFolderBrowser.SelectedPath;
            }
        }

        private void outputSelectBtn_Click(object sender, EventArgs e)
        {
            if (outputFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                outputFolderTB.Text = outputFolderBrowser.SelectedPath;
            }
        }

        private void decryptBtn_Click(object sender, EventArgs e)
        {
            string inputFolder = inputFolderTB.Text;
            string outputFolder = outputFolderTB.Text;

            if (string.IsNullOrWhiteSpace(inputFolder))
            {
                MessageBox.Show("No input folder specified. Please select an input folder", "Folder Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(outputFolder))
            {
                MessageBox.Show("No output folder specified. Please select an output folder", "Folder Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(inputFolder))
            {
                MessageBox.Show("Specified input folder does not exist. Please try again", "Folder Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }
            else if (Directory.GetFiles(outputFolder).Length != 0)
            {
                if (MessageBox.Show("Output folder is not empty - existing files with the same name will be overwritten.\n\nDo you still want to proceed?", "Output Folder Not Empty", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }

            // enumerate files in folder 
            string[] files = Directory.GetFiles(inputFolder, "*", SearchOption.TopDirectoryOnly);

            if (files.Length == 0)
            {
                MessageBox.Show("No files were found in the specified input folder. Nothing to do.", "No Files Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int processedFiles = 0;
            foreach (string filename in files)
            {
                // ensure we have a CS file
                if (CourseSmartFile.IsCourseSmartFile(filename))
                {
                    try
                    {
                        using (CourseSmartFile file = new CourseSmartFile(filename))
                        {
                            // write swf movie data to output folder
                            string outputFilename = file.GetFilename();
                            File.WriteAllBytes(outputFolder + Path.DirectorySeparatorChar + outputFilename + ".swf", file.MovieData);
                            processedFiles++;   
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Something went wrong loading file:\n\n" + filename + "\n\nException Details:\n\n" + ex.ToString() + "\n\nIf you believe this file should parse, please contact me with the details provided above, and I will see what I can do.", "File Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            MessageBox.Show("All done! Processed " + processedFiles + " file" + (processedFiles == 1 ? "" : "s"), "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
