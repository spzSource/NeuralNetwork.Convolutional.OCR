namespace DigitR.Core.NeuralNetwork.InputProvider.Training.Mnist
{
    public class ThresholdConverter
    {
        private readonly byte threshold;

        public ThresholdConverter(byte threshold)
        {
            this.threshold = threshold;
        }

        public double[] Convert(byte[] source)
        {
            double[] result = new double[source.Length];
            
            for (int i = 0; i < source.Length; i++)
            {
                result[i] = source[i] < threshold ? -1.0 : 1.0;
            }

            return result;
        }
    }
}
