
using System;
using GameMastersTools.View;
using GameMastersTools.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GMToolsUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        #region Login/Logout
        [TestMethod]
        public void UserNameTest()
        {   // Test whether the system can recognise a correct Username
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.UserName = "Hans";
            userViewModel.Password = "12345678";
            userViewModel.Login();
            Assert.IsFalse(userViewModel.UserDoesNotExist);   }

        [TestMethod]
        public void UserNameTest2()
        {   // Test whether lower case or upper case letters influence the UserName, 
            // as well as if the system checks if the password is correct. 
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.UserName = "hans";
            userViewModel.Password = "12345678";
            userViewModel.Login();
            Assert.IsTrue(userViewModel.UserDoesNotExist); }

        [TestMethod]
        public void PasswordTest()
        {   // Test whether the method can recognise a correct password
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.UserName = "Hans";
            userViewModel.Password = "12345678";
            userViewModel.Login();
            Assert.IsFalse(userViewModel.PasswordIsIncorrect); }

        [TestMethod]
        public void PasswordTest2()
        {   // Test whether the method checks if the password is wrong. 
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.UserName = "Hans";
            userViewModel.Password = "123456789";
            userViewModel.Login();
            Assert.IsTrue(userViewModel.PasswordIsIncorrect); }

        [TestMethod]
        public void LogOutTest()
        {   // Test whether the LogOut() method sets LoggedInUserId = 0
            MainPageViewModel mainPageViewModel = new MainPageViewModel();
            mainPageViewModel.LogOut();
            Assert.AreSame(UserViewModel.LoggedInUserId,0); }

        [TestMethod]
        public void LogOutTest2()
        {   // Test whether the LogOut() method sets LoggedInUser = null
            MainPageViewModel mainPageViewModel = new MainPageViewModel();
            mainPageViewModel.LogOut();
            Assert.IsNull(UserViewModel.LoggedInUser); }
        #endregion
    }
}
