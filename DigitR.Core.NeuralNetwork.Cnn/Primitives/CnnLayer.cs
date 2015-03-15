using System;
using System.Diagnostics.Contracts;

using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Primitives
{
    public class CnnLayer : ILayer<CnnNeuron, CnnWeight>
    {
        private readonly CnnNeuron[] neurons;
        private readonly CnnWeight[] weights;

        public CnnLayer(int neuronsCount)
        {
            Contract.Requires<ArgumentException>(neuronsCount > 0);

            neurons = new CnnNeuron[neuronsCount];
            weights = new CnnWeight[neuronsCount];
        }

        public CnnNeuron[] Neurons
        {
            get
            {
                return neurons;
            }
        }

        public CnnWeight[] Weights
        {
            get
            {
                return weights;
            }
        }

        public void Calculate()
        {
            foreach (CnnNeuron neuron in Neurons)
            {
                neuron.Output = neuron.Calculate();
            }
        }

        /// <summary>
        /// Connects this layer to layer passed as parameter.
        /// </summary>
        public void ConnectToLayer(ILayer<CnnNeuron, CnnWeight> layer)
        {
            throw new NotImplementedException();
        }
    }
}
