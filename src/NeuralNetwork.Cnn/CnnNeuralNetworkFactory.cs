using System;
using System.Collections.Generic;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Factories;
using DigitR.NeuralNetwork.Cnn.Algorithms.WeightsSigning;
using DigitR.NeuralNetwork.Cnn.Factories;

using LayerConfiguration = System.Collections.Generic.KeyValuePair<
    DigitR.Core.NeuralNetwork.Primitives.ILayer<
        DigitR.Core.NeuralNetwork.Primitives.INeuron<double>, 
        DigitR.Core.NeuralNetwork.Factories.IConnectionFactory<double, double>>, 
    DigitR.Core.NeuralNetwork.IConnectionScheme<
        DigitR.Core.NeuralNetwork.Primitives.INeuron<double>, 
        DigitR.Core.NeuralNetwork.Factories.IConnectionFactory<double, double>>>;

using LayerType = DigitR.Core.NeuralNetwork.Primitives.ILayer<
    DigitR.Core.NeuralNetwork.Primitives.INeuron<double>, 
    DigitR.Core.NeuralNetwork.Factories.IConnectionFactory<double, double>>;

namespace DigitR.NeuralNetwork.Cnn
{
    public class CnnNeuralNetworkFactory : INeuralNetworkFactory<double>
    {
        private readonly IConnectionFactory<double, double> connectionFactory =
            new CnnConnectionFactory(new CnnWeightFactory(new NormalWeightSigner()));

        private ILayersConfigurator<double, double> layersConfigurator;

        public ILayersConfigurator<double, double> LayersConfigurator => null;

        public INeuralNetwork<double> Create(
            IList<LayerConfiguration> layersData)
        {
            if (layersData.Count == 0)
            {
                throw new ArgumentException(nameof(layersData));
            }

            if (layersConfigurator == null)
            {
                layersConfigurator = new DefaultLayersConfigurator<double, double>(connectionFactory);
            }

            IReadOnlyCollection<LayerType> layers = layersConfigurator.Configure(layersData);

            return new MultiLayerNeuralNetwork<double>(layers);
        }
    }
}
