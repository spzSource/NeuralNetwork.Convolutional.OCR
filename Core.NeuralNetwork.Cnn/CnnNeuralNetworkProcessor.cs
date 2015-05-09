using System.Collections.Generic;
using System.Linq;
using System.Threading;

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.Output;

namespace DigitR.Core.NeuralNetwork.Cnn
{
    public class CnnNeuralNetworkProcessor : INeuralNetworkProcessor<INeuralNetwork<double[]>>
    {
        private readonly ITrainingAlgorithm<INeuralNetwork<double[]>, IInputTrainingPattern<double[]>> trainingAlgorithm;
        private readonly IProcessingAlgorithm<INeuralNetwork<double[]>, IInputPattern<double[]>> processingAlgorithm;

        private IMultiLayerNeuralNetwork<double> network; 

        public CnnNeuralNetworkProcessor(
            INeuralNetworkBuilder<double> networkBuilder,
            ITrainingAlgorithm<INeuralNetwork<double[]>, IInputTrainingPattern<double[]>> trainingAlgorithm,
            IProcessingAlgorithm<INeuralNetwork<double[]>, IInputPattern<double[]>> processingAlgorithm)
        {
            this.trainingAlgorithm = trainingAlgorithm;
            this.processingAlgorithm = processingAlgorithm;

            network = networkBuilder.Build() as IMultiLayerNeuralNetwork<double>;
        }
        
        public INeuralNetwork<double[]> NeuralNetwork
        {
            get
            {
                return network;
            }
        }

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
