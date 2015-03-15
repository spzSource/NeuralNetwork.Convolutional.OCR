namespace DigitR.Core.NeuralNetwork.Algorithms
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    /// <typeparam name="TInducedArea"></typeparam>
    public interface IActivationAlgorithm<out TOutput, in TInducedArea>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TOutput Calculate(TInducedArea inducedArea);
    }
}
