using System.Collections.Generic;

using DigitR.Core.InputProvider;

namespace DigitR.Core.NeuralNetwork.Behaviours
{
    public interface ITrainable<in TData>
    {
        bool ProcessTraining(IEnumerable<IInputTrainingPattern<TData>> patterns);
    }
}