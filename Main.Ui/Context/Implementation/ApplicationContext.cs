using System.IO;

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
            NetworkAlreadyTrained = File.Exists(InputSettings.StateFilePath);
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

        public bool NetworkAlreadyTrained
        {
            get;
            set;
        }

        public bool UserTrainingCollection
        {
            get;
            set;
        }
    }
}