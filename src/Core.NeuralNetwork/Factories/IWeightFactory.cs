using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Factories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TWeightValue"></typeparam>
    public interface IWeightFactory<TWeightValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IWeight<TWeightValue> Create();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weightsCount"></param>
        /// <returns></returns>
        IWeight<TWeightValue>[] CreateMany(int weightsCount);
    }
}