using DigitR.Core.NeuralNetwork.Cnn.Primitives;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation
{
    internal class FullyConnectedScheme : IConnectionScheme<CnnNeuron>
    {
        public void Apply(ILayer<CnnNeuron> leftLayer, ILayer<CnnNeuron> rightLayer)
        {
            foreach (CnnNeuron currentRightNeuron in rightLayer.Neurons)
            {
                foreach (CnnNeuron currentLeftNeuron in leftLayer.Neurons)
                {
                    CnnWeight commonWeight = new CnnWeight();

                    currentRightNeuron.Inputs.Add(new CnnConnection(
                        currentLeftNeuron,
                        commonWeight));

                    currentLeftNeuron.Outputs.Add(new CnnConnection(
                        currentRightNeuron,
                        commonWeight));
                }
            }
        }
    }
}
