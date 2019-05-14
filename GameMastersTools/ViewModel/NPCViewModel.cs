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

namespace GameMastersTools.ViewModel
{
    class NPCViewModel : INotifyPropertyChanged
    {
        #region Backing Fields
        
        private string _description;
        private string _name;
        private ObservableCollection<NPC> _npCs;

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

            NPCs.Add(new NPC("Danny Boy","This Dude Sucks", UserViewModel.LoggedInUserId));
            NPCs.Add(new NPC("Danny Boy", "This Dude Sucks", UserViewModel.LoggedInUserId));
            NPCs.Add(new NPC("Danny Boy", "This Dude Sucks", UserViewModel.LoggedInUserId));
            NPCs.Add(new NPC("Danny Boy", "This Dude Sucks", UserViewModel.LoggedInUserId));
            NPCs.Add(new NPC("Danny Boy", "This Dude Sucks", UserViewModel.LoggedInUserId));
            NPCs.Add(new NPC("Danny Boy", "This Dude Sucks", UserViewModel.LoggedInUserId));
            NPCs.Add(new NPC("Danny Boy", "This Dude Sucks", UserViewModel.LoggedInUserId));
            NPCs.Add(new NPC("Danny Boy", "This Dude Sucks", UserViewModel.LoggedInUserId));
            NPCs.Add(new NPC("Danny Boy", "This Dude Sucks", UserViewModel.LoggedInUserId));
            NPCs.Add(new NPC("Danny Boy", "This Dude Sucks", UserViewModel.LoggedInUserId));
            NPCs.Add(new NPC("Danny Boy", "This Dude Sucks", UserViewModel.LoggedInUserId));
            NPCs.Add(new NPC("Danny Boy", "This Dude Sucks", UserViewModel.LoggedInUserId));
            NPCs.Add(new NPC("Danny Boy", "This Dude Sucks", UserViewModel.LoggedInUserId));
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
