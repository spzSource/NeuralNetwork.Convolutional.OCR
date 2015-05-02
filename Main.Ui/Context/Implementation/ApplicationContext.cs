using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Cnn;

namespace DigitR.Ui.Context.Implementation
{
    public class ApplicationContext : IApplicationContext
    {
        public ApplicationContext(
            INeuralNetworkBuilder<double> networkBuilder)
        {
            InputSettings = new InputSettings();
            NeuralNetworkProcessor = new CnnNeuralNetworkProcessor(networkBuilder);
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

        public INeuralNetworkProcessor NeuralNetworkProcessor
        {
            get;
            private set;
        }
    }
}