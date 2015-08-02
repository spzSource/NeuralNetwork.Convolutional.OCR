using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
            new CnnConnectionFactory(
                new CnnWeightFactory(
                    new NormalWeightSigner()));

        public INeuralNetwork<double[]> Create(
            IList<LayerConfiguration> layersData)
        {
            if (layersData.Count == 0)
            {
                throw new ArgumentException(nameof(layersData));
            }

            InitializeConnectionSchemes(layersData);

            List<LayerType> layers = new List<LayerType>(layersData.Count) { layersData.First().Key };

            layers.AddRange(ConfigureIntermediateLayers(layersData));

            return new CnnNeuralNetwork(new ReadOnlyCollection<LayerType>(layers));
        }

        private void InitializeConnectionSchemes(IEnumerable<LayerConfiguration> layersData)
        {
            foreach (var layerData in layersData)
            {
                layerData.Value?.SetConnectionFactory(connectionFactory);
            }
        }

        private static IEnumerable<LayerType> ConfigureIntermediateLayers(
            IList<LayerConfiguration> layersData)
        {
            IList<LayerType> result = new List<LayerType>();

            for (int layerIndex = 1; layerIndex < layersData.Count; layerIndex++)
            {
                LayerConfiguration currentLayerData = layersData[layerIndex];
                LayerConfiguration prevLayerData = layersData[layerIndex - 1];

                prevLayerData.Key.ConnectToLayer(currentLayerData.Key, currentLayerData.Value);

                result.Add(currentLayerData.Key);
            }

            return result;
        }
    }
}
