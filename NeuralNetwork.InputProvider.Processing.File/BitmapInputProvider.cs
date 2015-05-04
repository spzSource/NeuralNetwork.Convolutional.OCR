using System.Collections.Generic;
using System.Drawing;

using DigitR.Core.InputProvider;

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
            double[] retrieved = new double[source.Height * source.Width];

            for (int heightIndex = 0; heightIndex < source.Height; heightIndex++)
            {
                for (int widthIndex = 0; widthIndex < source.Width; widthIndex++)
                {
                    retrieved[source.Height * heightIndex + widthIndex] =
                        ApplayThreshold(
                            source.GetPixel(widthIndex, heightIndex));
                }
            }

            Current = new BitmapInputPattern(ExtendSource(retrieved));
            yield return Current;
        }

        private double[] ExtendSource(double[] sourceForExtend)
        {
            double[] extendSource = new double[ExtendedPatternSize * ExtendedPatternSize];

            for (int rowIndex = 0; rowIndex < ExtendedPatternSize; rowIndex++)
            {
                if (rowIndex == 0 || rowIndex == ExtendedPatternSize - 1)
                {
                    for (int columnIndex = 0; columnIndex < ExtendedPatternSize; columnIndex++)
                    {
                        extendSource[ExtendedPatternSize * rowIndex + columnIndex] = 0;
                    }
                }
                else
                {
                    for (int columnIndex = 0; columnIndex < ExtendedPatternSize; columnIndex++)
                    {
                        if (columnIndex == 0 || columnIndex == ExtendedPatternSize - 1)
                        {
                            extendSource[ExtendedPatternSize * rowIndex + columnIndex] = 0;
                        }
                        else
                        {
                            extendSource[ExtendedPatternSize * rowIndex + columnIndex] =
                                sourceForExtend[PatternSize * rowIndex + columnIndex];
                        }
                    }
                }
            }

            return extendSource;
        }

        private double ApplayThreshold(Color color)
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
