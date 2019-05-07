using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMastersTools.ViewModel;

namespace GameMastersTools.Handler

    
{
    class UserHandler
    {
        public UserViewModel UserViewModel { get; set; }

        public UserHandler(UserViewModel userViewModel)
        {
            UserViewModel = userViewModel;
        }

        public void CreateUser()
        {
                UserViewModel.Add(UserViewModel.UserName,UserViewModel.UserPassword);
        
        }


    }
}
