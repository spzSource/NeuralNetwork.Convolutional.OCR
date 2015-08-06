using DigitR.Core.NeuralNetwork.Primitives;

namespace Tests.NeuralNetwork.Mock.Mock.Primitives
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