using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork;

namespace NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.BackPropagation.Steps
{
    internal interface IPropagationStep
    {
        void Process(
            IMultiLayerNeuralNetwork<double> network, 
            IInputTrainingPattern<double[]> pattern);
    }
}
