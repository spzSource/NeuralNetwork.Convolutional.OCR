using DigitR.Core.InputProvider;

namespace DigitR.Core.NeuralNetwork.InputProvider.Training.Mnist
{
    public class MnistImagePattern : IInputTrainingPattern<double[]>
    {
        private const int MnistPatternSize = 28;
        public const int MnistPatternSizeInBytes = MnistPatternSize * MnistPatternSize;

        private const int ExtendedPatternSize = 29;

        private readonly byte label;
        private readonly InputLabelConverter labelConverter;
        private readonly ThresholdConverter imageConverter;

        private readonly double[] convertedLabel;
        private readonly double[] convertedSource;

        private byte[] source;

        public MnistImagePattern(
            byte label,
            byte[] source,
            InputLabelConverter labelConverter,
            ThresholdConverter imageConverter)
        {
            this.label = label;
            this.source = ExtendSource(source);
            this.labelConverter = labelConverter;
            this.imageConverter = imageConverter;

            convertedLabel = labelConverter.Convert(this.label);
            convertedSource = imageConverter.Convert(this.source);
        }

        private byte[] ExtendSource(byte[] sourceForExtend)
        {
            source = new byte[ExtendedPatternSize * ExtendedPatternSize];

            for (int rowIndex = 0; rowIndex < ExtendedPatternSize; rowIndex++)
            {
                if (rowIndex == 0 || rowIndex == ExtendedPatternSize - 1)
                {
                    for (int columnIndex = 0; columnIndex < ExtendedPatternSize; columnIndex++)
                    {
                        source[ExtendedPatternSize * rowIndex + columnIndex] = 0;
                    }
                }
                else
                {
                    for (int columnIndex = 0; columnIndex < ExtendedPatternSize; columnIndex++)
                    {
                        if (columnIndex == 0 || columnIndex == ExtendedPatternSize - 1)
                        {
                            source[ExtendedPatternSize * rowIndex + columnIndex] = 0;
                        }
                        else
                        {
                            source[ExtendedPatternSize*rowIndex + columnIndex] =
                                sourceForExtend[MnistPatternSize*rowIndex + columnIndex];
                        }
                    }
                }
            }

            return source;
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
