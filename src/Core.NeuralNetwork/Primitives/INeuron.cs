using System.Collections.Generic;

namespace DigitR.Core.NeuralNetwork.Primitives
{
    /// <summary>
    /// Provides an interface for abstract neuron.
    /// </summary>
    public interface INeuron<TValue>
    {
        /// <summary>
        /// The identifier of this neuron.
        /// Should be uniqe in scope of layer only.
        /// </summary>
        int NeuronId
        {
            get;
        }

        /// <summary>
        /// Set of input connections for this neuron.
        /// </summary>
        IList<IConnection<TValue, TValue>> Inputs
        {
            get;
        }

        /// <summary>
        /// Set of output connections for this neuron.
        /// </summary>
        IList<IConnection<TValue, TValue>> Outputs
        {
            get;
        }
            
        /// <summary>
        /// The value of output for this neuron.
        /// </summary>
        TValue Output
        {
            get;
            set;
        }

        /// <summary>
        /// Determines whether is a bias neuron.
        /// </summary>
        bool IsBiasNeuron
        {
            get;
        }

        /// <summary>
        /// The additional information for this neuron.
        /// This property may be used to store a additional data during training process for example.
        /// </summary>
        object AdditionalInfo
        {
            get;
            set;
        }
    }
}
