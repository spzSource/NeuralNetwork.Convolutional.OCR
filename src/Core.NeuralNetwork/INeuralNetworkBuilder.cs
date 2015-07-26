using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public interface INeuralNetworkBuilder<TData>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        INeuralNetworkBuilder<TData> AddInputLayer(ILayer<INeuron<TData>> layer);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TScheme"></typeparam>
        /// <param name="layer"></param>
        /// <returns></returns>
        INeuralNetworkBuilder<TData> AddLayer<TScheme>(ILayer<INeuron<TData>> layer)
            where TScheme : IConnectionScheme<INeuron<TData>>, new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNeuralNetworkFactory"></typeparam>
        /// <returns></returns>
        INeuralNetwork<TData[]> Build<TNeuralNetworkFactory>()
            where TNeuralNetworkFactory : INeuralNetworkFactory<TData>, new();
    }
}