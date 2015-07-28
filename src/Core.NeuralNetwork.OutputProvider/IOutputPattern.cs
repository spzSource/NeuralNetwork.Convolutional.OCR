namespace DigitR.Core.NeuralNetwork.OutputProvider
{
    /// <summary>
    /// The item for output source.
    /// </summary>
    /// <typeparam name="TOutput">The type of value for this pattern.</typeparam>
    public interface IOutputPattern<TOutput>
    {
        /// <summary>
        /// The value of this pattern.
        /// </summary>
        TOutput Output
        {
            get;
            set;
        }
    }
}
