using System;
using System.Diagnostics.Contracts;
using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.Extensions;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation
{
    internal class BackPropagateOutputLayerStep : IPropagationStep
    {
        private readonly IActivationAlgorithm<double, double> activationAlgorithm;
        private readonly WeightCorrectionCalculator weightCorrectionCalculator;

        public BackPropagateOutputLayerStep(
            IActivationAlgorithm<double, double> activationAlgorithm,
            WeightCorrectionCalculator weightCorrectionCalculator)
        {
            if (activationAlgorithm == null)
            {
                throw new ArgumentNullException("activationAlgorithm");
            }
            if (weightCorrectionCalculator == null)
            {
                throw new ArgumentNullException("weightCorrectionCalculator");
            }
            this.activationAlgorithm = activationAlgorithm;
            this.weightCorrectionCalculator = weightCorrectionCalculator;
        }

        public void Process(IMultiLayerNeuralNetwork<double[], double[]> network, IInputTrainingPattern<double[], double[]> pattern)
        {
            ILayer<INeuron<double>> outputLayer = network.GetLayer(layer => layer.IsLast);

            for (int neuronIndex = 0; neuronIndex < outputLayer.Neurons.Length; neuronIndex++)
            {
                INeuron<double> currentNeuron = outputLayer.Neurons[neuronIndex];

                double errorSignal = pattern.Label[neuronIndex] - currentNeuron.Output;
                BackPropagateNeuronInfo currentNeuronInfo = (BackPropagateNeuronInfo)currentNeuron.AditionalInfo;

                Contract.Assert(double.IsNaN(currentNeuronInfo.LocalGradient));

                currentNeuronInfo.LocalGradient =
                    errorSignal * activationAlgorithm
                        .CalculateFirstDerivative(currentNeuronInfo.LastInducesLocalAreaValue);

                foreach (IConnection<double, double> inputConnections in currentNeuron.Inputs)
                {
                    inputConnections.AditionalInfo = new BackPropagateConnectionInfo
                    {
                        WeightCorrection = weightCorrectionCalculator
                            .Calculate(currentNeuronInfo.LocalGradient, inputConnections.Neuron.Output)
                    };
                }
            }
        }
    }
}
