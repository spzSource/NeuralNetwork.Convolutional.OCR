using DigitR.Common.Logging;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation;
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
        private const int FirstLayerSize = 29 * 29;
        private const int SecondLayerSize = 13 * 13 * 6;
        private const int ThirdLayerSize = 5 * 5 * 50;
        private const int FourthLayerSize = 100;
        private const int FifthLayerSize = 10;

        private readonly IBiasAssignee biasAssignee;
        private readonly IWeightSigner<double> weightSigner;
        private readonly ConnectionsCounter connectionsCounter;
        private readonly FeatureMapWeightsCreator featureMapWeightsCreator;
        private readonly NeuronsPerFeatureMapCounter neuronsPerFeatureMapCounter;

        public CnnNeuralNetworkBuilder()
        {
            weightSigner = new NormalWeightSigner();
            biasAssignee = new CnnBiasAssignee();
            connectionsCounter = new ConnectionsCounter(0);
            featureMapWeightsCreator = new FeatureMapWeightsCreator(weightSigner);
            neuronsPerFeatureMapCounter = new NeuronsPerFeatureMapCounter();
        }

        public INeuralNetwork<double[]> Build()
        {
            CnnLayer firstLayer  = new CnnLayer(0, FirstLayerSize,  isFirst: true,  isLast: false);
            CnnLayer secondLayer = new CnnLayer(1, SecondLayerSize, isFirst: false, isLast: false);
            CnnLayer thirdLayer  = new CnnLayer(2, ThirdLayerSize,  isFirst: false, isLast: false);
            CnnLayer fourthLayer = new CnnLayer(3, FourthLayerSize, isFirst: false, isLast: false);
            CnnLayer fifthsLayer = new CnnLayer(4, FifthLayerSize,  isFirst: false,  isLast: true);

            firstLayer.ConnectToLayer(
                secondLayer,
                new FirstToSecondConnectionScheme(
                    source2DSize: 29,
                    featureMapCount: 6,
                    kernelSize: 5,
                    neuronsPerFeatureMapCounter: new NeuronsPerFeatureMapCounter(),
                    weightSigner: weightSigner,
                    connectionsCounter: connectionsCounter,
                    biasAssignee: biasAssignee));

            secondLayer.ConnectToLayer(
                thirdLayer,
                new SecondToThirdConnectionScheme(
                    source2DSize: 13,
                    featureMapCount: 50,
                    kernelSize: 5,
                    neuronsPerFeatureMapCounter: new NeuronsPerFeatureMapCounter(),
                    weightSigner: weightSigner,
                    connectionsCounter: connectionsCounter,
                    biasAssignee: biasAssignee));

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

            Log.Current.Error("The total number of created connections : {0}", connectionsCounter.CurrentValue);

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
