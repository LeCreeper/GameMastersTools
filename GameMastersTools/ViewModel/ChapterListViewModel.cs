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
        private static Chapter _selectedChapter;

        #endregion

        #region ICommands

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

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

        public ObservableCollection<PC> PCs { get; set; }

        public string SelectedCampaignName { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        #region Constructor

        public ChapterListViewModel()
        {
            Chapters = new ObservableCollection<Chapter>();
            PCs = new ObservableCollection<PC>();
            AddCommand = new RelayCommand(AddChapter);
            DeleteCommand = new RelayCommand(DeleteChapter);
            SelectedCampaignName = CampaignVM.SelectedCampaignName;
            LoadCampaignChapters();
            _selectedChapter = null;

        }

        #endregion

        #region Methods

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
                LoadCampaignChapters();
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

            try
            {
                GenericDbPersistency<Chapter>.DeleteObj("api/Chapters/", SelectedChapter.ChapterId);
                Chapters = new ObservableCollection<Chapter>();
                LoadCampaignChapters();
            }
            catch (Exception e)
            {
                new MessageDialog(e.Message);
            }
        }
        //CampaignDBPersistency.LoadCampaigns().Result



        public void LoadCampaignChapters()
        {
            foreach (var chapter in GenericDbPersistency<Chapter>.GetObj("api/Chapters").Result)
            {
                if (chapter.CampaignId == CampaignVM.SelectedCampaignId)
                {
                    Chapters.Add(chapter);
                }
            }
        }

        private void ClearNameAndDescription()
        {
            Name = null;
            Description = null;
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
