using System.Diagnostics.Contracts;
using System.Linq;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.InputProvider;
using DigitR.Core.NeuralNetwork.Primitives;
using DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation.Common;
using DigitR.NeuralNetwork.Cnn.Algorithms.Extensions;

namespace DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation
{
    internal class BackPropagateHiddenLayersStep : IPropagationStep
    {
        private readonly double learningSpeed;
        private readonly IActivationAlgorithm<double, double> activationAlgorithm;

        public BackPropagateHiddenLayersStep(
            double learningSpeed,
            IActivationAlgorithm<double, double> activationAlgorithm)
        {
            this.learningSpeed = learningSpeed;
            this.activationAlgorithm = activationAlgorithm;
        }

        public void Process(IMultiLayerNeuralNetwork<double> network, IInputTrainingPattern<double[]> pattern)
        {
            foreach (ILayer<INeuron<double>, IConnectionFactory<double, double>> layer in network.Layers.Where(layer => !layer.IsLast && !layer.IsFirst).Reverse())
            {
                foreach (INeuron<double> currentNeuron in layer.Neurons)
                {
                    BackPropagateNeuronInfo currentNeuronInfo = (BackPropagateNeuronInfo)currentNeuron.AdditionalInfo;

                    Contract.Assert(double.IsNaN(currentNeuronInfo.LocalGradient));
                    Contract.Assert(currentNeuron.Outputs.Count > 0, "Wrong number of inputs.");
                    
                    double weightedGradientsSum = currentNeuron.Outputs
                        .Sum(connection => connection.Neuron.GetNeuronInfo<BackPropagateNeuronInfo>().LocalGradient * connection.Weight.Value);

                    double localGradient = activationAlgorithm.CalculateFirstDerivative(currentNeuron.Output) * weightedGradientsSum;
                    currentNeuronInfo.LocalGradient = localGradient;

                    foreach (IConnection<double, double> connection in currentNeuron.Inputs)
                    {
                        double weightCorrection = learningSpeed * localGradient * connection.Neuron.Output;
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
