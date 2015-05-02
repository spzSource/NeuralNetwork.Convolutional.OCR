using System;

using DigitR.Core.NeuralNetwork.Algorithms;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms
{
    public class SigmoidActivationAlgorithm : IActivationAlgorithm<double, double>
    {
        private const double Alpha = 1;

        public double Calculate(double inducedArea)
        {
            return CalculateSigmoid(inducedArea);
        }

        public double CalculateFirstDerivative(double output)
        {
            //double activationValue = CalculateSigmoid(inducedArea);

            return Alpha * output * (1 - output);
        }

        private double CalculateSigmoid(double source)
        {
            return 1.0 / (1 + Math.Exp(-1 * Alpha * source));
        }
    }
}
