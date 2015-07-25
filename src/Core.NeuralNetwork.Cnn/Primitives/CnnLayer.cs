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
        private readonly IList<FeatureMap<INeuron<double>>> innerFeatureMaps;

        public CnnLayer(
            int layerId, 
            int sourceSize, 
            int feautureMapCount, 
            int kernelSize, 
            bool isFirst, 
            bool isLast)
        {
            LayerId = layerId;
            FeautureMapCount = feautureMapCount;
            KernelSize = kernelSize;
            IsFirst = isFirst;
            IsLast = isLast;

            Neurons = new INeuron<double>[sourceSize * sourceSize];

            for (int neuronIndex = 0; neuronIndex < Neurons.Length; neuronIndex++)
            {
                Neurons[neuronIndex] = new CnnNeuron(neuronIndex, isBias: false);
            }

            innerFeatureMaps = new List<FeatureMap<INeuron<double>>>();
        }

        public IReadOnlyList<FeatureMap<INeuron<double>>> FeatureMaps => new ReadOnlyCollection<FeatureMap<INeuron<double>>>(innerFeatureMaps);

        public INeuron<double>[] Neurons
        {
            get;
        }

        public int LayerId
        {
            get;
        }

        public int FeautureMapCount
        {
            get;
            set;
        }

        public int KernelSize { get; set; }

        public bool IsFirst
        {
            get;
        }

        public bool IsLast
        {
            get;
        }

        public void AddFeatureMap(FeatureMap<INeuron<double>> featureMap)
        {
            innerFeatureMaps.Add(featureMap);
        }

        public void ConnectToLayer<TScheme>(ILayer<INeuron<double>> layer)
            where TScheme : IConnectionScheme<INeuron<double>>, new()
        {
            new TScheme().Apply(this, layer);
        }
    }
}
