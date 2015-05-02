using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork;
using DigitR.Ui.Context.Implementation;

namespace DigitR.Ui.Context
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