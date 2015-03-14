using System.Collections.Generic;

using DigitR.Core.InputProvider;

namespace DigitR.Core.NeuralNetwork
{
    /// <summary>
    /// Provides an interface of neural network.
    /// </summary>
    public interface INeuralNetwork<in TInput, in TOuput>
    {
        /// <summary>
        /// Initialize a specific network instance.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Deinitialize a specific network instance.
        /// </summary>
        void Deinitialize();

        /// <summary>
        /// Provides a training logic according to passed as parameters patterns.
        /// </summary>
        /// <returns>The result successful flag.</returns>
        bool ProcessTraining(IEnumerable<IInputPattern<TInput, TOuput>> trainingPatterns);

        /// <summary>
        /// Provides a determination logic according to input pattern 
        /// and current state of this instance of neuran network.
        /// </summary>
        /// <param name="inputPattern">The input pattern for determine.</param>
        /// <returns>The result successful flag.</returns>
        bool Process(IInputPattern<TInput, TOuput> inputPattern);
    }
}
