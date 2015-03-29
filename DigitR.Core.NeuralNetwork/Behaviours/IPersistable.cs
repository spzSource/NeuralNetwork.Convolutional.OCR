// Creator: Popitich Aleksandr Date: 29 03 2015 17:32
namespace DigitR.Core.NeuralNetwork.Behaviours
{
    public interface IPersistable
    {
        /// <summary>
        /// Initialize a specific network instance.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Deinitialize a specific network instance.
        /// </summary>
        void Deinitialize(); 
    }
}