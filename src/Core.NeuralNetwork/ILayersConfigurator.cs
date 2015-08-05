using System.Collections.Generic;

using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNeuronData"></typeparam>
    /// <typeparam name="TWeightData"></typeparam>
    public interface ILayersConfigurator<TNeuronData, TWeightData>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layersData"></param>
        /// <returns></returns>
        IReadOnlyCollection<ILayer<INeuron<TNeuronData>, IConnectionFactory<TNeuronData, TWeightData>>> Configure(
            IList<KeyValuePair<
                ILayer<INeuron<TNeuronData>, IConnectionFactory<TNeuronData, TWeightData>>,
                IConnectionScheme<INeuron<TNeuronData>, IConnectionFactory<TNeuronData, TWeightData>>>> layersData);
    }
}