using System;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace DigitR.ViewModel.Teach
{
    public class ConfigureInputPageViewModel : ViewModelBase
    {
        private string inputImagesPath;
        private string inputLabelsPath;

        public ConfigureInputPageViewModel()
        {
            inputImagesPath = String.Empty;
            inputLabelsPath = String.Empty;

            OpenImagesCommand = new RelayCommand(OpenImages);
            OpenLabelsCommand = new RelayCommand(OpenLabels);
        }

        public ICommand OpenImagesCommand
        {
            get;
            set;
        }

        public ICommand OpenLabelsCommand
        {
            get;
            set;
        }

        public string InputImagesPath
        {
            get
            {
                return inputImagesPath;
            }
            set
            {
                inputImagesPath = value;
                RaisePropertyChanged(() => InputImagesPath);
            }
        }

        public string InputLabelsPath
        {
            get
            {
                return inputLabelsPath;
            }
            set
            {
                inputLabelsPath = value;
                RaisePropertyChanged(() => InputLabelsPath);
            }
        }

        private void OpenLabels()
        {
            throw new System.NotImplementedException();
        }

        private void OpenImages()
        {
            throw new System.NotImplementedException();
        }
    }
}
