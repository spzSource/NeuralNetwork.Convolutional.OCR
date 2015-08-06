using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Primitives;
using DigitR.NeuralNetwork.Cnn.Algorithms.WeightsSigning;

using Tests.NeuralNetwork.Mock.Mock.Primitives;

using Xunit;

namespace Tests.NeuralNetwork.Cnn.Algorithms.Test.WeightSigning
{
    public class NormalWeightSignerTest
    {
        private readonly IWeightSigner<double> weightSigner = new NormalWeightSigner();

        [Fact]
        public void SigningTest()
        {
            IWeight<double> weightMock = new WeightMock();

            weightSigner.Sign(weightMock);

            Assert.True(weightMock.Value <= 1);
            Assert.True(weightMock.Value >= -1);
        }
    }
}
