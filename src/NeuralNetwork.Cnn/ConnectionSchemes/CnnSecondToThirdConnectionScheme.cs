using System.Collections.Generic;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;
using DigitR.NeuralNetwork.Cnn.Algorithms.WeightsSigning;
using DigitR.NeuralNetwork.Cnn.Factories;
using DigitR.NeuralNetwork.Cnn.Primitives;

namespace DigitR.NeuralNetwork.Cnn.ConnectionSchemes
{
    public class CnnSecondToThirdConnectionScheme : 
        IConnectionScheme<
            INeuron<double>, 
            IConnectionFactory<double, double>>
    {
        private const int Step = 2;
        private const int KernelSize = 5;
        private const int FeatureMapCount = 50;
        private const int SourceSizeForSecondLayer = 13;

        private IConnectionFactory<double, double> connectionFactory; 
        private readonly IWeightFactory<double> weightFactory = new CnnWeightFactory(new NormalWeightSigner()); 

        public void SetConnectionFactory(IConnectionFactory<double, double> factory)
        {
            connectionFactory = factory;
        }

        public void Apply(ILayer<INeuron<double>, IConnectionFactory<double, double>> leftLayer, ILayer<INeuron<double>, IConnectionFactory<double, double>> rightLayer)
        {
            int rightLayerNeuronIndex = 0;

            for (int featureMapIndex = 0; featureMapIndex < FeatureMapCount; featureMapIndex++)
            {
                CnnLayer cnnLeftLayer = (CnnLayer)leftLayer;

                IWeight<double>[][] weights = CreateWeights(cnnLeftLayer, weightFactory);

                FeatureMapEnumerator[] featureMapEnumerators = CreateFeatureMapEnumerators(cnnLeftLayer);

                while (MoveNext(featureMapEnumerators))
                {
                    INeuron<double> currentRightNeuron = rightLayer.Neurons[rightLayerNeuronIndex];

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

        private IWeight<double>[][] CreateWeights(CnnLayer cnnLeftLayer, IWeightFactory<double> weightFactory)
        {
            IWeight<double>[][] weights = new IWeight<double>[cnnLeftLayer.FeatureMaps.Count][];

            for (int weightIndex = 0; weightIndex < cnnLeftLayer.FeatureMaps.Count; weightIndex++)
            {
                weights[weightIndex] = weightFactory.CreateMany(KernelSize * KernelSize);
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
