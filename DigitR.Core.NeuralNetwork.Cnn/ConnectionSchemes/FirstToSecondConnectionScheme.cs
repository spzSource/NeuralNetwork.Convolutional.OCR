using System.Collections.Generic;
using System.Collections.ObjectModel;

using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Enumerators;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes
{
    internal class FirstToSecondConnectionScheme : IConnectionScheme<CnnNeuron>
    {
        private readonly int source2DSize;
        private readonly int featureMapCount;
        private readonly int neuronsPerKernel;

        public FirstToSecondConnectionScheme(
            int source2DSize,
            int featureMapCount,
            int neuronsPerKernel)
        {
            this.source2DSize = source2DSize;
            this.featureMapCount = featureMapCount;
            this.neuronsPerKernel = neuronsPerKernel;
        }

        public void Apply(
            IList<CnnNeuron> leftNeurons,
            IList<CnnNeuron> rightNeurons)
        {
            for (int featureMapIndex = 0; featureMapIndex < featureMapCount; featureMapIndex++)
            {
                CnnWeight[] weights = CreateWeights(neuronsPerKernel);

                FeatureMapEnumerator featureMapEnumerator = new FeatureMapEnumerator(
                    source2DSize,
                    new ReadOnlyCollection<CnnNeuron>(leftNeurons));

                int rightLayerNeuronIndex = 0;

                while (featureMapEnumerator.MoveNext())
                {
                    IReadOnlyList<CnnNeuron> kernelNeurons = featureMapEnumerator.Current;

                    CnnNeuron currentRightNeuron = rightNeurons[rightLayerNeuronIndex];

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
                }
            }
        }

        private CnnWeight[] CreateWeights(int weightsCount)
        {
            CnnWeight[] weights = new CnnWeight[weightsCount];

            for (int weightIndex = 0; weightIndex < weights.Length; weightIndex++)
            {
                weights[weightIndex] = new CnnWeight { AdditionalInfo = null, Value = 0.0 };
            }

            return weights;
        }
    }
}
