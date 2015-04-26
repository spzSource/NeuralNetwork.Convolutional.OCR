using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;
using DigitR.Core.NeuralNetwork.Primitives;

using NetworkInterface = DigitR.Core.NeuralNetwork.IMultiLayerNeuralNetwork<double[], double[]>;
using TrainingPatternInterface = DigitR.Core.InputProvider.IInputTrainingPattern<double[], double[]>;

namespace DigitR.Core.NeuralNetwork.Cnn
{
    public class CnnNeuralNetwork : IMultiLayerNeuralNetwork<double[], double[]>
    {
        private readonly IReadOnlyCollection<ILayer<object>> layers;
        private readonly ITrainingAlgorithm<NetworkInterface, TrainingPatternInterface> trainingAlgorithm;

        public CnnNeuralNetwork(
            IReadOnlyCollection<CnnLayer> layers,
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
            this.layers = new ReadOnlyCollection<ILayer<object>>(
                layers.Cast<ILayer<object>>().ToList());
            this.trainingAlgorithm = trainingAlgorithm;
        }

        /// <summary>
        /// All layers.
        /// </summary>
        public IReadOnlyCollection<ILayer<object>> Layers
        {
            get
            {
                return layers;
            }
        }

        public void ProcessTraining(IEnumerable<IInputTrainingPattern<double[], double[]>> patterns)
        {
            trainingAlgorithm.ProcessTraining(this, patterns);
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
