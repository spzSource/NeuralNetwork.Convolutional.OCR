using System.Diagnostics.Contracts;
using System.Linq;

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation.Common;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.Extensions;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation
{
    internal class BackPropagateHiddenLayersStep : IPropagationStep
    {
        private const double LearningSpeed = 0.7;

        private readonly IActivationAlgorithm<double, double> activationAlgorithm;

        public BackPropagateHiddenLayersStep(
            IActivationAlgorithm<double, double> activationAlgorithm)
        {
            this.activationAlgorithm = activationAlgorithm;
        }

        public void Process(IMultiLayerNeuralNetwork<double> network, IInputTrainingPattern<double[]> pattern)
        {
            foreach (ILayer<INeuron<double>> layer in network.Layers.Where(layer => !layer.IsLast && !layer.IsFirst).Reverse())
            {
                foreach (INeuron<double> currentNeuron in layer.Neurons)
                {
                    BackPropagateNeuronInfo currentNeuronInfo = (BackPropagateNeuronInfo)currentNeuron.AdditionalInfo;

                    Contract.Assert(double.IsNaN(currentNeuronInfo.LocalGradient));
                    Contract.Assert(currentNeuron.Outputs.Count > 0, "Wrong number of inputs.");
                    
                    double weightedGradientsSum = currentNeuron.Outputs
                        .Sum(connection => connection.Neuron.GetNeuronInfo<BackPropagateNeuronInfo>().LocalGradient * connection.Weight.Value);

                    double localGradient = currentNeuron.Output * (1 - currentNeuron.Output) * weightedGradientsSum;
                    currentNeuronInfo.LocalGradient = localGradient;

                    foreach (IConnection<double, double> connection in currentNeuron.Inputs)
                    {
                        double weightCorrection = LearningSpeed * localGradient * connection.Neuron.Output;
                        StoreWeightCorrection(connection, weightCorrection);
                    }
                }
            }
        }

        private static void StoreWeightCorrection(IConnection<double, double> connection, double correction)
        {
            if (connection.Weight.AdditionalInfo == null)
            {
                connection.Weight.AdditionalInfo = new BackPropagateWeightInfo { WeightCorrection = correction };
            }
            else
            {
                connection.Weight.GetInfo<BackPropagateWeightInfo>().WeightCorrection += correction;
            }
        }
    }
}
