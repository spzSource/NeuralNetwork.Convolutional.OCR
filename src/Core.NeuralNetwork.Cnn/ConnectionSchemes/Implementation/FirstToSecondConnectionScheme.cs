using System.Collections.Generic;
using System.Collections.ObjectModel;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Primitives;
using DigitR.NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.WeightsSigning;
using DigitR.NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.WeightsSigning.Implementation;
using DigitR.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Common;
using DigitR.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Enumerators;
using DigitR.NeuralNetwork.Cnn.Primitives;

namespace DigitR.NeuralNetwork.Cnn.ConnectionSchemes.Implementation
{
    public class FirstToSecondConnectionScheme : IConnectionScheme<INeuron<double>>
    {
        private const int Step = 2;
        private const int KernelSize = 5;
        private const int FeatureMapCount = 6;
        private const int SourceSizeForFirstLayer = 29;

        private readonly IWeightSigner<double> weightSigner = new NormalWeightSigner();
        private readonly IBiasAssignee biasAssignee = new CnnBiasAssignee();
        
        public void Apply(
            ILayer<INeuron<double>> leftLayer,
            ILayer<INeuron<double>> rightLayer)
        {
            FeatureMapWeightsCreator featureMapWeightsCreator = new FeatureMapWeightsCreator(weightSigner);

            int rightLayerNeuronIndex = 0;

            for (int featureMapIndex = 0; featureMapIndex < FeatureMapCount; featureMapIndex++)
            {
                FeatureMap<INeuron<double>> featureMap = new FeatureMap<INeuron<double>>();

                FeatureMapEnumerator featureMapEnumerator = new FeatureMapEnumerator(
                    Step,
                    KernelSize,
                    SourceSizeForFirstLayer,
                    new ReadOnlyCollection<INeuron<double>>(leftLayer.Neurons));


                CnnWeight biasWeight = featureMapWeightsCreator.CreateWeight();
                CnnWeight[] weights = featureMapWeightsCreator.CreateWeights(KernelSize * KernelSize);

                while (featureMapEnumerator.MoveNext())
                {
                    INeuron<double> currentRightNeuron = rightLayer.Neurons[rightLayerNeuronIndex];

                    IReadOnlyList<INeuron<double>> kernelNeurons = featureMapEnumerator.Current;

                    biasAssignee.Assign(currentRightNeuron, biasWeight);
                    
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
            }
        }
    }
}
