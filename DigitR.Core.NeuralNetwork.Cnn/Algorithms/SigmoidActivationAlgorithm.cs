using System;

using DigitR.Core.NeuralNetwork.Algorithms;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms
{
    public class SigmoidActivationAlgorithm : IActivationAlgorithm<double, double>
    {
        /// <summary>
        /// Calculates a sigmoid function.
        /// </summary>
        public double Calculate(double inducedArea)
        {
            return 1.0 / (1 + Math.Exp(-1 * inducedArea));
        }
    }
}
