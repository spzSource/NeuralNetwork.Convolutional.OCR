using DigitR.Core.InputProvider;
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
    }
}