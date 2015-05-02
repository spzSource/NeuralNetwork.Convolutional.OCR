using System;
using System.Collections.Generic;
using System.Linq;

using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms
{
    public class ForwardOutputAlgorithm : IOutputAlgorithm<double, IConnection<double, double>>
    {
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
        public double Calculate(IReadOnlyCollection<IConnection<double, double>> inputConnections)
        {
            double inducedLocalArea = inputConnections
                .Sum(connection => connection.Weight.Value * connection.Neuron.Output);
            
            return activationAlgorithm.Calculate(inducedLocalArea);
        }
    }
}
