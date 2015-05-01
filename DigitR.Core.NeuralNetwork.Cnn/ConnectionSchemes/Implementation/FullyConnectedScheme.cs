using System;

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

        public FullyConnectedScheme(
            IWeightSigner<double> weightSigner,
            ConnectionsCounter connectionsCounter)
        {
            if (weightSigner == null)
            {
                throw new ArgumentNullException("weightSigner");
            }
            this.weightSigner = weightSigner;
            this.connectionsCounter = connectionsCounter;
        }

        public void Apply(ILayer<INeuron<double>> leftLayer, ILayer<INeuron<double>> rightLayer)
        {
            FeatureMapWeightsCreator weightsCreator = new FeatureMapWeightsCreator(weightSigner);

            foreach (INeuron<double> currentRightNeuron in rightLayer.Neurons)
            {
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
