﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.ConnectionSchemes;
using DigitR.Core.NeuralNetwork.InputProvider;

using DigitR.NeuralNetwork.Cnn.Algorithms.Activation;
using DigitR.NeuralNetwork.Cnn.Algorithms.Processing;

using Tests.NeuralNetwork.Cnn.Algorithms.Test.Processing.Mock;
using Tests.NeuralNetwork.Mock.Mock;
using Tests.NeuralNetwork.Mock.Mock.Primitives;

using Xunit;

namespace Tests.NeuralNetwork.Cnn.Algorithms.Test.Processing
{
    public class ForwardPropagationAlgorithmTests
    {
        private readonly IInputPattern<double[]> inputPatternMock =
            new InputPatternMock(new[] { 1.0, -1.0, 1.0 });

        private readonly INeuralNetworkBuilder<double> networkBuilder =
           new NeuralNetworkBuilder<double>();

        private readonly ForwardPropagationAlgorithm processingAlgorithm = 
            new ForwardPropagationAlgorithm(new HyperbolicTgActivationAlgorithm());

        [Fact]
        public void InputsSourceAndInputNeuronsNumbersMismatchTest()
        {
            INeuralNetwork<double> neuralNetwork = networkBuilder
                .AddInputLayer(new LayerMock(0, inputPatternMock.Source.Length + 1, true, false))
                .AddLayer<FullyConnectedScheme<double>>(new LayerMock(1, 2, false, true))
                .Build<NeuralNetworkFactoryMock>();

            Assert.NotNull(neuralNetwork);
            Assert.Throws<ArgumentException>(() => processingAlgorithm.Process(neuralNetwork, inputPatternMock));
        }

        [Fact]
        public void MissingOutputLayerTest()
        {
            INeuralNetwork<double> neuralNetwork = networkBuilder
                .AddInputLayer(new LayerMock(0, 3, true, false))
                .Build<NeuralNetworkFactoryMock>();

            Assert.NotNull(neuralNetwork);
            Exception exception = Assert.Throws<Exception>(
                () => processingAlgorithm.Process(neuralNetwork, inputPatternMock));

            Assert.Equal("Wrong layer", exception.Message);
        }

        [Fact]
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public void ForwardPropagationQualityTest()
        {
            IMultiLayerNeuralNetwork<double> neuralNetwork = networkBuilder
                .AddInputLayer(new LayerMock(0, 3, true, false))
                .AddLayer<FullyConnectedScheme<double>>(new LayerMock(1, 5, false, false))
                .AddLayer<FullyConnectedScheme<double>>(new LayerMock(2, 2, false, true))
                .Build<NeuralNetworkFactoryMock>() as IMultiLayerNeuralNetwork<double>;

            processingAlgorithm.Process(neuralNetwork, inputPatternMock);

            Assert.NotNull(neuralNetwork);
            Assert.Equal(inputPatternMock.Source, neuralNetwork.Layers.First().Neurons.Select(n => n.Output));
            Assert.Equal(neuralNetwork.Layers.ElementAt(1).Neurons.Length, 
                neuralNetwork.Layers.ElementAt(1).Neurons.Count(n => n.Output != 0.0));
            Assert.Equal(neuralNetwork.Layers.ElementAt(2).Neurons.Length, 
                neuralNetwork.Layers.ElementAt(2).Neurons.Count(n => n.Output != 0.0));
        }
    }
}
