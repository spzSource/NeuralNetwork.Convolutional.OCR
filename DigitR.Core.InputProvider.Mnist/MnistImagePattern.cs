using System;
using System.Diagnostics.Contracts;

using DigitR.Core.InputProvider;

namespace DigitR.Core.NeuralNetwork.InputProvider.Training.Mnist
{
    public class MnistImagePattern : IInputTrainingPattern<byte, byte[]>
    {
        private const int MnistPatternSize = 28;
        public  const int MnistPatternSizeInBytes = MnistPatternSize * MnistPatternSize;

        private readonly byte label;
        private readonly byte[] source;

        public MnistImagePattern(
            byte label,
            byte[] source)
        {
            Contract.Requires<ArgumentException>(source != null);
            Contract.Requires<ArgumentException>(source.Length > 0);

            this.label = label;
            this.source = source;
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

        public byte Label
        {
            get
            {
                return label;
            }
        }

        public byte[] Source
        {
            get
            {
                return source;
            }
        }
    }
}
