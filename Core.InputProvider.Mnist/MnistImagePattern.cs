using Core.Common.Image.Converters;

using DigitR.Core.InputProvider;
using DigitR.Core.InputProvider.Common;

namespace DigitR.Core.NeuralNetwork.InputProvider.Training.Mnist
{
    public class MnistImagePattern : IInputTrainingPattern<double[]>
    {
        public const int MnistPatternSizeInBytes = MnistPatternSize * MnistPatternSize;

        private const int MnistPatternSize = 28;
        private const int ExtendedPatternSize = 29;

        private readonly byte label;
        private readonly byte[] source;
        private readonly double[] convertedLabel;
        private readonly double[] convertedSource;

        public MnistImagePattern(
            byte label,
            byte[] source,
            InputLabelConverter labelConverter,
            ThresholdConverter imageConverter)
        {
            this.label = label;
            this.source = SourceDataExtender.ExtendSource(source, MnistPatternSize, ExtendedPatternSize);

            convertedLabel = labelConverter.Convert(this.label);
            convertedSource = imageConverter.Convert(this.source);
        }

        public int Height
        {
            get
            {
                return ExtendedPatternSize;
            }
        }

        public int Weight
        {
            get
            {
                return ExtendedPatternSize;
            }
        }

        public double[] Label
        {
            get
            {
                return convertedLabel;
            }
        }

        public double[] Source
        {
            get
            {
                return convertedSource;
            }
        }

        public byte InnerLabel
        {
            get
            {
                return label;
            }
        }

        public byte[] InnerSource
        {
            get
            {
                return source;
            }
        }
    }
}
