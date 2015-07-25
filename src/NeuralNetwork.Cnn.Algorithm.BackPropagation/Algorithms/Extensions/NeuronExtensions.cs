using DigitR.Core.NeuralNetwork.Primitives;

namespace NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.Extensions
{
    public static class NeuronExtensions
    {
        public static TInfo GetNeuronInfo<TInfo>(this INeuron<double> neuron)
        {
            return (TInfo)neuron.AdditionalInfo;
        }
    }
}
