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
        public CreateUserViewModel CreateUserViewModel { get; set; }

        public UserHandler(CreateUserViewModel createUserViewModel)
        {
            CreateUserViewModel = createUserViewModel;
        }

        public void CreateUser()
        {
            CreateUserViewModel.Add(CreateUserViewModel.UserName, CreateUserViewModel.UserPassword);

        }


    }
}
