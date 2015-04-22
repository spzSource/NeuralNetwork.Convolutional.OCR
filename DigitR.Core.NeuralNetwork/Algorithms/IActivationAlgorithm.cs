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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inducedArea"></param>
        /// <returns></returns>
        TOutput CalculateFirstDerivative(TInducedArea inducedArea);
    }
}
