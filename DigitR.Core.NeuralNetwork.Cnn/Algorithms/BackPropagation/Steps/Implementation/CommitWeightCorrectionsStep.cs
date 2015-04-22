using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.Extensions;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation
{
    internal class CommitWeightCorrectionsStep : IPropagationStep
    {
        public void Process(IMultiLayerNeuralNetwork<double[], double[]> network, IInputTrainingPattern<double[], double[]> pattern)
        {
            foreach (ILayer<object> layer in network.Layers)
            {
                foreach (INeuron<double> neuron in layer.Neurons)
                {
                    foreach (IConnection<double, double> connection in neuron.Inputs)
                    {
                        connection.Weight.Value +=
                            connection.GetConnectionInfo<BackPropagateConnectionInfo>().WeightCorrection;
                    }
                }
            }
        }
    }
}
