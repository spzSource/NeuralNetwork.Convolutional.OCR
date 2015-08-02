using System;
using System.Collections.Generic;

using DigitR.Core.NeuralNetwork.Factories;
using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork
{
    public class NeuralNetworkBuilder<TData> : INeuralNetworkBuilder<TData>
    {
        private readonly IList<KeyValuePair<ILayer<INeuron<TData>, IConnectionFactory<TData, TData>>, IConnectionScheme<INeuron<TData>, IConnectionFactory<TData, TData>>>> layersData = 
            new List<KeyValuePair<ILayer<INeuron<TData>, IConnectionFactory<TData, TData>>, IConnectionScheme<INeuron<TData>, IConnectionFactory<TData, TData>>>>();

        public INeuralNetworkBuilder<TData> AddInputLayer(ILayer<INeuron<TData>, IConnectionFactory<TData, TData>> layer)
        {
            if (layersData.Count > 0)
            {
                throw new Exception("Input layer should be added first.");
            }

            layersData.Add(new KeyValuePair<ILayer<INeuron<TData>, IConnectionFactory<TData, TData>>, IConnectionScheme<INeuron<TData>, IConnectionFactory<TData, TData>>>(layer, null));

            return this;
        }

        public INeuralNetworkBuilder<TData> AddLayer<TScheme>(ILayer<INeuron<TData>, IConnectionFactory<TData, TData>> layer)
            where TScheme : IConnectionScheme<INeuron<TData>, IConnectionFactory<TData, TData>>, new()
        {
            if (layersData.Count == 0)
            {
                throw new Exception("At first need to add a input layer.");
            }

            layersData.Add(new KeyValuePair<ILayer<INeuron<TData>, IConnectionFactory<TData, TData>>, IConnectionScheme<INeuron<TData>, IConnectionFactory<TData, TData>>>(layer, new TScheme()));

            return this;
        }

        public INeuralNetwork<TData> Build<TNeuralNetworkFactory>()
            where TNeuralNetworkFactory : INeuralNetworkFactory<TData>, new()
        {
            INeuralNetworkFactory<TData> factory = new TNeuralNetworkFactory();

            return factory.Create(layersData);
        }  
    }
}