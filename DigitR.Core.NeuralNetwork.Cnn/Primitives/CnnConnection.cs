using System.Diagnostics;

using DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation;
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

        public object AdditionalInfo
        {
            get;
            set;
        }

        // NOTE : for debug only. Should be removed.
        private BackPropagateNeuronInfo Info
        {
            get
            {
                return (BackPropagateNeuronInfo) AdditionalInfo;
            }
        }
    }
}
