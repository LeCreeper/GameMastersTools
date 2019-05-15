using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GameMastersTools.Annotations;
using GameMastersTools.Model;
using GameMastersTools.Persistency;

namespace GameMastersTools.ViewModel
{
    class NPCViewModel : INotifyPropertyChanged
    {
        #region Backing Fields
        
        private string _description;
        private string _name;
        private ObservableCollection<NPC> _npCs;
        private const string PostNGet = "api/NPCs";
        private const string PutNDelete = "api/NPCs/";

        #endregion


        #region Properties

        public string Name
        {
            get => _name;
            set
            {
                _name = value;  
                OnPropertyChanged();
            }

        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<NPC> NPCs
        {
            get => _npCs;
            set
            {
                _npCs = value; 
                OnPropertyChanged();
            }
        }

        #endregion


        #region MyRegion

        public NPCViewModel()
        {

            NPCs = new ObservableCollection<NPC>();
            
            GetNPCs();
            
           
        }

        #endregion

        #region Methods
        
        public void AddNPC()
        {
           GenericDbPersistency<NPC>.PostObj(new NPC(Name, Description, UserViewModel.LoggedInUserId), PostNGet );
           NPCs = new ObservableCollection<NPC>();
           GetNPCs();
        }

        public void GetNPCs()
        {
            foreach (var npc in GenericDbPersistency<NPC>.GetObj(PostNGet).Result)
            {
                NPCs.Add(npc);
            }
           
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
