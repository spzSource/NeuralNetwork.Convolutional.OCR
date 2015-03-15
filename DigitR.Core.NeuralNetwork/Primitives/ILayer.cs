namespace DigitR.Core.NeuralNetwork.Primitives
{
    /// <summary>
    /// The layer for specific neural network.
    /// </summary>
    /// <typeparam name="TNeuron">The type of neuron for this layer.</typeparam>
    /// <typeparam name="TWeight">The type of weight for this layer.</typeparam>
    public interface ILayer<TNeuron, TWeight>
    {
        /// <summary>
        /// All neurons for this layer.
        /// </summary>
        TNeuron[] Neurons
        {
            get;
        }

        /// <summary>
        /// All weights for this layer.
        /// </summary>
        TWeight[] Weights
        {
            get;
        }

        /// <summary>
        /// Performs calculation for each of neurons in this layer.
        /// </summary>
        void Calculate();

        /// <summary>
        /// Connects this layer to layer passed as parameter.
        /// </summary>
        void ConnectToLayer(ILayer<TNeuron, TWeight> layer);
    }
}
