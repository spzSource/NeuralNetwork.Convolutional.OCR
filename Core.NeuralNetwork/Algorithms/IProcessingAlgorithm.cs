namespace DigitR.Core.NeuralNetwork.Algorithms
{
    public interface IProcessingAlgorithm<in TNeuralNetwork, in TPattern>
    {
        double[] Process(
            TNeuralNetwork network,
            TPattern inputPattern);
    }
}