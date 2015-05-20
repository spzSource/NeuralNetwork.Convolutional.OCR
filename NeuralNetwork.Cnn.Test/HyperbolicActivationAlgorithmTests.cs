using System;

using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.Activation;

using NUnit.Framework;

namespace NeuralNetwork.Cnn.Test
{
    [TestFixture]
    public class HyperbolicActivationAlgorithmTests
    {
        [Test(Description = "Test output signals using specified induced local areas.")]
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
    }
}
