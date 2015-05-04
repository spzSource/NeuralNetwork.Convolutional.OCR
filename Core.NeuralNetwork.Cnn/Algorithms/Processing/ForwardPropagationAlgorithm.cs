using System.Diagnostics.Contracts;
using System.Linq;

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.Extensions;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.Processing
{
    public class ForwardPropagationAlgorithm : 
        IProcessingAlgorithm<INeuralNetwork<double[]>, IInputPattern<double[]>>
    {
        private readonly IActivationAlgorithm<double, double> activationAlgorithm;

        public ForwardPropagationAlgorithm(
            IActivationAlgorithm<double, double> activationAlgorithm)
        {
            this.activationAlgorithm = activationAlgorithm;
        }

        public double[] Process(
            INeuralNetwork<double[]> network, 
            IInputPattern<double[]> inputPattern)
        {
            double[] outputSignals = null;

            IMultiLayerNeuralNetwork<double> multiLayerNeuralNetwork = (IMultiLayerNeuralNetwork<double>)network;

            ILayer<INeuron<double>> inputLayer = multiLayerNeuralNetwork.GetLayer(layer => layer.IsFirst);
            ILayer<INeuron<double>> outputLayer = multiLayerNeuralNetwork.GetLayer(layer => layer.IsLast);

            Contract.Assert(inputPattern.Source.Length == inputLayer.Neurons.Length,
                "The input image should has the same size as number of neural network inputs.");

            for (int neuronIndex = 0; neuronIndex < inputLayer.Neurons.Length; neuronIndex++)
            {
                inputLayer.Neurons[neuronIndex].Output = inputPattern.Source[neuronIndex];
            }

            foreach (ILayer<INeuron<double>> layer in multiLayerNeuralNetwork.Layers.Where(layer => !layer.IsFirst))
            {
                foreach (INeuron<double> neuron in layer.Neurons)
                {
                    double inducedLocalArea = neuron.Inputs
                        .Sum(connection => connection.Weight.Value * connection.Neuron.Output);

                    neuron.Output = activationAlgorithm.Calculate(inducedLocalArea);
                }
            }

            outputSignals = outputLayer.Neurons.Select(neuron => neuron.Output).ToArray();

            return outputSignals;
        }
    }
}
