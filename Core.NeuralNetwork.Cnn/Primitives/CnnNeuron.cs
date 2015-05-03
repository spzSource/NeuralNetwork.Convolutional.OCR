using System;
using System.Collections.Generic;
using System.Diagnostics;

using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Primitives
{
    [Serializable]
    [DebuggerDisplay("Neuron = {NeuronId}, Output = {Output}, isBias = {IsBiasNeuron}")]
    public class CnnNeuron : INeuron<double>
    {
        private readonly int neuronId;
        private readonly bool isBias;

        private readonly IList<IConnection<double, double>> inputConnections = 
            new List<IConnection<double, double>>();
        
        private readonly IList<IConnection<double, double>> outputConnections =
            new List<IConnection<double, double>>();

        private double output;

        public CnnNeuron(
            int neuronId,
            bool isBias)
        {
            this.neuronId = neuronId;
            this.isBias = isBias;
            
            if (isBias)
            {
                output = 1;
            }
        }

        public int NeuronId
        {
            get
            {
                return neuronId;
            }
        }

        public IList<IConnection<double, double>> Inputs
        {
            get
            {
                if (isBias)
                {
                    throw new NotSupportedException("Bias neuron does not support input connections.");
                }
                return inputConnections;
            }
        }

        public IList<IConnection<double, double>> Outputs
        {
            get
            {
                return outputConnections;
            }
        }

        public double Output
        {
            get
            {
                return output;
            }
            set
            {
                if (isBias)
                {
                    throw new NotSupportedException("Bias neuron does not support changing the output.");
                }
                output = value;
            }
        }

        public bool IsBiasNeuron
        {
            get
            {
                return isBias;
            }
        }

        public object AdditionalInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Performs output calculation for this neuron.
        /// </summary>
        public void CalculateOutput()
        {
        }
    }
}
