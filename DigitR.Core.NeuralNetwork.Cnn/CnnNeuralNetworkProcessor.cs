using System;
using System.Collections.Generic;
using System.Linq;

using DigitR.Core.InputProvider;
using DigitR.Core.Output;

namespace DigitR.Core.NeuralNetwork.Cnn
{
    public class CnnNeuralNetworkProcessor : INeuralNetworkProcessor
    {
        private readonly IMultiLayerNeuralNetwork<double> network; 

        public CnnNeuralNetworkProcessor(
            INeuralNetworkBuilder<double[], double[]> networkBuilder)
        {
            if (networkBuilder == null)
            {
                throw new ArgumentNullException("networkBuilder");
            }
            network = networkBuilder.Build() as IMultiLayerNeuralNetwork<double>;
        }

        public bool Process(IInputProvider inputProvider, IOutputProvider outputProvider)
        {
            foreach (IInputPattern<double[]> input in inputProvider.Retrieve())
            {
                outputProvider.Push(network.Process(input));
            }

            return true;
        }

        public bool Train(IInputProvider trainingInputProvider)
        {
            IEnumerable<IInputTrainingPattern<double[], double[]>> patterns = trainingInputProvider
                .Retrieve()
                .Cast<IInputTrainingPattern<double[], double[]>>();
                
            network.ProcessTraining(patterns);

            return true;
        }
    }
}
