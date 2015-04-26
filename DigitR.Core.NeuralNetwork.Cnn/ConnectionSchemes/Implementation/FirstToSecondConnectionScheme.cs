using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Common;
using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Enumerators;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation
{
    internal class FirstToSecondConnectionScheme : IConnectionScheme<CnnNeuron>
    {
        private const int Step = 2;

        private readonly int source2DSize;
        private readonly int featureMapCount;
        private readonly int kernelSize;
        private readonly NeuronsPerFeatureMapCounter neuronsPerFeatureMapCounter;

        public FirstToSecondConnectionScheme(
            int source2DSize,
            int featureMapCount,
            int kernelSize,
            NeuronsPerFeatureMapCounter neuronsPerFeatureMapCounter)
        {
            this.source2DSize = source2DSize;
            this.featureMapCount = featureMapCount;
            this.kernelSize = kernelSize;
            this.neuronsPerFeatureMapCounter = neuronsPerFeatureMapCounter;
        }

        public void Apply(
            ILayer<CnnNeuron> leftLayer,
            ILayer<CnnNeuron> rightLayer)
        {
            FeatureMapWeightsCreator featureMapWeightsCreator = new FeatureMapWeightsCreator();

            FeatureMapEnumerator featureMapEnumerator = new FeatureMapEnumerator(
                Step,
                kernelSize,
                source2DSize,
                new ReadOnlyCollection<CnnNeuron>(leftLayer.Neurons));

            for (int featureMapIndex = 0; featureMapIndex < featureMapCount; featureMapIndex++)
            {
                FeatureMap<CnnNeuron> featureMap = new FeatureMap<CnnNeuron>();

                CnnWeight[] weights = featureMapWeightsCreator.CreateWeights(kernelSize * kernelSize + 1);

                int rightLayerNeuronIndex = 0;

                while (featureMapEnumerator.MoveNext())
                {
                    IReadOnlyList<CnnNeuron> kernelNeurons = featureMapEnumerator.Current;

                    CnnNeuron currentRightNeuron = rightLayer.Neurons[rightLayerNeuronIndex];

                    for (int kernelNeuronIndex = 0; kernelNeuronIndex < kernelNeurons.Count; kernelNeuronIndex++)
                    {
                        currentRightNeuron.Inputs.Add(
                            new CnnConnection(
                                kernelNeurons[kernelNeuronIndex],
                                weights[kernelNeuronIndex]));

                        kernelNeurons[kernelNeuronIndex].Outputs.Add(
                            new CnnConnection(
                                currentRightNeuron,
                                weights[kernelNeuronIndex]));
                    }

                    ++rightLayerNeuronIndex;

                    featureMap.AddNeuron(currentRightNeuron);
                }

                ((CnnLayer)rightLayer).AddFeatureMap(featureMap);

                Contract.Assert(neuronsPerFeatureMapCounter.Count(source2DSize, kernelSize, Step) == featureMap.Neuron.Count,
                    String.Format("Connection scheme {{first-to-second}}. Wrong number of neuron inputs. Possible wrong implementation of enumeartor (type: {0}).",
                        featureMapEnumerator.GetType().Name));
            }
        }

    }
}
