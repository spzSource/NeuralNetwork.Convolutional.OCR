using DigitR.Core.NeuralNetwork.Factories;

namespace DigitR.Core.NeuralNetwork.Primitives
{
    /// <summary>
    /// The layer for specific neural network.
    /// </summary>
    /// <typeparam name="TNeuron">The type of neuron for this layer.</typeparam>
    /// <typeparam name="TConnectionFactory"></typeparam>
    public interface ILayer<TNeuron, TConnectionFactory>
    {
        /// <summary>
        /// The identifier for this layer.
        /// </summary>
        int LayerId
        {
            get;
        }

        /// <summary>
        /// Determines whether this is the first layer.
        /// </summary>
        bool IsFirst
        {
            get;
        }

        /// <summary>
        /// Determines whether this is a last layer.
        /// </summary>
        bool IsLast
        {
            get;
        }

        /// <summary>
        /// All neurons for this layer.
        /// </summary>
        TNeuron[] Neurons
        {
            get;
        }

        /// <summary>
        /// Connects this layer to layer passed as parameter.
        /// </summary>
        void ConnectToLayer(
            ILayer<TNeuron, TConnectionFactory> layer, 
            IConnectionScheme<TNeuron, TConnectionFactory> connectionScheme);
    }
}
