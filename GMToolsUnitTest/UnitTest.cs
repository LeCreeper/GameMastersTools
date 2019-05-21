
using System;
using Windows.Security.Cryptography.Core;
using Windows.UI.Popups;
using GameMastersTools.Handler;
using GameMastersTools.Model;
using GameMastersTools.Persistency;
using GameMastersTools.View;
using GameMastersTools.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GMToolsUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        #region Login/Logout


        private UserViewModel userViewModel = new UserViewModel();

        [TestMethod]
        public void UserNameTest()
        {   // Test whether the system can recognise a correct Username
            userViewModel.UserName = "Hans";
            userViewModel.Password = "12345678";
            userViewModel.Login();
            Assert.IsFalse(userViewModel.UserDoesNotExist);   }

        [TestMethod]
        public void UserNameTest2()
        {   // Test whether lower case or upper case letters influence the UserName, 
            // as well as if the system checks if the password is correct. 
            userViewModel.UserName = "hans";
            userViewModel.Password = "12345678";
            userViewModel.Login();
            Assert.IsTrue(userViewModel.UserDoesNotExist); }
        
        [TestMethod]
        public void PasswordTest()
        {   // Test whether the method can recognise a correct password
            userViewModel.UserName = "Hans";
            userViewModel.Password = "12345678";
            userViewModel.Login();
            Assert.IsFalse(userViewModel.PasswordIsIncorrect); }

        [TestMethod] 
        public void PasswordTest2()
        {   // Test whether the method checks if the password is wrong. 
            userViewModel.UserName = "Hans";
            userViewModel.Password = "123456789";
            userViewModel.Login();
            Assert.IsTrue(userViewModel.PasswordIsIncorrect); }

        [TestMethod]
        public void LogOutTest()
        {   // Test whether the LogOut() method sets LoggedInUserId = 0
            MainPageViewModel mainPageViewModel = new MainPageViewModel();
            mainPageViewModel.LogOut();
            Assert.AreEqual(UserViewModel.LoggedInUserId,0); }

        [TestMethod]
        public void LogOutTest2()
        {   // Test whether the LogOut() method sets LoggedInUser = null
            MainPageViewModel mainPageViewModel = new MainPageViewModel();
            mainPageViewModel.LogOut();
            Assert.IsNull(UserViewModel.LoggedInUser); }
        #endregion



        #region CreateUserTest WIP
        // Name is empty, password is empty, password is below limit, password is below limit, both are below limit

        //[TestMethod]
        public void NameCanBe_BelowLimit_ReturnFalse()
        {
            // Arrange
            CreateUserViewModel vm = new CreateUserViewModel();

            // Add
            string IllegalName = "Bob";
            string LegalPassword = "123456";

            // Assert
            try
            {
                vm.Add(IllegalName, LegalPassword);
               
            }
            catch (Exception e)
            {
                Assert.Fail("Der skete noget\n"+ e.Message);
            }
        }

        //[TestMethod]
        public void NameCanBe_WithinTheLimit_ReturnTrue()
        {
            // Arrange
            CreateUserViewModel vm = new CreateUserViewModel();

            // Add
            string LegalName = "Bobbie";
            string LegalPassword = "12345678";

            // Assert
            try
            {
                vm.Add(LegalName, LegalPassword);
                Assert.Fail("Succesfully added user with " + LegalName + " and " + LegalPassword);

            }
            catch (Exception e)
            {
                Assert.Fail("Threw an exception\n\n" + e.Message);
            }
        }

        #endregion

        #region PlayerCharacterTest
        // Still succeeds despite ...
        //[TestMethod] 
        public void NameCanBe_TheSameAsExisting_ReturnFalse()
        {
            // Arrange
            PcViewModel pcvm = new PcViewModel();

            pcvm.PcName = "Crane";
            pcvm.PcDescription = "UnitTest";
            UserViewModel.LoggedInUserId = 8;
          

            pcvm.PcHandler.CreatePc();


            // Act
            // Assert
        }

        #endregion

        #region AddPlayer to database
        //[TestMethod]
        public void Player_HasBeenAdded_PlayerListPlusOne()
        {
            PcViewModel pcvm = new PcViewModel();

            int beforeAdd = pcvm.PcCount();

            

            pcvm.UserPcs.Add(new PC("UnitTests", "A description", 8));

          

            int afterAdd = beforeAdd + 1;

            Assert.IsTrue(beforeAdd == afterAdd );
        }
        

        #endregion
    }
}
