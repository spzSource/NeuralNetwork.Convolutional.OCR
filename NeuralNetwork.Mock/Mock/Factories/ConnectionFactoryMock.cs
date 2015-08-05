using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;

using NeuralNetwork.Mock.Mock.Primitives;

namespace NeuralNetwork.Mock.Mock.Factories
{
    public class ConnectionFactoryMock : IConnectionFactory<double, double>
    {
        private readonly IWeightFactory<double> weightFactory;

        public ConnectionFactoryMock(
            IWeightFactory<double> weightFactory)
        {
            this.weightFactory = weightFactory;
        }

        public IConnection<double, double> Create(INeuron<double> neuron)
        {
            return new ConnectionMock(neuron, weightFactory.Create());
        }

        public IConnection<double, double> CreateWithWeight(INeuron<double> neuron, IWeight<double> weight)
        {
            return new ConnectionMock(neuron, weight);
        }
    }
}