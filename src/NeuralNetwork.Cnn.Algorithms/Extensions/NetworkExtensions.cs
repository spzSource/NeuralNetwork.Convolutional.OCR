using System;
using System.Linq;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.NeuralNetwork.Cnn.Algorithms.Extensions
{
    public static class NetworkExtensions
    {
        public static ILayer<INeuron<double>, IConnectionFactory<double, double>> GetLayer(
            this IMultiLayerNeuralNetwork<double> network,
            Func<ILayer<INeuron<double>, IConnectionFactory<double, double>>, bool> predicate)
        {
            ILayer<INeuron<double>, IConnectionFactory<double, double>> inputLayer = network.Layers.FirstOrDefault(predicate);
            if (inputLayer == null)
            {
                throw new Exception("Wrong layer");
            }
            return inputLayer;
        }
    }
}
