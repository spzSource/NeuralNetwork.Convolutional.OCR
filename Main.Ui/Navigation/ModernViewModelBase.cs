using FirstFloor.ModernUI.Windows.Navigation;

using GalaSoft.MvvmLight;

namespace DigitR.Ui.Navigation
{
    public class ModernViewModelBase : ViewModelBase
    {
        public virtual void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
        }

        public virtual void OnNavigatedFrom(NavigationEventArgs e)
        {
        }
        
        public virtual void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        
        public virtual void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}
