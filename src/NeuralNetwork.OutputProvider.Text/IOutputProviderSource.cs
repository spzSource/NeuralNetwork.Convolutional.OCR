namespace DigitR.NeuralNetwork.OutputProvider.Gui
{
    public interface IOutputProviderSource<TData>
    {
        TData OutputSource
        {
            get;
            set;
        } 
    }
}