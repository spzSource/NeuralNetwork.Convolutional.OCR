﻿namespace DigitR.Core.NeuralNetwork.OutputProvider
{
    /// <summary>
    /// Provides a output logic for neural network recognition results.
    /// </summary>
    public interface IOutputProvider
    {
        /// <summary>
        /// Push the output results to specific destination output.
        /// </summary>
        bool Push(object data);
    }
}
