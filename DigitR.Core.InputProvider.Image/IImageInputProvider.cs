namespace DigitR.Core.InputProvider.Image
{
    public interface IImageInputProvider<out TLabel, out TSource> 
        : IAppInputProvider<TLabel, TSource>
    {
    }
}
