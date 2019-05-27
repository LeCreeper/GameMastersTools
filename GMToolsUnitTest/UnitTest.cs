
using System;
using Windows.Security.Cryptography.Core;
using Windows.UI.Popups;
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
        //#region Login/Logout


        //private UserViewModel userViewModel = new UserViewModel();

        //[TestMethod]
        //public void UserNameTest()
        //{   // Test whether the system can recognise a correct Username
        //    userViewModel.UserName = "Hans";
        //    userViewModel.Password = "12345678";
        //    userViewModel.Login();
        //    Assert.IsFalse(userViewModel.UserDoesNotExist);
        //}

        //[TestMethod]
        //public void UserNameTest2()
        //{   // Test whether lower case or upper case letters influence the UserName, 
        //    // as well as if the system checks if the password is correct. 
        //    userViewModel.UserName = "hans";
        //    userViewModel.Password = "12345678";
        //    userViewModel.Login();
        //    Assert.IsTrue(userViewModel.UserDoesNotExist); }

        //[TestMethod]
        //public void PasswordTest()
        //{   // Test whether the method can recognise a correct password
        //    userViewModel.UserName = "Hans";
        //    userViewModel.Password = "12345678";
        //    userViewModel.Login();
        //    Assert.IsFalse(userViewModel.PasswordIsIncorrect); }

        //[TestMethod] 
        //public void PasswordTest2()
        //{   // Test whether the method checks if the password is wrong. 
        //    userViewModel.UserName = "Hans";
        //    userViewModel.Password = "123456789";
        //    userViewModel.Login();
        //    Assert.IsTrue(userViewModel.PasswordIsIncorrect); }

        //[TestMethod]
        //public void LogOutTest()
        //{   // Test whether the LogOut() method sets LoggedInUserId = 0
        //    MainPageViewModel mainPageViewModel = new MainPageViewModel();
        //    mainPageViewModel.LogOut();
        //    Assert.AreEqual(UserViewModel.LoggedInUserId,0); }

        //[TestMethod]
        //public void LogOutTest2()
        //{   // Test whether the LogOut() method sets LoggedInUser = null
        //    MainPageViewModel mainPageViewModel = new MainPageViewModel();
        //    mainPageViewModel.LogOut();
        //    Assert.IsNull(UserViewModel.LoggedInUser); }
        //#endregion

        //#region CreateUserTest WIP
        //// Name is empty, password is empty, password is below limit, password is below limit, both are below limit

        //[TestMethod]
        //public void NameCanBe_BelowLimit_ReturnFalse()
        //{
        //    // Arrange
        //    CreateUserViewModel vm = new CreateUserViewModel();

        //    // Add
        //    string IllegalName = "Bob";
        //    string LegalPassword = "123456";

        //    // Assert
        //    try
        //    {
        //        vm.Add(IllegalName, LegalPassword);

        //    }
        //    catch (Exception e)
        //    {
        //        Assert.Fail("Der skete noget\n"+ e.Message);
        //    }
        //}

        //[TestMethod]
        //public void NameCanBe_WithinTheLimit_ReturnTrue()
        //{
        //    // Arrange
        //    CreateUserViewModel vm = new CreateUserViewModel();

        //    // Add
        //    string LegalName = "Bobbie";
        //    string LegalPassword = "12345678";

        //    // Assert
        //    try
        //    {
        //        vm.Add(LegalName, LegalPassword);
        //        Assert.Fail("Succesfully added user with " + LegalName + " and " + LegalPassword);

        //    }
        //    catch (Exception e)
        //    {
        //        Assert.Fail("Threw an exception\n\n" + e.Message);
        //    }
        //}

        //#endregion

        //#region PlayerCharacterTest

        //public void NameCanBe_TheSameAsExisting_ReturnFalse()
        //{
        //    // Arrange



        //    // Act
        //    // Assert
        //}

        //#endregion

        #region Create Campaign

        private CampaignVM campaignVm = new CampaignVM(); 
        //[TestMethod]
        public void CampaignNameIsTooLongTest() //Husk at udkommentere message diaglogues, da testen ellers ikke vil køre!
        {
            //This method tests if the campaign name has a length below the valid limit
            UserViewModel.LoggedInUserId = 1;
            campaignVm.Name = "Et suuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuper kampagne navn"; //length = 61 characters
            campaignVm.AddCampaign();
            Assert.IsFalse(campaignVm.AddIsSuccessful);
        }

        //[TestMethod]

        public void CampaignNameAlreadyExistsTest() //Husk at udkommentere message diaglogues, da testen ellers ikke vil køre!
        {
            //Tests that you can only add a campaign if the user doesn't already have a campaign with that name
            UserViewModel.LoggedInUserId = 1;
            campaignVm.Name = "A Tale of Awakening1";
            campaignVm.AddCampaign();
            Assert.IsTrue(campaignVm.AddIsSuccessful); //Husk at ændre test navnet, hvis testen køres igen,
                                                       //da navnet ellers vil findes i databasen og derfor ikke blive oprettet
            campaignVm.Name = "A Tale of Awakening1";
            campaignVm.AddCampaign();
            Assert.IsFalse(campaignVm.AddIsSuccessful);
        }

        [TestMethod]

        public void DeleteCampaignTest() //Husk at udkommentere message diaglogues, da testen ellers ikke vil køre!
        {
            //Tests that the delete method works, by generating a new campaign object and adding it to the database and thereafter deleting it as if it had been selected through the GUI
            Campaign campaign = new Campaign("En Kampagne1","En beskrivelse", 1);
            GenericDbPersistency<Campaign>.PostObj(campaign, "api/Campaigns");
            campaignVm.SelectedCampaign = campaign;
            campaignVm.DeleteCampaign();
            Assert.IsTrue(campaignVm.DeleteIsSuccessful);
        }

        //[TestMethod]
        public void DeleteNullCampaignTest() //Husk at udkommentere message diaglogues, da testen ellers ikke vil køre!
        {
            //Tests that you can't delete a campaign before selecting one
            campaignVm.DeleteCampaign();
            Assert.IsFalse(campaignVm.DeleteIsSuccessful);
        }

        #endregion

        private ChapterListViewModel chaptervm = new ChapterListViewModel();

        // Tests that the method creates an object of Chapter
        [TestMethod]
        public void CreateChapter()
        {
            UserViewModel.LoggedInUserId = 1;
            CampaignVM.SelectedCampaignId = 1;
            chaptervm.Name = "A chapter2";
            chaptervm.Description = "geh";
            chaptervm.AddChapter();
            Assert.IsTrue(chaptervm.CreateChapterIsSuccessful);
        }

        [TestMethod]

        public void ChapterNameAlreadyExists()
        {
            UserViewModel.LoggedInUserId = 1;
            CampaignVM.SelectedCampaignId = 1;
            chaptervm.LoadChapters();
            chaptervm.Name = "A Chapter";
            chaptervm.Description = "geh";
            chaptervm.AddChapter();
            Assert.IsTrue(chaptervm.NameAlreadyExists);
        }

        [TestMethod]

        public void DeleteChapterTest()
        {
            Chapter chapter = new Chapter("Et Kapitel", "En beskrivelse", 1);
            GenericDbPersistency<Chapter>.PostObj(chapter, "api/Chapters");
            chaptervm.SelectedChapter = chapter;
            chaptervm.DeleteChapter();
            Assert.IsTrue(chaptervm.DeleteChapterIsSuccessful);
        }
    }
}
