using System.Collections.Generic;
using System.Threading;

namespace DigitR.Core.NeuralNetwork.Algorithms
{
    public interface ITrainingAlgorithm<in TNeuralNetwork, in TPattern>
    {
        bool ProcessTraining(
            TNeuralNetwork network,
            IEnumerable<TPattern> patterns,
            CancellationToken cancellationToken);
    }
}