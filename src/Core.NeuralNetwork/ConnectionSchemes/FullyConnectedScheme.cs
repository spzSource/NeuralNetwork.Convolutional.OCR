using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.ConnectionSchemes
{
    public class FullyConnectedScheme<TData> : IConnectionScheme<INeuron<TData>, IConnectionFactory<TData, TData>>
    {
        private IConnectionFactory<TData, TData> connectionFactory;

        public void SetConnectionFactory(IConnectionFactory<TData, TData> factory)
        {
            connectionFactory = factory;
        }

        public void Apply(
            ILayer<INeuron<TData>, IConnectionFactory<TData, TData>> leftLayer, 
            ILayer<INeuron<TData>, IConnectionFactory<TData, TData>> rightLayer)
        {
            foreach (INeuron<TData> currentRightNeuron in rightLayer.Neurons)
            {
                foreach (INeuron<TData> currentLeftNeuron in leftLayer.Neurons)
                {
                    currentRightNeuron.Inputs.Add(connectionFactory.Create(currentLeftNeuron));
                    currentLeftNeuron.Outputs.Add(connectionFactory.Create(currentRightNeuron));
                }
            }
        }
    }
}
