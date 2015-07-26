using System;

using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.Activation;

using Xunit;

namespace NeuralNetwork.Cnn.Test.Activation
{
    public class HyperbolicActivationAlgorithmTests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(-1, -1)]
        [InlineData(0, 0)]
        [InlineData(-100, -1)]
        [InlineData(100, 1)]
        public void CheckAntiSymmetricQualityTest(double inducedLocalArea, double expectedSignal)
        {
            IActivationAlgorithm<double, double> hyperbolicAlgorithm =
                new HyperbolicTgActivationAlgorithm();

            double realSignal = hyperbolicAlgorithm.Calculate(inducedLocalArea);

            Assert.True(Math.Abs(expectedSignal - realSignal) <= 0.00001,
                "The values of expected and real signal must be equals.");
        }

        [Theory]
        [InlineData(0.1, 1, 0)]
        [InlineData(0.5, 1, 0)]
        [InlineData(5, 1, 0)]
        [InlineData(10, 1, 0)]
        [InlineData(-0.1, 0, -1)]
        [InlineData(-0.5, 0, -1)]
        [InlineData(-5, 0, -1)]
        [InlineData(-10, 0, -1)]
        public void CheckPositiveInternalPointsTest(double inducedLocalArea, double highBound, double lowBound)
        {
            IActivationAlgorithm<double, double> hyperbolicAlgorithm =
                new HyperbolicTgActivationAlgorithm();

            double realSignal = hyperbolicAlgorithm.Calculate(inducedLocalArea);

            Assert.True(realSignal <= highBound);
            Assert.True(realSignal >= lowBound);
        }
    }
}
