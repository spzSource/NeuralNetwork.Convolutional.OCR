using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.InputProvider.Training.Mnist;
using DigitR.Ui.Context;
using DigitR.Ui.Utils;

using FirstFloor.ModernUI.Presentation;

using GalaSoft.MvvmLight;

namespace DigitR.Ui.ViewModel.Teach
{
    public class StartTeachingViewModel : ViewModelBase
    {
        private readonly IApplicationContext context;
        private readonly ByteArrayToBitmapConverter byteArrayToBitmapConverter;
        private readonly IInputProvider trainingInputProvider;

        private BitmapSource currentInputPatternImageSource;
        
        public StartTeachingViewModel(IApplicationContext context)
        {
            this.context = context;
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            byteArrayToBitmapConverter = new ByteArrayToBitmapConverter();

            // TODO: need to use IoC instead of explicit instantiation
            trainingInputProvider = new MnistImageInputProvider(
                context.InputSettings.LabelPath,
                context.InputSettings.SourcePath,
                NewInputTrainingPatternRetrievedCallback);

            context.CurrentTrainingInputProvider = trainingInputProvider;

            ProcessTrainingCommand = new RelayCommand(ProcessTraining);
        }

        private async void ProcessTraining(object state)
        {
            bool result = await Task.Run(() => context.NeuralNetworkProcessor.Train(trainingInputProvider));
        }

        private void NewInputTrainingPatternRetrievedCallback(object pattern)
        {
            MnistImagePattern mnistPattern = (MnistImagePattern) pattern;

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

        public ICommand ProcessTrainingCommand
        {
            get;
            private set;
        }
    }
}
