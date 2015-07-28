using System.Drawing;

namespace DigitR.Core.Common.Image.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Bitmap"/> class type.
    /// </summary>
    public static class BitmapExtension
    {
        /// <summary>
        /// Executes resizing for the <see cref="Bitmap"/> source.
        /// </summary>
        /// <param name="source">The <see cref="Bitmap"/> source.</param>
        /// <param name="width">Desired width.</param>
        /// <param name="height">Desired height.</param>
        /// <returns>Resized <see cref="Bitmap"/> image.</returns>
        public static Bitmap Resize(this Bitmap source, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.DrawImage(source, 0, 0, width, height);
            }
            return result;
        }
    }
}
