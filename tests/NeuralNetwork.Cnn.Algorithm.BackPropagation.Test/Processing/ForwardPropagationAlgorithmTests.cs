using System;

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork;
using DigitR.NeuralNetwork.Cnn;
using DigitR.NeuralNetwork.Cnn.Algorithms.Activation;
using DigitR.NeuralNetwork.Cnn.Algorithms.Processing;
using DigitR.NeuralNetwork.Cnn.ConnectionSchemes.Implementation;
using DigitR.NeuralNetwork.Cnn.Primitives;

using NeuralNetwork.Cnn.Test.Processing.Mock;

using Xunit;

namespace NeuralNetwork.Cnn.Test.Processing
{
    public class ForwardPropagationAlgorithmTests
    {
        private readonly INeuralNetworkBuilder<double> networkBuilder =
           new NeuralNetworkBuilder<double>();

        [Fact]
        public void InputsSourceAndInputNeuronsNumbersMismatchTest()
        {
            IInputPattern<double[]> inputPattern = new InputPatternMock(new [] { 1.0, -1.0, 1.0 });

            INeuralNetwork<double[]> neuralNetwork = networkBuilder
                .AddInputLayer(new CnnLayer(0, inputPattern.Source.Length + 1, true, false))
                .AddLayer<FullyConnectedScheme>(new CnnLayer(1, 2, false, true))
                .Build<CnnNeuralNetworkFactory>();

            var processingAlgorithm = new ForwardPropagationAlgorithm(new HyperbolicTgActivationAlgorithm());

            Assert.NotNull(neuralNetwork);
            Assert.Throws<ArgumentException>(() => processingAlgorithm.Process(neuralNetwork, inputPattern));
        }
    }
}
