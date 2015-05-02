using System.Collections.Generic;

using DigitR.Common.Logging;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.WeightsSigning;
using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Common;
using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Enumerators;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation
{
    internal class SecondToThirdConnectionScheme : IConnectionScheme<INeuron<double>>
    {
        private const int Step = 2;

        private readonly int source2DSize;
        private readonly int featureMapCount;
        private readonly int kernelSize;
        private readonly NeuronsPerFeatureMapCounter neuronsPerFeatureMapCounter;
        private readonly IWeightSigner<double> weightSigner;
        private readonly ConnectionsCounter connectionsCounter;
        private readonly IBiasAssignee biasAssignee;

        public SecondToThirdConnectionScheme(
            int source2DSize,
            int featureMapCount,
            int kernelSize,
            NeuronsPerFeatureMapCounter neuronsPerFeatureMapCounter,
            IWeightSigner<double> weightSigner,
            ConnectionsCounter connectionsCounter,
            IBiasAssignee biasAssignee)
        {
            this.source2DSize = source2DSize;
            this.featureMapCount = featureMapCount;
            this.kernelSize = kernelSize;
            this.neuronsPerFeatureMapCounter = neuronsPerFeatureMapCounter;
            this.weightSigner = weightSigner;
            this.connectionsCounter = connectionsCounter;
            this.biasAssignee = biasAssignee;
        }

        public void Apply(
            ILayer<INeuron<double>> leftLayer,
            ILayer<INeuron<double>> rightLayer)
        {
            int rightLayerNeuronIndex = 0;

            ConnectionsCounter innerConnectionsCounter = new ConnectionsCounter(0);

            for (int featureMapIndex = 0; featureMapIndex < featureMapCount; featureMapIndex++)
            {
                CnnLayer cnnLeftLayer = (CnnLayer)leftLayer;

                FeatureMapWeightsCreator weightsCreator = new FeatureMapWeightsCreator(weightSigner);

                CnnWeight[] biasWeights = weightsCreator.CreateWeights(featureMapCount);
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

                            connectionsCounter.Increment();
                            innerConnectionsCounter.Increment();
                        }
                    }
                    ++rightLayerNeuronIndex;
                }
            }
            Log.Current.Info("The total number of created connections between second and third layers.");
        }

        private FeatureMapEnumerator[] CreateFeatureMapEnumerators(CnnLayer cnnLeftLayer)
        {
            FeatureMapEnumerator[] featureMapEnumerators = new FeatureMapEnumerator[cnnLeftLayer.FeatureMaps.Count];

            for (int enumeratorIndex = 0; enumeratorIndex < cnnLeftLayer.FeatureMaps.Count; enumeratorIndex++)
            {
                featureMapEnumerators[enumeratorIndex] = new FeatureMapEnumerator(
                    Step,
                    kernelSize,
                    source2DSize,
                    cnnLeftLayer.FeatureMaps[enumeratorIndex].Neurons);
            }

            return featureMapEnumerators;
        }

        private CnnWeight[][] CreateWeights(CnnLayer cnnLeftLayer, FeatureMapWeightsCreator weightsCreator)
        {
            CnnWeight[][] weights = new CnnWeight[cnnLeftLayer.FeatureMaps.Count][];

            for (int weightIndex = 0; weightIndex < cnnLeftLayer.FeatureMaps.Count; weightIndex++)
            {
                weights[weightIndex] = weightsCreator.CreateWeights(kernelSize * kernelSize);
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
