using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork
{
    public class NeuralNetworkBuilder<TData> : INeuralNetworkBuilder<TData>
    {
        private readonly IList<ILayer<INeuron<TData>>> networkLayers = new List<ILayer<INeuron<TData>>>();

        public INeuralNetworkBuilder<TData> AddInputLayer(ILayer<INeuron<TData>> layer)
        {
            if (networkLayers.Count > 0)
            {
                throw new Exception("Input layer should be added first.");
            }

            networkLayers.Add(layer);

            return this;
        }

        public INeuralNetworkBuilder<TData> AddLayer<TScheme>(ILayer<INeuron<TData>> layer)
            where TScheme : IConnectionScheme<INeuron<TData>>, new()
        {
            if (networkLayers.Count == 0)
            {
                throw new Exception("At first need to add a input layer.");
            }

            networkLayers.LastOrDefault()?.ConnectToLayer<TScheme>(layer);
            networkLayers.Add(layer);

            return this;
        }

        public INeuralNetwork<TData[]> Build<TNeuralNetworkFactory>()
            where TNeuralNetworkFactory : INeuralNetworkFactory<TData>, new()
        {
            INeuralNetworkFactory<TData> factory = new TNeuralNetworkFactory();

            return factory.Create(new ReadOnlyCollection<ILayer<INeuron<TData>>>(networkLayers));
        }  
    }
}