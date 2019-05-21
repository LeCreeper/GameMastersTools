using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GameMastersTools.ViewModel;
using System.Threading;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GameMastersTools.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PCDetailsPage : Page
    {
        private bool hasFocus = false;

        public PCDetailsPage()
        {
            this.InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PCPage));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PopUpEditRelativePanel.Visibility = Visibility.Visible;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PopUpEditRelativePanel.Visibility = Visibility.Collapsed;
        }

        private async void PopUpDescriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PcViewModel vm = new PcViewModel();

            hasFocus = true;

            while (hasFocus)
            {
                vm.PcSingleton.UpdatePc(PcViewModel.SelectedPc);
                await Task.Delay(10000);
                SaveTextBlock.Text = "Changes saved";
                await Task.Delay(1000);
                SaveTextBlock.Text = "";
                
            }
        }

        private void PopUpDescriptionTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            hasFocus = false;
        }
    }
}
