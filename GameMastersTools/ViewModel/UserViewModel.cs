using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameMastersTools.Annotations;
using GameMastersTools.Common;
using GameMastersTools.Handler;
using GameMastersTools.Model;
using GameMastersTools.View;
using GameMastersTools.Persistency;

namespace GameMastersTools.ViewModel
{
    class UserViewModel : INotifyPropertyChanged
    {
      //properties for login system
      public ICommand LoginCommand { get; set; }

        public List<User> Users { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public static int LoggedInUserId { get; set; }

        public static User LoggedInUser { get; set; }
    
        // UserName & UserPassword properties | Med Fejlmeddelelser
        #region Fejl meddelelser i UserName & UserPassword properties
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                
                UserErrorMessage = "Ok";
                _userName = value;

                if (value.Length < 6)
                {
                    NameErrorVisibility = "Visible";
                    UserErrorMessage = $"Your username length needs to be greater than {_userName.Length} characters | At least 6";
                }
                else if (value.Length > 20)
                {
                    NameErrorVisibility = "Visible";
                    UserErrorMessage = $"Username needs to be less than {_userName.Length} | Max 20 characters";
                }

               

            }
        }

        private string _userPassword;
        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                _userPassword = value;


                PasswordErrorMessage = "Ok";

                if (value.Length < 8)
                {
                    PasswordErrorMessage = $"Password length needs to be greater than {_userPassword.Length} characters | At least 8";
                }
            }
        }

        private string _userPasswordRepeat;
        public string UserPasswordRepeat
        {
            get { return _userPasswordRepeat; }
            set
            {
                _userPasswordRepeat = value;

                PasswordErrorMessage = "Please write down your password, you won't get a new one";

                if (value != _userPassword)
                {
                    PasswordErrorMessage = $"Passwords need to be the same";
                }
            }
        }
        #endregion

        // UserName & UserPassword Error Storing properties
        #region UserName & UserPassword ErrorStoring properties
        private string _userErrorMessage;
        public string UserErrorMessage
        {
            get { return _userErrorMessage; }
            set { _userErrorMessage = value; OnPropertyChanged(); }
        }

        private string _passwordErrorMessage;
        public string PasswordErrorMessage
        {
            get { return _passwordErrorMessage; }
            set { _passwordErrorMessage = value; OnPropertyChanged(); }
        }
        #endregion

        // Skulle vise og gemme error tekstboksen - fungerer ikke helt optimalt
        #region Viser og skjuler en textboks der viser fejl
        private string _nameErrorVisibility;
        public string NameErrorVisibility
        {
            get { return _nameErrorVisibility; }
            set { _nameErrorVisibility = value; OnPropertyChanged(); }
        }

        private string _passwordErrorVisibility;
        public string PasswordErrorVisibility
        {
            get { return _passwordErrorVisibility; }
            set { _passwordErrorVisibility = value; OnPropertyChanged(); }
        }
        #endregion


        // HandlerClass | Ikke helt sikker på hvad formålet med den her er endnu, om vi overhovedet vil beholde den
        public Handler.UserHandler UserHandler { get; set; }

        // AddUser ICommand -> RelayCommand
        private ICommand _addUserCommand;
        public ICommand AddUserCommand
        {
            get { return _addUserCommand ?? (_addUserCommand = new RelayCommand(UserHandler.CreateUser)); }
            set { _addUserCommand = value; }
        }

        

        // Constructor
        public UserViewModel()
        {
            
            UserHandler = new Handler.UserHandler(this);
            LoginCommand = new RelayCommand(Login);
            
            Users =  DatabasePersistency.LoadUsers().Result.ToList();
            LoggedInUserId = 0;

        }


        // Add user method | Tjekker om navn og password har de rigtige længder,
        // hvis det er tilfældet, kører den en database metode (Hold musen over CheckThenPost metode for mere info)

        public async void Add(string name, string password)
        {
            User user = new User(name, password);
            
            

            if (name != null && name.Length > 5 && name.Length < 21)
            {
                if (password.Length > 7)
                {

                    await Persistency.DatabasePersistency.CheckThenPost(user, name);

                    //try
                    //{
                    //    await Persistency.DatabasePersistency.CheckThenPost(user, name);
                    //}
                    //catch (Exception e)
                    //{
                    //    await new MessageDialog(e.Message).ShowAsync();
                    //}
                    
                }
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
                        new MessageDialog("USER LOGGED IN").ShowAsync();

                        //Static ID for logged in User
                        LoggedInUserId = user.UserId;
                        passwordIsInCorrect = false;


                        //Returned User Object

                        LoggedInUser = DatabasePersistency.GetSingleUser(user.UserId);
                        break;
                    }
                
                }

            }
            
            if (userDoesNotExist)
            {
                //new MessageDialog("Invalid Username").ShowAsync();
            }

            else if (passwordIsInCorrect)
            {
                //new MessageDialog("Invalid Password").ShowAsync();
            }

            
        }
        }

        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
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

