using System.Collections.Generic;

using DigitR.Core.InputProvider;

namespace DigitR.Core.NeuralNetwork.Cnn
{
    public class CnnNeuralNetwork : INeuralNetwork<byte[], byte>
    {
        /// <summary>
        /// Initialize a specific network instance.
        /// </summary>
        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Deinitialize a specific network instance.
        /// </summary>
        public void Deinitialize()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Provides a determination logic according to input pattern 
        /// and current state of this instance of neuran network.
        /// </summary>
        /// <param name="inputPattern">The input pattern for determine.</param>
        /// <returns>The result successful flag.</returns>
        public bool Process(IInputPattern<byte[], byte> inputPattern)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Provides a training logic according to passed as parameters patterns.
        /// </summary>
        /// <returns>The result successful flag.</returns>
        public bool ProcessTraining(IEnumerable<IInputPattern<byte[], byte>> trainingPatterns)
        {
            throw new System.NotImplementedException();
        }
    }
}
