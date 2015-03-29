namespace DigitR.Core.InputProvider
{
    /// <summary>
    /// Provides the input data pattern for specific input source item for processing.
    /// </summary>
    /// <typeparam name="TSource">The soure data item type.</typeparam>
    public interface IInputPattern<out TSource>
    {
        /// <summary>
        /// The source for specific input pattern.
        /// </summary>
        TSource Source
        {
            get;
        }
    }
}
