using System;
using System.Linq;

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Primitives;
using DigitR.NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.BackPropagation.Common;

namespace DigitR.NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.BackPropagation.Steps.Implementation
{
    internal class ForwardPropagateLayersStep : IPropagationStep
    {
        private readonly IActivationAlgorithm<double, double> activationAlgorithm;

        public ForwardPropagateLayersStep(IActivationAlgorithm<double, double> activationAlgorithm)
        {
            this.activationAlgorithm = activationAlgorithm;
        }

        public void Process(IMultiLayerNeuralNetwork<double> network, IInputTrainingPattern<double[]> pattern)
        {
            foreach (ILayer<INeuron<double>> layer in network.Layers.Where(layer => !layer.IsFirst))
            {
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
                }
            }
        }
    }
}
