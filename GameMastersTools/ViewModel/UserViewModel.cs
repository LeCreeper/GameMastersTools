using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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

namespace GameMastersTools.ViewModel
{
    class UserViewModel : INotifyPropertyChanged
    {


        // Fejlmeddelelser i UserName & UserPassword properties
        #region Fejl meddelelser i UserName & UserPassword properties
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;

                UserErrorMessage = "Ok";

                if (_userName.Length < 6)
                {

                    NameErrorVisibility = "Visible";
                    UserErrorMessage = $"Your username needs to be greater than {_userName.Length} please | Six or more characters.";
                }
                else if (_userName.Length > 20)
                {
                    NameErrorVisibility = "Visible";
                    UserErrorMessage = $"Username needs to be less than {_userName.Length} please | Max 20 characters";
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

                if (_userPassword.Length < 8)
                {
                    PasswordErrorMessage = $"Password needs to be greater than {_userPassword.Length} characters please | At least 8";
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

                PasswordErrorMessage = "Please write down your password, you WILL forget it.";

                if (_userPasswordRepeat != _userPassword)
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

        }


        // Add user method | Tjekker om navn og password har de rigtige længder,
        // hvis det er tilfældet, kører den en database metode (Hold musen over CheckThenPost metode for mere info)

        public async void Add(string name, string password)
        {
            User user = new User(name, password);
            
            if (name.Length > 5 && name.Length < 21)
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
