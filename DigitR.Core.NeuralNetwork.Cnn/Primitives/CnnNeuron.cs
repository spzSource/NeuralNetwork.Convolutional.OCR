using System.Collections.Generic;

using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Primitives
{
    public class CnnNeuron : INeuron<double>
    {
        private readonly bool isBias;
        private readonly IList<IConnection<double, double>> inputConnections = 
            new List<IConnection<double, double>>();
        private readonly IList<IConnection<double, double>> outputConnections =
            new List<IConnection<double, double>>();

        public CnnNeuron(bool isBias)
        {
            this.isBias = isBias;
            
            if (isBias)
            {
                Output = 1;
            }
        }

        public IList<IConnection<double, double>> Inputs
        {
            get
            {
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
