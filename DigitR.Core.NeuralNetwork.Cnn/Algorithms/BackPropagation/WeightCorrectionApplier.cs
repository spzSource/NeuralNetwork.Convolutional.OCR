using DigitR.Core.NeuralNetwork.Cnn.Algorithms.Extensions;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation
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
            foreach (IConnection<double, double> connections in neuron.Inputs)
            {
                double weightCorrection = trainingSpeed 
                    * neuron.GetNeuronInfo<BackPropagateNeuronInfo>().LocalGradient
                    * connections.Neuron.Output;

                if (connections.Weight.AdditionalInfo == null)
                {
                    connections.Weight.AdditionalInfo = new BackPropagateWeightInfo
                    {
                        WeightCorrection = weightCorrection
                    };
                }
                else
                {
                    connections.Weight.GetInfo<BackPropagateWeightInfo>().WeightCorrection += weightCorrection;
                }
            }
        }
    }
}
