using DigitR.Core.InputProvider;

namespace DigitR.NeuralNetwork.InputProvider.Processing.File
{
    public class BitmapInputPattern : IInputPattern<double[]>
    {
        public BitmapInputPattern(double[] source)
        {
            Source = source;
        }

       public double[] Source
        {
            get;
            private set;
        }
    }
}
