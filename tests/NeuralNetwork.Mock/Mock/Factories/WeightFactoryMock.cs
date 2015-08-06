using System;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;

using Tests.NeuralNetwork.Mock.Mock.Primitives;

namespace Tests.NeuralNetwork.Mock.Mock.Factories
{
    public class WeightFactoryMock : IWeightFactory<double>
    {
        private readonly IWeightSigner<double> weightSigner;

        public WeightFactoryMock(IWeightSigner<double> weightSigner)
        {
            if (weightSigner == null)
            {
                throw new ArgumentNullException(nameof(weightSigner));
            }
            this.weightSigner = weightSigner;
        }

        public IWeight<double> Create()
        {
            WeightMock newWeight = new WeightMock { AdditionalInfo = null, Value = 0.0 };

            weightSigner.Sign(newWeight);

            return newWeight;
        }

        public IWeight<double>[] CreateMany(int weightsCount)
        {
            IWeight<double>[] weights = new IWeight<double>[weightsCount];

            for (int weightIndex = 0; weightIndex < weights.Length; weightIndex++)
            {
                WeightMock newWeight = new WeightMock { AdditionalInfo = null, Value = 0.0 };

                weightSigner.Sign(newWeight);

                weights[weightIndex] = newWeight;
            }

            return weights;
        }
    }
}