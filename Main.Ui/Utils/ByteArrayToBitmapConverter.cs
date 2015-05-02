using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace DigitR.Ui.Utils
{
    public class ByteArrayToBitmapConverter
    {
        public Bitmap Convert(int height, int width, byte[] source)
        {
            Bitmap bitmap = new Bitmap(width, height);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    bitmap.SetPixel(x, y, PixelToColor(source[width * y + x]));
                }
            }

            return bitmap;
        }

        private Color PixelToColor(byte value)
        {
            byte brightness = (byte)(255 - value);
            return Color.FromArgb(255, brightness, brightness, brightness);
        }

        public BitmapSource ToWpfBitmap(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }
    }
}