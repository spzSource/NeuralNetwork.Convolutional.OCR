using DigitR.Core.NeuralNetwork;
using DigitR.NeuralNetwork.Cnn;
using DigitR.NeuralNetwork.Cnn.ConnectionSchemes.Implementation;
using DigitR.NeuralNetwork.Cnn.Primitives;

namespace NeuralNetwork.Cnn.Test.Fixtures
{
    public class CnnNeuralNetworkFixture
    {
        private IMultiLayerNeuralNetwork<double> neuralNetwork; 

        public IMultiLayerNeuralNetwork<double> MultiLayerNeuralNetwork
        {
            get
            {
                if (neuralNetwork == null)
                {
                    INeuralNetworkBuilder<double> builder = new NeuralNetworkBuilder<double>();

                    neuralNetwork = builder
                        .AddInputLayer(new CnnLayer(0, 3, true, false))
                        .AddLayer<FullyConnectedScheme>(new CnnLayer(1, 5, false, false))
                        .AddLayer<FullyConnectedScheme>(new CnnLayer(2, 2, false, true))
                        .Build<CnnNeuralNetworkFactory>() as IMultiLayerNeuralNetwork<double>;
                }
                return neuralNetwork;
            }
        }
    }
}