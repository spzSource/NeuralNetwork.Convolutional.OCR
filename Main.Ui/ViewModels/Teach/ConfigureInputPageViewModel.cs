using System;
using System.Configuration;
using System.Windows.Input;

using DigitR.Ui.Context;
using DigitR.Ui.Navigation;

namespace DigitR.Ui.ViewModels.Teach
{
    public class ConfigureInputPageViewModel : ModernViewModelBase
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
