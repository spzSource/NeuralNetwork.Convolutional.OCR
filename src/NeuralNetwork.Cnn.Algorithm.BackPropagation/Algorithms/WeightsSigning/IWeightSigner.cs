using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.WeightsSigning
{
    public interface IWeightSigner<TValue>
    {
        void Sign(IWeight<TValue> weight);
    }
}