using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork
{
    public class DefaultLayersConfigurator<TNeuronData, TWeightData> : ILayersConfigurator<TNeuronData, TWeightData>
    {
        private readonly IConnectionFactory<TNeuronData, TWeightData> connectionFactory;

        public DefaultLayersConfigurator(IConnectionFactory<TNeuronData, TWeightData> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public IReadOnlyCollection<ILayer<INeuron<TNeuronData>, IConnectionFactory<TNeuronData, TWeightData>>> Configure(
            IList<KeyValuePair<
                ILayer<INeuron<TNeuronData>, IConnectionFactory<TNeuronData, TWeightData>>,
                IConnectionScheme<INeuron<TNeuronData>, IConnectionFactory<TNeuronData, TWeightData>>>> layersData)
        {
            InitializeConnectionSchemes(layersData);

            var layers = new List<ILayer<INeuron<TNeuronData>, IConnectionFactory<TNeuronData, TWeightData>>>(layersData.Count)
            {
                layersData.First().Key
            };

            layers.AddRange(ConfigureIntermediateLayers(layersData));

            return new ReadOnlyCollection<ILayer<INeuron<TNeuronData>, IConnectionFactory<TNeuronData, TWeightData>>>(layers);
        }

        private void InitializeConnectionSchemes(
            IEnumerable<KeyValuePair<
                ILayer<INeuron<TNeuronData>, IConnectionFactory<TNeuronData, TWeightData>>, 
                IConnectionScheme<INeuron<TNeuronData>, IConnectionFactory<TNeuronData, TWeightData>>>> layersData)
        {
            foreach (var layerData in layersData)
            {
                layerData.Value?.SetConnectionFactory(connectionFactory);
            }
        }

        private static IEnumerable<ILayer<INeuron<TNeuronData>, IConnectionFactory<TNeuronData, TWeightData>>> ConfigureIntermediateLayers(
            IList<KeyValuePair<
                ILayer<INeuron<TNeuronData>, IConnectionFactory<TNeuronData, TWeightData>>,
                IConnectionScheme<INeuron<TNeuronData>, IConnectionFactory<TNeuronData, TWeightData>>>> layersData)
        {
            var result = new List<ILayer<INeuron<TNeuronData>, IConnectionFactory<TNeuronData, TWeightData>>>();

            for (int layerIndex = 1; layerIndex < layersData.Count; layerIndex++)
            {
                var currentLayerData = layersData[layerIndex];
                var prevLayerData = layersData[layerIndex - 1];

                prevLayerData.Key.ConnectToLayer(currentLayerData.Key, currentLayerData.Value);

                result.Add(currentLayerData.Key);
            }

            return result;
        }
    }
}