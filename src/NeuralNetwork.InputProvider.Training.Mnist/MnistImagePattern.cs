using DigitR.Core.Common.Image.Converters;
using DigitR.Core.NeuralNetwork.InputProvider.Common;

namespace DigitR.Core.NeuralNetwork.InputProvider.Training.Mnist
{
    public class MnistImagePattern : IInputTrainingPattern<double[]>
    {
        public const int MnistPatternSizeInBytes = MnistPatternSize * MnistPatternSize;

        private const int MnistPatternSize = 28;
        private const int ExtendedPatternSize = 29;

        public MnistImagePattern(
            byte label,
            byte[] source,
            InputLabelConverter labelConverter,
            ThresholdConverter imageConverter)
        {
            InnerLabel = label;
            InnerSource = SourceDataExtender.ExtendSource(source, MnistPatternSize, ExtendedPatternSize);

            Label = labelConverter.Convert(this.InnerLabel);
            Source = imageConverter.Convert(this.InnerSource);
        }

        public int Height => ExtendedPatternSize;

        public int Weight => ExtendedPatternSize;

        public double[] Label { get; }

        public double[] Source { get; }

        public byte InnerLabel { get; }

        public byte[] InnerSource { get; }
    }
}
