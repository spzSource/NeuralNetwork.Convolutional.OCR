using System;
using System.Collections.Generic;

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Primitives;

using NetworkInterface = DigitR.Core.NeuralNetwork.IMultiLayerNeuralNetwork<double>;
using TrainingPatternInterface = DigitR.Core.InputProvider.IInputTrainingPattern<double[], double[]>;

namespace DigitR.Core.NeuralNetwork.Cnn
{
    public class CnnNeuralNetwork : IMultiLayerNeuralNetwork<double>
    {
        private readonly IReadOnlyCollection<ILayer<INeuron<double>>> layers;
        private readonly ITrainingAlgorithm<NetworkInterface, TrainingPatternInterface> trainingAlgorithm;

        public CnnNeuralNetwork(
            IReadOnlyCollection<ILayer<INeuron<double>>> layers,
            ITrainingAlgorithm<NetworkInterface, TrainingPatternInterface> trainingAlgorithm)
        {
            if (layers == null)
            {
                throw new ArgumentNullException("layers");
            }
            if (trainingAlgorithm == null)
            {
                throw new ArgumentNullException("trainingAlgorithm");
            }
            this.layers = layers;
            this.trainingAlgorithm = trainingAlgorithm;
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

        public bool ProcessTraining(IEnumerable<TrainingPatternInterface> patterns)
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
