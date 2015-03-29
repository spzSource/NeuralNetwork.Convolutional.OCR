using System;

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deinitialize a specific network instance.
        /// </summary>
        public void Deinitialize()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Provides a determination logic according to input pattern 
        /// and current state of this instance of neuran network.
        /// </summary>
        /// <param name="inputPattern">The input pattern for determine.</param>
        /// <returns>The result successful flag.</returns>
        public byte Process(IInputPattern<byte[]> inputPattern)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Provides a training logic according to passed as parameters patterns.
        /// </summary>
        /// <returns>The result successful flag.</returns>
        public bool ProcessTraining(IInputPattern<byte[]> inputPattern)
        {
            throw new NotImplementedException();
        }
    }
}
