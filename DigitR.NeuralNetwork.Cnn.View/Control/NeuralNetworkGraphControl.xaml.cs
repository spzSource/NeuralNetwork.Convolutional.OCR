using System.Windows;
using System.Windows.Controls;

namespace DigitR.NeuralNetwork.Cnn.View.Control
{
    /// <summary>
    /// Interaction logic for NeuralNetworkGraph.xaml
    /// </summary>
    public partial class NeuralNetworkGraphControl : UserControl
    {
        //private static readonly DependencyProperty NeuralNetworkGraphProperty = 
        //    DependencyProperty.Register(
        //        "NeuralNetworkGraph",
        //        typeof(NeuralNetworkGraph),
        //        typeof(NeuralNetworkGraphControl));

        public NeuralNetworkGraphControl()
        {
            InitializeComponent();
        }

        //public NeuralNetworkGraph NeuralNetworkGraph
        //{
        //    get
        //    {
        //        return (NeuralNetworkGraph)GetValue(NeuralNetworkGraphProperty);
        //    }
        //    set
        //    {
        //        SetValue(NeuralNetworkGraphProperty, value);
        //    }
        //}
    }
}
