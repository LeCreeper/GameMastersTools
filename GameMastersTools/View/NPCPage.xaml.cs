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
using GameMastersTools.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GameMastersTools.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NPCPage : Page
    {
        private NPCViewModel npcViewModel;
        public NPCPage()
        {
            this.InitializeComponent();
            npcViewModel = new NPCViewModel();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PopupAddNewStackPanel.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PopupAddNewStackPanel.Visibility = Visibility.Visible;
            npcViewModel.Description = "Gender: \nRace: \nVoice: \nPersonality: \nLikes: \nDislikes: \nQuirks: ";
        }


    }
}
