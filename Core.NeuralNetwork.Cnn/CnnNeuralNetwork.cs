using System;
using System.Collections.Generic;

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn
{
    [Serializable]
    public class CnnNeuralNetwork : IMultiLayerNeuralNetwork<double>
    {
        private readonly IReadOnlyCollection<ILayer<INeuron<double>>> layers;

        public CnnNeuralNetwork(
            IReadOnlyCollection<ILayer<INeuron<double>>> layers)
        {
            if (layers == null)
            {
                throw new ArgumentNullException("layers");
            }
            this.layers = layers;
        }

        /// <summary>
        /// All layers.
        /// </summary>
        public IReadOnlyCollection<ILayer<INeuron<double>>> Layers
        {
            get
            {
                return layers;
            }
        }

        public bool ProcessTraining(
            IEnumerable<IInputTrainingPattern<double[]>> patterns,
            ITrainingAlgorithm<INeuralNetwork<double[]>, IInputTrainingPattern<double[]>> trainingAlgorithm)
        {
            return trainingAlgorithm.ProcessTraining(this, patterns);
        }

        /// <summary>
        /// Provides a determination logic according to input pattern 
        /// and current state of this instance of neuran network.
        /// </summary>
        /// <param name="inputPattern">The input pattern for determine.</param>
        /// <returns>The result successful flag.</returns>
        public double[] Process(IInputPattern<double[]> inputPattern)
        {
            throw new NotImplementedException();
        }
    }
}
