using System.Collections.Generic;
using System.Linq;

using DigitR.Common.Logging;
using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Primitives;
using DigitR.NeuralNetwork.Cnn.View.Elements;

using GalaSoft.MvvmLight;

namespace DigitR.NeuralNetwork.Cnn.View.ViewModel
{
    public class NeuralNetworkGraphViewModel : ViewModelBase
    {
        private NeuralNetworkGraph graph;

        public NeuralNetworkGraphViewModel()
        {
            graph = new NeuralNetworkGraph();
        }

        public NeuralNetworkGraph NeuralNetworkGraph
        {
            get
            {
                return graph;
            }
            set
            {
                graph = value;
                RaisePropertyChanged(() => NeuralNetworkGraph);
            }
        }

        public void BuildNeuralNetworkGraph(
            IMultiLayerNeuralNetwork<double> multiLayerNeuralNetwork)
        {
            IReadOnlyCollection<ILayer<INeuron<double>>> layers = 
                multiLayerNeuralNetwork.Layers;

            NeuronVertex[][] vertexCache = CreateVertexCache(layers);

            ILayer<INeuron<double>> lastLayer = layers.First(layer => layer.IsLast);

            foreach (INeuron<double> currentNeuron in lastLayer.Neurons)
            {
                BuildNeuron(currentNeuron, 4, vertexCache);
            }
        }

        private void BuildNeuron(INeuron<double> neuron, int layerIndex,  NeuronVertex[][] cache)
        {
            bool isNewVertex;

            NeuronVertex rightVertex = GetOrCreate(cache, neuron, layerIndex, out isNewVertex);

            if (isNewVertex)
            {
                graph.AddVertex(rightVertex);
            }

            if (neuron.Inputs.Count == 0)
            {
                return;
            }

            foreach (IConnection<double, double> connection in neuron.Inputs)
            {
                INeuron<double> currentLeftNeuron = connection.Neuron;

                NeuronVertex currentLeftNeuronVertex = GetOrCreate(cache, currentLeftNeuron, layerIndex - 1, out isNewVertex);

                if (isNewVertex)
                {
                    graph.AddVertex(currentLeftNeuronVertex);

                    NeuronEdge edge = new NeuronEdge(currentLeftNeuronVertex, rightVertex, connection);

                    graph.AddEdge(edge);
                }

                BuildNeuron(currentLeftNeuron, layerIndex - 1, cache);
            }
        }

        private static NeuronVertex[][] CreateVertexCache(IReadOnlyCollection<ILayer<INeuron<double>>> layers)
        {
            NeuronVertex[][] vertexCache = new NeuronVertex[layers.Count][];
            
            for (int layerIndex = 0; layerIndex < vertexCache.Length; layerIndex++)
            {
                vertexCache[layerIndex] = new NeuronVertex[layers.ElementAt(layerIndex).Neurons.Length];
            }

            return vertexCache;
        }

        private static NeuronVertex GetOrCreate(
            NeuronVertex[][] cache, 
            INeuron<double> neuron, 
            int layerIndex,
            out bool isNew)
        {
            isNew = false;

            NeuronVertex vertex = cache[layerIndex][neuron.NeuronId];
            
            if (vertex == null)
            {
                vertex = new NeuronVertex(layerIndex, neuron.NeuronId, neuron);

                cache[layerIndex][neuron.NeuronId] = vertex;

                isNew = true;
            }

            return vertex;
        }
    }
}
