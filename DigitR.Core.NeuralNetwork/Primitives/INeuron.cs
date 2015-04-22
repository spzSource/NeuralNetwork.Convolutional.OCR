using System.Collections.Generic;

namespace DigitR.Core.NeuralNetwork.Primitives
{
    /// <summary>
    /// Provides an interface for abstract neuron.
    /// </summary>
    public interface INeuron<TOutput>
    {
        IList<IConnection<TOutput, TOutput>> Inputs
        {
            get;
        }
            
        TOutput Output
        {
            get;
            set;
        }

        bool IsBiasNeuron
        {
            get;
        }

        object AditionalInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Performs output calculation for this neuron.
        /// </summary>
        void CalculateOutput();
    }
}
