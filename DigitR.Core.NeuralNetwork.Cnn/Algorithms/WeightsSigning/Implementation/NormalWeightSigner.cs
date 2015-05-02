using System;

using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.WeightsSigning.Implementation
{
    internal class NormalWeightSigner : IWeightSigner<double>
    {
        private readonly Random rand = new Random(DateTime.Now.Millisecond);

        public void Sign(IWeight<double> weight)
        {
            weight.Value = 0.5 - rand.NextDouble();
        }
    }
}
