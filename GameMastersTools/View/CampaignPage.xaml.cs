using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GameMastersTools.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CampaignPage : Page
    {
        public CampaignPage()
        {
            this.InitializeComponent();
        }

        private void NavigateToCampaignButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChapterList));
        }

        private void ShowDeletePopupOffsetClicked(object sender, RoutedEventArgs e)
        {
            // open the Popup if it isn't open already 
            if (!DeletePopUp.IsOpen) { DeletePopUp.IsOpen = true; }
        }

        private void CloseDeletePopupClicked(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 
            if (DeletePopUp.IsOpen) { DeletePopUp.IsOpen = false; }
        }

        private void CampaignGridview_ItemClick(object sender, ItemClickEventArgs e)
        {
            CampaignDetails.Visibility = Visibility.Visible;
        }

        private void CloseCampaignDetails_OnClick(object sender, RoutedEventArgs e)
        {
            CampaignDetails.Visibility = Visibility.Collapsed;
        }

        private void CampaignGridview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
