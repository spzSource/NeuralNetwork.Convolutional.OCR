using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.InputProvider;

namespace DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IPropagationStep
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="network"></param>
        /// <param name="pattern"></param>
        void Process(
            IMultiLayerNeuralNetwork<double> network, 
            IInputTrainingPattern<double[]> pattern);
    }
}
