using System;
using DigitR.Core.NeuralNetwork.Primitives;

namespace NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.WeightsSigning.Implementation
{
    public class NormalWeightSigner : IWeightSigner<double>
    {
        private readonly Random rand = new Random(DateTime.Now.Millisecond);

        public void Sign(IWeight<double> weight)
        {
            weight.Value = 0.5 - rand.NextDouble();
        }
    }
}
