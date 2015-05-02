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
            #region

            Log.Current.Info("Back propagation. BackPropagateHiddenLayersStep begin.");

            #endregion

            foreach (ILayer<INeuron<double>> layer in network.Layers.Where(layer => !layer.IsLast && !layer.IsFirst).Reverse())
            {
                #region

                Log.Current.Info("Layer-{0}. Start.", layer.LayerId);

                #endregion

                foreach (INeuron<double> currentNeuron in layer.Neurons)
                {
                    BackPropagateNeuronInfo currentNeuronInfo = (BackPropagateNeuronInfo)currentNeuron.AdditionalInfo;

                    //double derivative = activationAlgorithm
                    //.CalculateFirstDerivative(currentNeuronInfo.LastInducesLocalAreaValue);

                    Contract.Assert(currentNeuron.Outputs.Count > 0, "Wrong number of inputs.");

                    double weightedGradientsSum = currentNeuron.Outputs
                        .Sum(connection => connection.Neuron.GetNeuronInfo<BackPropagateNeuronInfo>().LocalGradient * connection.Weight.Value);

                    double localGradient = currentNeuron.Output * (1 - currentNeuron.Output) * weightedGradientsSum;//derivative * weightedGradientsSum;

                    Contract.Assert(double.IsNaN(currentNeuronInfo.LocalGradient));
                    currentNeuronInfo.LocalGradient = localGradient;
                   
                    weightCorrectionApplier.Apply(currentNeuron);
                }
            }

            #region

            Log.Current.Info("Back propagation. BackPropagateHiddenLayersStep end.");

            #endregion
        }
    }
}
