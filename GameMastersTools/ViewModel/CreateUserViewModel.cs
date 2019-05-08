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
    class CreateUserViewModel : INotifyPropertyChanged
    {
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

        private ICommand _goBackCommand;

        public ICommand GoBackCommand
        {
            get { return _goBackCommand ?? (_goBackCommand = new RelayCommand(GoBack));}
            set { _goBackCommand = value; }
        }

        //private string _goBack;
        //public string GoBack
        //{
        //    get { return _goBack; }
        //    set
        //    {
        //        _goBack = Window.Current.Content as Frame;
        //        _goBack = value;

        //    }
        //} 

        // Constructor
        public CreateUserViewModel()
        {

            UserHandler = new Handler.UserHandler(this);

        }

        public void GoBack()
        {
            Frame newFrame = Window.Current.Content as Frame;
            if (newFrame != null)
            {
                if (newFrame.CanGoBack)
                {
                    newFrame.GoBack();
                }
            }
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

                    //await Persistency.DatabasePersistency.CheckThenPost(user, name);

                    try
                    {
                        await Persistency.DatabasePersistency.CheckThenPost(user, name);
                    }
                    catch (Exception e)
                    {
                        await new MessageDialog("Noget er gået galt\n" + e.Message).ShowAsync();
                    }

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
