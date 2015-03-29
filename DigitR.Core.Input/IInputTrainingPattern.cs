namespace DigitR.Core.InputProvider
{
    /// <summary>
    /// Provides the input data pattern for specific input source item and specified label for training process.
    /// </summary>
    /// <typeparam name="TSource">The soure data item type.</typeparam>
    /// <typeparam name="TLabel">The label for specified source.</typeparam>
    public interface IInputTrainingPattern<out TLabel, out TSource> : IInputPattern<TSource>
    {
        /// <summary>
        /// The label for specific source item.
        /// </summary>
        TLabel Label
        {
            get;
        }
    }
}