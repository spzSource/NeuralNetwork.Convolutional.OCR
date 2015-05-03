using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.InputProvider.Training.Mnist;
using DigitR.Core.NeuralNetwork.Serializer;
using DigitR.Ui.Context;
using DigitR.Ui.Navigation;
using DigitR.Ui.Utils;

using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Navigation;

namespace DigitR.Ui.ViewModels.Teach
{
    public class StartTeachingViewModel : ModernViewModelBase
    {
        private readonly IApplicationContext context;
        private readonly INeuralNetworkSerializer<double[]> neuralNetworkSerializer;
        private readonly INeuralNetworkProcessor<INeuralNetwork<double[]>> neuralNetworkProcessor;
        private readonly ByteArrayToBitmapConverter byteArrayToBitmapConverter;
        private readonly IInputProvider trainingInputProvider;

        private bool networkAlreadyTrained;
        private bool networkOperationInProgress;
        private BitmapSource currentInputPatternImageSource;

        public StartTeachingViewModel(
            IApplicationContext context,
            INeuralNetworkSerializer<double[]> neuralNetworkSerializer,
            INeuralNetworkProcessor<INeuralNetwork<double[]>> neuralNetworkProcessor)
        {
            this.context = context;
            this.neuralNetworkSerializer = neuralNetworkSerializer;
            this.neuralNetworkProcessor = neuralNetworkProcessor;

            byteArrayToBitmapConverter = new ByteArrayToBitmapConverter();

            //
            // TODO: need to use IoC instead of explicit instantiation
            //
            trainingInputProvider = new MnistImageInputProvider(
                context.InputSettings.LabelPath,
                context.InputSettings.SourcePath,
                NewInputTrainingPatternRetrievedCallback);

            ProcessTrainingCommand = new RelayCommand(ProcessTraining);
            LoadNetworkStateCommand = new RelayCommand(LoadNetworkState);
        }

        public ICommand ProcessTrainingCommand
        {
            get;
            private set;
        }

        public ICommand LoadNetworkStateCommand
        {
            get;
            private set;
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

        public bool NetworkOperationInProgress
        {
            get
            {
                return networkOperationInProgress;
            }
            set
            {
                networkOperationInProgress = value;
                RaisePropertyChanged(() => NetworkOperationInProgress);
            }
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            NetworkAlreadyTrained = context.NetworkAlreadyTrained;
        }

        private async void LoadNetworkState(object obj)
        {
            NetworkOperationInProgress = true;
            try
            {
                INeuralNetwork<double[]> network =
                    await neuralNetworkSerializer.DeserializeAsync(context.InputSettings.StateFilePath);

                neuralNetworkProcessor.Initialize(network);
            }
            finally
            {
                NetworkOperationInProgress = false;
            }
        }

        private async void ProcessTraining(object state)
        {
            NetworkOperationInProgress = true;
            try
            {
                bool trained = await Task.Run(() => neuralNetworkProcessor.Train(trainingInputProvider));
                if (trained)
                {
                    context.NetworkAlreadyTrained = true;
                    NetworkAlreadyTrained = true;
                }
            }
            finally
            {
                NetworkOperationInProgress = false;
            }

        }

        private void NewInputTrainingPatternRetrievedCallback(object pattern)
        {
            MnistImagePattern mnistPattern = (MnistImagePattern)pattern;

            ProcessedImage = byteArrayToBitmapConverter.ToWpfBitmap(
                byteArrayToBitmapConverter.Convert(
                    mnistPattern.Height,
                    mnistPattern.Weight,
                    mnistPattern.InnerSource));
        }

        public BitmapSource ProcessedImage
        {
            get
            {
                return currentInputPatternImageSource;
            }
            set
            {
                currentInputPatternImageSource = value;
                RaisePropertyChanged(() => ProcessedImage);
            }
        }
    }
}
