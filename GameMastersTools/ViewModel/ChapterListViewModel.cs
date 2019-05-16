using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using GameMastersTools.Annotations;
using GameMastersTools.Common;
using GameMastersTools.Model;
using GameMastersTools.Persistency;

namespace GameMastersTools.ViewModel
{
    class ChapterListViewModel : INotifyPropertyChanged
    {
        #region BackingFields

        private ObservableCollection<Chapter> _chapters;
        private ObservableCollection<PC> _usersPCs;
        private ObservableCollection<PC> _campaignPCs;
        private static Chapter _selectedChapter;

        #endregion

        #region ICommands

        public ICommand AddChapterCommand { get; set; }
        public ICommand DeleteChapterCommand { get; set; }

        public ICommand AddPcCommand { get; set; }
        public ICommand RemovePcCommand { get; set; }

        #endregion

        #region Properties

        public ObservableCollection<Chapter> Chapters
        {
            get => _chapters;
            set
            {
                _chapters = value;
                OnPropertyChanged();
            }
        }

        public Chapter SelectedChapter
        {
            get => _selectedChapter;
            set
            {
                _selectedChapter = value;
                OnPropertyChanged();
            }
        }

        public PC SelectedCampaignPC { get; set; }

        public PC SelectedUsersPC { get; set; }

        public ObservableCollection<PC> UsersPCs
        {
            get => _usersPCs;
            set
            {
                _usersPCs = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PC> CampaignPCs
        {
            get => _campaignPCs;
            set
            {
                _campaignPCs = value; 
                OnPropertyChanged();
            }
        }

        public string SelectedCampaignName { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        #region Constructor

        public ChapterListViewModel()
        {
            AddChapterCommand = new RelayCommand(AddChapter);
            DeleteChapterCommand = new RelayCommand(DeleteChapter);
            AddPcCommand = new RelayCommand(AddPCToCampaign);
            RemovePcCommand = new RelayCommand(RemovePCFromCampaign);
            SelectedCampaignName = CampaignVM.SelectedCampaignName;
            LoadChapters();
            LoadUsersPCs();
            LoadCampaignPCs();
            _selectedChapter = null;

        }

        #endregion

        #region Load Methods

        public void LoadChapters()
        {
            Chapters = new ObservableCollection<Chapter>();

            foreach (var chapter in GenericDbPersistency<Chapter>.GetObj("api/Chapters").Result)
            {
                if (chapter.CampaignId == CampaignVM.SelectedCampaignId)
                {
                    Chapters.Add(chapter);
                }
            }
        }

        public void LoadUsersPCs()
        {
            UsersPCs = new ObservableCollection<PC>();

            foreach (var pc in GenericDbPersistency<PC>.GetObj("api/pcs").Result)
            {
                if (pc.UserId == UserViewModel.LoggedInUserId)
                {
                    UsersPCs.Add(pc);
                }
            }
        }

        public void LoadCampaignPCs()
        {
            CampaignPCs = new ObservableCollection<PC>();

            foreach (var campaignPC in GenericDbPersistency<CampaignPC>.GetObj("api/CampaignPCs").Result)
            {
                if (campaignPC.CampaignId == CampaignVM.SelectedCampaignId)
                {
                    foreach (var pc in GenericDbPersistency<PC>.GetObj("api/pcs").Result)
                    {
                        if (pc.PcId == campaignPC.PCId)
                        {
                            CampaignPCs.Add(pc);
                        }
                    }
                }
            }
        }

        #endregion

        #region ChapterMethods

        public void AddChapter()
        {
            bool nameAlreadyExists = false;
            foreach (var chapter in Chapters)
            {
                if (Name == chapter.ChapterName)
                {
                    nameAlreadyExists = true;
                }
            }

            if (nameAlreadyExists == false)
            {
                GenericDbPersistency<Chapter>.PostObj(new Chapter(Name, Description, CampaignVM.SelectedCampaignId), "api/Chapters");
                Chapters = new ObservableCollection<Chapter>();
                LoadChapters();
            }

            else
            {
                ChapterListViewModel.MessageDialogHelper.Show(
                    "You already have a campaign with this name. Please choose a unique name for your campaign.",
                    "Invalid campaign name");
            }

            ClearNameAndDescription();
        }


        public void DeleteChapter()
        {
            if (SelectedChapter == null)
            {
                new MessageDialog("Please select a campaign to delete", "No campaign selected");
            }

            else try
            {
                GenericDbPersistency<Chapter>.DeleteObj("api/Chapters/", SelectedChapter.ChapterId);
                Chapters = new ObservableCollection<Chapter>();
                LoadChapters();
            }
            catch (Exception e)
            {
                new MessageDialog(e.Message);
            }
        }
        //CampaignDBPersistency.LoadCampaigns().Result

        private void ClearNameAndDescription()
        {
            Name = null;
            Description = null;
        }

        #endregion

        #region PCMethods

        public void AddPCToCampaign()
        {
            if (SelectedUsersPC != null) 
            {
                GenericDbPersistency<CampaignPC>.PostObj(new CampaignPC(CampaignVM.SelectedCampaignId, SelectedUsersPC.PcId), "api/CampaignPCs");
                LoadCampaignPCs();
                SelectedUsersPC = null;
            }

            else
            {
                MessageDialogHelper.Show("Please select a PC to add", "No Pc selected");
            }

        }

        public void RemovePCFromCampaign()
        {
            if (SelectedCampaignPC != null)
            {
                foreach (var campaignPC in GenericDbPersistency<CampaignPC>.GetObj("api/CampaignPCs").Result)
                {
                    if (SelectedCampaignPC.PcId == campaignPC.PCId && campaignPC.CampaignId == CampaignVM.SelectedCampaignId)
                    {
                        GenericDbPersistency<CampaignPC>.DeleteObj("api/CampaignPCs/", campaignPC.CampaignPCId);
                        break;
                    }
                }
            }
            LoadCampaignPCs();
        }

        #endregion

        #region INotify

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion

        #region MessageDialogHelper

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
