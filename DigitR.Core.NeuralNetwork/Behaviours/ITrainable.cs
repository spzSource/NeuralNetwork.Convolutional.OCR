// Creator: Popitich Aleksandr Date: 20 04 2015 17:16
using System.Collections.Generic;

using DigitR.Core.InputProvider;

namespace DigitR.Core.NeuralNetwork.Behaviours
{
    public interface ITrainable<in TLabel, in TSource>
    {
        void ProcessTraining(IEnumerable<IInputTrainingPattern<TLabel, TSource>> patterns);
    }
}