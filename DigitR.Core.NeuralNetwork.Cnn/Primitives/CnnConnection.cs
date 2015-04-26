using System;
using System.Diagnostics.Contracts;

using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Primitives
{
    public class CnnConnection : IConnection<double, double>
    {
        private readonly INeuron<double> neuron;
        private readonly IWeight<double> weight;

        public CnnConnection(
            INeuron<double> neuron,
            IWeight<double> weight)
        {
            //Contract.Requires<ArgumentException>(neuron != null);
            //Contract.Requires<ArgumentException>(weight != null);

            this.neuron = neuron;
            this.weight = weight;
        }

        public INeuron<double> Neuron
        {
            get
            {
                return neuron;
            }
        }

        public IWeight<double> Weight
        {
            get
            {
                return weight;
            }
        }

        public object AditionalInfo
        {
            get;
            set;
        }
    }
}
