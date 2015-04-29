using System;

using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.WeightsSigning.Implementation
{
    internal class NormalWeightSigner : IWeightSigner<double>
    {
        public void Sign(IWeight<double> weight)
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            weight.Value = 0.5 - rand.NextDouble();
        }
    }
}
