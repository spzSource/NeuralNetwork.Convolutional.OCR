namespace DigitR.Core.InputProvider.Image
{
    public interface IImageInputProvider<out TLable, out TSource> 
        : IAppInputProvider<TLable, TSource>
    {
    }
}
