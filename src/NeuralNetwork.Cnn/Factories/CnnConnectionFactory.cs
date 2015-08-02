using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;
using DigitR.NeuralNetwork.Cnn.Primitives;

namespace DigitR.NeuralNetwork.Cnn.Factories
{
    public class CnnConnectionFactory : IConnectionFactory<double, double>
    {
        private readonly IWeightFactory<double> weightFactory;

        public CnnConnectionFactory(IWeightFactory<double> weightFactory)
        {
            this.weightFactory = weightFactory;
        }

        public IConnection<double, double> Create(
            INeuron<double> neuron)
        {
            return new CnnConnection(neuron, weightFactory.Create());
        }

        public IConnection<double, double> CreateWithWeight(
            INeuron<double> neuron, 
            IWeight<double> weight)
        {
            return new CnnConnection(neuron, weight);
        }
    }
}