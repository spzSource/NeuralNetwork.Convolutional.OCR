using DigitR.Core.NeuralNetwork.Primitives;

namespace Tests.NeuralNetwork.Cnn.Algorithms.Test.WeightSigning.Mock
{
    public class WeightMock : IWeight<double>
    {
        public double Value
        {
            get;
            set;
        }

        public object AdditionalInfo
        {
            get;
            set;
        }

        public TInfo GetInfo<TInfo>()
        {
            return (TInfo)AdditionalInfo;
        }
    }
}