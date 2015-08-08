using System;
using System.Linq;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.ConnectionSchemes;
using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;

using DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation.Common;
using DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps;
using DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation;
using DigitR.NeuralNetwork.Cnn.Algorithms.Extensions;

using NSubstitute;

using Tests.NeuralNetwork.Mock.Mock;
using Tests.NeuralNetwork.Mock.Mock.Primitives;

using Xunit;

namespace Tests.NeuralNetwork.Cnn.Algorithms.Test.BackPropagation
{
    public class ForwardPropagateLayersStepTests
    {
        private readonly IPropagationStep testableStep;
        private readonly IMultiLayerNeuralNetwork<double> network;

        public ForwardPropagateLayersStepTests()
        {
            IActivationAlgorithm<double, double> activationAlgorithmMock =
                Substitute.For<IActivationAlgorithm<double, double>>();

            // Since the activation algorithm is already covered by tests,
            // we may to mock this class.
            activationAlgorithmMock.Calculate(Arg.Is(3.0)).Returns(-1.0);

            INeuralNetworkBuilder<double> networkBuilder = new NeuralNetworkBuilder<double>();

            network = networkBuilder
                .AddInputLayer(new LayerMock(0, 3, true, false))
                .AddLayer<FullyConnectedScheme<double>>(new LayerMock(1, 3, false, true))
                .Build<NeuralNetworkFactoryMock>() as IMultiLayerNeuralNetwork<double>;

            testableStep = new ForwardPropagateLayersStep(activationAlgorithmMock);
        }

        [Fact]
        public void NeuronOutputsTest()
        {
            FillOutputsForLayer(network.Layers.First(), 1.0);

            testableStep.Process(network, null);

            Assert.Collection(network.Layers,
                layer => Assert.All(layer.Neurons, neuron => Assert.Equal(1.0, neuron.Output)),
                layer => Assert.All(layer.Neurons, neuron => Assert.Equal(-1.0, neuron.Output)));
        }

        [Fact]
        public void SecondLayerAdditionalInfoExistenceTest()
        {
            FillOutputsForLayer(network.Layers.First(), -1.0);

            testableStep.Process(network, null);

            var secondLayerNeurons = network.Layers.ElementAt(1);

            Assert.All(secondLayerNeurons.Neurons,
                neuron =>
                {
                    Assert.Equal(Double.NaN, neuron.GetNeuronInfo<BackPropagateNeuronInfo>().LocalGradient);
                    Assert.Equal(-3.0, neuron.GetNeuronInfo<BackPropagateNeuronInfo>().LastInducesLocalAreaValue);
                });
        }

        private static void FillOutputsForLayer(
            ILayer<INeuron<double>, IConnectionFactory<double, double>> sourceLayer, double outputValue)
        {
            foreach (INeuron<double> neuron in sourceLayer.Neurons)
            {
                neuron.Output = outputValue;
            }
        }
    }
}
