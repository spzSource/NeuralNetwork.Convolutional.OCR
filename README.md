# NeuralNetwork.Convolutional.OCR [![Build status](https://ci.appveyor.com/api/projects/status/qmsystqp5rsntkep?svg=true)](https://ci.appveyor.com/project/spzSource/neuralnetwork-convolutional-ocr)
Prototype for recognition of handwritten digits.

Repo includes following components:
- convolutional neural network (CNN) as a base component;
- back-propagation training algorithms;
- simple GUI app to demonstrate NN working process.

## Usage
### 1. Builder for your neural network
You may create your own builder with specific logic for your NN. Or simply use existing builder named NeuralNetworkBuilder.
```cs
  INeuralNetworkBuilder<double> networkBuilder = new NeuralNetworkBuilder<double>();
```

### 2. Factory to create specific instance of NN.
By default the the library has already the default implementation of factory for NN.
```cs
public class SpecificNeuralNetworkFactory : INeuralNetworkFactory<double>
{
  ...
}
```

### 3. Connection schemes.
Additionally, you can configure different schemes of connections between layers in case multi-layer NN.
And if you want to connect layers using any specific logic - just create a specific implementation of  IConnectionScheme interface.

For example, scheme to connect each neurons with each other (fully-connected layers), specified below:
```cs
public class FullyConnectedScheme<TData> 
  : IConnectionScheme<INeuron<TData>, IConnectionFactory<TData, TData>>
{
    private IConnectionFactory<TData, TData> connectionFactory;

    public void SetConnectionFactory(IConnectionFactory<TData, TData> factory)
    {
        connectionFactory = factory;
    }

    public void Apply(
        ILayer<INeuron<TData>, IConnectionFactory<TData, TData>> leftLayer, 
        ILayer<INeuron<TData>, IConnectionFactory<TData, TData>> rightLayer)
    {
        foreach (INeuron<TData> currentRightNeuron in rightLayer.Neurons)
        {
            foreach (INeuron<TData> currentLeftNeuron in leftLayer.Neurons)
            {
                currentRightNeuron.Inputs.Add(connectionFactory.Create(currentLeftNeuron));
                currentLeftNeuron.Outputs.Add(connectionFactory.Create(currentRightNeuron));
            }
        }
    }
}
```

### 4. Configure NN using connection schemes and specific factory.
The example, specified below, shows how to create a neural network with three layers which connected using FullyConnectedScheme scheme.
```cs
IMultiLayerNeuralNetwork<double> neuralNetwork = networkBuilder
    .AddInputLayer(new Layer(0, 3, true, false))
    .AddLayer<FullyConnectedScheme<double>>(new Layer(1, 5, false, false))
    .AddLayer<FullyConnectedScheme<double>>(new Layer(2, 2, false, true))
    .Build<NeuralNetworkFactory>() as IMultiLayerNeuralNetwork<double>;
```
