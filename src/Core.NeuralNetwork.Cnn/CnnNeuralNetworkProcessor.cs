using System.Collections.Generic;
using System.Linq;
using System.Threading;

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;
using DigitR.Core.Output;

namespace DigitR.Core.NeuralNetwork.Cnn
{
    public class CnnNeuralNetworkProcessor : INeuralNetworkProcessor<INeuralNetwork<double[]>>
    {
        private readonly IProcessingAlgorithm<INeuralNetwork<double[]>, IInputPattern<double[]>> processingAlgorithm;
        private readonly ITrainingAlgorithm<INeuralNetwork<double[]>, IInputTrainingPattern<double[]>> trainingAlgorithm;

        private IMultiLayerNeuralNetwork<double> network;

        public CnnNeuralNetworkProcessor(
            INeuralNetworkBuilder<double> networkBuilder,
            ITrainingAlgorithm<INeuralNetwork<double[]>, IInputTrainingPattern<double[]>> trainingAlgorithm,
            IProcessingAlgorithm<INeuralNetwork<double[]>, IInputPattern<double[]>> processingAlgorithm)
        {
            this.trainingAlgorithm = trainingAlgorithm;
            this.processingAlgorithm = processingAlgorithm;

            network = networkBuilder
                .AddLayer<FirstToSecondConnectionScheme>(new CnnLayer(0, 29 * 29, 6, 5, true, false))
                .AddLayer<SecondToThirdConnectionScheme>(new CnnLayer(1, 13 * 13 * 6, 50, 5, false, false))
                .AddLayer<FullyConnectedScheme>(new CnnLayer(2, 5 * 5 * 50, 0, 0, false, false))
                .AddLayer<FullyConnectedScheme>(new CnnLayer(3, 100, 0, 0, false, false))
                .AddLayer<FullyConnectedScheme>(new CnnLayer(4, 10, 0, 0, false, false))
                .Build<CnnNeuralNetworkFactory>() as IMultiLayerNeuralNetwork<double>;
        }

        public INeuralNetwork<double[]> NeuralNetwork => network;

        public void Initialize(INeuralNetwork<double[]> neuralNetwork)
        {
            network = neuralNetwork as IMultiLayerNeuralNetwork<double>;
        }

        public bool Process(IInputProvider inputProvider, IOutputProvider outputProvider)
        {
            foreach (IInputPattern<double[]> input in inputProvider.Retrieve())
            {
                double[] output = network.Process(input, processingAlgorithm);
                outputProvider.Push(output);
            }

            return true;
        }

        public bool Train(IInputProvider trainingInputProvider, CancellationToken cancellationToken)
        {
            bool trained;

            do
            {
                IEnumerable<IInputTrainingPattern<double[]>> patterns =
                    trainingInputProvider
                        .Retrieve()
                        .Cast<IInputTrainingPattern<double[]>>();

                trained = network.ProcessTraining(patterns, trainingAlgorithm, cancellationToken);

            } while (!trained && !cancellationToken.IsCancellationRequested);

            return trained;
        }
    }
}
