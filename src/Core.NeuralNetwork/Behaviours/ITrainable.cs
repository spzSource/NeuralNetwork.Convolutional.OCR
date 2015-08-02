using System.Collections.Generic;
using System.Threading;

using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.InputProvider;

namespace DigitR.Core.NeuralNetwork.Behaviours
{
    /// <summary>
    /// Provides behaviour for training.
    /// </summary>
    /// <typeparam name="TData">The trainable data type.</typeparam>
    public interface ITrainable<TData>
    {
        /// <summary>
        /// Executes training.
        /// </summary>
        /// <param name="patterns">The set of patterns for training.</param>
        /// <param name="trainingAlgorithm">The training algorithm.</param>
        /// <param name="cancellationToken">The <see cref="CancellationTokenSource"/> for cancel training process if required.</param>
        /// <returns>The flag that indicates the training status for current instance of neural network.</returns>
        bool ProcessTraining(
            IEnumerable<IInputTrainingPattern<TData[]>> patterns,
            ITrainingAlgorithm<INeuralNetwork<TData>, IInputTrainingPattern<TData[]>> trainingAlgorithm,
            CancellationToken cancellationToken);
    }
}