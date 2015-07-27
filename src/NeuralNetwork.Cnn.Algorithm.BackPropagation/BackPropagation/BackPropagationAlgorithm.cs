using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;

using DigitR.Common.Logging;
using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps;
using DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation;

namespace DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation
{
    public class BackPropagationAlgorithm
        : ITrainingAlgorithm<
            INeuralNetwork<double[]>,
            IInputTrainingPattern<double[]>>
    {
        private const double ErrorEps = 0.00001;
        private const double LearningSpeed = 0.05;
        private const double MinErrorEnergy = 10;

        private readonly IReadOnlyCollection<IPropagationStep> algorithmSteps;

        private double prevErrorEnergy;
        private double currentErrorEnergy;


        public BackPropagationAlgorithm(
            IActivationAlgorithm<double, double> activationAlgorithm)
        {
            prevErrorEnergy = 0;
            currentErrorEnergy = Double.MaxValue;

            algorithmSteps = new ReadOnlyCollection<IPropagationStep>(
                new List<IPropagationStep>
                {
                    new SetNetworkInputStep(),
                    new ForwardPropagateLayersStep(activationAlgorithm),
                    new BackPropagateOutputLayerStep(LearningSpeed, activationAlgorithm),
                    new BackPropagateHiddenLayersStep(LearningSpeed, activationAlgorithm),
                    new CommitWeightCorrectionsStep()
                });
        }

        public bool ProcessTraining(
            INeuralNetwork<double[]> network,
            IEnumerable<IInputTrainingPattern<double[]>> patterns,
            CancellationToken cancellationToken)
        {
            IMultiLayerNeuralNetwork<double> multiLayerNeuralNetwork = (IMultiLayerNeuralNetwork<double>)network;

            int patternsCount = 0;
            double energySum = 0;

            foreach (IInputTrainingPattern<double[]> pattern in patterns)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                Log.Current.Info("Pattern #{0} started to processing.", patternsCount + 1);

                ProcessPattern(multiLayerNeuralNetwork, pattern);

                double[] realOutputs = GetOutputSignals(multiLayerNeuralNetwork);

                LogOutputs(pattern, realOutputs);

                energySum += CalculateErrorEnergy(realOutputs, pattern.Label);

                Log.Current.Info("Energy sum = {0}", energySum);

                ++patternsCount;
            }

            currentErrorEnergy = energySum / patternsCount;

            bool trained = currentErrorEnergy <= MinErrorEnergy 
                && Math.Abs(prevErrorEnergy - currentErrorEnergy) <= ErrorEps;

            Log.Current.Info("Current error energy = {0}, previous error energy = {1}", 
                currentErrorEnergy, prevErrorEnergy);

            prevErrorEnergy = currentErrorEnergy;
            currentErrorEnergy = 0;

            return trained;
        }

        private void ProcessPattern(
            IMultiLayerNeuralNetwork<double> network,
            IInputTrainingPattern<double[]> pattern)
        {
            foreach (IPropagationStep propagationStep in algorithmSteps)
            {
                propagationStep.Process(network, pattern);
            }
        }

        private static double[] GetOutputSignals(IMultiLayerNeuralNetwork<double> multiLayerNeuralNetwork)
        {
            double[] realOutputs = multiLayerNeuralNetwork.Layers
                .First(layer => layer.IsLast).Neurons
                .Select(neuron => neuron.Output)
                .ToArray();
            return realOutputs;
        }

        private double CalculateErrorEnergy(double[] realOutput, double[] desiredOuput)
        {
            Contract.Assert(realOutput.Length == desiredOuput.Length, "The length of real and desired outputs does not match.");

            double errorsSum = realOutput
                .Select((realCurrentOutput, outputIndex) => Math.Pow(desiredOuput[outputIndex] - realCurrentOutput, 2))
                .Sum();

            double energy = errorsSum / 2;

            return energy;
        }

        private static void LogOutputs(IInputTrainingPattern<double[]> pattern, double[] realOutputs)
        {
            Log.Current.Info("Desired output = {0}, real output = {1}",
                pattern.Label.Aggregate(new StringBuilder(), (builder, element) => builder.AppendFormat(" | {0:N}", Math.Round(element, 3)))
                    .ToString(),
                realOutputs.Aggregate(new StringBuilder(), (builder, element) => builder.AppendFormat(" | {0:N}", Math.Round(element, 3)))
                    .ToString());
        }
    }
}