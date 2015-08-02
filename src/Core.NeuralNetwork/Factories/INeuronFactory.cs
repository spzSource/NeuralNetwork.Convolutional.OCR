using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Factories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNeuronOutput"></typeparam>
    public interface INeuronFactory<TNeuronOutput>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        INeuron<TNeuronOutput> Create(int neuronId, bool isBias);
    }
}