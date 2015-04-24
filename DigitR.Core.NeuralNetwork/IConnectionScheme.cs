using System.Collections.Generic;

namespace DigitR.Core.NeuralNetwork
{
    public interface IConnectionScheme<TNeuron>
    {
        void Apply(
            IList<TNeuron> leftNeurons,
            IList<TNeuron> rightNeurons);
    }
}
