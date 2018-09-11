using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace Assets
{
    public static class FileManager
    {
        public static void SaveNetworkToFile(NeuralNetwork neuralNetwork, string fileName)
        {
            using (var file = File.CreateText(fileName))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, neuralNetwork);
            }
        }

        public static NeuralNetwork ReadNetworkFromFile(string fileName)
        {
            using (var file = File.OpenText(fileName))
            {
                var serializer = new JsonSerializer();
                var network = (NeuralNetwork)serializer.Deserialize(file, typeof(NeuralNetwork));

                return network;
            }
        }
    }
}