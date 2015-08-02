using System.Collections.Generic;
using System.Collections.ObjectModel;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;
using DigitR.NeuralNetwork.Cnn.Algorithms.WeightsSigning;
using DigitR.NeuralNetwork.Cnn.Factories;
using DigitR.NeuralNetwork.Cnn.Primitives;

namespace DigitR.NeuralNetwork.Cnn.ConnectionSchemes
{
    public class CnnFirstToSecondConnectionScheme 
        : IConnectionScheme<
            INeuron<double>, 
            IConnectionFactory<double, double>>
    {
        private const int Step = 2;
        private const int KernelSize = 5;
        private const int FeatureMapCount = 6;
        private const int SourceSizeForFirstLayer = 29;

        private readonly IWeightFactory<double> weightFactory = new CnnWeightFactory(new NormalWeightSigner());

        private IConnectionFactory<double, double> connectionFactory;

        public void SetConnectionFactory(IConnectionFactory<double, double> factory)
        {
            connectionFactory = factory;
        }

        public void Apply(
            ILayer<INeuron<double>, IConnectionFactory<double, double>> leftLayer,
            ILayer<INeuron<double>, IConnectionFactory<double, double>> rightLayer)
        {
            int rightLayerNeuronIndex = 0;

            for (int featureMapIndex = 0; featureMapIndex < FeatureMapCount; featureMapIndex++)
            {
                FeatureMap<INeuron<double>> featureMap = new FeatureMap<INeuron<double>>();

                FeatureMapEnumerator featureMapEnumerator = new FeatureMapEnumerator(
                    Step,
                    KernelSize,
                    SourceSizeForFirstLayer,
                    new ReadOnlyCollection<INeuron<double>>(leftLayer.Neurons));


                IWeight<double>[] weights = weightFactory.CreateMany(KernelSize * KernelSize);

                while (featureMapEnumerator.MoveNext())
                {
                    INeuron<double> currentRightNeuron = rightLayer.Neurons[rightLayerNeuronIndex];

                    IReadOnlyList<INeuron<double>> kernelNeurons = featureMapEnumerator.Current;

                    for (int kernelNeuronIndex = 0; kernelNeuronIndex < kernelNeurons.Count; kernelNeuronIndex++)
                    {
                        currentRightNeuron.Inputs.Add(
                            connectionFactory.CreateWithWeight(
                                kernelNeurons[kernelNeuronIndex], 
                                weights[kernelNeuronIndex]));

                        kernelNeurons[kernelNeuronIndex].Outputs.Add(
                            connectionFactory.CreateWithWeight(
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
