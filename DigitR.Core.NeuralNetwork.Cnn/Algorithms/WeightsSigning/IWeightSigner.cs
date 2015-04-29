using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.WeightsSigning
{
    internal interface IWeightSigner<TValue>
    {
        void Sign(IWeight<TValue> weight);
    }
}