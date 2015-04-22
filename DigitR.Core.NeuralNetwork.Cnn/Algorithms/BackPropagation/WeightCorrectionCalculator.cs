namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.BackPropagation
{
    internal class WeightCorrectionCalculator
    {
        private readonly double trainingSpeed;

        public WeightCorrectionCalculator(double trainingSpeed)
        {
            this.trainingSpeed = trainingSpeed;
        }

        public double Calculate(double localGradient, double output)
        {
            return trainingSpeed * localGradient * output;
        }
    }
}
