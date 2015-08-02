using DigitR.Core.NeuralNetwork.InputProvider;

namespace Tests.NeuralNetwork.Cnn.Algorithms.Test.BackPropagation.Mock
{
    public class InputTrainingPatternMock : IInputTrainingPattern<double[]>
    {
        public double[] Source
        {
            get;
            set;
        }

        public double[] Label
        {
            get;
            set;
        }
    }
}