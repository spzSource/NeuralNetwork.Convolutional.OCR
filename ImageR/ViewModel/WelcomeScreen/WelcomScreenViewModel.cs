using System;
using System.Windows.Input;

using DigitR.Core.NeuralNetwork;
using DigitR.NeuralNetwork.Cnn.View.ViewModel;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace DigitR.Ui.ViewModel.WelcomeScreen
{
    public class WelcomScreenViewModel : ViewModelBase
    {
        private readonly NeuralNetworkGraphViewModel networkGraphViewModel;

        public WelcomScreenViewModel(
            INeuralNetworkProcessor networkProcessor)
        {
            if (networkProcessor == null)
            {
                throw new ArgumentNullException("networkProcessor");
            }
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
