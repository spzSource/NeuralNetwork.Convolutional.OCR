using System.Threading.Tasks;

namespace DigitR.Core.NeuralNetwork.Serializer
{
    /// <summary>
    /// Provides access to specific implementation of serializer for neural network.
    /// </summary>
    /// <typeparam name="TData">The serializable data type.</typeparam>
    public interface INeuralNetworkSerializer<TData>
    {
        /// <summary>
        /// Executes async serialization for neural network passed as parameter.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="network"></param>
        Task<bool> SerializeAsync(string path, INeuralNetwork<TData> network);

        /// <summary>
        /// Executes async deserialization for neural network.
        /// </summary>
        /// <returns>The deserialized instance of neural network.</returns>
        Task<INeuralNetwork<TData>> DeserializeAsync(string path);
    }
}