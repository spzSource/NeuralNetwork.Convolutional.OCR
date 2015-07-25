using System.Collections.Generic;

using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn
{
    public class CnnNeuralNetworkFactory : INeuralNetworkFactory<double>
    {
        public INeuralNetwork<double[]> Create(
            IReadOnlyCollection<ILayer<INeuron<double>>> layers)
        {
            return new CnnNeuralNetwork(layers);
        }
    }
}
