using System;
using System.Linq;

using DigitR.Common.Logging;
using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.Extensions;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation
{
    internal class ForwardPropagateLayersStep : IPropagationStep
    {
        private readonly IActivationAlgorithm<double, double> activationAlgorithm;

        public ForwardPropagateLayersStep(IActivationAlgorithm<double, double> activationAlgorithm)
        {
            if (activationAlgorithm == null)
            {
                throw new ArgumentNullException("activationAlgorithm");
            }
            this.activationAlgorithm = activationAlgorithm;
        }

        public void Process(IMultiLayerNeuralNetwork<double> network, IInputTrainingPattern<double[], double[]> pattern)
        {
            #region 

            Log.Current.Info("Back propagation. ForwardPropagateLayersStep begin.");

            #endregion

            foreach (ILayer<INeuron<double>> layer in network.Layers.Where(layer => !layer.IsFirst))
            {
                #region 

                Log.Current.Info("Layer-{0}. Start.", layer.LayerId);

                #endregion

                foreach (INeuron<double> neuron in layer.Neurons)
                {
                    double inducedLocalArea = neuron.Inputs
                        .Sum(connection => connection.Weight.Value * connection.Neuron.Output);

                    neuron.Output = activationAlgorithm.Calculate(inducedLocalArea);
                    neuron.AdditionalInfo = new BackPropagateNeuronInfo
                    {
                        LocalGradient = Double.NaN,
                        LastInducesLocalAreaValue = inducedLocalArea
                    };

                    #region 

                    Log.Current.Info("Layer-{0} Neuron-{1} : output = {2}, inducedLocalArea = {3}",
                        layer.LayerId,
                        neuron.NeuronId,
                        neuron.Output,
                        neuron.GetNeuronInfo<BackPropagateNeuronInfo>().LastInducesLocalAreaValue);

                    #endregion
                }
            }

            #region 

            Log.Current.Info("Back propagation. ForwardPropagateLayersStep end.");

            #endregion
        }
    }
}
