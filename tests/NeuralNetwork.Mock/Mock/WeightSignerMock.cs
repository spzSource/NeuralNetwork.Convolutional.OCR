using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Primitives;

namespace Tests.NeuralNetwork.Mock.Mock
{
    public class WeightSignerMock : IWeightSigner<double>
    {
        private readonly double weightValue;

        public WeightSignerMock(double weightValue)
        {
            this.weightValue = weightValue;
        }

        public void Sign(IWeight<double> weight)
        {
            weight.Value = weightValue;
        }
    }
}