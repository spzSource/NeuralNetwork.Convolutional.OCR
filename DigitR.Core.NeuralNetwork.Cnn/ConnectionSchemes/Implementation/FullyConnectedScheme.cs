using DigitR.Core.NeuralNetwork.Cnn.Primitives;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation
{
    internal class FullyConnectedScheme : IConnectionScheme<INeuron<double>>
    {
        public void Apply(ILayer<INeuron<double>> leftLayer, ILayer<INeuron<double>> rightLayer)
        {
            foreach (INeuron<double> currentRightNeuron in rightLayer.Neurons)
            {
                foreach (INeuron<double> currentLeftNeuron in leftLayer.Neurons)
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
