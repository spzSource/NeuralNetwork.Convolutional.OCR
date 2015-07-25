using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Common;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;
using DigitR.Core.NeuralNetwork.Primitives;

using NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.WeightsSigning;
using NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.WeightsSigning.Implementation;

namespace DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation
{
    internal class FullyConnectedScheme : IConnectionScheme<INeuron<double>>
    {
        private readonly IWeightSigner<double> weightSigner = new NormalWeightSigner();
        private readonly IBiasAssignee biasAssignee = new CnnBiasAssignee();

        public void Apply(ILayer<INeuron<double>> leftLayer, ILayer<INeuron<double>> rightLayer)
        {
            FeatureMapWeightsCreator weightsCreator = new FeatureMapWeightsCreator(weightSigner);

            foreach (INeuron<double> currentRightNeuron in rightLayer.Neurons)
            {
                biasAssignee.Assign(currentRightNeuron, weightsCreator.CreateWeight());

                foreach (INeuron<double> currentLeftNeuron in leftLayer.Neurons)
                {
                    CnnWeight commonWeight = weightsCreator.CreateWeight();

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
