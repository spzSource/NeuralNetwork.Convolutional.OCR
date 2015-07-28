using System.Threading;

using DigitR.Core.NeuralNetwork.InputProvider;
using DigitR.Core.NeuralNetwork.OutputProvider;

namespace DigitR.Core.NeuralNetwork
{
    /// <summary>
    /// Provides access to specific implementation of processor for neural network.
    /// </summary>
    public interface INeuralNetworkProcessor<TNetwork>
    {
        /// <summary>
        /// Returns a source neural network instance.
        /// </summary>
        TNetwork NeuralNetwork
        {
            get;
        }

        /// <summary>
        /// Provides lazy initialization of this processor.
        /// </summary>
        /// <param name="network">The specific neural network instance.</param>
        void Initialize(TNetwork network);

        /// <summary>
        /// Executes pattern processing (recognition).
        /// </summary>
        /// <param name="inputProvider">The provider that makes access to source items (patterns).</param>
        /// <param name="outputProvider">The provider that makes access to output source after processing.</param>
        /// <returns>The flag that indicates about status of processing.</returns>
        bool Process(IInputProvider inputProvider, IOutputProvider outputProvider);

        /// <summary>
        /// Executes training process.
        /// </summary>
        /// <param name="trainingInputProvider">The source of input patterns.</param>
        /// <param name="cancellationToken">The <see cref="CancellationTokenSource"/> to stop training proces if required.</param>
        /// <returns>The flag that indicates about status of training process.</returns>
        bool Train(IInputProvider trainingInputProvider, CancellationToken cancellationToken);
    }
}
