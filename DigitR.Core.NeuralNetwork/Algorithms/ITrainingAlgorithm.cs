// Creator: Popitich Aleksandr Date: 20 04 2015 17:07
using System.Collections.Generic;

namespace DigitR.Core.NeuralNetwork.Algorithms
{
    public interface ITrainingAlgorithm<in TNeuralNetwork, in TPattern>
    {
        void ProcessTraining(
            TNeuralNetwork network,
            IEnumerable<TPattern> patterns);
    }
}