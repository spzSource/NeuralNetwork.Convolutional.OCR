using System.Collections.Generic;
using System.Linq;
using System.Threading;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.ConnectionSchemes;
using DigitR.Core.NeuralNetwork.InputProvider;
using DigitR.Core.NeuralNetwork.OutputProvider;

using DigitR.NeuralNetwork.Cnn.ConnectionSchemes;
using DigitR.NeuralNetwork.Cnn.Primitives;

namespace DigitR.NeuralNetwork.Cnn
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
                .AddInputLayer(new CnnLayer(0, 29 * 29, true, false))
                .AddLayer<CnnFirstToSecondConnectionScheme>(new CnnLayer(1, 13 * 13 * 6, false, false))
                .AddLayer<CnnSecondToThirdConnectionScheme>(new CnnLayer(2, 5 * 5 * 50, false, false))
                .AddLayer<FullyConnectedScheme<double>>(new CnnLayer(3, 100, false, false))
                .AddLayer<FullyConnectedScheme<double>>(new CnnLayer(4, 10, false, true))
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
