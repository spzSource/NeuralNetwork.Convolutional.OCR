using System;
using System.Collections.Generic;
using System.Linq;
using DigitR.Core.InputProvider;
using DigitR.Core.Output;

namespace DigitR.Core.NeuralNetwork.Cnn
{
    public class CnnNeuralNetworkProcessor : INeuralNetworkProcessor
    {
        private readonly IInputProvider inputProvider;
        private readonly IInputProvider trainingInputProvider;
        private readonly IOutputProvider outputProvider;
        private readonly INeuralNetworkBuilder<double[], double[]> networkBuilder;
        private readonly IMultiLayerNeuralNetwork<double> network; 

        public CnnNeuralNetworkProcessor(
            //IInputProvider inputProvider,
            IInputProvider trainingInputProvider,
            IOutputProvider outputProvider,
            INeuralNetworkBuilder<double[], double[]> networkBuilder)
        {
            if (networkBuilder == null)
            {
                throw new ArgumentNullException("networkBuilder");
            }
            //if (inputProvider == null)
            //{
            //    throw new ArgumentNullException("inputProvider");
            //}
            if (outputProvider == null)
            {
                throw new ArgumentNullException("outputProvider");
            }
            if (trainingInputProvider == null)
            {
                throw new ArgumentNullException("trainingInputProvider");
            }

            this.inputProvider = inputProvider;
            this.trainingInputProvider = trainingInputProvider;
            this.outputProvider = outputProvider;
            this.networkBuilder = networkBuilder;

            network = networkBuilder.Build() as IMultiLayerNeuralNetwork<double>;
        }

        public bool Process()
        {
            foreach (IInputPattern<double[]> input in inputProvider.Retrieve())
            {
                outputProvider.Push(network.Process(input));
            }

            return true;
        }

        public bool Train()
        {
            IEnumerable<IInputTrainingPattern<double[], double[]>> patterns = trainingInputProvider
                .Retrieve()
                .Cast<IInputTrainingPattern<double[], double[]>>();
                
            network.ProcessTraining(patterns);

            return true;
        }
    }
}
