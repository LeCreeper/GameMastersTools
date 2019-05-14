﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using GameMastersTools.Annotations;
using GameMastersTools.Model;
using GameMastersTools.Persistency;

namespace GameMastersTools.ViewModel
{
    class ChapterListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Chapter> _chapters;
        private static Chapter _selectedCampaign;

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
            get => _selectedCampaign;
            set
            {
                _selectedCampaign = value;
                OnPropertyChanged();
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }



        public ChapterListViewModel()
        {

        }

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
                ChapterPersistency.PostChapter(new Chapter(Name, Description, CampaignVM.SelectedCampaignId));
                Chapters = new ObservableCollection<Chapter>();
                LoadCampaignChapters();
            }

            else
            {
                ChapterListViewModel.MessageDialogHelper.Show(
                    "You already have a campaign with this name. Please choose a unique name for your campaign.",
                    "Invalid campaign name");
            }
        }


        public void DeleteChapter()
        {
            if (SelectedChapter == null)
            {
                new MessageDialog("Please select a campaign to delete", "No campaign selected");
            }

            try
            {
                ChapterPersistency.DeleteChapter(SelectedChapter);
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
            foreach (var chapter in ChapterPersistency.LoadChapters().Result)
            {
                Chapters.Add(chapter);
            }
        }

        public void ClearNameAndDescription()
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