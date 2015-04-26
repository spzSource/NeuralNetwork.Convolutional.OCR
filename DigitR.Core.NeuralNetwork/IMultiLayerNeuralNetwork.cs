using System.Collections.Generic;
using DigitR.Core.NeuralNetwork.Behaviours;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork
{
    /// <summary>
    /// Provides a multilayer neural network.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IMultiLayerNeuralNetwork<in TInput, out TOutput>
        : INeuralNetwork<TInput, TOutput>, ITrainable<double[], double[]>
    {
        /// <summary>
        /// All layers.
        /// </summary>
        IReadOnlyCollection<ILayer<object>> Layers
        {
            get;
        }
    }
}