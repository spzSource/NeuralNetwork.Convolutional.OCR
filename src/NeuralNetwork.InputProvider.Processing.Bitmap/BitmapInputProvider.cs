using System.Collections.Generic;
using System.Drawing;

using DigitR.Core.Common.Image.Extensions;
using DigitR.Core.NeuralNetwork.InputProvider;
using DigitR.Core.NeuralNetwork.InputProvider.Common;

namespace DigitR.NeuralNetwork.InputProvider.Processing.Bitmap
{
    public class BitmapInputProvider : IInputProvider
    {
        private const byte PatternSize = 28;
        private const byte ExtendedPatternSize = 29;
        private const byte Threshold = 100;

        private readonly System.Drawing.Bitmap source;

        public BitmapInputProvider(
            System.Drawing.Bitmap source)
        {
            this.source = source;
        }

        public object Current
        {
            get;
            private set;
        }

        public IEnumerable<object> Retrieve()
        {
            System.Drawing.Bitmap resizedBitmap = source.Resize(PatternSize, PatternSize);

            double[] binarizedSource = Binarize(resizedBitmap);

            Current = new BitmapInputPattern(
                SourceDataExtender.ExtendSource(
                    binarizedSource,
                    PatternSize,
                    ExtendedPatternSize));

            return new [] { Current };
        }

        private double[] Binarize(System.Drawing.Bitmap resizedBitmap)
        {
            double[] result = new double[resizedBitmap.Height * resizedBitmap.Width];

            for (int heightIndex = 0; heightIndex < resizedBitmap.Height; heightIndex++)
            {
                for (int widthIndex = 0; widthIndex < resizedBitmap.Width; widthIndex++)
                {
                    result[resizedBitmap.Height * heightIndex + widthIndex] =
                        ApplyThreshold(resizedBitmap.GetPixel(widthIndex, heightIndex));
                }
            }
            return result;
        }

        private double ApplyThreshold(Color color)
        {
            double result = 0.0;

            byte brightness = Brightness(color);
            if (brightness < Threshold)
            {
                result = 1.0;
            }

            return result;
        }

        public byte Brightness(Color color)
        {
            return (byte)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
        }
    }
}
