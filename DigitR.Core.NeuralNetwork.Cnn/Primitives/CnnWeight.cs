using System.Diagnostics;

using DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Primitives
{
    [DebuggerDisplay("Value = {Value}, Correction = {Info.WeightCorrection}")]
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

        // NOTE: for debug only. Should be removed.
        public BackPropagateWeightInfo Info
        {
            get
            {
                return GetInfo<BackPropagateWeightInfo>();
            }
        }
    }
}
