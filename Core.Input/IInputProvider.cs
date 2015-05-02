using System.Collections.Generic;

namespace DigitR.Core.InputProvider
{
    /// <summary>
    /// Provides access to input data store.
    /// </summary>
    public interface IInputProvider
    {
        object Current
        {
            get;
        }

        /// <summary>
        /// Retrieves input data items from specific data source.
        /// </summary>
        /// <returns>The enumerator for specific source.</returns>
        IEnumerable<object> Retrieve();
    }
}
