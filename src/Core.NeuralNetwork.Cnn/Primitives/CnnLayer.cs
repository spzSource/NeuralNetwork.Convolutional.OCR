using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Primitives
{
    [Serializable]
    [DebuggerDisplay("Layer-{LayerId}")]
    public class CnnLayer : ILayer<INeuron<double>>
    {
        private readonly int layerId;
        private readonly bool isFirst;
        private readonly bool isLast;

        private readonly CnnNeuron[] neurons;
        private readonly IList<FeatureMap<INeuron<double>>> innerFeatureMaps;

        public CnnLayer(int layerId, int neuronsCount, bool isFirst, bool isLast)
        {
            this.layerId = layerId;
            this.isFirst = isFirst;
            this.isLast = isLast;

            neurons = new CnnNeuron[neuronsCount];

            for (int neuronIndex = 0; neuronIndex < neurons.Length; neuronIndex++)
            {
                neurons[neuronIndex] = new CnnNeuron(neuronIndex, isBias: false);
            }    
            
            innerFeatureMaps = new List<FeatureMap<INeuron<double>>>();
        }

        public int LayerId
        {
            get
            {
                return layerId;
            }
        }

        public bool IsFirst
        {
            get
            {
                return isFirst;
            }
        }

        public bool IsLast
        {
            get
            {
                return isLast;
            }
        }

        public INeuron<double>[] Neurons
        {
            get
            {
                return neurons;
            }
        }

        public IReadOnlyList<FeatureMap<INeuron<double>>> FeatureMaps
        {
            get
            {
                return new ReadOnlyCollection<FeatureMap<INeuron<double>>>(innerFeatureMaps);
            }
        }

        public void AddFeatureMap(FeatureMap<INeuron<double>> featureMap)
        {
            innerFeatureMaps.Add(featureMap);
        }

        public void Calculate()
        {
        }

        public void ConnectToLayer(
            ILayer<INeuron<double>> layer,
            IConnectionScheme<INeuron<double>> connectionScheme)
        {
            connectionScheme.Apply(this, layer);
        }
    }
}
