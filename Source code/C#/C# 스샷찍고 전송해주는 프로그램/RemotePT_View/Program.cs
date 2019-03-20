using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Net;
using System.Net.Sockets;
namespace RemotePT_View
{
    class Program
    {
        static private bool ImageCmp(Bitmap src, Bitmap bmp)
        {
            string srcInfo, bmpInfo;

            int Width = src.Width;
            int Height = src.Height;
            int Area_tmp = Width * Height / 1000000;

            int inc_Width = Width / 70;
            int inc_Height = Height / 70;

            for (int i = 0; i < src.Width ; i+=inc_Width)
            {
                for (int j = 0; (j < src.Height) && (Area_tmp != 0); j+=inc_Height)
                {
                    srcInfo = src.GetPixel(i, j).ToString();
                    bmpInfo = bmp.GetPixel(i, j).ToString();

                    if (srcInfo != bmpInfo)
                    {
                        Area_tmp--;
                    }
                }
            }
            return (Area_tmp != 0);
        }

        static private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        enum FLAG { NoChange, Change_Before, Change_After };
        static FLAG condition;
        static Bitmap OriBmp;

        static public void prt_do(Bitmap bmp)
        {
            if (OriBmp == null) 
                OriBmp = (Bitmap)bmp.Clone();
            switch (condition)
            {
                case FLAG.NoChange :
                    if (ImageCmp(OriBmp, bmp)) 
                    { }
                    else 
                    {
                        condition = FLAG.Change_Before;
                    }
                    OriBmp = (Bitmap)bmp.Clone();
                    break;

                case FLAG.Change_Before:
                    bmp.encodingNpost(server);//.Save(@"c:\fuck.bmp");
                    if (ImageCmp(OriBmp, bmp))
                    {
                        condition = FLAG.NoChange;
                    }
                    else
                    {
                        condition = FLAG.Change_After;
                    }
                    OriBmp = (Bitmap)bmp.Clone();
                    break;

                case FLAG.Change_After:
                    if (ImageCmp(OriBmp, bmp))
                    {
                        condition = FLAG.NoChange;
                        bmp.encodingNpost(server);
                    }
                    else
                    {
                    }
                    OriBmp = (Bitmap)bmp.Clone();
                    break;

            }
        }
        static SERVER server;
        static void Main(string[] args)
        {
            server = new SERVER(9999);
            server.SeverOn();
            condition = FLAG.Change_After;    
            PrtScn PRT = new PrtScn();
            PRT.prt_event += new MYevent(prt_do);
            
            PRT.run();

            server.endSERVER();
        }
    }
}
