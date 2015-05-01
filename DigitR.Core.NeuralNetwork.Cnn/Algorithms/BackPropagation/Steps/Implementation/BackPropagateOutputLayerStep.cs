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
            Log.Current.Info("Back propagation. BackPropagateOutputLayerStep begin.");

            ILayer<INeuron<double>> outputLayer = network.GetLayer(layer => layer.IsLast);

            Log.Current.Info("Layer-5. Start.");

            for (int neuronIndex = 0; neuronIndex < outputLayer.Neurons.Length; neuronIndex++)
            {
                INeuron<double> currentNeuron = outputLayer.Neurons[neuronIndex];

                double errorSignal = pattern.Label[neuronIndex] - currentNeuron.Output;
                BackPropagateNeuronInfo currentNeuronInfo = (BackPropagateNeuronInfo)currentNeuron.AditionalInfo;

                Contract.Assert(double.IsNaN(currentNeuronInfo.LocalGradient));

                currentNeuronInfo.LocalGradient =
                    errorSignal * activationAlgorithm
                        .CalculateFirstDerivative(currentNeuronInfo.LastInducesLocalAreaValue);

                Log.Current.Info("Layer-4. Neuron-{0} : errorSignal = {1}, localGradient = {2}",
                    neuronIndex,
                    errorSignal,
                    currentNeuronInfo.LocalGradient);

                weightCorrectionApplier.Apply(currentNeuron);
            }

            Log.Current.Info("Back propagation. BackPropagateOutputLayerStep end.");
        }
    }
}
