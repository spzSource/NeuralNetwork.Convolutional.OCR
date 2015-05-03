using System.Collections.Generic;

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;

namespace DigitR.Core.NeuralNetwork.Behaviours
{
    public interface ITrainable<TData>
    {
        bool ProcessTraining(
            IEnumerable<IInputTrainingPattern<TData>> patterns,
            ITrainingAlgorithm<INeuralNetwork<TData>, IInputTrainingPattern<TData>> trainingAlgorithm);
    }
}