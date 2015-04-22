using System;
using System.Collections.Generic;
using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn
{
    public class CnnNeuralNetwork : IMultiLayerNeuralNetwork<double[], double[]>
    {
        private readonly IOutputAlgorithm<double, CnnConnection> outputAlgorithm;
        private readonly IActivationAlgorithm<double, double> activationAlgorithm;
        private readonly ITrainingAlgorithm<IMultiLayerNeuralNetwork<double[], double[]>, IInputTrainingPattern<double[], double[]>> trainingAlgorithm;

        public CnnNeuralNetwork(
            IOutputAlgorithm<double, CnnConnection> outputAlgorithm, 
            IActivationAlgorithm<double, double> activationAlgorithm,
            ITrainingAlgorithm<IMultiLayerNeuralNetwork<double[], double[]>, 
            IInputTrainingPattern<double[], double[]>> trainingAlgorithm)
        {
            if (outputAlgorithm == null)
            {
                throw new ArgumentNullException("outputAlgorithm");
            }
            if (activationAlgorithm == null)
            {
                throw new ArgumentNullException("activationAlgorithm");
            }
            if (trainingAlgorithm == null)
            {
                throw new ArgumentNullException("trainingAlgorithm");
            }
            this.outputAlgorithm = outputAlgorithm;
            this.activationAlgorithm = activationAlgorithm;
            this.trainingAlgorithm = trainingAlgorithm;
        }

        /// <summary>
        /// All layers.
        /// </summary>
        public ILayer<object>[] Layers
        {
            get
            {
                return null;
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
