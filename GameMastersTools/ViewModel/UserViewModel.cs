using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using GameMastersTools.Common;
using GameMastersTools.Model;
using GameMastersTools.Persistency;

namespace GameMastersTools.ViewModel
{
    class UserViewModel
    {
        public ICommand LoginCommand { get; set; }

        public List<User> Users { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public UserViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            
            Users =  DatabasePersistency.LoadUsers().Result.ToList();
            
        }

        

        public void Login()
        {
            foreach (var user in Users)
            {
                if (UserName == user.UserName && Password == user.UserPassword)
                {
                    new MessageDialog("USER LOGGED IN").ShowAsync();

                }

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
