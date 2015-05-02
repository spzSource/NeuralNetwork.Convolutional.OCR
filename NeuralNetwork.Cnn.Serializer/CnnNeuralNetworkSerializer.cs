using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

using DigitR.Core.NeuralNetwork;
using DigitR.Core.NeuralNetwork.Serializer;

namespace DigitR.NeuralNetwork.Cnn.Serializer
{
    public class CnnNeuralNetworkSerializer : INeuralNetworkSerializer<double[]>
    {
        private readonly string path;
        private readonly BinaryFormatter binaryFormatter;

        public CnnNeuralNetworkSerializer(
            string path)
        {
            Contract.Assert(!String.IsNullOrWhiteSpace(path), "Serialization path cannot be null.");
            this.path = path;

            binaryFormatter = new BinaryFormatter();
        }

        public Task<bool> SerializeAsync(INeuralNetwork<double[]> network)
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

        public Task<INeuralNetwork<double[]>> DeserializeAsync()
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
