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
     public class UserViewModel
    {
        #region Properties

        public ICommand LoginCommand { get; set; }

        /// <summary> This is a list of User objects that stores Users from the database.  </summary>
        public List<User> Users { get; set; }

        /// <summary> This UserName property is used to store the users input from the usernamebox from the login page. </summary>
        public string UserName { get; set; }

        /// <summary> This password property is used to store the users input from the passwordbox from the login page. </summary>
        public string Password { get; set; }
        
        public bool UserDoesNotExist { get; set; }

        public bool PasswordIsIncorrect { get; set; }

        /// <summary> This static LoggedInUserId stores the ID of the user who logs in. </summary>
        public static int LoggedInUserId { get; set; }

        /// <summary>  This property stores the user, who logs in, as a static object. </summary>
        public static User LoggedInUser { get; set; }

        public string BackgroundImage { get; set; }

        #endregion

        #region Constructor

        public UserViewModel()
        {
            LoginCommand = new RelayCommand(Login);

            


            

        }

        #endregion


        #region Methods

        /// <summary>
        /// This method checks if the inputted UserName and Password matches a users UserName and UserPassword in the database.
        /// If it does the user is logged in, if not then an error message is shown.
        /// Once a user has logged in it then stores the user as an object as well as stores the users UserId
        /// </summary>
        public void Login()
        {   Users = DatabasePersistency.LoadUsers().Result.ToList();
            UserDoesNotExist = true;
            PasswordIsIncorrect = true;
            
            foreach (var user in Users)
            {
                if (UserName == user.UserName)
                {
                    UserDoesNotExist = false;
                    if (Password == user.UserPassword)
                    {
                        //Static ID for logged in User
                        LoggedInUserId = user.UserId;
                        PasswordIsIncorrect = false;

                        //Returned User Object
                        LoggedInUser =  DatabasePersistency.GetSingleUser(user.UserId);
                        MainPageViewModel.LoggedInUserName = LoggedInUser.UserName;

                        //Navigation
                        Frame loginFrame = Window.Current.Content as Frame;
                        if (loginFrame != null)
                        { loginFrame.Navigate(typeof(MainPage)); } break;
                    }
                }
            }
            if (UserDoesNotExist)
            { new MessageDialog("Invalid Username").ShowAsync();}
            else if (PasswordIsIncorrect)
            { new MessageDialog("Invalid Password").ShowAsync();}

            
        }

        #endregion

        #region MessageDialogHelper

        private class MessageDialogHelper
        {
            public static async void Show(string content, string title)
            { 
                MessageDialog messageDialog = new MessageDialog(content, title);
                await messageDialog.ShowAsync();
            }
        }

        #endregion



    }

}
