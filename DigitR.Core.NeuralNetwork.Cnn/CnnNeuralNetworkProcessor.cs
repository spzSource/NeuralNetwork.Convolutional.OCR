using System;

using DigitR.Core.InputProvider;
using DigitR.Core.Output;

namespace DigitR.Core.NeuralNetwork.Cnn
{
    public class CnnNeuralNetworkProcessor : INeuralNetworkProcessor
    {
        private readonly IInputProvider inputProvider;
        private readonly IInputProvider trainingInputProvider;
        private readonly IOutputProvider outputProvider;
        private readonly INeuralNetwork<byte[], byte> network;

        public CnnNeuralNetworkProcessor(
            IInputProvider inputProvider,
            IInputProvider trainingInputProvider,
            IOutputProvider outputProvider,
            INeuralNetwork<byte[], byte> network)
        {
            if (network == null)
            {
                throw new ArgumentNullException("network");
            }
            if (inputProvider == null)
            {
                throw new ArgumentNullException("inputProvider");
            }
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
            this.network = network;
        }

        public bool Process()
        {
            foreach (IInputPattern<byte[]> input in inputProvider.Retrieve())
            {
                outputProvider.Push(network.Process(input));
            }

            return true;
        }

        public bool Train()
        {
            foreach (IInputTrainingPattern<byte, byte[]> trainingPattern in trainingInputProvider.Retrieve())
            {
                network.ProcessTraining(trainingPattern);
            }

            return true;
        }
    }
}
