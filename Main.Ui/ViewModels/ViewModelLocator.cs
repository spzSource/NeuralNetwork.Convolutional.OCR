/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:ImageR.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms;
using DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation;
using DigitR.Core.NeuralNetwork.InputProvider.Training.Mnist;
using DigitR.Core.NeuralNetwork.Serializer;
using DigitR.Core.Output;
using DigitR.NeuralNetwork.Cnn.Serializer;
using DigitR.NeuralNetwork.OutputProvider.Text;
using DigitR.Ui.Context;
using DigitR.Ui.Context.Implementation;
using DigitR.Ui.ViewModels.Teach;
using DigitR.Ui.ViewModels.WelcomeScreen;

using GalaSoft.MvvmLight.Ioc;

using Microsoft.Practices.ServiceLocation;

namespace DigitR.Ui.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Core dependencies.
            SimpleIoc.Default.Register<IApplicationContext, ApplicationContext>();

            SimpleIoc.Default.Register<IInputProvider, MnistImageInputProvider>();
            SimpleIoc.Default.Register<IOutputProvider, TextOutputProvider>();
            SimpleIoc.Default.Register<INeuralNetworkBuilder<double>, CnnNeuralNetworkBuilder>();
            SimpleIoc.Default.Register<INeuralNetworkProcessor<INeuralNetwork<double[]>>, CnnNeuralNetworkProcessor>();
            SimpleIoc.Default.Register<INeuralNetworkSerializer<double[]>, CnnNeuralNetworkSerializer>();

            SimpleIoc.Default.Register<IActivationAlgorithm<double, double>, SigmoidActivationAlgorithm>();
            SimpleIoc.Default.Register<ITrainingAlgorithm<INeuralNetwork<double[]>, IInputTrainingPattern<double[]>>, BackPropagationAlgorithm>();

            // View-models.
            SimpleIoc.Default.Register<WelcomScreenViewModel>();

            SimpleIoc.Default.Register<ConfigureInputPageViewModel>();
            SimpleIoc.Default.Register<StartTeachingViewModel>();
            SimpleIoc.Default.Register<StateSavingViewModel>();
        }

        public WelcomScreenViewModel WelcomScreenViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WelcomScreenViewModel>();
            }
        }   

        public ConfigureInputPageViewModel ConfigureInputPageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ConfigureInputPageViewModel>();
            }
        }

        public StartTeachingViewModel StartTeachingViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StartTeachingViewModel>();
            }
        }

        public StateSavingViewModel StateSavingViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StateSavingViewModel>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}