using System.Linq;

using NeuralNetwork.Cnn.Test.Fixtures;

using Xunit;

namespace NeuralNetwork.Cnn.Test
{
    public class CnnNeuralNetworkTests : IClassFixture<CnnNeuralNetworkFixture>
    {
        private readonly CnnNeuralNetworkFixture neuralNetworkFixture;

        public CnnNeuralNetworkTests(CnnNeuralNetworkFixture neuralNetworkFixture)
        {
            this.neuralNetworkFixture = neuralNetworkFixture;
        }

        [Fact]
        public void CnnNeuralNetworkBuilderTest()
        {
            var layers = neuralNetworkFixture.MultiLayerNeuralNetwork.Layers;

            Assert.Equal(layers.Count, 3);
            Assert.Equal(layers.ElementAt(0).Neurons.Count(), 9);
            Assert.Equal(layers.ElementAt(1).Neurons.Count(), 25);
            Assert.Equal(layers.ElementAt(2).Neurons.Count(), 4);

            Assert.Contains(layers, layer => layer.IsFirst);
            Assert.Contains(layers, layer => layer.IsLast);
        }
    }
}
