using System.Diagnostics.Contracts;

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation.Common;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.Extensions;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation
{
    internal class BackPropagateOutputLayerStep : IPropagationStep
    {
        private const double LearningSpeed = 0.05;

        private readonly IActivationAlgorithm<double, double> activationAlgorithm;

        public BackPropagateOutputLayerStep(
            IActivationAlgorithm<double, double> activationAlgorithm)
        {
            this.activationAlgorithm = activationAlgorithm;
        }

        public void Process(IMultiLayerNeuralNetwork<double> network, IInputTrainingPattern<double[]> pattern)
        {
            ILayer<INeuron<double>> outputLayer = network.GetLayer(layer => layer.IsLast);

            for (int neuronIndex = 0; neuronIndex < outputLayer.Neurons.Length; neuronIndex++)
            {
                INeuron<double> currentNeuron = outputLayer.Neurons[neuronIndex];
                BackPropagateNeuronInfo currentNeuronInfo = (BackPropagateNeuronInfo)currentNeuron.AdditionalInfo;

                Contract.Assert(double.IsNaN(currentNeuronInfo.LocalGradient));
                Contract.Assert(currentNeuron.Inputs.Count > 0, "Wrong number of inputs.");

                double errorSignal = pattern.Label[neuronIndex] - currentNeuron.Output;
                double currentLocalGradient = errorSignal * currentNeuron.Output * (1 - currentNeuron.Output);
                
                currentNeuronInfo.LocalGradient = currentLocalGradient;

                foreach (IConnection<double, double> connection in currentNeuron.Inputs)
                {
                    double correction = LearningSpeed * currentLocalGradient * connection.Neuron.Output;
                    StoreWeightCorrection(connection, correction);
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
