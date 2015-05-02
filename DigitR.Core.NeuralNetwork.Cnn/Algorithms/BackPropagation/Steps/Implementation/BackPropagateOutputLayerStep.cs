using System;
using System.Diagnostics.Contracts;

using DigitR.Common.Logging;
using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.Extensions;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation
{
    internal class BackPropagateOutputLayerStep : IPropagationStep
    {
        private readonly IActivationAlgorithm<double, double> activationAlgorithm;
        private readonly WeightCorrectionApplier weightCorrectionApplier;

        public BackPropagateOutputLayerStep(
            IActivationAlgorithm<double, double> activationAlgorithm,
            WeightCorrectionApplier weightCorrectionApplier)
        {
            this.activationAlgorithm = activationAlgorithm;
            this.weightCorrectionApplier = weightCorrectionApplier;
        }

        public void Process(IMultiLayerNeuralNetwork<double> network, IInputTrainingPattern<double[], double[]> pattern)
        {
            ILayer<INeuron<double>> outputLayer = network.GetLayer(layer => layer.IsLast);

            for (int neuronIndex = 0; neuronIndex < outputLayer.Neurons.Length; neuronIndex++)
            {
                INeuron<double> currentNeuron = outputLayer.Neurons[neuronIndex];

                double errorSignal = pattern.Label[neuronIndex] - currentNeuron.Output;
                BackPropagateNeuronInfo currentNeuronInfo = (BackPropagateNeuronInfo)currentNeuron.AdditionalInfo;

                Contract.Assert(double.IsNaN(currentNeuronInfo.LocalGradient));

                //currentNeuronInfo.LocalGradient =
                //    errorSignal * activationAlgorithm
                //        .CalculateFirstDerivative(currentNeuronInfo.LastInducesLocalAreaValue);

                currentNeuronInfo.LocalGradient = -1 * errorSignal * currentNeuron.Output * (1 - currentNeuron.Output);

                
                weightCorrectionApplier.Apply(currentNeuron);
            }
        }
    }
}
