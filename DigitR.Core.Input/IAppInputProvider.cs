using System.Collections.Generic;

namespace DigitR.Core.InputProvider
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppInputProvider<out TLable, out TSource>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TLable"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        IEnumerable<IInputPattern<TLable, TSource>> Retrieve();
    }
}
