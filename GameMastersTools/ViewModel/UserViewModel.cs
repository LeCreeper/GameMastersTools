using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameMastersTools.Common;
using GameMastersTools.Model;
using GameMastersTools.Persistency;
using GameMastersTools.View;

namespace GameMastersTools.ViewModel
{
    class UserViewModel
    {
        public ICommand LoginCommand { get; set; }

        public List<User> Users { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public static int LoggedInUserId { get; set; }

        public static User LoggedInUser { get; set; }

        public UserViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            
            Users =  DatabasePersistency.LoadUsers().Result.ToList();
            LoggedInUserId = 0;
        }



        /// <summary>
        /// This method checks if the inputted UserName and Password matches a users UserName and UserPassword in the database.
        /// If it does the user is logged in, if not then an error message is shown
        /// </summary>
        public void Login()
        {
            bool userDoesNotExist = true;
            bool passwordIsInCorrect = true;
            foreach (var user in Users)
            {
                if (UserName == user.UserName)
                {
                    userDoesNotExist = false;
                    if (Password == user.UserPassword)
                    {
                        new MessageDialog($"Welcome {user.UserName}").ShowAsync();

                        //Static ID for logged in User
                        LoggedInUserId = user.UserId;
                        passwordIsInCorrect = false;

                        Frame loginFrame = Window.Current.Content as Frame;
                        if (loginFrame != null)
                        {
                            loginFrame.Navigate(typeof(MainPage));
                        }

                        
                        //Returned User Object


                        //LoggedInUser =  DatabasePersistency.GetSingleUser(user.UserId);


                        break;
                    }
                
                }

            }
            
            if (userDoesNotExist)
            {
                new MessageDialog("Invalid Username").ShowAsync();
            }

            else if (passwordIsInCorrect)
            {
                new MessageDialog("Invalid Password").ShowAsync();
            }

            
        }
        
        private class MessageDialogHelper
        {
            public static async void Show(string content, string title)
            { 
                MessageDialog messageDialog = new MessageDialog(content, title);
                await messageDialog.ShowAsync();
            }
        }



    }

}
