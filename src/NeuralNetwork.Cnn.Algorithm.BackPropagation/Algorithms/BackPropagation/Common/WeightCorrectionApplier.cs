﻿using System.Diagnostics.Contracts;

using DigitR.Core.NeuralNetwork.Primitives;
using DigitR.NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.Extensions;

namespace DigitR.NeuralNetwork.Cnn.Algorithm.BackPropagation.Algorithms.BackPropagation.Common
{
    internal class WeightCorrectionApplier
    {
        private readonly double trainingSpeed;

        public WeightCorrectionApplier(double trainingSpeed)
        {
            this.trainingSpeed = trainingSpeed;
        }

        public void Apply(INeuron<double> neuron)
        {
            Contract.Assert(neuron.Inputs.Count > 0, "Wrong number of inputs.");

            foreach (IConnection<double, double> connection in neuron.Inputs)
            {
            
                double weightCorrection = trainingSpeed 
                    * neuron.GetNeuronInfo<BackPropagateNeuronInfo>().LocalGradient
                    * connection.Neuron.Output;

                connection.Weight.Value -= weightCorrection;

                //if (connection.Weight.AdditionalInfo == null)
                //{
                //    connection.Weight.AdditionalInfo = new BackPropagateWeightInfo
                //    {
                //        WeightCorrection = weightCorrection
                //    };
                //}
                //else
                //{
                //    connection.Weight.GetInfo<BackPropagateWeightInfo>().WeightCorrection += weightCorrection;
                //}
            }
        }
    }
}