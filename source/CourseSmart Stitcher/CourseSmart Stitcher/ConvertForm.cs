using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace CourseSmart_Stitcher
{
    public partial class ConvertForm : Form
    {
        /// <summary>
        /// Possible Scaling Modes of the ActiveX object
        /// </summary>
        public enum ScalingModes { ExactFit, NoBorder, NoScale, ShowAll, };

        /// <summary>
        /// Output size of image, default to 400x500
        /// </summary>
        Size OutputSize = new Size(400, 500);

        /// <summary>
        /// Padding around ActiveX object, to ensure form is larger than control
        /// </summary>
        const int FormPadding = 100;

        public ConvertForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the desired output size of the image
        /// </summary>
        /// <param name="width">Desired output width</param>
        /// <param name="height">Desired output height</param>
        public void SetOutputSize(int width, int height)
        {
            OutputSize = new Size(width, height);
        }

        /// <summary>
        /// Sets the scaling mode of the ActiveX object
        /// </summary>
        /// <param name="scalingMode">Desired scaling mode</param>
        public void SetScalingMode(ScalingModes scalingMode)
        {
            switch (scalingMode)
            {
                case ScalingModes.ExactFit:
                    axShockwaveFlash1.CtlScale = "ExactFit";
                    break;
                case ScalingModes.NoBorder:
                    axShockwaveFlash1.CtlScale = "NoBorder";
                    break;
                case ScalingModes.NoScale:
                    axShockwaveFlash1.CtlScale = "NoScale";
                    break;
                case ScalingModes.ShowAll:
                    axShockwaveFlash1.CtlScale = "ShowAll";
                    break;
            }
        }

        /// <summary>
        /// Convert the specified SWF file into a PNG. The general flow is:
        /// * load SWF into Shockwave ActiveX control
        /// * resize movie & form to desired output size
        /// * show form & capture image of ActiveX control
        /// * hide form
        /// We must show & hide the form, otherwise the image will turn out blank.
        /// 
        /// This was the simplest thing I could think of - unfortunately
        /// the resulting file will not have selectable text. If there is a better
        /// way to do this and keep the text, please contact me <parkinglotlust@gmail.com>
        /// </summary>
        /// <param name="inputFile"></param>
        public Bitmap GetBitmap(string inputFile)
        {
            string filename = Path.GetFileNameWithoutExtension(inputFile);

            // resize form to fit movie
            this.Width = OutputSize.Width + FormPadding;
            this.Height = OutputSize.Height + FormPadding;

            // move form off screen so it doesnt bother anyone (surprisingly it still captures fine)
            int maxHeight = Screen.PrimaryScreen.Bounds.Height;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, maxHeight + 1);

            // load movie & resize
            axShockwaveFlash1.Movie = inputFile;
            axShockwaveFlash1.Size = OutputSize;

            this.Show();

            // .Refresh() is SUPER IMPORTANT and must be done AFTER this.Show();
            // without it, the control isnt updated until after returning from Convert(),
            // and the saved image turns out blank
            axShockwaveFlash1.Refresh();

            Bitmap bitmap = new Bitmap(OutputSize.Width, OutputSize.Height);
            this.CopyFlashToBitmap(ref bitmap);

            this.Hide();

            return bitmap;
        }

        /// <summary>
        /// Copies the current image displayed in the ActiveX Flash object to the specified Bitmap
        /// </summary>
        /// <param name="bitmap">Bitmap to be filled</param>
        private void CopyFlashToBitmap(ref Bitmap bitmap)
        {
            Graphics g = Graphics.FromImage(bitmap);

            IntPtr bmDC = g.GetHdc();
            IntPtr srcDC = GetDC(axShockwaveFlash1.Handle);

            const Int32 SRCCOPY = 0x00CC0020;
            StretchBlt(bmDC, 0, 0, OutputSize.Width, OutputSize.Height, srcDC, 0, 0, OutputSize.Width, OutputSize.Height, SRCCOPY);
            ReleaseDC(axShockwaveFlash1.Handle, srcDC);

            g.ReleaseHdc(bmDC);
            g.Dispose();
        }

        #region DLL Imports
        [DllImport("gdi32.dll")]
        private static extern bool StretchBlt(
            IntPtr hdcDest,          // handle to destination DC 
            int nXDest,                // x-coord of destination upper-left corn.er 
            int nYDest,                // y-coord of destination upper-left corner 
            int nWidth,               // width of destination rectangle 
            int nHeight,              // height of destination rectangle 
            IntPtr hdcSrc,            // handle to source DC 
            int nXSrc,                  // x-coordinate of source upper-left corner 
            int nYSrc,                  // y-coordinate of source upper-left corner 
            int nSrcWidth,
            int nSrcHeight,
            Int32 dwRop  // raster operation code 
        );

        [DllImport("User32.dll")]
        public extern static IntPtr GetDC(IntPtr hWnd);

        [DllImport("User32.dll")]
        public extern static int ReleaseDC(IntPtr hWnd, IntPtr hDC); //modified to include hWnd
        #endregion
    }
}
