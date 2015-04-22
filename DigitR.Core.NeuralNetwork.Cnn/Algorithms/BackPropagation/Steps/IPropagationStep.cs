using DigitR.Core.InputProvider;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps
{
    internal interface IPropagationStep
    {
        void Process(
            IMultiLayerNeuralNetwork<double[], double[]> network, 
            IInputTrainingPattern<double[], double[]> pattern);
    }
}
