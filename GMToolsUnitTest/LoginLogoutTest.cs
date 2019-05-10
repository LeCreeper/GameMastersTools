using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMastersTools.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GMToolsUnitTest
{
    [TestClass]
    class LoginLogoutTest
    {
        public string message = "Test Failed";
        GameMastersTools.ViewModel.UserViewModel userViewModel = new UserViewModel();
        
        [TestMethod]
        public void LoginTest1()
        {
            userViewModel.UserName = "Hans";
            userViewModel.Password = "12345678";
            userViewModel.Login();

            Assert.IsFalse(userViewModel.UserDoesNotExist);

            
            

        }
    }
}
