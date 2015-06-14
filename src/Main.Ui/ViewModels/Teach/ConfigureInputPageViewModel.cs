using System;
using System.Configuration;
using System.Windows.Forms;
using System.Windows.Input;

using DigitR.Ui.Context;
using DigitR.Ui.Navigation;

using FirstFloor.ModernUI.Presentation;

namespace DigitR.Ui.ViewModels.Teach
{
    public class ConfigureInputPageViewModel : ModernViewModelBase
    {
        private string inputImagesPath;
        private string inputLabelsPath;

        public ConfigureInputPageViewModel(IApplicationContext context)
        {
            InputImagesPath = ConfigurationManager.AppSettings["InputMnistTrainImagesPath"];
            InputLabelsPath = ConfigurationManager.AppSettings["InputMnistTrainLabelsPath"];

            context.InputSettings.SourcePath = InputImagesPath;
            context.InputSettings.LabelPath = InputLabelsPath;

            OpenImagesCommand = new RelayCommand(state =>
                SetInputImageFilePath(selectedFilePath =>
                {
                    InputImagesPath = selectedFilePath;
                    context.InputSettings.SourcePath = selectedFilePath;
                }));


            OpenLabelsCommand = new RelayCommand(state => 
                SetInputLabelFilePath(selectedFilePath =>
                {
                    InputLabelsPath = selectedFilePath;
                    context.InputSettings.LabelPath = selectedFilePath;
                }));
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

        private void SetInputImageFilePath(Action<string> setAction)
        {
            OpenFileDialog openDialog = new OpenFileDialog { Multiselect = false };
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                setAction(openDialog.FileName);
            }
        }

        private void SetInputLabelFilePath(Action<string> setAction)
        {
            OpenFileDialog openDialog = new OpenFileDialog { Multiselect = false };
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                setAction(openDialog.FileName);
            }
        }
    }
}
