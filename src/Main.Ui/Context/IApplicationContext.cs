using DigitR.Core.NeuralNetwork.InputProvider;
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

        bool NetworkAlreadyTrained
        {
            get;
            set;
        }

        bool UserTrainingCollection
        {
            get;
            set;
        }
    }
}