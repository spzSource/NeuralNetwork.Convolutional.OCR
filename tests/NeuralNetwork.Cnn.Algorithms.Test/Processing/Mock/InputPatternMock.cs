using DigitR.Core.NeuralNetwork.InputProvider;

namespace Tests.NeuralNetwork.Cnn.Algorithms.Test.Processing.Mock
{
    public class InputPatternMock : IInputPattern<double[]>
    {
        public InputPatternMock(double[] source)
        {
            Source = source;
        }

        public double[] Source { get; }
    }
}