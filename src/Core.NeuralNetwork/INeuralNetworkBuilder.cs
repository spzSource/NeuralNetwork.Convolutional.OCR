using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork
{
    /// <summary>
    /// Provides <see cref="INeuralNetwork{TData}"/> building.
    /// </summary>
    /// <typeparam name="TData">The data type for <see cref="INeuralNetwork{TData}"/> implementation.</typeparam>
    public interface INeuralNetworkBuilder<TData>
    {
        /// <summary>
        /// Adds a input layer to network structure.
        /// </summary>
        /// <param name="layer">Implementation of <see cref="ILayer{TNeuron, TConnectionfactory}"/>.</param>
        /// <returns>The <see cref="INeuralNetworkBuilder{TData}"/> instance.</returns>
        INeuralNetworkBuilder<TData> AddInputLayer(ILayer<INeuron<TData>, IConnectionFactory<TData, TData>> layer);

        /// <summary>
        /// Adds a layer that uses <see cref="TScheme"/> for connecting with previous layer.
        /// </summary>
        /// <typeparam name="TScheme">A scheme for connecting current layer with previous.</typeparam>
        /// <param name="layer">The current layer for construction.</param>
        /// <returns>The <see cref="INeuralNetworkBuilder{TData}"/> instance.</returns>
        INeuralNetworkBuilder<TData> AddLayer<TScheme>(ILayer<INeuron<TData>, IConnectionFactory<TData, TData>> layer)
            where TScheme : IConnectionScheme<INeuron<TData>, IConnectionFactory<TData, TData>>, new();

        /// <summary>
        /// Creates a instance of <see cref="INeuralNetwork{TData}"/> using existing layers and <see cref="TNeuralNetworkFactory"/> as a network factory.
        /// </summary>
        /// <typeparam name="TNeuralNetworkFactory">The factory for <see cref="INeuralNetwork{TData}"/> creation.</typeparam>
        /// <returns>The implementation of <see cref="INeuralNetwork{TData}"/></returns>
        INeuralNetwork<TData> Build<TNeuralNetworkFactory>()
            where TNeuralNetworkFactory : INeuralNetworkFactory<TData>, new();
    }
}