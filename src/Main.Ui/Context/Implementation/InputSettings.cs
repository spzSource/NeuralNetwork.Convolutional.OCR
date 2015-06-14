using System.Configuration;

namespace DigitR.Ui.Context.Implementation
{
    public class InputSettings
    {
        public InputSettings()
        {
            StateFilePath = ConfigurationManager.AppSettings["NeuralNetworkStateFilePath"];
        }

        public string SourcePath
        {
            get;
            set;
        }

        public string LabelPath
        {
            get;
            set;
        }

        public string StateFilePath
        {
            get;
            private set;
        }
    }
}