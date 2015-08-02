using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Factories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNeuronOutput"></typeparam>
    /// <typeparam name="TWeightValue"></typeparam>
    public interface IConnectionFactory<TNeuronOutput, TWeightValue>
    {
        IConnection<TNeuronOutput, TWeightValue> Create(
            INeuron<TNeuronOutput> neuron);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="neuron"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        IConnection<TNeuronOutput, TWeightValue> CreateWithWeight(
            INeuron<TNeuronOutput> neuron,
            IWeight<TWeightValue> weight);
    }
}