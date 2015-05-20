using System;

using DigitR.Core.NeuralNetwork.Algorithms;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.Activation
{
    public class HyperbolicTgActivationAlgorithm : IActivationAlgorithm<double, double>
    {
        private const double A = 1.7159;
        private const double B = 2 / 3.0;
        private const double Eps = 0.7159;

        /// <summary>
        /// Calculates output value using the induced local area value.
        /// </summary>
        /// <returns>The output value.</returns>
        public double Calculate(double inducedArea)
        {
            double outputSignal = A * Math.Tanh(B * inducedArea);
            if (outputSignal <= -A)
            {
                outputSignal += Eps;
            }
            else if (outputSignal >= A)
            {
                outputSignal -= Eps;
            }
            return outputSignal;
        }

        /// <summary>
        /// Calculates the first derrivative of current activation function.
        /// </summary>
        /// <param name="inducedArea">The value of induced local area.</param>
        /// <returns>The value of function derrivative.</returns>
        public double CalculateFirstDerivative(double inducedArea)
        {
            return 1 / (Math.Pow(Math.Cosh(B * inducedArea), 2));
        }
    }
}
