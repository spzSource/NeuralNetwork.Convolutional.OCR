using System;
using System.Linq;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.Extensions
{
    public static class NetworkExtensions
    {
        public static ILayer<INeuron<double>> GetLayer(
            this IMultiLayerNeuralNetwork<double[], double[]> network,
            Func<ILayer<object>, bool> predicate)
        {
            ILayer<INeuron<double>> inputLayer = network.Layers.FirstOrDefault(predicate) as ILayer<INeuron<double>>;
            if (inputLayer == null)
            {
                throw new Exception("Wrong layer");
            }
            return inputLayer;
        }
    }
}
