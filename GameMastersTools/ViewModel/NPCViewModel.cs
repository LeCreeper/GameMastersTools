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
        private string _filterText;
        private ObservableCollection<NPC> _npCsToBeFiltered;

        /// <summary> This field is used to hold the API string for Post and Get methods. </summary>
        private const string PostNGet = "api/NPCs";

        /// <summary> This field is used to hold the API string for Put and Delete methods. </summary>
        private const string PutNDelete = "api/NPCs/";


        #endregion
        
        #region Properties

        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand TemplateCommand { get; set; }

        public static NPC StaticSelectedNpc { get; set; }

        /// <summary> This property is used to filter the list of NPCs. </summary>
        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value; 
                OnPropertyChanged();
                FilterNPCs();
            }

        }

        

        /// <summary> This property is used to store the selected NPC from the listview in the UI. </summary>
        public NPC selectedNPC
        {
            get => _selectedNpc;
            set
            {
                _selectedNpc = value;
                StaticSelectedNpc = _selectedNpc;
                OnPropertyChanged();
            }
        }

        /// <summary> This property is used to store the users input in the UI. </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;  
                OnPropertyChanged();
            }

        }

        /// <summary> This property is used to store the users input in the UI </summary>
        public string Description
        {
            get => _description;
            set
            {
                _description = value; 
                OnPropertyChanged();
            }
        }

        /// <summary> This is a collection of NPCs from the database. </summary>
        public ObservableCollection<NPC> NPCs
        {
            get => _npCs;
            set
            {
                _npCs = value; 
                OnPropertyChanged();
            }
        }

        /// <summary> This is a collection of NPCs taken from the NPCs collection after creation of an NPC or filtering. The listview in the UI is bound to this. </summary>
        public ObservableCollection<NPC> NPCsToBeFiltered
        {
            get => _npCsToBeFiltered;
            set
            {
                _npCsToBeFiltered = value;
                OnPropertyChanged();
            }
        }

        

        #endregion


        #region Constructor

        public NPCViewModel()
        {
            NPCs = new ObservableCollection<NPC>();
            AddCommand = new RelayCommand(AddNPC);
            RemoveCommand = new RelayCommand(DeleteNPC);
            TemplateCommand = new RelayCommand(NPCTemplate);
            
            GetNPCs();
            NPCsToBeFiltered = NPCs;


        }

        #endregion

        #region Methods

        /// <summary> This method makes a template for the description property that is entered in the UI. </summary>
        public void NPCTemplate()
        {
            Description = "Gender: \nRace: \nVoice: \nPersonality: \nLikes: \nDislikes: \nQuirks: ";
        }

        

        /// <summary> This method adds NPCs, whose names doesn't already exist, to the database, and reloads the list of NPCs.(NPCs) </summary>
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
                
                OnPropertyChanged();
            }
            else
            {
                new MessageDialog("The name you're trying to use has already been taken!", "Please choose a different name").ShowAsync();
            }
           
        }

        
        


        /// <summary> This method loads NPCs from the database that matches the logged in user's ID, as well as refreshing NPCsToBeFiltered </summary>
        public void GetNPCs()
        {
           
            foreach (var npc in GenericDbPersistency<NPC>.GetObj(PostNGet).Result)
            {
                if (npc.UserId == UserViewModel.LoggedInUserId)
                {
                    NPCs.Add(npc);
                    OnPropertyChanged();
                }
                
            }
            
            NPCsToBeFiltered = NPCs;
            
        }

        
        /// <summary> This method deletes an NPC by using the API (PutNDelete) and the selectedNPCs ID </summary>
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

        /// <summary> This method updates the selected NPC that is being edited. It is called from NPCPage.xaml.cs </summary>
        public static void UpdateNPC()
        {
            
            if (StaticSelectedNpc != null)
            {
                GenericDbPersistency<NPC>.UpdateObj(StaticSelectedNpc, PutNDelete + StaticSelectedNpc.NPCId);
            }
            else
            {
                new MessageDialog("Please select an NPC to update.", "Error! No NPC selected.").ShowAsync();
            }
            
        }

        /// <summary> This method filters NPCs by the given filter word in the UI.  </summary>
        public void FilterNPCs()
        {
            
            if (FilterText == null) _filterText = "";
            {
                NPCsToBeFiltered = new ObservableCollection<NPC>(NPCs.Where(
                    e =>
                        e.UserId == UserViewModel.LoggedInUserId 
                        && (e.NPCName.ToLower().Contains(FilterText.ToLower()) 
                        || e.NPCDescription.ToLower().Contains(FilterText.ToLower()))
                    
                    ));
                OnPropertyChanged();

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
