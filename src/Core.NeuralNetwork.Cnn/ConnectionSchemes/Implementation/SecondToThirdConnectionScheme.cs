using System.Collections.Generic;

using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Common;
using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Enumerators;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;
using DigitR.Core.NeuralNetwork.Primitives;

using NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.WeightsSigning;
using NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.WeightsSigning.Implementation;

namespace DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation
{
    public class SecondToThirdConnectionScheme : IConnectionScheme<INeuron<double>>
    {
        private const int Step = 2;
        private const int KernelSize = 5;
        private const int FeatureMapCount = 50;
        private const int SourceSizeForSecondLayer = 13;

        private readonly IWeightSigner<double> weightSigner = new NormalWeightSigner();
        private readonly IBiasAssignee biasAssignee = new CnnBiasAssignee();

        public void Apply(
            ILayer<INeuron<double>> leftLayer,
            ILayer<INeuron<double>> rightLayer)
        {
            int rightLayerNeuronIndex = 0;

            ConnectionsCounter innerConnectionsCounter = new ConnectionsCounter();

            for (int featureMapIndex = 0; featureMapIndex < FeatureMapCount; featureMapIndex++)
            {
                CnnLayer cnnLeftLayer = (CnnLayer)leftLayer;

                FeatureMapWeightsCreator weightsCreator = new FeatureMapWeightsCreator(weightSigner);

                CnnWeight[] biasWeights = weightsCreator.CreateWeights(FeatureMapCount);
                CnnWeight[][] weights = CreateWeights(cnnLeftLayer, weightsCreator);

                FeatureMapEnumerator[] featureMapEnumerators = CreateFeatureMapEnumerators(cnnLeftLayer);

                while (MoveNext(featureMapEnumerators))
                {
                    INeuron<double> currentRightNeuron = rightLayer.Neurons[rightLayerNeuronIndex];

                    biasAssignee.Assign(currentRightNeuron, biasWeights[featureMapIndex]);

                    for (int enumeratorIndex = 0; enumeratorIndex < cnnLeftLayer.FeatureMaps.Count; enumeratorIndex++)
                    {
                        IReadOnlyList<INeuron<double>> kernelNeurons = featureMapEnumerators[enumeratorIndex].Current;

                        for (int kernelNeuronIndex = 0; kernelNeuronIndex < kernelNeurons.Count; kernelNeuronIndex++)
                        {
                            currentRightNeuron.Inputs.Add(
                                new CnnConnection(
                                    kernelNeurons[kernelNeuronIndex],
                                    weights[enumeratorIndex][kernelNeuronIndex]));

                            kernelNeurons[kernelNeuronIndex].Outputs.Add(
                                new CnnConnection(
                                    currentRightNeuron,
                                    weights[enumeratorIndex][kernelNeuronIndex]));

                            innerConnectionsCounter.Increment();
                        }
                    }
                    ++rightLayerNeuronIndex;
                }
            }
        }

        private FeatureMapEnumerator[] CreateFeatureMapEnumerators(CnnLayer cnnLeftLayer)
        {
            FeatureMapEnumerator[] featureMapEnumerators = new FeatureMapEnumerator[cnnLeftLayer.FeatureMaps.Count];

            for (int enumeratorIndex = 0; enumeratorIndex < cnnLeftLayer.FeatureMaps.Count; enumeratorIndex++)
            {
                featureMapEnumerators[enumeratorIndex] = new FeatureMapEnumerator(
                    Step,
                    KernelSize,
                    SourceSizeForSecondLayer,
                    cnnLeftLayer.FeatureMaps[enumeratorIndex].Neurons);
            }

            return featureMapEnumerators;
        }

        private CnnWeight[][] CreateWeights(CnnLayer cnnLeftLayer, FeatureMapWeightsCreator weightsCreator)
        {
            CnnWeight[][] weights = new CnnWeight[cnnLeftLayer.FeatureMaps.Count][];

            for (int weightIndex = 0; weightIndex < cnnLeftLayer.FeatureMaps.Count; weightIndex++)
            {
                weights[weightIndex] = weightsCreator.CreateWeights(KernelSize * KernelSize);
            }

            return weights;
        }

        private bool MoveNext(FeatureMapEnumerator[] featureMapEnumerators)
        {
            bool result = true;

            foreach (FeatureMapEnumerator featureMapEnumerator in featureMapEnumerators)
            {
                result = result && featureMapEnumerator.MoveNext();
            }

            return result;
        }
    }
}
