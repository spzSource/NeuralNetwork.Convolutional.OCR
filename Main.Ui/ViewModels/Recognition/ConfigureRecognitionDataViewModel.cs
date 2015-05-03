using System;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using DigitR.Core.NeuralNetwork;
using DigitR.Ui.Context;
using DigitR.Ui.Navigation;
using DigitR.Ui.Utils;

using FirstFloor.ModernUI.Presentation;

namespace DigitR.Ui.ViewModels.Recognition
{
    public class ConfigureRecognitionDataViewModel : ModernViewModelBase
    {
        private ByteArrayToBitmapConverter byteArrayToBitmapConverter;
        private readonly IApplicationContext context;
        private readonly INeuralNetworkProcessor<INeuralNetwork<double[]>> neuranNeuralNetworkProcessor;

        private bool ableToSelectFile;
        private bool useTrainingCollection;
        private ImageSource selectedImageSource;

        public ConfigureRecognitionDataViewModel(
            IApplicationContext context,
            INeuralNetworkProcessor<INeuralNetwork<double[]>> neuranNeuralNetworkProcessor)
        {
            this.context = context;
            this.neuranNeuralNetworkProcessor = neuranNeuralNetworkProcessor;

            byteArrayToBitmapConverter = new ByteArrayToBitmapConverter();
            OpenFileCommand = new RelayCommand(OpenFile);

            UseTrainingCollection = false;
        }

        public ICommand OpenFileCommand
        {
            get;
            set;
        }

        public ImageSource SelectedImageSource
        {
            get
            {
                return selectedImageSource;
            }
            set
            {
                selectedImageSource = value;
                RaisePropertyChanged(() => SelectedImageSource);
            }
        }

        public bool UseTrainingCollection
        {
            get
            {
                return useTrainingCollection;
            }
            set
            {
                context.UserTrainingCollection = value;
                useTrainingCollection = value;
                AbleToSelectFile = !value;
                RaisePropertyChanged(() => UseTrainingCollection);
            }
        }

        public bool AbleToSelectFile
        {
            get
            {
                return ableToSelectFile;
            }
            set
            {
                ableToSelectFile = value;
                RaisePropertyChanged(() => AbleToSelectFile);
            }
        }

        private void OpenFile(object state)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Multiselect = false };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedImageSource = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}