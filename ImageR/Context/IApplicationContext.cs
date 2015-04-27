using DigitR.Context.Implementation;
using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork;

namespace DigitR.Context
{
    public interface IApplicationContext
    {
        InputSettings InputSettings
        {
            get;
        }

        IInputProvider CurrentTrainingInputProvider
        {
            get;
            set;
        }

        INeuralNetworkProcessor NeuralNetworkProcessor
        {
            get;
        }
    }
}