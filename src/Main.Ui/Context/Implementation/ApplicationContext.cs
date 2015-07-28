using System.IO;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.InputProvider;

namespace DigitR.Ui.Context.Implementation
{
    public class ApplicationContext : IApplicationContext
    {
        public ApplicationContext(
            INeuralNetworkFactory<double> networkBuilder)
        {
            InputSettings = new InputSettings();
            NetworkAlreadyTrained = File.Exists(InputSettings.StateFilePath);
        }

        public InputSettings InputSettings
        {
            get;
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