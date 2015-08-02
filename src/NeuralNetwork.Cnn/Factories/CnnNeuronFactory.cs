using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.NeuralNetwork.Cnn.Factories
{
    public class CnnNeuronFactory : INeuronFactory<double>
    {
        public INeuron<double> Create(int neuronId, bool isBias)
        {
            throw new System.NotImplementedException();
        }
    }
}