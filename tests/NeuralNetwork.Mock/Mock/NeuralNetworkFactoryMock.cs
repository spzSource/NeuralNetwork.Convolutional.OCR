using System;
using System.Collections.Generic;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;

using Tests.NeuralNetwork.Mock.Mock.Factories;

using LayerType = DigitR.Core.NeuralNetwork.Primitives.ILayer<
    DigitR.Core.NeuralNetwork.Primitives.INeuron<double>,
    DigitR.Core.NeuralNetwork.Factories.IConnectionFactory<double, double>>;

namespace Tests.NeuralNetwork.Mock.Mock
{
    public class NeuralNetworkFactoryMock : INeuralNetworkFactory<double>
    {
        private readonly IConnectionFactory<double, double> connectionFactory = new ConnectionFactoryMock(
            new WeightFactoryMock(new WeightSignerMock(1.0)));

        private DefaultLayersConfigurator<double, double> layersConfigurator;

        public ILayersConfigurator<double, double> LayersConfigurator => null;

        public INeuralNetwork<double> Create(
            IList<KeyValuePair<
                ILayer<INeuron<double>, IConnectionFactory<double, double>>, 
                IConnectionScheme<INeuron<double>, IConnectionFactory<double, double>>>> layersData)
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