using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameMastersTools.Common;
using GameMastersTools.View;

namespace GameMastersTools.ViewModel
{
    public class MainPageViewModel
    {
        public static string LoggedInUserName { get; set; }
        public ICommand LogOutCommand { get; set; }

        public MainPageViewModel()
        {
            
            LogOutCommand = new RelayCommand(LogOut);
        }

        /// <summary> This method logs the user out and returns the user to the login page </summary>
        public void LogOut()
        {
            //Logging User Out
            UserViewModel.LoggedInUser = null;
            UserViewModel.LoggedInUserId = 0;

            //Navigation
            Frame loginFrame = Window.Current.Content as Frame;
            if (loginFrame != null)
            {
                loginFrame.Navigate(typeof(LoginPage));
            }

            
        }
    }
}
