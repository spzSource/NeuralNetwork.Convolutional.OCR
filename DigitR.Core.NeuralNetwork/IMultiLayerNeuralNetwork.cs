using System.Collections.Generic;
using DigitR.Core.NeuralNetwork.Behaviours;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork
{
    /// <summary>
    /// Provides a multilayer neural network.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface IMultiLayerNeuralNetwork<TValue>
        : INeuralNetwork<TValue[], TValue[]>, ITrainable<TValue[], TValue[]>
    {
        /// <summary>
        /// All layers.
        /// </summary>
        IReadOnlyCollection<ILayer<INeuron<TValue>>> Layers
        {
            get;
        }
    }
}