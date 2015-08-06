using System.Linq;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.ConnectionSchemes;
using DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps;
using DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation;
using DigitR.NeuralNetwork.Cnn.Algorithms.Extensions;

using Tests.NeuralNetwork.Cnn.Algorithms.Test.BackPropagation.Mock;
using Tests.NeuralNetwork.Mock.Mock;
using Tests.NeuralNetwork.Mock.Mock.Primitives;

using Xunit;

namespace Tests.NeuralNetwork.Cnn.Algorithms.Test.BackPropagation
{
    public class SetNetworkInputStepTests
    {
        private readonly IPropagationStep testableStep = new SetNetworkInputStep();
        private readonly INeuralNetworkBuilder<double> networkBuilder =
           new NeuralNetworkBuilder<double>();

        [Fact]
        public void SetNetworkInputStepProcessingTest()
        {
            InputTrainingPatternMock patternMock =
                new InputTrainingPatternMock
                {
                    Source = new[] { 1.0, -1.0, 1.0 }
                };

            IMultiLayerNeuralNetwork<double> neuralNetwork = networkBuilder
                .AddInputLayer(new LayerMock(0, patternMock.Source.Length, true, false))
                .AddLayer<FullyConnectedScheme<double>>(new LayerMock(1, 2, false, true))
                .Build<NeuralNetworkFactoryMock>() as IMultiLayerNeuralNetwork<double>;

            testableStep.Process(neuralNetwork, patternMock);

            var inputLayer = neuralNetwork.GetLayer(layer => layer.IsFirst);
            
            Assert.Equal(patternMock.Source, inputLayer.Neurons.Select(n => n.Output));
        }
    }
}
