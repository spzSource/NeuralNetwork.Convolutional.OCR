// Creator: Popitich Aleksandr Date: 20 04 2015 18:03

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation
{
    public class BackPropagationAlgorithm
        : ITrainingAlgorithm<
            IMultiLayerNeuralNetwork<double[], double[]>,
            IInputTrainingPattern<double[], double[]>>
    {
        private const double TrainingSpeed = 0.5;

        private readonly IDictionary<INeuron<double>, BackPropagateInfo> neuronInfos =
            new Dictionary<INeuron<double>, BackPropagateInfo>();

        private readonly IOutputAlgorithm<double, IConnection<double, double>> outputAlgorithm;
        private readonly IActivationAlgorithm<double, double> activationAlgorithm;

        public BackPropagationAlgorithm(
            IOutputAlgorithm<double, IConnection<double, double>> outputAlgorithm,
            IActivationAlgorithm<double, double> activationAlgorithm)
        {
            if (outputAlgorithm == null)
            {
                throw new ArgumentNullException("outputAlgorithm");
            }
            if (activationAlgorithm == null)
            {
                throw new ArgumentNullException("activationAlgorithm");
            }
            this.outputAlgorithm = outputAlgorithm;
            this.activationAlgorithm = activationAlgorithm;
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
            SetNetworkInputSignals(network, pattern);
            CalculateNetworkOutputSignals(network);
            
            CalculateOutputLocalGradients(network, pattern);
        }

        private void CalculateOutputLocalGradients(
            IMultiLayerNeuralNetwork<double[], double[]> network, 
            IInputTrainingPattern<double[], double[]> pattern)
        {
            ILayer<INeuron<double>> outputLayer = GetLayer(network, layer => layer.IsLast);

            for (int neuronIndex = 0; neuronIndex < outputLayer.Neurons.Length; neuronIndex++)
            {
                double errorSignal = pattern.Label[neuronIndex] - outputLayer.Neurons[neuronIndex].Output;
                BackPropagateInfo neuronInfo = (BackPropagateInfo)outputLayer.Neurons[neuronIndex].AditionalInfo;

                Contract.Assert(double.IsNaN(neuronInfo.LocalGradient));

                neuronInfo.LocalGradient =
                    errorSignal * activationAlgorithm
                        .CalculateFirstDerivative(neuronInfo.LastInducesLocalAreaValue);
            }
        }

        private void CalculateNetworkOutputSignals(
            IMultiLayerNeuralNetwork<double[], double[]> network)
        {
            foreach (ILayer<object> layer in network.Layers.Where(layer => !layer.IsFirst))
            {
                foreach (INeuron<double> neuron in layer.Neurons)
                {
                    double inducedLocalArea = neuron.Inputs
                        .Sum(connection => connection.Weight.Value * connection.Neuron.Output);

                    neuron.Output = activationAlgorithm.Calculate(inducedLocalArea);
                    neuron.AditionalInfo = new BackPropagateInfo
                    {
                        LocalGradient = Double.NaN,
                        LastInducesLocalAreaValue = inducedLocalArea
                    };
                }
            }
        }

        private static void SetNetworkInputSignals(
            IMultiLayerNeuralNetwork<double[], double[]> network,
            IInputTrainingPattern<double[], double[]> pattern)
        {
            ILayer<INeuron<double>> inputLayer = GetLayer(network, layer => layer.IsFirst);
            for (int neuronIndex = 0; neuronIndex < inputLayer.Neurons.Length; neuronIndex++)
            {
                inputLayer.Neurons[neuronIndex].Output = pattern.Source[neuronIndex];
            }
        }

        private static ILayer<INeuron<double>> GetLayer(
            IMultiLayerNeuralNetwork<double[], double[]> network,
            Func<ILayer<object>, bool> predicate)
        {
            ILayer<INeuron<double>> inputLayer = network.Layers.FirstOrDefault(predicate) as ILayer<INeuron<double>>;
            if (inputLayer == null)
            {
                throw new Exception("Wrong layer");
            }
            return inputLayer;
        }
    }
}