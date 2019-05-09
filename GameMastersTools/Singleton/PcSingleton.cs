using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.OnlineId;
using Windows.UI.Popups;
using GameMastersTools.Model;
using GameMastersTools.Persistency;

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
        
        public void PostPc(string name,string description, int userid)
        {
            // Creating a new user to be added, with the values coming in from the view, through the PcHandler and PcViewModel
            PC newPc = new PC(name,description,userid);

            // Trying to post the user to the database, if successful, it returns and then adds it to the list in the app to be shown in the UI
            GenericDbPersistency<PC>.PostObj(newPc, "api/pcs");
            Pcs.Add(newPc);
        }

        // Loading all player characters from the database into a list
        public async void LoadPc()
        {
            //if (UserId = LoggedInUser ??) { } <--- Need to only get the player characters that belong to the user who is logged in
            try
            {
                var pcs = await GenericDbPersistency<PC>.GetObj("api/pcs");

                if (pcs != null)
                {
                    foreach (var pc in pcs)
                    {
                        Pcs.Add(pc);
                    }
                }
            }
            catch (Exception e)
            {
                new MessageDialog("ERROE\n\n" + e.Message);

            }

        }
    }
}
