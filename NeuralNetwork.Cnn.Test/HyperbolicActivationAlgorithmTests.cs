using System;

using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.Activation;

using NUnit.Framework;

namespace NeuralNetwork.Cnn.Test
{
    [TestFixture]
    public class HyperbolicActivationAlgorithmTests
    {
        [TestCase(1, 1)]
        [TestCase(-1, -1)]
        [TestCase(0, 0)]
        [TestCase(-100, -1)]
        [TestCase(100, 1)]
        public void CheckAntiSymmetricQualityTest(double inducedLocalArea, double expectedSignal)
        {
            IActivationAlgorithm<double, double> hyperbolicAlgorithm =
                new HyperbolicTgActivationAlgorithm();

            double realSignal = hyperbolicAlgorithm.Calculate(inducedLocalArea);

            Assert.LessOrEqual(Math.Abs(expectedSignal - realSignal), 0.00001,
                "The values of expected and real signal must be equals.");
        }

        [TestCase(0.1, 1, 0)]
        [TestCase(0.5, 1, 0)]
        [TestCase(5, 1, 0)]
        [TestCase(10, 1, 0)]
        [TestCase(-0.1, 0, -1)]
        [TestCase(-0.5, 0, -1)]
        [TestCase(-5, 0, -1)]
        [TestCase(-10, 0, -1)]
        public void CheckPositiveInternalPointsTest(double inducedLocalArea, double highBound, double lowBound)
        {
            IActivationAlgorithm<double, double> hyperbolicAlgorithm =
                new HyperbolicTgActivationAlgorithm();

            double realSignal = hyperbolicAlgorithm.Calculate(inducedLocalArea);

            Assert.LessOrEqual(realSignal, highBound);
            Assert.GreaterOrEqual(realSignal, lowBound);
        }
    }
}
