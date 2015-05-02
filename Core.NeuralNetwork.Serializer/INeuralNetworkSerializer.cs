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
        /// <param name="network"></param>
        Task<bool> SerializeAsync(INeuralNetwork<TData> network);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<INeuralNetwork<TData>> DeserializeAsync();
    }
}