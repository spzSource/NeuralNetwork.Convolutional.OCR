using System;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

using Microsoft.Win32;

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

            OpenImagesCommand = new RelayCommand(() => SetInputFilePath(selectedFilePath => InputImagesPath = selectedFilePath));
            OpenLabelsCommand = new RelayCommand(() => SetInputFilePath(selectedFilePath => InputLabelsPath = selectedFilePath));
        }

        #region Properties

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

        #endregion

        private void SetInputFilePath(Action<string> setAction)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
            };

            bool? dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == true)
            {
                setAction(openFileDialog.FileName);
            }
        }
    }
}
