using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
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
    public sealed partial class PCPage : Page
    {
        public PCPage()
        {
            this.InitializeComponent();
            
        }
        NPCViewModel npcViewModel = new NPCViewModel();

        private void EnkanpButton_OnClick(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(PCDetailsPage),sender);
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            PopUpAddNewRelativePanel.Visibility = Visibility.Collapsed;
            npcViewModel.NPCTemplate();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            PopUpAddNewRelativePanel.Visibility = Visibility.Visible;
           
        }

        private void Page_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PCDetailsPage), sender);
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PCDetailsPage), sender);
        }

        //private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        //{
        //    EditStackPanel.Visibility = Visibility.Collapsed;
        //}
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
