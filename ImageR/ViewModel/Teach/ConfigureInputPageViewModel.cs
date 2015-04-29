using System;
using System.Configuration;
using System.Windows.Input;

using DigitR.Context;

using GalaSoft.MvvmLight;

using Microsoft.Win32;

namespace DigitR.ViewModel.Teach
{
    public class ConfigureInputPageViewModel : ViewModelBase
    {
        private readonly IApplicationContext context;

        private string inputImagesPath;
        private string inputLabelsPath;

        public ConfigureInputPageViewModel(IApplicationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this.context = context;
            inputImagesPath = String.Empty;
            inputLabelsPath = String.Empty;

            //OpenImagesCommand = new RelayCommand(() =>
            //{
            //    SetInputImageFilePath(selectedFilePath => {
            //        InputImagesPath = selectedFilePath;
            //        context.InputSettings.SourcePath = selectedFilePath;
            //    });
            //});
            //OpenLabelsCommand = new RelayCommand(() =>
            //{
            //    SetInputLabelFilePath(selectedFilePath => {
            //        InputLabelsPath = selectedFilePath;
            //        context.InputSettings.LabelPath = selectedFilePath;
            //    });
            //});
            SetInputImageFilePath(selectedFilePath =>
            {
                InputImagesPath = selectedFilePath;
                context.InputSettings.SourcePath = selectedFilePath;
            });
            SetInputLabelFilePath(selectedFilePath =>
            {
                InputLabelsPath = selectedFilePath;
                context.InputSettings.LabelPath = selectedFilePath;
            });
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
            OpenFileDialog openFileDialog = new OpenFileDialog { Multiselect = false };

            bool? dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == true)
            {
                setAction(openFileDialog.FileName);
            }
        }

        private void SetInputImageFilePath(Action<string> setAction)
        {
            string path = ConfigurationManager.AppSettings["InputMnistTrainImagesPath"];
            setAction(path);
        }

        private void SetInputLabelFilePath(Action<string> setAction)
        {
            string path = ConfigurationManager.AppSettings["InputMnistTrainLabelsPath"];
            setAction(path);
        }
    }
}
