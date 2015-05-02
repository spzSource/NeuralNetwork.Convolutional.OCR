using DigitR.Common.Dependencies;
using DigitR.Ui.ViewModel;

using FirstFloor.ModernUI.Windows.Controls;

using Ninject;

namespace DigitR.Ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            IKernel kernel = new StandardKernel(new MainNinjectModule());

            Closing += (s, e) => ViewModelLocator.Cleanup();
        }
    }
}