using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork
{
    public interface IConnectionScheme<TNeuron>
    {
        void Apply(
            ILayer<TNeuron> leftLayer,
            ILayer<TNeuron> rightLayer);
    }
}
