namespace DigitR.Core.InputProvider.Image
{
    /// <summary>
    /// Represents the input image pattern.
    /// </summary>
    /// <typeparam name="TLabel">The type for image label.</typeparam>
    /// <typeparam name="TSource">The type for image source.</typeparam>
    public interface IImagePattern<out TLabel, out TSource> 
        : IInputPattern<TLabel, TSource>
    {
        int Height 
        { 
            get; 
        }

        int Weight 
        { 
            get; 
        }      
    }
}
