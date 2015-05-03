using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork;

namespace DigitR.Ui.Context.Implementation
{
    public class ApplicationContext : IApplicationContext
    {
        public ApplicationContext(
            INeuralNetworkBuilder<double> networkBuilder)
        {
            InputSettings = new InputSettings();
        }

        public InputSettings InputSettings
        {
            get;
            private set;
        }

        public IInputProvider CurrentTrainingInputProvider
        {
            get;
            set;
        }
    }
}