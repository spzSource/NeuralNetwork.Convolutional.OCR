using System;
using System.Windows.Input;

using DigitR.Context;
using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork.InputProvider.Training.Mnist;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace DigitR.ViewModel.Teach
{
    public class StartTeachingViewModel : ViewModelBase
    {
        public StartTeachingViewModel(IApplicationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            // TODO: need to use IoC instead of explicit instantiation
            IInputProvider trainingInputProvider = new MnistImageInputProvider(
                context.InputSettings.LabelPath, 
                context.InputSettings.SourcePath);

            context.CurrentTrainingInputProvider = trainingInputProvider;

            ProcessTrainingCommand = new RelayCommand(() =>
            {
                context.NeuralNetworkProcessor.Train(trainingInputProvider);
            });
        }

        public ICommand ProcessTrainingCommand
        {
            get;
            private set;
        }
    }
}
