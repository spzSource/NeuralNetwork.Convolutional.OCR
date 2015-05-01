namespace DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Common
{
    public class ConnectionsCounter
    {
        private readonly int startValue;

        private int currentValue;

        public ConnectionsCounter(int startValue)
        {
            this.startValue = startValue;
            
            currentValue = startValue;
        }

        public int CurrentValue
        {
            get
            {
                return currentValue;
            }
        }

        public void Increment()
        {
            ++currentValue;
        }

        public void Reset()
        {
            currentValue = startValue;
        }
    }
}
