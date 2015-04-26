﻿using System.Collections.Generic;

using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Enumerators;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Common
{
    internal class FeatureMapConnector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="featureMapEnumerator"></param>
        /// <param name="featureMapWeights"></param>
        /// <param name="rightCnnLayer"></param>
        public FeatureMap<CnnNeuron> Connect(
            FeatureMapEnumerator featureMapEnumerator,
            CnnWeight[] featureMapWeights,
            ILayer<CnnNeuron> rightCnnLayer)
        {
            FeatureMap<CnnNeuron> featureMap = new FeatureMap<CnnNeuron>();

            int rightLayerNeuronIndex = 0;

            while (featureMapEnumerator.MoveNext())
            {
                IReadOnlyList<CnnNeuron> kernelNeurons = featureMapEnumerator.Current;

                CnnNeuron currentRightNeuron = rightCnnLayer.Neurons[rightLayerNeuronIndex];

                for (int kernelNeuronIndex = 0; kernelNeuronIndex < kernelNeurons.Count; kernelNeuronIndex++)
                {
                    currentRightNeuron.Inputs.Add(
                        new CnnConnection(
                            kernelNeurons[kernelNeuronIndex],
                            featureMapWeights[kernelNeuronIndex]));

                    kernelNeurons[kernelNeuronIndex].Outputs.Add(
                        new CnnConnection(
                            currentRightNeuron,
                            featureMapWeights[kernelNeuronIndex]));
                }

                ++rightLayerNeuronIndex;

                featureMap.AddNeuron(currentRightNeuron);
            }

            return featureMap;
        }
    }
}
