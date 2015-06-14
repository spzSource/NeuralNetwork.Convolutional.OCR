using System.Collections.Generic;

namespace DigitR.Core.NeuralNetwork.Algorithms
{
    /// <summary>
    /// Provides access to spceific implementation of output algorithm for neuron.
    /// </summary>
    /// <typeparam name="TOutput">The output value.</typeparam>
    /// <typeparam name="TConnection">The type of used connection.</typeparam>
    public interface IOutputAlgorithm<out TOutput, in TConnection>
    {
        /// <summary>
        /// Calculates value of output using incoming inputs.
        /// </summary>
        /// <returns></returns>
        TOutput Calculate(IReadOnlyCollection<TConnection> inputConnections);
    }
}
