using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Primitives
{
    public class CnnLayer : ILayer<CnnNeuron>
    {
        private readonly bool isFirst;
        private readonly bool isLast;

        private readonly CnnNeuron[] neurons;
        private readonly IList<FeatureMap<CnnNeuron>> innerFeatureMaps; 

        public CnnLayer(int neuronsCount, bool isFirst, bool isLast)
        {
            Contract.Requires<ArgumentException>(neuronsCount > 0);

            this.isFirst = isFirst;
            this.isLast = isLast;

            neurons = new CnnNeuron[neuronsCount + 1];
            
            neurons[0] = new CnnNeuron(isBias: true);
            for (int neuronIndex = 1; neuronIndex < neurons.Length; neuronIndex++)
            {
                neurons[neuronIndex] = new CnnNeuron(isBias: false);
            }
            innerFeatureMaps = new List<FeatureMap<CnnNeuron>>();
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

        public CnnNeuron[] Neurons
        {
            get
            {
                return neurons;
            }
        }

        public IReadOnlyList<FeatureMap<CnnNeuron>> FeatureMaps
        {
            get
            {
                return new ReadOnlyCollection<FeatureMap<CnnNeuron>>(innerFeatureMaps);
            }
        }

        public void AddFeatureMap(FeatureMap<CnnNeuron> featureMap)
        {
            innerFeatureMaps.Add(featureMap);
        }

        public void Calculate()
        {
        }

        public void ConnectToLayer(ILayer<CnnNeuron> layer, IConnectionScheme<CnnNeuron> connectionScheme)
        {
            connectionScheme.Apply(this, layer);
        }
    }
}
