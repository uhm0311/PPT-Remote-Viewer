using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PPTRemoteViewerServer.Utils.Statics
{
    public partial class BitmapManager
    {
        public static bool BitmapChanged(Bitmap source, Bitmap target, int thresholdFactor, int unitFactor)
        {
            if (source.Size.Equals(target.Size))
            {
                try
                {
                    string sourcePixel, targetPixel;

                    int width = source.Width;
                    int height = source.Height;
                    int threshold = width * height / thresholdFactor;

                    int unitWidth = width / unitFactor;
                    int unitHeight = height / unitFactor;

                    for (int i = 0; i < source.Width; i += unitWidth)
                    {
                        for (int j = 0; j < source.Height; j += unitHeight)
                        {
                            sourcePixel = source.GetPixel(i, j).ToString();
                            targetPixel = target.GetPixel(i, j).ToString();

                            if (!sourcePixel.Equals(targetPixel))
                            {
                                threshold--;

                                if (threshold <= 0)
                                    break;
                            }
                        }
                    }

                    return (threshold <= 0);
                }
                catch
                {
                    return false;
                }
            }
            else return true;
        }

        public static Bitmap CloneBitmap(Bitmap source)
        {
            return (Bitmap)source.Clone();
        }

        public static byte[] EncodeToJPEG(Bitmap source, double widthFactor, double heightFactor, double quality)
        {
            int sizedWidth = (int)(source.Width * widthFactor);
            int sizedHeight = (int)(source.Height * heightFactor);

            using (Bitmap sized = new Bitmap(sizedWidth, sizedHeight))
            {
                using (Graphics sizedGraphics = Graphics.FromImage(sized))
                {
                    sizedGraphics.InterpolationMode = InterpolationMode.Low;
                    sizedGraphics.DrawImage(source, 0, 0, sizedWidth, sizedHeight);
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    ImageCodecInfo jpeg = GetCodecInfo(ImageFormat.Jpeg);
                    EncoderParameters encoderParameters = new EncoderParameters(1);

                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, (long)(Math.Min(1, quality) * 100));
                    sized.Save(stream, jpeg, encoderParameters);
                    stream.Position = 0L;

                    return stream.ToArray();
                }
            }
        }

        private static ImageCodecInfo GetCodecInfo(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                    return codec;
            }
            return null;
        }
    }
}
