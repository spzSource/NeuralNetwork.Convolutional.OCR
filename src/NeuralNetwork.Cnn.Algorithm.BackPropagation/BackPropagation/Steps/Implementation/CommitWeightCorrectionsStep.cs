using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Primitives;
using DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation.Common;

namespace DigitR.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation
{
    internal class CommitWeightCorrectionsStep : IPropagationStep
    {
        public void Process(IMultiLayerNeuralNetwork<double> network, IInputTrainingPattern<double[]> pattern)
        {
            foreach (ILayer<INeuron<double>> layer in network.Layers)
            {
                foreach (INeuron<double> neuron in layer.Neurons)
                {
                    foreach (IConnection<double, double> connection in neuron.Inputs)
                    {
                        double correction = connection.Weight.GetInfo<BackPropagateWeightInfo>().WeightCorrection;
                        connection.Weight.Value += correction;
                    }
                }
            }

            foreach (ILayer<INeuron<double>> layer in network.Layers)
            {
                foreach (INeuron<double> neuron in layer.Neurons)
                {
                    foreach (IConnection<double, double> connection in neuron.Inputs)
                    {
                        connection.Weight.AdditionalInfo = null;
                    }
                    foreach (IConnection<double, double> connection in neuron.Outputs)
                    {
                        connection.Weight.AdditionalInfo = null;
                    }
                    neuron.AdditionalInfo = null;
                }
            }
        }
    }
}
