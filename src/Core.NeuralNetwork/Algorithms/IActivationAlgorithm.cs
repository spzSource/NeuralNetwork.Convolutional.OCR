namespace DigitR.Core.NeuralNetwork.Algorithms
{
    /// <summary>
    /// Provides universal access to specific activation function implementation.
    /// </summary>
    /// <typeparam name="TOutput">The type of output value.</typeparam>
    /// <typeparam name="TInducedArea">The type for induced local area.</typeparam>
    public interface IActivationAlgorithm<out TOutput, in TInducedArea>
    {
        /// <summary>
        /// Calculates output value using the induced local area value.
        /// </summary>
        /// <returns>The output value.</returns>
        TOutput Calculate(TInducedArea inducedArea);

        /// <summary>
        /// Calculates the first derrivative of current activation function.
        /// </summary>
        /// <param name="inducedArea">The value of induced local area.</param>
        /// <returns>The value of function derrivative.</returns>
        TOutput CalculateFirstDerivative(TInducedArea inducedArea);
    }
}
