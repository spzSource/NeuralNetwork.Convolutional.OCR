using System;

using DigitR.Core.NeuralNetwork.Primitives;

using QuickGraph;

namespace DigitR.NeuralNetwork.Cnn.View.Elements
{
    public class NeuronEdge : Edge<NeuronVertex>
    {
        private readonly IConnection<double, double> connection;

        public NeuronEdge(
            NeuronVertex source, 
            NeuronVertex target,
            IConnection<double, double> connection) 
                : base(source, target)
        {
            this.connection = connection;
        }

        public override string ToString()
        {
            return String.Format("Source : (Layer id = {0}, Neuron id = {1}) <=> Target : (Layer id = {2}, Neuron id = {3})",
                Source.LayerId,
                Source.NeuronId,
                Target.LayerId,
                Target.NeuronId); 
        }
    }
}
