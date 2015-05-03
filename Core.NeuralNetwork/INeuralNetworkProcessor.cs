using DigitR.Core.InputProvider;
using DigitR.Core.Output;

namespace DigitR.Core.NeuralNetwork
{
    /// <summary>
    /// 
    /// </summary>
    public interface INeuralNetworkProcessor<TNetwork>
    {
        TNetwork NeuralNetwork
        {
            get;
        }

        void Initialize(TNetwork network);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputProvider"></param>
        /// <param name="outputProvider"></param>
        /// <returns></returns>
        bool Process(IInputProvider inputProvider, IOutputProvider outputProvider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trainingInputProvider"></param>
        /// <returns></returns>
        bool Train(IInputProvider trainingInputProvider);
    }
}
