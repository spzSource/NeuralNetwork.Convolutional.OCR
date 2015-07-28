using DigitR.Core.NeuralNetwork.OutputProvider;

namespace DigitR.NeuralNetwork.OutputProvider.Gui
{
    public class GuiTextOutputProvider : IOutputProvider
    {
        private readonly IOutputProviderSource<string> outputSource;

        public GuiTextOutputProvider(
            IOutputProviderSource<string> outputSource)
        {
            this.outputSource = outputSource;
        }

        public bool Push(object data)
        {
            int indexWithMaxValue = GetMaxValueIndex(data);

            outputSource.OutputSource = indexWithMaxValue.ToString();

            return true;
        }

        private static int GetMaxValueIndex(object data)
        {
            int indexWithMaxValue = 0;

            double maxValue = -1;
            double[] recognized = (double[])data;

            for (int index = 0; index < recognized.Length; index++)
            {
                if (recognized[index] > maxValue)
                {
                    indexWithMaxValue = index;
                    maxValue = recognized[index];
                }
            }

            return indexWithMaxValue;
        }
    }
}
