using System;

using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.NeuralNetwork.Cnn.Algorithms.Activation;

using Xunit;

namespace NeuralNetwork.Cnn.Test.Activation
{
    public class SigmoidActivationAlgorithmTests
    {
        private readonly IActivationAlgorithm<double, double> activationAlgorithm =
            new SigmoidActivationAlgorithm();

        [Theory]
        [InlineData(6, 1)]
        [InlineData(-6, 0)]
        [InlineData(0, 0.5)]
        [InlineData(-100, 0)]
        [InlineData(100, 1)]
        public void CheckSymetricQualityTest(double inducedLocalArea, double expectedSignal)
        {
            double realSignal = activationAlgorithm.Calculate(inducedLocalArea);

            Assert.True(Math.Abs(expectedSignal - realSignal) <= 0.01,
                "The values of expected and real signal must be equals.");
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0.5, 0.25)]
        [InlineData(3, -6)]
        [InlineData(-3, -12)]
        public void CheckSymethicDerivativeQualityTest(double inducedLocalArea, double expectedSignal)
        {
            double realSignal = activationAlgorithm.CalculateFirstDerivative(inducedLocalArea);

            Assert.True(Math.Abs(expectedSignal - realSignal) <= 0.01,
                "The values of expected and real signal must be equals.");
        }
    }
}
