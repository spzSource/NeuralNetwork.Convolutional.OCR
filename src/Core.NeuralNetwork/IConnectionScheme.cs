using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork
{
    /// <summary>
    /// Provides access for specific implementation of scheme of connections for specified sets of neurons.
    /// </summary>
    /// <typeparam name="TNeuron">The type of used neurons.</typeparam>
    /// <typeparam name="TConnectionFactory"></typeparam>
    public interface IConnectionScheme<TNeuron, TConnectionFactory>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        void SetConnectionFactory(TConnectionFactory factory);

        /// <summary>
        /// Applies a specific scheme of connections from neurons on the left side to the neurons on the right side.
        /// </summary>
        /// <param name="leftLayer">The set of neurons on the left side (a child layer).</param>
        /// <param name="rightLayer">The set of neurons on the right side (a parent layer).</param>
        void Apply(
            ILayer<TNeuron, TConnectionFactory> leftLayer,
            ILayer<TNeuron, TConnectionFactory> rightLayer);
    }
}
