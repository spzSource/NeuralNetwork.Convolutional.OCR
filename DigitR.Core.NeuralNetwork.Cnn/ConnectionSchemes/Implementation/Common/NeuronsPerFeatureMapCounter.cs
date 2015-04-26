using System;

namespace DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Common
{
    internal class NeuronsPerFeatureMapCounter
    {
        public int Count(int sourceSize, int kernelSize, int step)
        {
            return (int)Math.Pow((sourceSize - kernelSize) / step + 1, 2);
        }
    }
}
