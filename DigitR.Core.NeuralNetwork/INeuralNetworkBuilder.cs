namespace DigitR.Core.NeuralNetwork
{
    /// <summary>
    /// Provides a specific neural network building.
    /// </summary>
    /// <typeparam name="TInput">The input type for neural network.</typeparam>
    /// <typeparam name="TOutput">The output type for neural network.</typeparam>
    public interface INeuralNetworkBuilder<in TInput, in TOutput>
    {
        /// <summary>
        /// Performs building specific neural network instance.
        /// </summary>
        /// <returns>The instance of specific neural network.</returns>
        INeuralNetwork<TInput, TOutput> Build();
    }
}
