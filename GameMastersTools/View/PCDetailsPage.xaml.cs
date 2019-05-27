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
using GameMastersTools.Model;
using GameMastersTools.Persistency;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GameMastersTools.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PCDetailsPage : Page
    {
        private Timer timer;

        private bool hasFocus = false;
        PcViewModel pcvm = new PcViewModel();

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
            timer = new Timer(SaveObj, null, 3000, 5000);
            ResetTimer();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PopUpEditRelativePanel.Visibility = Visibility.Collapsed;
            timer.Dispose();
        }

        private void PopUpDescriptionTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // Når man bliver ved med at taste i tekstblokken vil ResetTimer metoden hele tiden blive kaldt, den nulstiller tiden på en timer. Hvis man holder op med at taste
            // vil timeren løbe ud og en metode der gemmer vil blive kaldt.
           ResetTimer();


            SaveTextBlock.Text = "Saving...";

          


            //await new Task(Timer);

            //Task.Delay(3000);


            //SaveTextBlock.Text = "Changes Saved";



            //Task.Delay(1000);

            //SaveTextBlock.Text = "";

            //if (Task.Delay(3000).IsCompleted)
            //{
            //    SaveTextBlock.Text = "Changes saved";
            //}


            //Task createTime = new Task( () => Task.Delay(3000));
            //createTime.Start();







            //await Task.Delay(1000);

            //SaveTextBlock.Text = "";


            //if (startTimer.IsCompleted)
            //{
            //    SaveTextBlock.Text = "Changes saved";
            //    pcvm.PcSingleton.UpdatePc(PcViewModel.SelectedPc);

            //}


        }

        public async void SaveObj(object state)
        {
            
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () =>
                {

                    SaveTextBlock.Text = "Changes has been saved";
                    // Database Logic here
                    pcvm.PcSingleton.UpdatePc(PcViewModel.SelectedPc);

                    // await Task.Delay(1000);

                    // PcViewModel.ErrorMessage = "_";
                }
            );

        }
        public void ResetTimer()
        {

            timer.Change(TimeSpan.FromMilliseconds(3000), TimeSpan.FromMilliseconds(5000));
        }

        //private async void PopUpDescriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    PcViewModel vm = new PcViewModel();

        //    hasFocus = true;

        //    while (hasFocus)
        //    {
        //        vm.PcSingleton.UpdatePc(PcViewModel.SelectedPc);
        //        await Task.Delay(10000);
        //        SaveTextBlock.Text = "Changes saved";
        //        await Task.Delay(1000);
        //        SaveTextBlock.Text = "";

        //    }
        //}

        //private void PopUpDescriptionTextBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    hasFocus = false;
        //}

        

        public async void Timer()
        {
            await Task.Delay(3000);
        }

        
      

        private void PopUpDescriptionTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {

        }
      
   
    }
}
