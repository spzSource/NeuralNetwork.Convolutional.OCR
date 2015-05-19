using System.Collections.Generic;
using System.Drawing;

using Core.Common.Image.Extensions;

using DigitR.Core.InputProvider;
using DigitR.Core.InputProvider.Common;

namespace DigitR.NeuralNetwork.InputProvider.Processing.File
{
    public class BitmapInputProvider : IInputProvider
    {
        private const byte PatternSize = 28;
        private const byte ExtendedPatternSize = 29;
        private const byte Threshold = 100;

        private readonly Bitmap source;

        public BitmapInputProvider(
            Bitmap source)
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
            Bitmap resizedBitmap = source.Resize(PatternSize, PatternSize);

            double[] binarizedSource = Binarize(resizedBitmap);

            Current = new BitmapInputPattern(
                SourceDataExtender.ExtendSource(
                    binarizedSource,
                    PatternSize,
                    ExtendedPatternSize));

            return new [] { Current };
        }

        private double[] Binarize(Bitmap resizedBitmap)
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
            double result = 0;

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
