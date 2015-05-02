using DigitR.Common.Logging;
using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.Extensions;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation.Steps.Implementation
{
    internal class SetNetworkInputStep : IPropagationStep
    {
        public void Process(
            IMultiLayerNeuralNetwork<double> network, 
            IInputTrainingPattern<double[], double[]> pattern)
        {
            #region 

            Log.Current.Info("Back propagation. SetNetworkInputStep begin.");

            #endregion

            ILayer<INeuron<double>> inputLayer = network.GetLayer(layer => layer.IsFirst);

            for (int neuronIndex = 0; neuronIndex < inputLayer.Neurons.Length; neuronIndex++)
            {
                inputLayer.Neurons[neuronIndex].Output = pattern.Source[neuronIndex];

                #region 

                Log.Current.Info("Layer-1. Neuron-{0} : output = {1}.",
                    neuronIndex,
                    inputLayer.Neurons[neuronIndex].Output);

                #endregion
            }

            #region 

            Log.Current.Info("Back propagation. SetNetworkInputStep end.");

            #endregion
        }
    }
}
