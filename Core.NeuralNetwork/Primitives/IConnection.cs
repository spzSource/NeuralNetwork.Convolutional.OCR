namespace DigitR.Core.NeuralNetwork.Primitives
{
    /// <summary>
    /// Provides a abstract primitive for neuron connection.
    /// </summary>
    public interface IConnection<TNeuronOutput, TWeightValue>
    {
        INeuron<TNeuronOutput> Neuron
        {
            get;
        }

        IWeight<TWeightValue> Weight
        {
            get;
        }

        object AdditionalInfo
        {
            get;
            set;
        }
    }
}
