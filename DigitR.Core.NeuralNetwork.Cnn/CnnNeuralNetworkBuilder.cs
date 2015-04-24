using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn
{
    public class CnnNeuralNetworkBuilder : INeuralNetworkBuilder<double[], double[]>
    {
        private const int KernelSize = 5;
        private const int LayersCount = 5;

        private const int FirstLayerSize = 29 * 29;
        private const int SecondLayerSize = 13 * 13 * 6;
        private const int ThirdLayerSize = 5 * 5 * 50;
        private const int FourthLayerSize = 100;
        private const int FifthLayerSize = 10;

        public INeuralNetwork<double[], double[]> Build()
        {
            CnnLayer firstLayer  = new CnnLayer(FirstLayerSize,  isFirst: true,  isLast: false);
            CnnLayer secondLayer = new CnnLayer(SecondLayerSize, isFirst: false, isLast: false);
            CnnLayer thirdLayer  = new CnnLayer(ThirdLayerSize,  isFirst: false, isLast: false);
            CnnLayer fourthLayer = new CnnLayer(FourthLayerSize, isFirst: false, isLast: false);
            CnnLayer fifthsLayer = new CnnLayer(FifthLayerSize,  isFirst: true,  isLast: true);

            firstLayer.ConnectToLayer(secondLayer, new FirstToSecondConnectionScheme());
        }
    }
}
