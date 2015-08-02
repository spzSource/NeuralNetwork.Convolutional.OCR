using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.NeuralNetwork.Cnn.ConnectionSchemes
{
    internal interface IBiasAssignee
    {
        void Assign(INeuron<double> currentRightNeuron, IWeight<double> biasWeight);
    }
}