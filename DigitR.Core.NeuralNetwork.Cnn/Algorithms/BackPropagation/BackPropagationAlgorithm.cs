using System.Collections.Generic;
using System.Collections.ObjectModel;

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation
{
    public class BackPropagationAlgorithm
        : ITrainingAlgorithm<
            IMultiLayerNeuralNetwork<double[], double[]>,
            IInputTrainingPattern<double[], double[]>>
    {
        private const double TrainingSpeed = 0.5;

        private readonly IReadOnlyCollection<IPropagationStep> algorithmSteps;

        public BackPropagationAlgorithm(
            IActivationAlgorithm<double, double> activationAlgorithm)
        {
            WeightCorrectionApplier weightCorrectionsApplier = new WeightCorrectionApplier(TrainingSpeed);

            algorithmSteps = new ReadOnlyCollection<IPropagationStep>(new List<IPropagationStep>
            {
                new SetNetworkInputStep(),
                new ForwardPropagateLayersStep(activationAlgorithm),
                new BackPropagateOutputLayerStep(activationAlgorithm, weightCorrectionsApplier),
                new BackPropagateHiddenLayersStep(activationAlgorithm, weightCorrectionsApplier),
                new CommitWeightCorrectionsStep()
            });
        }

        public void ProcessTraining(
            IMultiLayerNeuralNetwork<double[], double[]> network,
            IEnumerable<IInputTrainingPattern<double[], double[]>> patterns)
        {
            foreach (IInputTrainingPattern<double[], double[]> pattern in patterns)
            {
                ProcessPattern(network, pattern);
            }
        }

        private void ProcessPattern(
            IMultiLayerNeuralNetwork<double[], double[]> network,
            IInputTrainingPattern<double[], double[]> pattern)
        {
            foreach (IPropagationStep propagationStep in algorithmSteps)
            {
                propagationStep.Process(network, pattern);
            }
        }
    }
}