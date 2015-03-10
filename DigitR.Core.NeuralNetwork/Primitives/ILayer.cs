namespace DigitR.Core.NeuralNetwork.Primitives
{
    public interface ILayer<out TNeuron, out TWeight>
    {
        TNeuron[] Neurons
        {
            get;
        }

        TWeight[] Weights
        {
            get;
        }

        void Calculate();
    }
}
