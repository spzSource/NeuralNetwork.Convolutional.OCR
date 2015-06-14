using System;

using DigitR.Core.NeuralNetwork.Cnn.Algorithms.WeightsSigning;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Common
{
    internal class FeatureMapWeightsCreator
    {
        private readonly IWeightSigner<double> weightSigner;

        public FeatureMapWeightsCreator(
            IWeightSigner<double> weightSigner)
        {
            if (weightSigner == null)
            {
                throw new ArgumentNullException("weightSigner");
            }
            this.weightSigner = weightSigner;
        }

        public CnnWeight[] CreateWeights(int weightsCount)
        {
            CnnWeight[] weights = new CnnWeight[weightsCount];

            for (int weightIndex = 0; weightIndex < weights.Length; weightIndex++)
            {
                CnnWeight newWeight = new CnnWeight { AdditionalInfo = null, Value = 0.0 };

                weightSigner.Sign(newWeight);

                weights[weightIndex] = newWeight;
            }

            return weights;
        }

        public CnnWeight CreateWeight()
        {
            CnnWeight newWeight = new CnnWeight { AdditionalInfo = null, Value = 0.0 };

            weightSigner.Sign(newWeight);
            
            return newWeight;
        }
    }
}
