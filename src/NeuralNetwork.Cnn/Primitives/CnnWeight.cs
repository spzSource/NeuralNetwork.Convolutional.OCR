using System;
using System.Diagnostics;

using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.NeuralNetwork.Cnn.Primitives
{
    [Serializable]
    [DebuggerDisplay("Value = {Value}")]
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
