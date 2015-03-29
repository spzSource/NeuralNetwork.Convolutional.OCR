using System;
using System.Collections.Generic;
using System.Linq;

using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms
{
    public class ForwardOutputAlgorithm : IOutputAlgorithm<double, CnnConnection>
    {
        private const int A = 1;

        private readonly IActivationAlgorithm<double, double> activationAlgorithm;

        public ForwardOutputAlgorithm(
            IActivationAlgorithm<double, double> activationAlgorithm)
        {
            if (activationAlgorithm == null)
            {
                throw new ArgumentNullException("activationAlgorithm");
            }

            this.activationAlgorithm = activationAlgorithm;
        }

        /// <summary>
        /// Calculates the derivative of output value for specific neuron.
        /// </summary>
        public double Calculate(IReadOnlyCollection<CnnConnection> inputConnections)
        {
            double inducedLocalArea = inputConnections
                .Sum(connection => connection.Weight.Value * connection.Neuron.Output);
            
            double activationValue = activationAlgorithm.Calculate(inducedLocalArea);
            
            return A * activationValue * (1 - activationValue);
        }
    }
}
