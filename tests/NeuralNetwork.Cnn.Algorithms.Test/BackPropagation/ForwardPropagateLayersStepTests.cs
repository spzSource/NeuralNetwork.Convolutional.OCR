using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.ConnectionSchemes;
using DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps;
using DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation;
using DigitR.NeuralNetwork.Cnn.Primitives;

using NSubstitute;

using Tests.NeuralNetwork.Mock.Mock;
using Tests.NeuralNetwork.Mock.Mock.Primitives;

using Xunit;

namespace Tests.NeuralNetwork.Cnn.Algorithms.Test.BackPropagation
{
    public class ForwardPropagateLayersStepTests
    {
        private readonly IPropagationStep testableStep;
        private readonly INeuralNetworkBuilder<double> networkBuilder;
        private readonly IActivationAlgorithm<double, double> activationAlgorithmMock;

        public ForwardPropagateLayersStepTests()
        {
            activationAlgorithmMock = Substitute.For<IActivationAlgorithm<double, double>>();

            // Since the activation algorithm is already covered by tests,
            // we may to mock this class.
            activationAlgorithmMock
                .Calculate(Arg.Any<double>())
                .Returns(1.0);

            networkBuilder = new NeuralNetworkBuilder<double>();
            testableStep = new ForwardPropagateLayersStep(activationAlgorithmMock);
        }

        [Fact]
        public void ForwardPropagateLayersProcessingStep()
        {
            IMultiLayerNeuralNetwork<double> network = networkBuilder
                .AddInputLayer(new LayerMock(0, 3, true, false))
                .AddLayer<FullyConnectedScheme<double>>(new CnnLayer(1, 3, false, true))
                .Build<NeuralNetworkFactoryMock>() as IMultiLayerNeuralNetwork<double>;
        }
    }
}
