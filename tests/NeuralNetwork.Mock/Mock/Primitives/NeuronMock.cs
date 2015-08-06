using System.Collections.Generic;

using DigitR.Core.NeuralNetwork.Primitives;

namespace Tests.NeuralNetwork.Mock.Mock.Primitives
{
    public class NeuronMock : INeuron<double>
    {
        public NeuronMock(int neuronId, bool isBias)
        {
            NeuronId = neuronId;
            IsBiasNeuron = isBias;

            Inputs = new List<IConnection<double, double>>();
            Outputs = new List<IConnection<double, double>>();
        }

        public int NeuronId
        {
            get;
        }

        public IList<IConnection<double, double>> Inputs
        {
            get;
        }

        public IList<IConnection<double, double>> Outputs
        {
            get;
        }

        public double Output
        {
            get;
            set;
        }

        public bool IsBiasNeuron
        {
            get;
        }

        public object AdditionalInfo
        {
            get;
            set;
        }
    }
}