using System.Collections.Generic;

using DigitR.Core.InputProvider;

namespace DigitR.Core.NeuralNetwork.Behaviours
{
    public interface ITrainable<in TLabel, in TSource>
    {
        bool ProcessTraining(IEnumerable<IInputTrainingPattern<TLabel, TSource>> patterns);
    }
}