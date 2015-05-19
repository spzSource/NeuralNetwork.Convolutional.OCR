using DigitR.Common.Logging;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.WeightsSigning;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.WeightsSigning.Implementation;
using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes;
using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation;
using DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Common;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn
{
    public class CnnNeuralNetworkBuilder : INeuralNetworkBuilder<double>
    {
        private const int KernelSize = 5;
        private const int SourceSizeForFirstLayer = 29;
        private const int SourceSizeForSecondLayer = 13;
        private const int FeatureMapCountForFirstLayer = 6;
        private const int FeatureMapCountForSecondLayer = 50;
        private const int FirstLayerSize = SourceSizeForFirstLayer * SourceSizeForFirstLayer;
        private const int SecondLayerSize = SourceSizeForSecondLayer * SourceSizeForSecondLayer * FeatureMapCountForFirstLayer;
        private const int ThirdLayerSize = KernelSize * KernelSize * FeatureMapCountForSecondLayer;
        private const int FourthLayerSize = 100;
        private const int FifthLayerSize = 10;

        private readonly IBiasAssignee biasAssignee;
        private readonly IWeightSigner<double> weightSigner;
        private readonly ConnectionsCounter connectionsCounter;
        private readonly NeuronsPerFeatureMapCounter neuronsPerFeatureMapCounter;

        public CnnNeuralNetworkBuilder()
        {
            weightSigner = new NormalWeightSigner();
            biasAssignee = new CnnBiasAssignee();
            connectionsCounter = new ConnectionsCounter();
            neuronsPerFeatureMapCounter = new NeuronsPerFeatureMapCounter();
        }

        public INeuralNetwork<double[]> Build()
        {
            CnnLayer firstLayer  = new CnnLayer(0, FirstLayerSize,  isFirst: true,  isLast: false);
            CnnLayer secondLayer = new CnnLayer(1, SecondLayerSize, isFirst: false, isLast: false);
            CnnLayer thirdLayer  = new CnnLayer(2, ThirdLayerSize,  isFirst: false, isLast: false);
            CnnLayer fourthLayer = new CnnLayer(3, FourthLayerSize, isFirst: false, isLast: false);
            CnnLayer fifthsLayer = new CnnLayer(4, FifthLayerSize,  isFirst: false, isLast: true);

            firstLayer.ConnectToLayer(
                secondLayer,
                new FirstToSecondConnectionScheme(
                    SourceSizeForFirstLayer, 
                    FeatureMapCountForFirstLayer, 
                    KernelSize, 
                    neuronsPerFeatureMapCounter, 
                    weightSigner, 
                    connectionsCounter, 
                    biasAssignee));

            secondLayer.ConnectToLayer(
                thirdLayer,
                new SecondToThirdConnectionScheme(
                    SourceSizeForSecondLayer, 
                    FeatureMapCountForSecondLayer, 
                    KernelSize, 
                    neuronsPerFeatureMapCounter, 
                    weightSigner, 
                    connectionsCounter, 
                    biasAssignee));

            thirdLayer.ConnectToLayer(
                fourthLayer,
                new FullyConnectedScheme(
                    weightSigner, 
                    connectionsCounter,
                    biasAssignee));

            fourthLayer.ConnectToLayer(
                fifthsLayer,
                new FullyConnectedScheme(
                    weightSigner, 
                    connectionsCounter,
                    biasAssignee));

            Log.Current.Info("The total number of created connections : {0}", connectionsCounter.CurrentValue);

            return new CnnNeuralNetwork(
                new []
                {
                    firstLayer, 
                    secondLayer, 
                    thirdLayer, 
                    fourthLayer, 
                    fifthsLayer
                });
        }
    }
}
