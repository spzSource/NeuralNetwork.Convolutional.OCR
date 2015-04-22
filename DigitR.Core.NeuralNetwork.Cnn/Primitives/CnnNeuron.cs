using System.Collections.Generic;

using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Primitives
{
    public class CnnNeuron : INeuron<double>
    {
        private readonly bool isBias;
        private readonly IList<IConnection<double, double>> connections = 
            new List<IConnection<double, double>>();

        public CnnNeuron(bool isBias)
        {
            this.isBias = isBias;
            
            if (isBias)
            {
                connections = null;
                Output = 1;
            }
        }

        public IList<IConnection<double, double>> Inputs
        {
            get
            {
                return connections;
            }
        }

        public double Output
        {
            get;
            set;
        }

        public bool IsBiasNeuron
        {
            get
            {
                return isBias;
            }
        }

        public object AditionalInfo
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
