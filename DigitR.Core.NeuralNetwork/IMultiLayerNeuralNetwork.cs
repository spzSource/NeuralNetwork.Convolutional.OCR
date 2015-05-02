using System.Collections.Generic;
using DigitR.Core.NeuralNetwork.Behaviours;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork
{
    /// <summary>
    /// Provides a multilayer neural network.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public interface IMultiLayerNeuralNetwork<TData>
        : INeuralNetwork<TData[]>, ITrainable<TData[]>
    {
        /// <summary>
        /// All layers.
        /// </summary>
        IReadOnlyCollection<ILayer<INeuron<TData>>> Layers
        {
            get;
        }
    }
}