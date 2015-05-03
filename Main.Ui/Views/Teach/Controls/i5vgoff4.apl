using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DigitR.Pages.Teach.Steps.Controls
{
    /// <summary>
    /// Interaction logic for OpenFileControl.xaml
    /// </summary>
    public partial class OpenFileControl : UserControl
    {
        public static DependencyProperty OpenCommandProperty = 
            DependencyProperty.Register(
                "OpenCommand",
                typeof(ICommand),
                typeof(OpenFileControl));

        public static DependencyProperty OpenLabelProperty =
            DependencyProperty.Register(
                "OpenLabel",
                typeof(string),
                typeof(OpenFileControl));
        
        public OpenFileControl()
        {
            InitializeComponent();
        }

        public string OpenLabel
        {
            get
            {
                return (string)GetValue(OpenLabelProperty);
            }
            set
            {
                SetValue(OpenLabelProperty, value);
            }
        }
    }
}
