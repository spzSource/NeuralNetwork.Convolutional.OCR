namespace DigitR.Core.NeuralNetwork.Algorithms
{
    /// <summary>
    /// Provides access to specific implementation of processing (recognition) algorithm
    /// that executes after we trained neural network.
    /// </summary>
    /// <typeparam name="TNeuralNetwork">The type of neural network implementation.</typeparam>
    /// <typeparam name="TPattern">The type of patterns that uses for specified neural network implementation.</typeparam>
    public interface IProcessingAlgorithm<in TNeuralNetwork, in TPattern>
    {
        /// <summary>
        /// Executes processing using specified <see cref="TNeuralNetwork"/> and <see cref="TPattern"/> classes.
        /// </summary>
        /// <param name="network">The instance of <see cref="TNeuralNetwork"/> class.</param>
        /// <param name="inputPattern">The pattern (<see cref="TPattern"/>) for recognition.</param>
        /// <returns>The array of output signals.</returns>
        double[] Process(
            TNeuralNetwork network,
            TPattern inputPattern);
    }
}