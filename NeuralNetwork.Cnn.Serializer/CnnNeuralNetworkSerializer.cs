using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Serializer;

namespace DigitR.NeuralNetwork.Cnn.Serializer
{
    public class CnnNeuralNetworkSerializer : INeuralNetworkSerializer<double[]>
    {
        private readonly BinaryFormatter binaryFormatter;

        public CnnNeuralNetworkSerializer()
        {
            binaryFormatter = new BinaryFormatter();
        }

        public Task<bool> SerializeAsync(string path, INeuralNetwork<double[]> network)
        {
            return Task.Run(() =>
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    binaryFormatter.Serialize(fileStream, network);
                }
                return true;
            });
        }

        public Task<INeuralNetwork<double[]>> DeserializeAsync(string path)
        {
            return Task.Run(() =>
            {
                INeuralNetwork<double[]> networkInstance;
                
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    networkInstance = (INeuralNetwork<double[]>) binaryFormatter.Deserialize(fileStream);
                }

                return networkInstance;
            });
        }
    }
}
