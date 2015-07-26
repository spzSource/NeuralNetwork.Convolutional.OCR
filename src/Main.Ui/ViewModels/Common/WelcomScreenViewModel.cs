using DigitR.Core.NeuralNetwork;
using DigitR.Ui.Navigation;

namespace DigitR.Ui.ViewModels.Common
{
    public class WelcomScreenViewModel : ModernViewModelBase
    {
        public WelcomScreenViewModel(
            INeuralNetworkProcessor<INeuralNetwork<double[]>> networkProcessor)
        {
        }
    }
}
