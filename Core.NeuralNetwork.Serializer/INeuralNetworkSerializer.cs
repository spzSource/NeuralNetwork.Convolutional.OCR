using System.Threading.Tasks;

namespace DigitR.Core.NeuralNetwork.Serializer
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public interface INeuralNetworkSerializer<TData>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="network"></param>
        Task<bool> SerializeAsync(string path, INeuralNetwork<TData> network);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<INeuralNetwork<TData>> DeserializeAsync(string path);
    }
}