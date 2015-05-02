using System;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Cnn;

using Ninject.Modules;

namespace DigitR.Common.Dependencies
{
    public class MainNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<INeuralNetworkProcessor>().To<CnnNeuralNetworkProcessor>();
        }
    }
}
