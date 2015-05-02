using System.Collections.Generic;

namespace DigitR.Core.NeuralNetwork.Algorithms
{
    public interface ITrainingAlgorithm<in TNeuralNetwork, in TPattern>
    {
        bool ProcessTraining(
            TNeuralNetwork network,
            IEnumerable<TPattern> patterns);
    }
}