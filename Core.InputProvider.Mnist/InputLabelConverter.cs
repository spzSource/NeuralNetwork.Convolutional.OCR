using System;

namespace DigitR.Core.NeuralNetwork.InputProvider.Training.Mnist
{
    public class InputLabelConverter
    {
        private const byte Range = 10;

        public double[] Convert(byte label)
        {
            if (label >= Range)
            {
                throw new ArgumentException("The value should be in the range from 0 to 9.");
            }

            double[] converted = new double[Range];
            
            converted[label] = 1;

            return converted;
        }
    }
}
