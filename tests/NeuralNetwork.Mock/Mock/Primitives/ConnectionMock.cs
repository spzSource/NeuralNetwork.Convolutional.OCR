using DigitR.Core.NeuralNetwork.Primitives;

namespace Tests.NeuralNetwork.Mock.Mock.Primitives
{
    public class ConnectionMock : IConnection<double, double>
    {
        public ConnectionMock(
            INeuron<double> neuron,
            IWeight<double> weight)
        {
            Neuron = neuron;
            Weight = weight;
        }

        public INeuron<double> Neuron
        {
            get;
        }

        public IWeight<double> Weight
        {
            get;
        }

        public object AdditionalInfo
        {
            get;
            set;
        }
    }
}