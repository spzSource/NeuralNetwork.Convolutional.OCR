using DigitR.Core.NeuralNetwork.Cnn.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Common
{
    internal class FeatureMapWeightsCreator
    {
        public CnnWeight[] CreateWeights(int weightsCount)
        {
            CnnWeight[] weights = new CnnWeight[weightsCount];

            for (int weightIndex = 0; weightIndex < weights.Length; weightIndex++)
            {
                weights[weightIndex] = new CnnWeight { AdditionalInfo = null, Value = 0.0 };
            }

            return weights;
        }
    }
}
