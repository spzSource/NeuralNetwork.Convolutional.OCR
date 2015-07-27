using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork;

namespace DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps
{
    internal interface IPropagationStep
    {
        void Process(
            IMultiLayerNeuralNetwork<double> network, 
            IInputTrainingPattern<double[]> pattern);
    }
}
