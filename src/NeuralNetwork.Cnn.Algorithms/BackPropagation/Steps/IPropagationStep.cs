using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.InputProvider;

namespace DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps
{
    internal interface IPropagationStep
    {
        void Process(
            IMultiLayerNeuralNetwork<double> network, 
            IInputTrainingPattern<double[]> pattern);
    }
}
