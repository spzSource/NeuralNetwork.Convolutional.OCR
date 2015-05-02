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
            foreach (IConnection<double, double> connection in neuron.Inputs)
            {
                if (connection.Neuron.IsBiasNeuron)
                {
                    int i = 0;
                }

                double weightCorrection = trainingSpeed 
                    * neuron.GetNeuronInfo<BackPropagateNeuronInfo>().LocalGradient
                    * connection.Neuron.Output;

                if (connection.Weight.AdditionalInfo == null)
                {
                    connection.Weight.AdditionalInfo = new BackPropagateWeightInfo
                    {
                        WeightCorrection = weightCorrection
                    };
                }
                else
                {
                    connection.Weight.GetInfo<BackPropagateWeightInfo>().WeightCorrection += weightCorrection;
                }
            }
        }
    }
}
