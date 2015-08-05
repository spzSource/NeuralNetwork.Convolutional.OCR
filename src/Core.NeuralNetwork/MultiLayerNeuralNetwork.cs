using System;
using System.Collections.Generic;
using System.Threading;

using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.InputProvider;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork
{
    public class MultiLayerNeuralNetwork<TData> : IMultiLayerNeuralNetwork<TData>
    {
        private readonly IReadOnlyCollection<ILayer<INeuron<TData>, IConnectionFactory<TData, TData>>> layers;

        public MultiLayerNeuralNetwork(
            IReadOnlyCollection<ILayer<INeuron<TData>, IConnectionFactory<TData, TData>>> layers)
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
        public IReadOnlyCollection<ILayer<INeuron<TData>, IConnectionFactory<TData, TData>>> Layers => layers;

        public bool ProcessTraining(
            IEnumerable<IInputTrainingPattern<TData[]>> patterns,
            ITrainingAlgorithm<INeuralNetwork<TData>, IInputTrainingPattern<TData[]>> trainingAlgorithm,
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
        public TData[] Process(
            IInputPattern<TData[]> inputPattern,
            IProcessingAlgorithm<INeuralNetwork<TData>, IInputPattern<TData[]>, TData> processingAlgorithm)
        {
            return processingAlgorithm.Process(this, inputPattern);
        }
    }
}
