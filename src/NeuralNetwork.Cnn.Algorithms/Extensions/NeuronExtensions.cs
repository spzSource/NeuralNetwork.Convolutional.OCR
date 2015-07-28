using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.NeuralNetwork.Cnn.Algorithms.Extensions
{
    public static class NeuronExtensions
    {
        public static TInfo GetNeuronInfo<TInfo>(this INeuron<double> neuron)
        {
            return (TInfo)neuron.AdditionalInfo;
        }
    }
}
