using System;
using System.Threading.Tasks;
using System.Windows.Input;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Serializer;
using DigitR.Ui.Context;
using DigitR.Ui.Navigation;

using FirstFloor.ModernUI.Presentation;

using GalaSoft.MvvmLight;

namespace DigitR.Ui.ViewModels.Teach
{
    public class StateSavingViewModel : ModernViewModelBase
    {
        private bool savingInProgress;

        public StateSavingViewModel(
            IApplicationContext applicationContext,
            INeuralNetworkProcessor<INeuralNetwork<double[]>> neuralNetworkProcessor,
            INeuralNetworkSerializer<double[]> neuralNetworkSerializer)
        {
            SaveStateCommand = new RelayCommand(async state =>
            {
                SavingInProgress = true;

                bool result = await neuralNetworkSerializer.SerializeAsync(
                    applicationContext.InputSettings.StateFilePath,
                    neuralNetworkProcessor.NeuralNetwork);
                
                SavingInProgress = false;
            });
        }

        public ICommand SaveStateCommand
        {
            get;
            set;
        }

        public bool SavingInProgress
        {
            get
            {
                return savingInProgress;
            }
            set
            {
                savingInProgress = value;
                RaisePropertyChanged(() => SavingInProgress);
            }
        }
    }
}