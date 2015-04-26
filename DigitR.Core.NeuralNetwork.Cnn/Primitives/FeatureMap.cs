using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DigitR.Core.NeuralNetwork.Cnn.Primitives
{
    public class FeatureMap<TNeuron>
    {
        private readonly IList<TNeuron> internalNeurons;

        public FeatureMap()
        {
            internalNeurons = new List<TNeuron>();
        }

        public IReadOnlyCollection<TNeuron> Neurons
        {
            get
            {
                return new ReadOnlyCollection<TNeuron>(internalNeurons);
            }
        }

        public void AddNeuron(TNeuron neuron)
        {
            internalNeurons.Add(neuron);
        }
    }
}