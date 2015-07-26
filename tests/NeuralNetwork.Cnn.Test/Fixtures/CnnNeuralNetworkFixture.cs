using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Cnn;
using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;

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
                        .AddLayer<FullyConnectedScheme>(new CnnLayer(0, 5, false, false))
                        .AddLayer<FullyConnectedScheme>(new CnnLayer(0, 2, false, true))
                        .Build<CnnNeuralNetworkFactory>() as IMultiLayerNeuralNetwork<double>;
                }
                return neuralNetwork;
            }
        }
    }
}