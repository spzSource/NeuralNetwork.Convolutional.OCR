namespace DigitR.Core.NeuralNetwork.Primitives
{
    /// <summary>
    /// Provides a abstract primitive for neuron connection.
    /// </summary>
    public interface IConnection<out TNeuron, out TWeight>
    {
        TNeuron Neuron
        {
            get;
        }

        TWeight Weight
        {
            get;
        }
    }
}
