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
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MenuSplitView.IsPaneOpen = !MenuSplitView.IsPaneOpen;
        }

        private void IconsListbox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Campaign.IsSelected)
            {
                MainFrame.Navigate(typeof(CampaignPage));
                
            }
            else if (NPCs.IsSelected)
            {
                MainFrame.Navigate(typeof(NPCPage));
            }
            else if (PCs.IsSelected)
            {
                MainFrame.Navigate(typeof(PCPage));
            }
            else if (Encounters.IsSelected)
            {
                MainFrame.Navigate(typeof(EncounterPage));
            }
            else if (Locations.IsSelected)
            {
                MainFrame.Navigate(typeof(LocationPage));
            }
            else if (Items.IsSelected)
            {
                MainFrame.Navigate(typeof(ItemPage));
            }
            else if (Lore.IsSelected)
            {
                MainFrame.Navigate(typeof(LorePage));
            }
        }
    }
}
