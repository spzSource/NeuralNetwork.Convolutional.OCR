namespace DigitR.Core.InputProvider
{
    public interface IInputPattern<out TLabel, out TSource>
    {
        TLabel Label
        {
            get;
        }

        TSource Source
        {
            get;
        }
    }
}
