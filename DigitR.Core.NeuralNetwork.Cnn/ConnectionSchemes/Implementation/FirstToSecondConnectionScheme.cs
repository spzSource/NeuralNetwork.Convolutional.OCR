using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

using DigitR.Core.NeuralNetwork.Cnn.Algorithms.WeightsSigning;
using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Common;
using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Enumerators;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation
{
    internal class FirstToSecondConnectionScheme : IConnectionScheme<INeuron<double>>
    {
        private const int Step = 2;

        private readonly int source2DSize;
        private readonly int featureMapCount;
        private readonly int kernelSize;
        private readonly NeuronsPerFeatureMapCounter neuronsPerFeatureMapCounter;
        private readonly IWeightSigner<double> weightSigner;
        private readonly ConnectionsCounter connectionsCounter;

        public FirstToSecondConnectionScheme(
            int source2DSize,
            int featureMapCount,
            int kernelSize,
            NeuronsPerFeatureMapCounter neuronsPerFeatureMapCounter,
            IWeightSigner<double> weightSigner,
            ConnectionsCounter connectionsCounter)
        {
            this.source2DSize = source2DSize;
            this.featureMapCount = featureMapCount;
            this.kernelSize = kernelSize;
            this.neuronsPerFeatureMapCounter = neuronsPerFeatureMapCounter;
            this.weightSigner = weightSigner;
            this.connectionsCounter = connectionsCounter;
        }

        public void Apply(
            ILayer<INeuron<double>> leftLayer,
            ILayer<INeuron<double>> rightLayer)
        {
            FeatureMapWeightsCreator featureMapWeightsCreator = new FeatureMapWeightsCreator(weightSigner);

            int rightLayerNeuronIndex = 0;

            for (int featureMapIndex = 0; featureMapIndex < featureMapCount; featureMapIndex++)
            {
                FeatureMap<INeuron<double>> featureMap = new FeatureMap<INeuron<double>>();
                
                FeatureMapEnumerator featureMapEnumerator = new FeatureMapEnumerator(
                    Step,
                    kernelSize,
                    source2DSize,
                    new ReadOnlyCollection<INeuron<double>>(leftLayer.Neurons));

                CnnWeight[] weights = featureMapWeightsCreator.CreateWeights(kernelSize * kernelSize + 1);

                while (featureMapEnumerator.MoveNext())
                {
                    IReadOnlyList<INeuron<double>> kernelNeurons = featureMapEnumerator.Current;

                    INeuron<double> currentRightNeuron = rightLayer.Neurons[rightLayerNeuronIndex];
                    
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

                        connectionsCounter.Increment();
                    }

                    ++rightLayerNeuronIndex;

                    featureMap.AddNeuron(currentRightNeuron);
                }

                ((CnnLayer)rightLayer).AddFeatureMap(featureMap);

                int expectedCount = neuronsPerFeatureMapCounter.Count(source2DSize, kernelSize, Step);

                Contract.Assert(expectedCount == featureMap.Neurons.Count,
                    String.Format("Connection scheme {{first-to-second}}. Wrong number of neuron inputs. Possible wrong implementation of enumeartor (type: {0}).",
                        featureMapEnumerator.GetType().Name));
            }
        }

    }
}
