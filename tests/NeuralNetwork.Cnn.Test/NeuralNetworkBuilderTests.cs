using System;
using System.Linq;

using DigitR.Core.NeuralNetwork;
using DigitR.NeuralNetwork.Cnn;
using DigitR.NeuralNetwork.Cnn.ConnectionSchemes.Implementation;
using DigitR.NeuralNetwork.Cnn.Primitives;

using Xunit;

namespace Core.NeuralNetwork.Test
{
    public class NeuralNetworkBuilderTests
    {
        private readonly INeuralNetworkBuilder<double> networkBuilder = 
            new NeuralNetworkBuilder<double>();
        
        [Fact]
        public void NeuralNetworkStructureTest()
        {
            var neuralNetwork = networkBuilder
                .AddInputLayer(new CnnLayer(0, 3, true, false))
                .AddLayer<FullyConnectedScheme>(new CnnLayer(1, 5, false, false))
                .AddLayer<FullyConnectedScheme>(new CnnLayer(2, 2, false, true))
                .Build<CnnNeuralNetworkFactory>() as IMultiLayerNeuralNetwork<double>;

            Assert.NotNull(neuralNetwork);

            Assert.Equal(neuralNetwork.Layers.Count, 3);
            Assert.Equal(neuralNetwork.Layers.ElementAt(0).Neurons.Count(), 9);
            Assert.Equal(neuralNetwork.Layers.ElementAt(1).Neurons.Count(), 25);
            Assert.Equal(neuralNetwork.Layers.ElementAt(2).Neurons.Count(), 4);

            Assert.Contains(neuralNetwork.Layers, layer => layer.IsFirst);
            Assert.Contains(neuralNetwork.Layers, layer => layer.IsLast);
        }

        [Fact]
        public void NeuralNetworkHiddenLayerPositionTest()
        {
            Exception exception = Assert.Throws<Exception>(
                () => networkBuilder.AddLayer<FullyConnectedScheme>(new CnnLayer(0, 3, false, false)));

            Assert.Equal("At first need to add a input layer.", exception.Message);
        }
    }
}
