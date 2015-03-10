namespace DigitR.Core.InputProvider
{
    /// <summary>
    /// Provides the input data pattern for specific input source item.
    /// </summary>
    /// <typeparam name="TLabel">The input label type for specific data item.</typeparam>
    /// <typeparam name="TSource">The soure data item type.</typeparam>
    public interface IInputPattern<out TLabel, out TSource>
    {
        /// <summary>
        /// The label for specific source item.
        /// </summary>
        TLabel Label
        {
            get;
        }

        /// <summary>
        /// The source for specific input pattern.
        /// </summary>
        TSource Source
        {
            get;
        }
    }
}
