using System.Windows.Input;

using DigitR.Core.NeuralNetwork;
using DigitR.NeuralNetwork.Cnn.View.ViewModel;
using DigitR.Ui.Navigation;

using GalaSoft.MvvmLight.CommandWpf;

namespace DigitR.Ui.ViewModels.Common
{
    public class WelcomScreenViewModel : ModernViewModelBase
    {
        private readonly NeuralNetworkGraphViewModel networkGraphViewModel;

        public WelcomScreenViewModel(
            INeuralNetworkProcessor<INeuralNetwork<double[]>> networkProcessor)
        {
            networkGraphViewModel = new NeuralNetworkGraphViewModel();

            BuildNeuralNetworkGraphCommand = new RelayCommand(() => 
                networkGraphViewModel.BuildNeuralNetworkGraph(
                    (IMultiLayerNeuralNetwork<double>)networkProcessor.NeuralNetwork));
        }

        public NeuralNetworkGraphViewModel NeuralNetworkGraphViewModel
        {
            get
            {
                return networkGraphViewModel;
            }
        }

        public ICommand BuildNeuralNetworkGraphCommand
        {
            get;
            private set;
        }   
    }
}
