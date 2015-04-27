using DigitR.Core.InputProvider;

namespace DigitR.Core.NeuralNetwork.InputProvider.Training.Mnist
{
    public class MnistImagePattern : IInputTrainingPattern<double[], double[]>
    {
        private const int MnistPatternSize = 28;
        public  const int MnistPatternSizeInBytes = MnistPatternSize * MnistPatternSize;

        private readonly byte label;
        private readonly byte[] source;
        private readonly InputLabelConverter labelConverter;
        private readonly ThresholdConverter imageConverter;

        private readonly double[] convertedLabel;
        private readonly double[] convertedSource;

        public MnistImagePattern(
            byte label,
            byte[] source,
            InputLabelConverter labelConverter,
            ThresholdConverter imageConverter)
        {
            this.label = label;
            this.source = source;
            this.labelConverter = labelConverter;
            this.imageConverter = imageConverter;

            convertedLabel = labelConverter.Convert(label);
            convertedSource = imageConverter.Convert(source);
        }

        public int Height
        {
            get
            {
                return MnistPatternSize;
            }
        }

        public int Weight
        {
            get
            {
                return MnistPatternSize;
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
    }
}
