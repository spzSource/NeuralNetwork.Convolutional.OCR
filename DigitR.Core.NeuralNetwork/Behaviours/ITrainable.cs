// Creator: Popitich Aleksandr Date: 29 03 2015 17:30
using DigitR.Core.InputProvider;

namespace DigitR.Core.NeuralNetwork.Behaviours
{
    public interface ITrainable<in TInput>
    {
        /// <summary>
        /// Provides a training logic according to passed as parameters patterns.
        /// </summary>
        /// <returns>The result successful flag.</returns>
        bool ProcessTraining(IInputPattern<TInput> inputPattern);
    }
}