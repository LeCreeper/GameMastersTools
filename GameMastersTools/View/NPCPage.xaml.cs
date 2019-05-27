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

       
        private bool EditInProgress { get; set; }
        public NPCPage()
        {
            this.InitializeComponent();
         
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
                    
                    NPCViewModel.UpdateNPC();
                    EditButton.Content = "Edit Mode";
                    EditInProgress = false;
                    DetailedNPCDescription.IsReadOnly = true;
                    DetailedNPCName.IsReadOnly = true;
                    break;

            }
            
        }

        /// <summary>
        /// This method locks the Detailed NPC view, when the user selects a different NPC, as well as ensuring that the selectedNPC property is set correctly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditButton.Content = "Edit Mode";
            EditInProgress = false;
            DetailedNPCDescription.IsReadOnly = true;
            NPCViewModel.StaticSelectedNpc = (NPC) ListNPCS.SelectedItem;

        }

        
    }
}
