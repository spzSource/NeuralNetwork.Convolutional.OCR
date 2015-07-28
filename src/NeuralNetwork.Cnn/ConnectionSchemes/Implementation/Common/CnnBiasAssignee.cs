using DigitR.Core.NeuralNetwork.Primitives;
using DigitR.NeuralNetwork.Cnn.Primitives;

namespace DigitR.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Common
{
    internal class CnnBiasAssignee : IBiasAssignee
    {
        public void Assign(INeuron<double> currentRightNeuron, IWeight<double> biasWeight)
        {
            CnnNeuron bias = new CnnNeuron(-1, isBias: true);
            
            currentRightNeuron.Inputs.Add(
                new CnnConnection(
                    bias,
                    biasWeight));
        }
    }
}
