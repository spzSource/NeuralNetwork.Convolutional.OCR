using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Primitives;
using DigitR.NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.Extensions;

namespace DigitR.NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.BackPropagation.Steps.Implementation
{
    internal class SetNetworkInputStep : IPropagationStep
    {
        public void Process(
            IMultiLayerNeuralNetwork<double> network, 
            IInputTrainingPattern<double[]> pattern)
        {
            ILayer<INeuron<double>> inputLayer = network.GetLayer(layer => layer.IsFirst);

            for (int neuronIndex = 0; neuronIndex < inputLayer.Neurons.Length; neuronIndex++)
            {
                inputLayer.Neurons[neuronIndex].Output = pattern.Source[neuronIndex];
            }
        }
    }
}
