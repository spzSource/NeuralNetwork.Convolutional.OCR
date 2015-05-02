namespace DigitR.Core.Output
{
    public interface IOutputPattern<TOutput>
    {
        TOutput Output
        {
            get;
            set;
        }
    }
}
