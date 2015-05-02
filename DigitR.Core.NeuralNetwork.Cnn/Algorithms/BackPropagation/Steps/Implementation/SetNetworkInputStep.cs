using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.Extensions;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation
{
    internal class SetNetworkInputStep : IPropagationStep
    {
        public void Process(
            IMultiLayerNeuralNetwork<double> network, 
            IInputTrainingPattern<double[], double[]> pattern)
        {
            ILayer<INeuron<double>> inputLayer = network.GetLayer(layer => layer.IsFirst);

            for (int neuronIndex = 0; neuronIndex < inputLayer.Neurons.Length; neuronIndex++)
            {
                inputLayer.Neurons[neuronIndex].Output = pattern.Source[neuronIndex];
            }
        }
    }
}
