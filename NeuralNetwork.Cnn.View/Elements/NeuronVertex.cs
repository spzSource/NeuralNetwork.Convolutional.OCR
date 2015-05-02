using System.Diagnostics;

using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.NeuralNetwork.Cnn.View.Elements
{
    [DebuggerDisplay("Layet-{LayerId}, Neuron-{NeuronId}")]
    public class NeuronVertex
    {
        private readonly int layerId;
        private readonly int neuronId;
        private readonly INeuron<double> neuron;

        public NeuronVertex(
            int layerId,
            int neuronId,
            INeuron<double> neuron)
        {
            this.layerId = layerId;
            this.neuronId = neuronId;
            this.neuron = neuron;
        }

        public int LayerId
        {
            get
            {
                return layerId;
            }
        }

        public int NeuronId
        {
            get
            {
                return neuronId;
            }
        }
    }
}
