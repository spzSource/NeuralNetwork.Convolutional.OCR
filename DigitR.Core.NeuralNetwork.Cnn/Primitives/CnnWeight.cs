using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Primitives
{
    public class CnnWeight : IWeight<double>
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
