using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.OnlineId;
using Windows.UI.Popups;
using GameMastersTools.Annotations;
using GameMastersTools.Model;
using GameMastersTools.Persistency;
using GameMastersTools.ViewModel;
using System.Threading;

namespace GameMastersTools.Singleton
{
    public class PcSingleton
    {
        
        private static PcSingleton _instance = null;

        public static PcSingleton Instance
        {
            get { return _instance ?? (_instance = new PcSingleton()); }
        }

        public ObservableCollection<PC> Pcs { get; set; }
        
  
        private PcSingleton()
        {
           Pcs = new ObservableCollection<PC>();
            
           LoadPc();
        }

        public async void PostPc(string name, string description)
        {
            // Creating a new user to be added, with the values coming in from the view, through the PcHandler and PcViewModel - UserId is the one belonging to the logged in user

           
                PC newPc = new PC(name, description, UserViewModel.LoggedInUserId);
                // Trying to post the user to the database, if successful, it returns and then adds it to the list in the app to be shown in the UI
                try
                {
                    GenericDbPersistency<PC>.PostObj(newPc, "api/pcs");
                    //if (GenericDbPersistency<PC>.isPostSuccessful == true)
                    //{
                    //    Pcs.Add(newPc);
                    //}
                    Pcs.Add(newPc);
                // Hvordan stopper jeg dette hvis http post metoden ikke er succesfuld??

            } 
                catch (HttpRequestException e)
                {
                    await new MessageDialog(newPc.PcName + " did not get added in the database\n\n" + e.Message).ShowAsync();
                
                }
                
        }

        public void DeletePc(PC pc)
        {
           GenericDbPersistency<PC>.DeleteObj("api/pcs/", pc.PcId);
            Pcs.Remove(pc);
        }

        public void UpdatePc(PC pc)
        {
            GenericDbPersistency<PC>.UpdateObj(pc,"api/pcs/" + pc.PcId);
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

        public void SaveEdit()
        {

                //Task.Delay(5000);

                UpdatePc(PcViewModel.SelectedPc);
                    
           
        }

        
    }
}
