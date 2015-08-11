using System;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.ConnectionSchemes;

using Tests.NeuralNetwork.Mock.Mock;
using Tests.NeuralNetwork.Mock.Mock.Primitives;

using Xunit;

namespace Tests.Core.NeuralNetwork.Test
{
    public class NeuralNetworkBuilderTests
    {
        private readonly INeuralNetworkBuilder<double> networkBuilder = 
            new NeuralNetworkBuilder<double>();
        
        [Fact]
        public void NeuralNetworkStructureTest()
        {
            var neuralNetwork = networkBuilder
                .AddInputLayer(new LayerMock(0, 3, true, false))
                .AddLayer<FullyConnectedScheme<double>>(new LayerMock(1, 5, false, false))
                .AddLayer<FullyConnectedScheme<double>>(new LayerMock(2, 2, false, true))
                .Build<NeuralNetworkFactoryMock>() as IMultiLayerNeuralNetwork<double>;

            Assert.NotNull(neuralNetwork);

            Assert.Equal(neuralNetwork.Layers.Count, 3);

            Assert.Collection(neuralNetwork.Layers, 
                layer => Assert.Equal(3, layer.Neurons.Length),
                layer => Assert.Equal(5, layer.Neurons.Length),
                layer => Assert.Equal(2, layer.Neurons.Length));

            Assert.Contains(neuralNetwork.Layers, layer => layer.IsFirst);
            Assert.Contains(neuralNetwork.Layers, layer => layer.IsLast);
        }

        [Fact]
        public void NeuralNetworkHiddenLayerPositionTest()
        {
            Exception exception = Assert.Throws<Exception>(
                () => networkBuilder.AddLayer<FullyConnectedScheme<double>>(new LayerMock(0, 3, false, false)));

            Assert.Equal("At first need to add a input layer.", exception.Message);
        }
    }
}
