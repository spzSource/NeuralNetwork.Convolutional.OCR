using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;

namespace Tests.NeuralNetwork.Mock.Mock.Primitives
{
    public class LayerMock : ILayer<INeuron<double>, IConnectionFactory<double, double>>
    {
        public LayerMock(
            int layerId,
            int sourceSize,
            bool isFirst,
            bool isLast)
        {
            LayerId = layerId;
            IsFirst = isFirst;
            IsLast = isLast;

            Neurons = new INeuron<double>[sourceSize];

            for (int neuronIndex = 0; neuronIndex < Neurons.Length; neuronIndex++)
            {
                Neurons[neuronIndex] = new NeuronMock(neuronIndex, isBias: false);
            }
        }

        public int LayerId
        {
            get;
        }

        public bool IsFirst
        {
            get;
        }

        public bool IsLast
        {
            get;
        }

        public INeuron<double>[] Neurons
        {
            get;
        }

        public void ConnectToLayer(
            ILayer<INeuron<double>, IConnectionFactory<double, double>> layer, 
            IConnectionScheme<INeuron<double>, IConnectionFactory<double, double>> connectionScheme)
        {
            connectionScheme.Apply(this, layer);
        }
    }
}