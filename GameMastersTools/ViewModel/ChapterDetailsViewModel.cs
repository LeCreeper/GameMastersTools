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
    class ChapterDetailsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<NPC> _npCs;
        private ObservableCollection<Location> _locations;
        private ObservableCollection<Item> _items;
        private ObservableCollection<Encounter> _encounters;

        public ICommand SaveChapterCommand { get; set; }
        public Chapter Chapter { get; set; }

        public ObservableCollection<NPC> NPCs
        {
            get => _npCs;
            set
            {
                _npCs = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Location> Locations
        {
            get => _locations;
            set
            {
                _locations = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Encounter> Encounters
        {
            get => _encounters;
            set
            {
                _encounters = value; 
                OnPropertyChanged();
            }
        }

        public ChapterDetailsViewModel()
        {
            Chapter = GenericDbPersistency<Chapter>.GetSingleObj("api/Chapters/",
                ChapterListViewModel.SelectedChapterId);
            SaveChapterCommand = new RelayCommand(SaveChapterDetails);
        }

        public void SaveChapterDetails()
        {
            try
            {
                GenericDbPersistency<Chapter>.UpdateObj(Chapter, $"api/Chapters/{Chapter.ChapterId}");
            }
            catch { }
        }

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

        #region INotify

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
