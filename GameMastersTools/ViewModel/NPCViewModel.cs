using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.WiFiDirect.Services;
using Windows.UI.Popups;
using GameMastersTools.Annotations;
using GameMastersTools.Common;
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
        private NPC _selectedNpc;
        private const string PostNGet = "api/NPCs";
        private const string PutNDelete = "api/NPCs/";


        #endregion
        
        #region Properties

        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        public NPC selectedNPC
        {
            get => _selectedNpc;
            set
            {
                _selectedNpc = value;
                OnPropertyChanged();
            }
        }

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
            AddCommand = new RelayCommand(AddNPC);
            RemoveCommand = new RelayCommand(DeleteNPC);
            GetNPCs();
            
           
        }

        #endregion

        #region Methods
        
        public void AddNPC()
        {
            bool nameAlreadyExist = false;
            foreach (var npc in NPCs)
            {
                if (Name == npc.NPCName)
                {
                   
                    nameAlreadyExist = true;
                }
            }


            if (nameAlreadyExist == false) 
            {
                GenericDbPersistency<NPC>.PostObj(new NPC(Name, Description, UserViewModel.LoggedInUserId), PostNGet);
                NPCs = new ObservableCollection<NPC>();
                GetNPCs();
            }
            else
            {
                new MessageDialog("The name you're trying to use has already been taken!", "Please choose a different name").ShowAsync();
            }
           
        }

        public void GetNPCs()
        {
            foreach (var npc in GenericDbPersistency<NPC>.GetObj(PostNGet).Result)
            {
                if (npc.UserId == UserViewModel.LoggedInUserId)
                {
                    NPCs.Add(npc);
                }
            }
        }

        public void DeleteNPC()
        {
            if (selectedNPC != null)
            {
                GenericDbPersistency<NPC>.DeleteObj(PutNDelete, selectedNPC.NPCId);
                NPCs.Remove(selectedNPC);
            }
            else
            {
                new MessageDialog("Please select an NPC to delete.", "Error! No NPC selected!").ShowAsync();
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

        #region MessageDialog

        private class MessageDialogHelper
        {
            public static async void Show(string content, string title)
            {
                MessageDialog messageDialog = new MessageDialog(content, title);
                await messageDialog.ShowAsync();
            }
        }


        #endregion
    }
}
