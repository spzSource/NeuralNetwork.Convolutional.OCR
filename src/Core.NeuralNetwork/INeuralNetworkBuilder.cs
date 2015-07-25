using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork
{
    public interface INeuralNetworkBuilder<TData>
    {
        INeuralNetworkBuilder<TData> AddLayer<TScheme>(ILayer<INeuron<TData>> layer)
            where TScheme : IConnectionScheme<INeuron<TData>>, new();

        INeuralNetwork<TData[]> Build<TNeuralNetworkFactory>()
            where TNeuralNetworkFactory : INeuralNetworkFactory<TData>, new();
    }
}