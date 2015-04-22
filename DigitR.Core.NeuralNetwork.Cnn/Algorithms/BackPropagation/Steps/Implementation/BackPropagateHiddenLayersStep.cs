using System;
using System.Diagnostics.Contracts;
using System.Linq;
using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.Extensions;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation
{
    internal class BackPropagateHiddenLayersStep : IPropagationStep
    {
        private readonly IActivationAlgorithm<double, double> activationAlgorithm;
        private readonly WeightCorrectionCalculator weightCorrectionCalculator;

        public BackPropagateHiddenLayersStep(
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
            foreach (ILayer<object> layer in network.Layers.Where(layer => !layer.IsLast))
            {
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
}
