namespace DigitR.Core.NeuralNetwork.InputProvider
{
    /// <summary>
    /// Provides the input data pattern for specific input source item and specified label for training process.
    /// </summary>
    public interface IInputTrainingPattern<out TData> : IInputPattern<TData>
    {
        /// <summary>
        /// The label for specific source item.
        /// </summary>
        TData Label
        {
            get;
        }
    }
}