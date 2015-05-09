using System.Collections.Generic;
using System.Threading;

namespace DigitR.Core.NeuralNetwork.Algorithms
{
    /// <summary>
    /// Provides access to specific implementation of training algorithm.
    /// </summary>
    /// <typeparam name="TNeuralNetwork">The type of neural network.</typeparam>
    /// <typeparam name="TPattern">The type of used pattern for training.</typeparam>
    public interface ITrainingAlgorithm<in TNeuralNetwork, in TPattern>
    {
        /// <summary>
        /// Executes training process.
        /// </summary>
        /// <param name="network">The specific neural network implementation.</param>
        /// <param name="patterns">The collection for training.</param>
        /// <param name="cancellationToken">The <see cref="CancellationTokenSource"/> for cancel training process if required.</param>
        /// <returns>The flag that indicates the training status for current instance of neural network.</returns>
        bool ProcessTraining(
            TNeuralNetwork network,
            IEnumerable<TPattern> patterns,
            CancellationToken cancellationToken);
    }
}