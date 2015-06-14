namespace DigitR.Core.NeuralNetwork.Primitives
{
    /// <summary>
    /// Provides a abstract primitive for neuron connection.
    /// </summary>
    public interface IConnection<TNeuronOutput, TWeightValue>
    {
        /// <summary>
        /// The input (source) neuron.
        /// </summary>
        INeuron<TNeuronOutput> Neuron
        {
            get;
        }

        /// <summary>
        /// The weight for this connection.
        /// </summary>
        IWeight<TWeightValue> Weight
        {
            get;
        }

        /// <summary>
        /// The additional info for this connection.
        /// This property may be used to store a additional data (during training process for example).
        /// </summary>
        object AdditionalInfo
        {
            get;
            set;
        }
    }
}
