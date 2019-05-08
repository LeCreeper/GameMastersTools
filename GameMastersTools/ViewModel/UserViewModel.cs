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

        /// <summary> This is a list of User objects that stores Users from the database.  </summary>
        public List<User> Users { get; set; }

        /// <summary> This UserName property is used to store the users input from the usernamebox from the login page. </summary>

        public string UserName { get; set; }

        /// <summary> This password property is used to store the users input from the passwordbox from the login page. </summary>
        public string Password { get; set; }

        /// <summary> This static LoggedInUserId stores the ID of the user who logs in. </summary>
        public static int LoggedInUserId { get; set; }

        /// <summary>  This property stores the user, who logs in, as a static object. </summary>
        public static User LoggedInUser { get; set; }

        public UserViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            

            LoggedInUserId = 0;
        }

        
        /// <summary>
        /// This method checks if the inputted UserName and Password matches a users UserName and UserPassword in the database.
        /// If it does the user is logged in, if not then an error message is shown.
        /// Once a user has logged in it then stores the user as an object as well as stores the users UserId
        /// </summary>
        public void Login()
        {
            //TODO fix dis shit!!! important! wtf?!
            Users = DatabasePersistency.LoadUsers().Result.ToList();

            bool userDoesNotExist = true;
            bool passwordIsInCorrect = true;

            Users = DatabasePersistency.LoadUsers().Result.ToList();

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


                       // LoggedInUser =  DatabasePersistency.GetSingleUser(user.UserId);


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
