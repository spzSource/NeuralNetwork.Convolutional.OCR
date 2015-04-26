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
        private readonly WeightCorrectionApplier weightCorrectionApplier;

        public BackPropagateOutputLayerStep(
            IActivationAlgorithm<double, double> activationAlgorithm,
            WeightCorrectionApplier weightCorrectionApplier)
        {
            if (activationAlgorithm == null)
            {
                throw new ArgumentNullException("activationAlgorithm");
            }
            if (weightCorrectionApplier == null)
            {
                throw new ArgumentNullException("weightCorrectionApplier");
            }
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
                BackPropagateNeuronInfo currentNeuronInfo = (BackPropagateNeuronInfo)currentNeuron.AditionalInfo;

                Contract.Assert(double.IsNaN(currentNeuronInfo.LocalGradient));

                currentNeuronInfo.LocalGradient =
                    errorSignal * activationAlgorithm
                        .CalculateFirstDerivative(currentNeuronInfo.LastInducesLocalAreaValue);

                weightCorrectionApplier.Apply(currentNeuron);
            }
        }
    }
}
