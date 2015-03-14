namespace DigitR.Core.NeuralNetwork
{
    public interface INeuralNetworkBuilder<in TInput, in TOutput>
    {
        INeuralNetwork<TInput, TOutput> Build();
    }
}
