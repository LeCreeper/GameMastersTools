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
using GameMastersTools.Model;
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
        private bool EditInProgress { get; set; }
        public NPCPage()
        {
            this.InitializeComponent();
            npcViewModel = new NPCViewModel();
            EditInProgress = false;
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

        /// <summary>
        /// This method is used to control the Edit and Update feature of the detailed NPC view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditMode(object sender, RoutedEventArgs e)
        {

            switch (EditInProgress)
            {
                case false:
                    EditButton.Content = "Save Changes";
                    DetailedNPCDescription.IsReadOnly = false;
                    DetailedNPCName.IsReadOnly = false;
                    EditInProgress = true;

                    break;

                case true:
                    
                    npcViewModel.UpdateNPC();
                    EditButton.Content = "Edit Mode";
                    EditInProgress = false;
                    DetailedNPCDescription.IsReadOnly = true;
                    DetailedNPCName.IsReadOnly = true;
                    break;

            }
            
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditButton.Content = "Edit Mode";
            EditInProgress = false;
            DetailedNPCDescription.IsReadOnly = true;
            npcViewModel.selectedNPC = (NPC) ListNPCS.SelectedItem;

        }

        
    }
}
