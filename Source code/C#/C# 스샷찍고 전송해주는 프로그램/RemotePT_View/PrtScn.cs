using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace RemotePT_View
{
    public delegate void MYevent( Bitmap bmp );

    class PrtScn
    {
        public PrtScn()
        {        }

       

        public event MYevent prt_event; 
        private void prt(int Height, int Width, Size size, Bitmap bmp)
        {
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(0, 0, 0, 0, size);
            //bmp.Save(@"c:\fuck.bmp");

            prt_event(bmp);
            #region Encoding 2 jpeg
            /*
            
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;

            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 25L);
            myEncoderParameters.Param[0] = myEncoderParameter;


            Bitmap b = new Bitmap(Width / 2, Height / 2);
            using (Graphics G = Graphics.FromImage((Image)b))
            {
                //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                G.InterpolationMode = InterpolationMode.Low;

                G.DrawImage(bmp, 0, 0, Width / 2, Height / 2);

                //bmp.Dispose();
            }

            b.Save(@"c:\TestPhotoQualityZero.jpg", jgpEncoder, myEncoderParameters); 
            */
            #endregion
        }

        public void run()
        {
            int Height = Screen.PrimaryScreen.Bounds.Height;
            int Width = Screen.PrimaryScreen.Bounds.Width;

            Size size = new Size(Width, Height);
            Bitmap bmp = new Bitmap(Width, Height);
                
            while(true)
            {
                System.Threading.Thread.Sleep(100);
                prt(Height, Width, size, bmp);
            }
            bmp.Dispose();
        }

        
    }
}
