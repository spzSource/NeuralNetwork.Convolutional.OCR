// Creator: Popitich Aleksandr Date: 20 04 2015 17:32
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork
{
    /// <summary>
    /// Provides a multilayer neural network.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IMultiLayerNeuralNetwork<in TInput, out TOutput> : INeuralNetwork<TInput, TOutput>
    {
        /// <summary>
        /// All layers.
        /// </summary>
        ILayer<object>[] Layers
        {
            get;
        }
    }
}