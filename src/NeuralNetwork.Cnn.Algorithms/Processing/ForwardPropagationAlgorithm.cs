using System;
using System.Linq;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.InputProvider;
using DigitR.Core.NeuralNetwork.Primitives;
using DigitR.NeuralNetwork.Cnn.Algorithms.Extensions;

namespace DigitR.NeuralNetwork.Cnn.Algorithms.Processing
{
    public class ForwardPropagationAlgorithm : 
        IProcessingAlgorithm<INeuralNetwork<double>, IInputPattern<double[]>, double>
    {
        private readonly IActivationAlgorithm<double, double> activationAlgorithm;

        public ForwardPropagationAlgorithm(
            IActivationAlgorithm<double, double> activationAlgorithm)
        {
            this.activationAlgorithm = activationAlgorithm;
        }

        public double[] Process(
            INeuralNetwork<double> network, 
            IInputPattern<double[]> inputPattern)
        {
            IMultiLayerNeuralNetwork<double> multiLayerNeuralNetwork = (IMultiLayerNeuralNetwork<double>)network;

            ILayer<INeuron<double>, IConnectionFactory<double, double>> inputLayer = 
                multiLayerNeuralNetwork.GetLayer(layer => layer.IsFirst);

            ILayer<INeuron<double>, IConnectionFactory<double, double>> outputLayer = 
                multiLayerNeuralNetwork.GetLayer(layer => layer.IsLast);

            if (inputPattern.Source.Length != inputLayer.Neurons.Length)
            {
                throw new ArgumentException("The input pattern should has the same size as number of neural network inputs.");
            }

            for (int neuronIndex = 0; neuronIndex < inputLayer.Neurons.Length; neuronIndex++)
            {
                inputLayer.Neurons[neuronIndex].Output = inputPattern.Source[neuronIndex];
            }

            foreach (ILayer<INeuron<double>, IConnectionFactory<double, double>> layer 
                in multiLayerNeuralNetwork.Layers.Where(layer => !layer.IsFirst))
            {
                foreach (INeuron<double> neuron in layer.Neurons)
                {
                    double inducedLocalArea = neuron.Inputs
                        .Sum(connection => connection.Weight.Value * connection.Neuron.Output);

                    neuron.Output = activationAlgorithm.Calculate(inducedLocalArea);
                }
            }

            return outputLayer.Neurons.Select(neuron => neuron.Output).ToArray();
        }
    }
}
