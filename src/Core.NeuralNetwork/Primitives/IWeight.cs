namespace DigitR.Core.NeuralNetwork.Primitives
{
    /// <summary>
    /// Provides a abstraction for connection weight.
    /// </summary>
    /// <typeparam name="TWeightValue"></typeparam>
    public interface IWeight<TWeightValue>
    {
        /// <summary>
        /// The value of weight.
        /// </summary>
        TWeightValue Value
        {
            get;
            set;
        }

        /// <summary>
        /// The additional info for this weight.
        /// </summary>
        object AdditionalInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Provides a type casting for <see cref="AdditionalInfo"/> property.
        /// </summary>
        /// <typeparam name="TInfo">The type for <see cref="AdditionalInfo"/> property</typeparam>
        /// <returns>The object with required type.</returns>
        TInfo GetInfo<TInfo>();
    }
}
