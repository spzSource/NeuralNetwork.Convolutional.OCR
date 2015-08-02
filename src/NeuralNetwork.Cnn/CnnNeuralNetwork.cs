using System;
using System.Collections.Generic;
using System.Threading;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.InputProvider;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.NeuralNetwork.Cnn
{
    [Serializable]
    public class CnnNeuralNetwork : IMultiLayerNeuralNetwork<double>
    {
        private readonly IReadOnlyCollection<ILayer<INeuron<double>, IConnectionFactory<double, double>>> layers;

        public CnnNeuralNetwork(
            IReadOnlyCollection<ILayer<INeuron<double>, IConnectionFactory<double, double>>> layers)
        {
            if (layers == null)
            {
                throw new ArgumentNullException(nameof(layers));
            }
            this.layers = layers;
        }

        /// <summary>
        /// All layers.
        /// </summary>
        public IReadOnlyCollection<ILayer<INeuron<double>, IConnectionFactory<double, double>>> Layers => layers;

        public bool ProcessTraining(
            IEnumerable<IInputTrainingPattern<double[]>> patterns,
            ITrainingAlgorithm<INeuralNetwork<double>, IInputTrainingPattern<double[]>> trainingAlgorithm,
            CancellationToken cancellationToken)
        {
            return trainingAlgorithm.ProcessTraining(this, patterns, cancellationToken);
        }

        /// <summary>
        /// Provides a determination logic according to input pattern 
        /// and current state of this instance of neuran network.
        /// </summary>
        /// <param name="inputPattern">The input pattern for determine.</param>
        /// <param name="processingAlgorithm"></param>
        /// <returns>The result successful flag.</returns>
        public double[] Process(
            IInputPattern<double[]> inputPattern,
            IProcessingAlgorithm<INeuralNetwork<double>, IInputPattern<double[]>> processingAlgorithm)
        {
            return processingAlgorithm.Process(this, inputPattern);
        }
    }
}
