using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork;
using DigitR.NeuralNetwork.InputProvider.Processing.File;
using DigitR.NeuralNetwork.InputProvider.Processing.Ocr;
using DigitR.NeuralNetwork.OutputProvider.Gui;
using DigitR.Ui.Context;
using DigitR.Ui.Navigation;
using DigitR.Ui.Utils;

using FirstFloor.ModernUI.Presentation;

namespace DigitR.Ui.ViewModels.Recognition
{
    public class ConfigureRecognitionDataViewModel : ModernViewModelBase, IOutputProviderSource<string>
    {
        private readonly IApplicationContext context;
        private readonly INeuralNetworkProcessor<INeuralNetwork<double[]>> neuranNeuralNetworkProcessor;

        private bool ableToSelectFile;
        private bool useTrainingCollection;
        private bool networkAlreadyTrained;
        private string recognizedValue;
        private ImageSource selectedImageSource;
        private IInputProvider inputProvider;

        public ConfigureRecognitionDataViewModel(
            IApplicationContext context,
            INeuralNetworkProcessor<INeuralNetwork<double[]>> neuranNeuralNetworkProcessor)
        {
            this.context = context;
            this.neuranNeuralNetworkProcessor = neuranNeuralNetworkProcessor;

            OpenFileCommand = new RelayCommand(OpenFile);
            ProcessSourceImageCommand = new RelayCommand(ProcessSourceImage);
            SaveOutputCommand = new RelayCommand(SaveOutput);

            UseTrainingCollection = false;
            NetworkAlreadyTrained = context.NetworkAlreadyTrained;
        }
        
        public ICommand OpenFileCommand
        {
            get;
            set;
        }

        public ICommand ProcessSourceImageCommand
        {
            get;
            set;
        }

        public ICommand SaveOutputCommand
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

        public string RecognizedValue
        {
            get
            {
                return recognizedValue;
            }
            set
            {
                recognizedValue = value;
                RaisePropertyChanged(() => RecognizedValue);
            }
        }

        public string OutputSource
        {
            get
            {
                return RecognizedValue;
            }
            set
            {
                RecognizedValue = value;
            }
        }

        public bool NetworkAlreadyTrained
        {
            get
            {
                return networkAlreadyTrained;
            }
            set
            {
                networkAlreadyTrained = value;
                RaisePropertyChanged(() => NetworkAlreadyTrained);
            }
        }

        private void OpenFile(object state)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Multiselect = false };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedImageSource = new BitmapImage(new Uri(openFileDialog.FileName));

                inputProvider = new OcrInputProvider(openFileDialog.FileName);
            }
        }

        private async void ProcessSourceImage(object state)
        {
            ByteArrayToBitmapConverter byteArrayToBitmapConverter = new ByteArrayToBitmapConverter();
            Bitmap sourceBitmap = byteArrayToBitmapConverter.ConvertToBitmap((BitmapImage)SelectedImageSource);

            bool result = await Task.Run(() => 
                neuranNeuralNetworkProcessor.Process(
                    new BitmapInputProvider(sourceBitmap), 
                    new GuiTextOutputProvider(this)));
        }
        
        private async void SaveOutput(object state)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream stream = saveFileDialog.OpenFile())
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    await writer.WriteLineAsync(OutputSource);
                }
            }
        }
    }
}