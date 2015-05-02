namespace DigitR.Core.NeuralNetwork
{
    /// <summary>
    /// Provides a specific neural network building.
    /// </summary>
    public interface INeuralNetworkBuilder<TData>
    {
        /// <summary>
        /// Performs building specific neural network instance.
        /// </summary>
        /// <returns>The instance of specific neural network.</returns>
        INeuralNetwork<TData[]> Build();
    }
}
