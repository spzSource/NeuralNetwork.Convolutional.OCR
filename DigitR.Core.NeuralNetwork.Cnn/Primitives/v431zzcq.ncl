using System.Collections.Generic;

using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Primitives
{
    public class CnnNeuron : INeuron<double, CnnConnection>
    {
        private readonly IList<CnnConnection> connections = new List<CnnConnection>(); 

        public IList<CnnConnection> Inputs
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

        /// <summary>
        /// Performs output calculation for this neuron.
        /// </summary>
        public double Calculate()
        {
            throw new System.NotImplementedException();
        }
    }
}
