using System;
using System.Diagnostics.Contracts;

using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Primitives
{
    public class CnnConnection : IConnection<CnnNeuron, CnnWeight>
    {
        private readonly CnnNeuron neuron;
        private readonly CnnWeight weight;

        public CnnConnection(
            CnnNeuron neuron,
            CnnWeight weight)
        {
            Contract.Requires<ArgumentException>(neuron != null);
            Contract.Requires<ArgumentException>(weight != null);

            this.neuron = neuron;
            this.weight = weight;
        }

        public CnnNeuron Neuron
        {
            get
            {
                return neuron;
            }
        }

        public CnnWeight Weight
        {
            get
            {
                return weight;
            }
        }
    }
}
