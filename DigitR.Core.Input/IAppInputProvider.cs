using System.Collections.Generic;

namespace DigitR.Core.InputProvider
{
    /// <summary>
    /// Provides access to input data store.
    /// </summary>
    public interface IAppInputProvider<out TLable, out TSource>
    {
        /// <summary>
        /// Retrieves input data items from specific data source.
        /// </summary>
        /// <typeparam name="TLable">The type of input label for source data item.</typeparam>
        /// <typeparam name="TSource">The type of source data item.</typeparam>
        /// <returns>The enumerator for specific source.</returns>
        IEnumerable<IInputPattern<TLable, TSource>> Retrieve();
    }
}
