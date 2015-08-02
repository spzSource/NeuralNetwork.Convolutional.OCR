using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork
{
    public interface IWeightSigner<TValue>
    {
        void Sign(IWeight<TValue> weight);
    }
}