using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameMastersTools.Model;
using GameMastersTools.Persistency;
using GameMastersTools.View;
using GameMastersTools.ViewModel;

namespace GameMastersTools.Handler


{
    public class UserHandler
    {
        public CreateUserViewModel CreateUserViewModel { get; set; }
        public bool IsSuccesful = false;

        public UserHandler(CreateUserViewModel createUserViewModel)
        {
            CreateUserViewModel = createUserViewModel;
        }

        public  void CreateUser()
        {
     
                UserNameCheck();
                if (IsSuccesful) CreateUserViewModel.Add(CreateUserViewModel.UserName, CreateUserViewModel.UserPassword);
                

        }
        /// <summary>
        /// Checks if username has the correct form when signing up
        /// 
        /// </summary>
        public async void UserNameCheck()
        {


            //try
            //{
            //if (!Regex.IsMatch(CreateUserViewModel.UserName, "\\b[A - Za - z]{ 1}[a-zA-Z0-9-._]{3,19}"))
            //{
            //    CreateUserViewModel.UserErrorMessage = "Username must start with a letter, \ncontain letters, numbers and ._- and be between 4 - 20 characters";
            //    IsSuccesful = false;
            //    return;

            //}

            //// Does name start with a number
            //    if (Regex.IsMatch(CreateUserViewModel.UserName, "^[0-9._-]"))
            //    {
            //        CreateUserViewModel.UserErrorMessage = "Name has to start with a letter";
            //    }
            //    // Is name not in between 4 - 20 characters
            //    else if (!Regex.IsMatch(CreateUserViewModel.UserName, "^([\\w\\W]{4,20})$"))
            //    {
            //        CreateUserViewModel.UserErrorMessage = "Name has to be between 4 - 20 characters";

            //    }
            //    else if (!Regex.IsMatch(CreateUserViewModel.UserName, "^[a-zA-Z0-9.-_]$"))
            //    {
            //        CreateUserViewModel.UserErrorMessage = "Name can only contain [. - and _]";

            //    }
      


            //}

            if (CreateUserViewModel.UserName == null || CreateUserViewModel.UserName.Length < 4 || CreateUserViewModel.UserName.Length > 20)
                {
                    CreateUserViewModel.UserErrorMessage = "Username must be between 4 & 20 characters";
                    IsSuccesful = false;
                    return;
                    // throw new Exception("Username must be between 4 & 20 characters");
                }

                if (CreateUserViewModel.UserPassword == null || CreateUserViewModel.UserPassword.Length < 6)
                {
                    CreateUserViewModel.UserErrorMessage = "Password must have more than 5 characters";
                    IsSuccesful = false;
                    return;
                    // throw new Exception("Password must have more than 5 characters");
                }

                if (CreateUserViewModel.UserPassword != CreateUserViewModel.UserPasswordRepeat)
                {
                    CreateUserViewModel.UserErrorMessage = "Passwords must be the same";
                    IsSuccesful = false;
                    return;
                    //  throw new Exception("Passwords must be the same");
                }

                if (Regex.IsMatch(CreateUserViewModel.UserName, "\\W"))
                {
                    CreateUserViewModel.UserErrorMessage = "Name can not contain any special characters";
                    IsSuccesful = false;
                    return;
                    //  throw new Exception("Username can not contain any special characters");
                }

                if (Regex.IsMatch(CreateUserViewModel.UserName, "^[0-9\\W]"))
                {
                    CreateUserViewModel.UserErrorMessage = "Name must start with a letter";
                    IsSuccesful = false;
                    return;
            }


                foreach (var user in CreateUserViewModel.Users)
                {
                    if (CreateUserViewModel.UserName == user.UserName)
                    {
                        CreateUserViewModel.UserErrorMessage = "Username already exists";
                        IsSuccesful = false;
                        return;
                        // throw new Exception("User name already exists");
                    }

                    
                }

                IsSuccesful = true;
            //}
            //catch (Exception e)
            //{
            //    await new MessageDialog(e.Message).ShowAsync();
            //}

       


}

        public void NavigateFrontPage() { 

        // Navigating to LoginPage if successful
        if (Window.Current.Content is Frame frame) frame.Navigate(typeof(LoginPage));

        // Or like this:
        //Frame newFrame = Window.Current.Content as Frame;
        //if (newFrame != null) newFrame.Navigate(typeof(LoginPage));


        // Showing a message dialog if system is successful in adding the user to the database
        }
    }
}
