namespace DigitR.Core.InputProvider
{
    public class ThresholdConverter
    {
        private readonly byte threshold;

        public ThresholdConverter(byte threshold = 20)
        {
            this.threshold = threshold;
        }

        public double[] Convert(byte[] source)
        {
            double[] result = new double[source.Length];
            
            for (int i = 0; i < source.Length; i++)
            {
                result[i] = source[i] < threshold ? 0.0 : 1.0;
            }

            return result;
        }
    }
}
