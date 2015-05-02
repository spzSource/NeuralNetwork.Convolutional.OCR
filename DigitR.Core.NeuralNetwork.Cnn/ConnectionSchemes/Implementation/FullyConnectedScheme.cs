using DigitR.Core.NeuralNetwork.Cnn.Algorithms.WeightsSigning;
using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Common;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation
{
    internal class FullyConnectedScheme : IConnectionScheme<INeuron<double>>
    {
        private readonly IWeightSigner<double> weightSigner;
        private readonly ConnectionsCounter connectionsCounter;
        private readonly IBiasAssignee biasAssignee;

        public FullyConnectedScheme(
            IWeightSigner<double> weightSigner,
            ConnectionsCounter connectionsCounter,
            IBiasAssignee biasAssignee)
        {
            this.weightSigner = weightSigner;
            this.connectionsCounter = connectionsCounter;
            this.biasAssignee = biasAssignee;
        }

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

                    connectionsCounter.Increment();
                }
            }
        }
    }
}
