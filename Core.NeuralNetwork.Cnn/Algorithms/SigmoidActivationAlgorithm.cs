using System;

using DigitR.Core.NeuralNetwork.Algorithms;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms
{
    public class SigmoidActivationAlgorithm : IActivationAlgorithm<double, double>
    {
        private readonly double alpha;

        public SigmoidActivationAlgorithm(double alpha = 1.0)
        {
            this.alpha = alpha;
        }

        public double Calculate(double inducedArea)
        {
            return 1.0 / (1 + Math.Exp(-1 * alpha * inducedArea));
        }

        public double CalculateFirstDerivative(double output)
        {
            return alpha * output * (1 - output);
        }
    }
}
