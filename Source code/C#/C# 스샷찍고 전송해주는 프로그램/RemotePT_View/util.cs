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
    public class SERVER
    {
        int port;
        NetworkStream net_stream;
        TcpClient TCP_client;
        TcpListener TCP_Listen;
        public SERVER(int port)
        {
            this.port = port;
        }
        public void SeverOn()
        {
            TCP_Listen = new TcpListener(IPAddress.Any, port);
            TCP_Listen.Start();

            Console.WriteLine("연결중");
            /* TCPClient 객체 생성
             * TCPListener 객체로부터 accept 한 client */
            TCP_client = TCP_Listen.AcceptTcpClient();
            Console.WriteLine("연결완료!");
            net_stream = TCP_client.GetStream();
            
            ///
        }

        public void endSERVER()
        {
            net_stream.Close();
            TCP_client.Close();
            TCP_Listen.Stop();
        }

        public NetworkStream Stream
        {
            get 
            {
                return net_stream; 
            }
        }
    }
    public static class Util
    {
        

        public static void encodingNpost( this Bitmap bmp ,SERVER server)
        {
            try
            {
                bmp.Save(@"c:\fuck.bmp");//확인용
            }
            catch{}
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 25L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            int Width = bmp.Width;
            int Height = bmp.Height;

            Bitmap b = new Bitmap(Width / 2, Height / 2);
            using (Graphics G = Graphics.FromImage((Image)b))
            {
                //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                G.InterpolationMode = InterpolationMode.Low;

                G.DrawImage(bmp, 0, 0, Width / 2, Height / 2);

                //bmp.Dispose();
            }

            try
            {
                b.Save(server.Stream, jgpEncoder, myEncoderParameters);
            }
            catch { }
            b.Dispose();
            server.endSERVER();
            server.SeverOn();
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
    }
}
