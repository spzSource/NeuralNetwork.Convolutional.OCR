using System.Windows.Controls;

using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;

namespace DigitR.Ui.Navigation
{
    public class ModernPage : Page, IContent
    {
        /// <summary>  
        /// Called when navigation to a content fragment begins.  
        /// </summary>  
        /// <param name="e">An object that contains the navigation data.</param>  
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            ((ModernViewModelBase)DataContext).OnFragmentNavigation(e);
        }

        /// <summary>  
        /// Called when this instance is no longer the active content in a frame.  
        /// </summary>  
        /// <param name="e">An object that contains the navigation data.</param>  
        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            ((ModernViewModelBase)DataContext).OnNavigatedFrom(e);
        }

        /// <summary>  
        /// Called when a this instance becomes the active content in a frame.  
        /// </summary>  
        /// <param name="e">An object that contains the navigation data.</param>  
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            ((ModernViewModelBase)DataContext).OnNavigatedTo(e);
        }

        /// <summary>  
        /// Called just before this instance is no longer the active content in a frame.  
        /// </summary>  
        /// <param name="e">  
        /// An object that contains the navigation data.  
        /// </param>  
        /// <remarks>  
        /// The method is also invoked when parent frames are about to navigate.  
        /// </remarks>  
        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ((ModernViewModelBase)DataContext).OnNavigatingFrom(e);
        }
    }  
}
