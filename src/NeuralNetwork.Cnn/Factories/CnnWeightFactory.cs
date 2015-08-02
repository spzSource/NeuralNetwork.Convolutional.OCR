using System;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;
using DigitR.NeuralNetwork.Cnn.Primitives;

namespace DigitR.NeuralNetwork.Cnn.Factories
{
    public class CnnWeightFactory : IWeightFactory<double>
    {
        private readonly IWeightSigner<double> weightSigner;

        public CnnWeightFactory(IWeightSigner<double> weightSigner)
        {
            if (weightSigner == null)
            {
                throw new ArgumentNullException(nameof(weightSigner));
            }
            this.weightSigner = weightSigner;
        }

        public IWeight<double> Create()
        {
            CnnWeight newWeight = new CnnWeight { AdditionalInfo = null, Value = 0.0 };

            weightSigner.Sign(newWeight);

            return newWeight;
        }

        public IWeight<double>[] CreateMany(int weightsCount)
        {
            IWeight<double>[] weights = new IWeight<double>[weightsCount];

            for (int weightIndex = 0; weightIndex < weights.Length; weightIndex++)
            {
                CnnWeight newWeight = new CnnWeight { AdditionalInfo = null, Value = 0.0 };

                weightSigner.Sign(newWeight);

                weights[weightIndex] = newWeight;
            }

            return weights;
        }
    }
}