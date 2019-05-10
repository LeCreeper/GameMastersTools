using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.OnlineId;
using Windows.UI.Popups;
using GameMastersTools.Annotations;
using GameMastersTools.Model;
using GameMastersTools.Persistency;
using GameMastersTools.ViewModel;

namespace GameMastersTools.Singleton
{
    class PcSingleton
    {
        
        private static PcSingleton _instance = null;

        public static PcSingleton Instance
        {
            get { return _instance ?? (_instance = new PcSingleton()); }
        }

        public ObservableCollection<PC> Pcs { get; set; }
        
  
        public PcSingleton()
        {
           Pcs = new ObservableCollection<PC>();
            
           
           LoadPc();
            
        }

        public void PostPc(string name, string description)
        {
            // Creating a new user to be added, with the values coming in from the view, through the PcHandler and PcViewModel - UserId is the one belonging to the logged in user
            PC newPc = new PC(name, description, UserViewModel.LoggedInUserId);

            // Trying to post the user to the database, if successful, it returns and then adds it to the list in the app to be shown in the UI
            GenericDbPersistency<PC>.PostObj(newPc, "api/pcs");

            Pcs.Add(newPc);
            
        }

        public void DeletePc(PC pc)
        {
            //Pcs.Remove(pc);
            GenericDbPersistency<PC>.DeleteObj("api/pcs/", pc.PcId);
            Pcs.Remove(pc);
        }


        // Loading all player characters from the database into a list | Then sorting it via a linq query, so only the players belonging to the logged in user is shown
        public async void LoadPc()
        {
            try
            {
                var pcs = await GenericDbPersistency<PC>.GetObj("api/pcs");

                if (pcs != null)
                {
                    foreach (var p in pcs)
                    {
                        Pcs.Add(p);
                    }
                }
            }
            catch (Exception e)
            {
                new MessageDialog("ERROE\n\n" + e.Message);
            }

        }



        //public void PostPc(string name,string description)
        //{
        //    // Creating a new user to be added, with the values coming in from the view, through the PcHandler and PcViewModel - UserId is the one belonging to the logged in user
        //    PC newPc = new PC(name,description,UserViewModel.LoggedInUserId);

        //    // Trying to post the user to the database, if successful, it returns and then adds it to the list in the app to be shown in the UI
        //    GenericDbPersistency<PC>.PostObj(newPc, "api/pcs");
        //    Pcs.Add(newPc);
        //}

        //// Loading all player characters from the database into a list
        //public async void LoadPc()
        //{
        //    try
        //    {
        //        var pcs = await GenericDbPersistency<PC>.GetObj("api/pcs");

        //        if (pcs != null)
        //        {
        //            var userPcs = from pc in pcs
        //                where pc.UserId == UserViewModel.LoggedInUserId
        //                select pc;

        //            foreach (var p in userPcs)
        //            {
        //                Pcs.Add(p);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        new MessageDialog("ERROE\n\n" + e.Message);
        //    }

        //}
    }
}
