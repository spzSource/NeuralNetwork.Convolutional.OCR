using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DigitR.Ui.Controls
{
    /// <summary>
    /// Interaction logic for OpenFileControl.xaml
    /// </summary>
    public partial class OpenFileControl : UserControl
    {
        public static readonly DependencyProperty OpenCommandProperty = 
            DependencyProperty.Register(
                "OpenCommand",
                typeof(ICommand),
                typeof(OpenFileControl));

        public static readonly DependencyProperty OpenLabelProperty =
            DependencyProperty.Register(
                "OpenLabel",
                typeof(string),
                typeof(OpenFileControl));

        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(
                "Path",
                typeof(string),
                typeof(OpenFileControl));
        
        public OpenFileControl()
        {
            InitializeComponent();
        }

        public string Path
        {
            get
            {
                return (string)GetValue(PathProperty);
            }
            set
            {
                SetValue(PathProperty, value);
            }
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

        public ICommand OpenCommand
        {
            get
            {
                return (ICommand)GetValue(OpenCommandProperty);
            }
            set
            {
                SetValue(OpenCommandProperty, value);
            }
        }
    }
}
