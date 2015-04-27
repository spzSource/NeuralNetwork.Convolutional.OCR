/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:ImageR.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using DigitR.Context;
using DigitR.Context.Implementation;
using DigitR.Core.InputProvider;
using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Cnn;
using DigitR.Core.NeuralNetwork.InputProvider.Training.Mnist;
using DigitR.Core.Output;
using DigitR.ViewModel.Teach;

using DigitRNeuralNetwork.OutputProvider.Text;

using GalaSoft.MvvmLight.Ioc;

using Microsoft.Practices.ServiceLocation;

namespace DigitR.ViewModel
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

            SimpleIoc.Default.Register<IApplicationContext, ApplicationContext>();

            SimpleIoc.Default.Register<IInputProvider, MnistImageInputProvider>();
            SimpleIoc.Default.Register<IOutputProvider, TextOutputProvider>();
            SimpleIoc.Default.Register<INeuralNetworkBuilder<double[], double[]>, CnnNeuralNetworkBuilder>();
            SimpleIoc.Default.Register<INeuralNetworkProcessor, CnnNeuralNetworkProcessor>();

            // View-models
            SimpleIoc.Default.Register<ConfigureInputPageViewModel>();
            SimpleIoc.Default.Register<StartTeachingViewModel>();
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

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}