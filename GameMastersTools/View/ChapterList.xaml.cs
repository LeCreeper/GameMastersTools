using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class ChapterList : Page
    {
        public ChapterList()
        {
            this.InitializeComponent();
        }

        private void NavigateToChapterButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChapterDetails));
        }

        private void ShowRemoveCampaignPcPopupOffsetClicked(object sender, RoutedEventArgs e)
        {
            // open the Popup if it isn't open already 
            if (!RemoveCampaignPcPopUp.IsOpen) { RemoveCampaignPcPopUp.IsOpen = true; }
        }

        private void CloseRemoveCampaignPcPopupClicked(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 
            if (RemoveCampaignPcPopUp.IsOpen) { RemoveCampaignPcPopUp.IsOpen = false; }
        }

        private void ShowDeleteChapterPopupOffsetClicked(object sender, RoutedEventArgs e)
        {
            // open the Popup if it isn't open already 
            if (!DeleteChapterPopUp.IsOpen) { DeleteChapterPopUp.IsOpen = true; }
        }

        private void CloseDeleteChapterPopupClicked(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 
            if (DeleteChapterPopUp.IsOpen) { DeleteChapterPopUp.IsOpen = false; }
        }
    }
}
