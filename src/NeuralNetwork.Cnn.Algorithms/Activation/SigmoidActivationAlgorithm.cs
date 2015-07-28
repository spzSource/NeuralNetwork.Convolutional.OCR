using System;

using DigitR.Core.NeuralNetwork.Algorithms;

namespace DigitR.NeuralNetwork.Cnn.Algorithms.Activation
{
    public class SigmoidActivationAlgorithm : IActivationAlgorithm<double, double>
    {
        private const double Alpha = 1.0;

        public double Calculate(double inducedArea)
        {
            return 1.0 / (1 + Math.Exp(-1 * Alpha * inducedArea));
        }

        public double CalculateFirstDerivative(double output)
        {
            return Alpha * output * (1 - output);
        }
    }
}
