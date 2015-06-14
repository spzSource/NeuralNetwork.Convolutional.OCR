using Core.Common.Image.Converters;

using DigitR.Core.InputProvider;

namespace DigitR.NeuralNetwork.InputProvider.Processing.Ocr
{
    public class OcrInputPattern : IInputPattern<double[]>
    {
        private readonly double[] source;
        private readonly ThresholdConverter converter = new ThresholdConverter(30);

        public OcrInputPattern(byte[] byteSource)
        {
            source = converter.Convert(byteSource);
        }

        /// <summary>
        /// The source for specific input pattern.
        /// </summary>
        public double[] Source
        {
            get
            {
                return source;
            }
        }
    }
}
