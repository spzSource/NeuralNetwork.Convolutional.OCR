using System;

using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.NeuralNetwork.Cnn.Primitives
{
    [Serializable]
    public class CnnConnection : IConnection<double, double>
    {
        public CnnConnection(
            INeuron<double> neuron,
            IWeight<double> weight)
        {
            Neuron = neuron;
            Weight = weight;
        }

        public INeuron<double> Neuron { get; }

        public IWeight<double> Weight { get; }

        public object AdditionalInfo
        {
            get;
            set;
        }
    }
}
