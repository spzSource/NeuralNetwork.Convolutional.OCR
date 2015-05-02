namespace DigitR.Core.NeuralNetwork.Primitives
{
    /// <summary>
    /// Provides a abstraction for connection weight.
    /// </summary>
    /// <typeparam name="TWeightValue"></typeparam>
    public interface IWeight<TWeightValue>
    {
        TWeightValue Value
        {
            get;
            set;
        }

        object AdditionalInfo
        {
            get;
            set;
        }

        TInfo GetInfo<TInfo>();
    }
}
