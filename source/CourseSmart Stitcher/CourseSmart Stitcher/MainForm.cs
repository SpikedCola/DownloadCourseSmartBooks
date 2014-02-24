using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace CourseSmart_Stitcher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void inputFolderBtn_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                inputTB.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void outputFileBtn_Click(object sender, EventArgs e)
        {
            if (outputFileDialog.ShowDialog() == DialogResult.OK)
            {
                outputTB.Text = outputFileDialog.FileName;
            }
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            string inputFolder = inputTB.Text;

            if (string.IsNullOrWhiteSpace(inputFolder))
            {
                MessageBox.Show("Input Folder is missing.\n\nPlease choose an input folder before continuing.", "Input Folder Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(inputFolder))
            {
                MessageBox.Show("Specified input folder does not exist.\n\nPlease choose a valid input folder before continuing.", "Input Folder Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] swfFiles = Directory.GetFiles(inputFolder, "*.swf", SearchOption.AllDirectories);
            Array.Sort(swfFiles, new AlphanumComparatorFast());

            if (swfFiles.Length == 0)
            {
                MessageBox.Show("No SWF files were found in the specified input folder.\n\nNothing to do.", "No Files Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            pageList.Items.Clear();
            foreach (string file in swfFiles)
            {
                pageList.Items.Add(Path.GetFileNameWithoutExtension(file));
            }

            saveBtn.Enabled = true;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string outputFile = outputTB.Text;

            if (pageList.Items.Count == 0)
            {
                MessageBox.Show("No pages were found to stitch. Please don't remove all pages from the list.", "No Pages To Stitch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(outputFile))
            {
                MessageBox.Show("Output File is missing.\n\nPlease specify the file you want to save before continuing.", "Output File Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            loadBtn.Enabled = false;
            saveBtn.Enabled = false;
            saveBtn.Text = "Stitching...";
            topBtn.Enabled = false;
            upBtn.Enabled = false;
            downBtn.Enabled = false;
            bottomBtn.Enabled = false;
            pageList.Enabled = false;
            removeBtn.Enabled = false;

            progressBar.Maximum = pageList.Items.Count;

            // was going to do this in a thread, but:
            // - backgroundworker conflicts with the shockwave activex object (backgroundworker isnt STA)
            // - thread was giving me grief
            // so yeah. might flicker a little, but it works

            PdfDocument doc = new PdfDocument();

            // these worked for the book I was doing, might want to play with them
            double ratio = (658 / 568);
            int imageWidth = 1000;
            int imageHeight = (int)(imageWidth * ratio);

            // create new conversion form - we load the swf into an off-screen form
            // and return raw image data in a Bitmap
            using (ConvertForm cf = new ConvertForm())
            {
                cf.SetOutputSize(imageWidth, imageHeight);
                cf.SetScalingMode(ConvertForm.ScalingModes.ExactFit);

                for (int i = 0; i < pageList.Items.Count; i++)
                {
                    progressBar.Value = i + 1;

                    // create full path to file
                    string inputFile = inputTB.Text + Path.DirectorySeparatorChar + pageList.Items[i].ToString() + ".swf";

                    previewFlash.Movie = inputFile;
                    previewFlash.Refresh();
                    // create new page
                    PdfPage page = doc.AddPage();
                    page.Size = PdfSharp.PageSize.Letter;

                    // GetBitmap does the actual conversion
                    using (XGraphics gfx = XGraphics.FromPdfPage(page))
                    using (XImage ximage = XImage.FromGdiPlusImage(cf.GetBitmap(inputFile)))
                    {
                        // draw our image on the pdf page
                        gfx.DrawImage(ximage, 0, 0, page.Width, page.Height);
                    }
                }
            }

            // write pdf to outputFile
            doc.Save(outputFile);

            loadBtn.Enabled = true;
            saveBtn.Enabled = true;
            saveBtn.Text = "Save PDF";
            topBtn.Enabled = true;
            upBtn.Enabled = true;
            downBtn.Enabled = true;
            bottomBtn.Enabled = true;
            pageList.Enabled = true;
            removeBtn.Enabled = true;

            if (MessageBox.Show("Processing finished! PDF file created:\n" + outputTB.Text + "\n\nOpen output file?", "Done!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Process.Start(outputFile);
            }
        }

        #region ListBox Helpers

        private void topBtn_Click(object sender, EventArgs e)
        {
            pageList.BeginUpdate();
            int topIndex = 0;
            for (int i = 0; i < pageList.SelectedItems.Count; i++)
            {
                if (pageList.SelectedIndices[i] > 0)
                {
                    pageList.Items.Insert(topIndex, pageList.SelectedItems[i]);
                    pageList.Items.RemoveAt(pageList.SelectedIndices[i]);
                    pageList.SelectedItem = pageList.Items[topIndex];
                    topIndex++;
                }
            }
            pageList.EndUpdate();
        }

        private void upBtn_Click(object sender, EventArgs e)
        {
            pageList.BeginUpdate();
            for (int i = 0; i < pageList.SelectedItems.Count; i++)
            {
                if (pageList.SelectedIndices[i] > 0)
                {
                    int index = pageList.SelectedIndices[i] - 1;
                    pageList.Items.Insert(index, pageList.SelectedItems[i]);
                    pageList.Items.RemoveAt(pageList.SelectedIndices[i]);
                    pageList.SelectedItem = pageList.Items[index];
                }
            }
            pageList.EndUpdate();
        }

        private void downBtn_Click(object sender, EventArgs e)
        {
            pageList.BeginUpdate();
            for (int i = pageList.SelectedItems.Count - 1; i >= 0; i--)
            {
                if (pageList.SelectedIndices[i] < pageList.Items.Count - 1)
                {
                    int index = pageList.SelectedIndices[i] + 2;
                    pageList.Items.Insert(index, pageList.SelectedItems[i]);
                    pageList.Items.RemoveAt(index - 2);
                    pageList.SelectedItem = pageList.Items[index - 1];
                }
            }
            pageList.EndUpdate();
        }

        private void bottomBtn_Click(object sender, EventArgs e)
        {
            int bottomIndex = pageList.Items.Count - 1;

            pageList.BeginUpdate();
            for (int i = pageList.SelectedItems.Count - 1; i >= 0; i--)
            {
                if (pageList.SelectedIndices[i] < pageList.Items.Count - 1)
                {
                    pageList.Items.Insert(bottomIndex + 1, pageList.SelectedItems[i]);
                    pageList.Items.RemoveAt(pageList.SelectedIndices[i]);
                    pageList.SelectedItem = pageList.Items[bottomIndex];
                    bottomIndex--;
                }
            }
            pageList.EndUpdate();
        }

        private void pageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pageList.SelectedItem != null)
            {
                string file = inputTB.Text + Path.DirectorySeparatorChar + pageList.SelectedItem.ToString() + ".swf";
                previewFlash.Movie = file;
            }
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            if (pageList.SelectedItem != null)
            {
                pageList.Items.Remove(pageList.SelectedItem);
            }
        }

        #endregion

    }

    #region Natural Sort IComparer

    /// <summary>
    /// From http://www.dotnetperls.com/alphanumeric-sorting
    /// </summary>
    public class AlphanumComparatorFast : IComparer
    {
        public int Compare(object x, object y)
        {
            string s1 = x as string;
            if (s1 == null)
            {
                return 0;
            }
            string s2 = y as string;
            if (s2 == null)
            {
                return 0;
            }

            int len1 = s1.Length;
            int len2 = s2.Length;
            int marker1 = 0;
            int marker2 = 0;

            // Walk through two the strings with two markers.
            while (marker1 < len1 && marker2 < len2)
            {
                char ch1 = s1[marker1];
                char ch2 = s2[marker2];

                // Some buffers we can build up characters in for each chunk.
                char[] space1 = new char[len1];
                int loc1 = 0;
                char[] space2 = new char[len2];
                int loc2 = 0;

                // Walk through all following characters that are digits or
                // characters in BOTH strings starting at the appropriate marker.
                // Collect char arrays.
                do
                {
                    space1[loc1++] = ch1;
                    marker1++;

                    if (marker1 < len1)
                    {
                        ch1 = s1[marker1];
                    }
                    else
                    {
                        break;
                    }
                } while (char.IsDigit(ch1) == char.IsDigit(space1[0]));

                do
                {
                    space2[loc2++] = ch2;
                    marker2++;

                    if (marker2 < len2)
                    {
                        ch2 = s2[marker2];
                    }
                    else
                    {
                        break;
                    }
                } while (char.IsDigit(ch2) == char.IsDigit(space2[0]));

                // If we have collected numbers, compare them numerically.
                // Otherwise, if we have strings, compare them alphabetically.
                string str1 = new string(space1);
                string str2 = new string(space2);

                int result;

                if (char.IsDigit(space1[0]) && char.IsDigit(space2[0]))
                {
                    int thisNumericChunk = int.Parse(str1);
                    int thatNumericChunk = int.Parse(str2);
                    result = thisNumericChunk.CompareTo(thatNumericChunk);
                }
                else
                {
                    result = str1.CompareTo(str2);
                }

                if (result != 0)
                {
                    return result;
                }
            }
            return len1 - len2;
        }
    }

    #endregion

    public class Work
    {
        public ListBox.ObjectCollection Items;
        public string OutputFile;
    }

}
