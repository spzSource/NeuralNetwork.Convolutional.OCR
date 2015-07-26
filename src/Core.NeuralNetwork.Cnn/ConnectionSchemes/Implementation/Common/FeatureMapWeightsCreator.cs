using System;

using DigitR.NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.WeightsSigning;
using DigitR.NeuralNetwork.Cnn.Primitives;

namespace DigitR.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Common
{
    internal class FeatureMapWeightsCreator
    {
        private readonly IWeightSigner<double> weightSigner;

        public FeatureMapWeightsCreator(
            IWeightSigner<double> weightSigner)
        {
            if (weightSigner == null)
            {
                throw new ArgumentNullException(nameof(weightSigner));
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
