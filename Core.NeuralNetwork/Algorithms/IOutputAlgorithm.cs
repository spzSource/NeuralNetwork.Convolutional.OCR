using System.Collections.Generic;

namespace DigitR.Core.NeuralNetwork.Algorithms
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    /// <typeparam name="TConnection"></typeparam>
    public interface IOutputAlgorithm<out TOutput, in TConnection>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TOutput Calculate(IReadOnlyCollection<TConnection> inputConnections);
    }
}
