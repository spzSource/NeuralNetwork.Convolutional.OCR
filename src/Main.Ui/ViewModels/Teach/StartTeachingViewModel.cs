using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.InputProvider;
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
        
        private bool stateLoading;
        private bool networkAlreadyTrained;
        private bool operationInProgress;
        private bool networkOperationInProgress;

        private IInputProvider trainingInputProvider;
        private BitmapSource currentInputPatternImageSource;
        private CancellationTokenSource cancellationTokenSource;

        public StartTeachingViewModel(
            IApplicationContext context,
            INeuralNetworkSerializer<double[]> neuralNetworkSerializer,
            INeuralNetworkProcessor<INeuralNetwork<double[]>> neuralNetworkProcessor)
        {
            this.context = context;
            this.neuralNetworkSerializer = neuralNetworkSerializer;
            this.neuralNetworkProcessor = neuralNetworkProcessor;

            byteArrayToBitmapConverter = new ByteArrayToBitmapConverter();

            trainingInputProvider = new MnistImageInputProvider(
                context.InputSettings.LabelPath,
                context.InputSettings.SourcePath,
                NewInputTrainingPatternRetrievedCallback);

            ProcessTrainingCommand = new RelayCommand(ProcessTraining);
            LoadNetworkStateCommand = new RelayCommand(LoadNetworkState);
            CancelTrainingCommand = new RelayCommand(CancelTraining);

            SaveStateCommand = new RelayCommand(async state =>
            {
                OperationInProgress = true;

                bool result = await neuralNetworkSerializer.SerializeAsync(
                    context.InputSettings.StateFilePath,
                    neuralNetworkProcessor.NeuralNetwork);

                OperationInProgress = false;
            });
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

        public ICommand CancelTrainingCommand
        {
            get;
            private set;
        }

        public ICommand SaveStateCommand
        {
            get;
            set;
        }

        public bool StateLoading
        {
            get
            {
                return stateLoading;
            }
            set
            {
                stateLoading = value;
                RaisePropertyChanged(() => StateLoading);
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

        public bool OperationInProgress
        {
            get
            {
                return operationInProgress;
            }
            set
            {
                operationInProgress = value;
                RaisePropertyChanged(() => OperationInProgress);
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

            trainingInputProvider = new MnistImageInputProvider(
                context.InputSettings.LabelPath,
                context.InputSettings.SourcePath,
                NewInputTrainingPatternRetrievedCallback);
        }

        private async void LoadNetworkState(object obj)
        {
            OperationInProgress = true;
            try
            {
                INeuralNetwork<double[]> network =
                    await neuralNetworkSerializer.DeserializeAsync(context.InputSettings.StateFilePath);

                neuralNetworkProcessor.Initialize(network);
            }
            finally
            {
                OperationInProgress = false;
            }
        }

        private async void ProcessTraining(object state)
        {
            OperationInProgress = true;
            NetworkOperationInProgress = true;

            try
            {
                cancellationTokenSource = new CancellationTokenSource();

                bool trained = await Task.Run(() => neuralNetworkProcessor.Train(trainingInputProvider, cancellationTokenSource.Token));
                
                context.NetworkAlreadyTrained = true;
                NetworkAlreadyTrained = true;
                
            }
            finally
            {
                OperationInProgress = false;
                NetworkOperationInProgress = false;
            }
        }

        private void CancelTraining(object state)
        {
            cancellationTokenSource.Cancel();
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
