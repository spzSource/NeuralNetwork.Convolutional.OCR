using System;
using System.Diagnostics.Contracts;
using System.Linq;

using DigitR.Common.Logging;
using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.Extensions;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation
{
    internal class BackPropagateHiddenLayersStep : IPropagationStep
    {
        private readonly IActivationAlgorithm<double, double> activationAlgorithm;
        private readonly WeightCorrectionApplier weightCorrectionApplier;

        public BackPropagateHiddenLayersStep(
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
            Log.Current.Info("Back propagation. BackPropagateHiddenLayersStep begin.");

            int layerIndex = 3;

            foreach (ILayer<INeuron<double>> layer in network.Layers.Where(layer => !layer.IsLast && !layer.IsFirst).Reverse())
            {
                Log.Current.Info("Layer-{0}. Start.", layerIndex);

                int neuronIndex = 1;

                foreach (INeuron<double> currentNeuron in layer.Neurons)
                {
                    BackPropagateNeuronInfo currentNeuronInfo = (BackPropagateNeuronInfo)currentNeuron.AditionalInfo;

                    double derivative = activationAlgorithm
                        .CalculateFirstDerivative(currentNeuronInfo.LastInducesLocalAreaValue);

                    double weightedGradientsSum = currentNeuron.Outputs
                        .Sum(connection => connection.Neuron.GetNeuronInfo<BackPropagateNeuronInfo>().LocalGradient * connection.Weight.Value);

                    double localGradient = derivative * weightedGradientsSum;

                    Contract.Assert(double.IsNaN(currentNeuronInfo.LocalGradient));
                    currentNeuronInfo.LocalGradient = localGradient;

                    Log.Current.Info("Layer-{0} Neuron-{1} : derivative = {2}, weightedGradientSum = {3}, localGradient = {4}",
                        layerIndex,
                        neuronIndex,
                        derivative,
                        weightedGradientsSum,
                        localGradient);

                    weightCorrectionApplier.Apply(currentNeuron);

                    ++neuronIndex;
                }

                --layerIndex;
            }

            Log.Current.Info("Back propagation. BackPropagateHiddenLayersStep end.");
        }
    }
}
