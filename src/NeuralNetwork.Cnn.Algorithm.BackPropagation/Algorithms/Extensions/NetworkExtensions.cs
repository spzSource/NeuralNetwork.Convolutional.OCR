using System;
using System.Linq;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.Extensions
{
    public static class NetworkExtensions
    {
        public static ILayer<INeuron<double>> GetLayer(
            this IMultiLayerNeuralNetwork<double> network,
            Func<ILayer<INeuron<double>>, bool> predicate)
        {
            ILayer<INeuron<double>> inputLayer = network.Layers.FirstOrDefault(predicate);
            if (inputLayer == null)
            {
                throw new Exception("Wrong layer");
            }
            return inputLayer;
        }
    }
}
