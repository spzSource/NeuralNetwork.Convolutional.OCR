using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.Algorithms.Extensions
{
    public static class ConnectionExtensions
    {
        public static TInfo GetConnectionInfo<TInfo>(this IConnection<double, double> connection)
        {
            return (TInfo)connection.AdditionalInfo;
        }
    }
}
