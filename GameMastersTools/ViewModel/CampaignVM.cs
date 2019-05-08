using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameMastersTools.Annotations;
using GameMastersTools.Common;
using GameMastersTools.Model;
using GameMastersTools.Persistency;
using GameMastersTools.View;

namespace GameMastersTools.ViewModel
{
    class CampaignVM : INotifyPropertyChanged
    {
        #region Backingfields

        private ObservableCollection<Campaign> _campaigns;
        private static Campaign _selectedCampaign;

        #endregion

        #region ICommands

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        #endregion

        #region Properties

        public ObservableCollection<Campaign> Campaigns
        {
            get => _campaigns;
            set
            {
                _campaigns = value;
                OnPropertyChanged();
            }
        }

        public Campaign SelectedCampaign
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

        #endregion

        #region Constructor

        public CampaignVM()
        {
            Campaigns = new ObservableCollection<Campaign>();
            AddCommand = new RelayCommand(AddCampaign);
            DeleteCommand = new RelayCommand(DeleteCampaign);
            //UserViewModel.LoggedInUserId = 1;
            LoadUsersCampaigns();
        }

        #endregion

        #region Methods

        public void AddCampaign()
        {
            bool nameAlreadyExists = false;
            foreach (var campaign in Campaigns)
            {
                if (Name == campaign.CampaignName)
                {
                    nameAlreadyExists = true;
                }
            }

            if (nameAlreadyExists == false) 
            {
                CampaignDBPersistency.PostCampaigns(new Campaign(Name, Description, UserViewModel.LoggedInUserId));
                Campaigns = new ObservableCollection<Campaign>();
                LoadUsersCampaigns();
            }

            else
            {
                MessageDialogHelper.Show(
                    "You already have a campaign with this name. Please choose a unique name for your campaign.",
                    "Invalid campaign name");
            }
        }


        public void DeleteCampaign()
        {
            CampaignDBPersistency.DeleteCampaign(SelectedCampaign);
            Campaigns = new ObservableCollection<Campaign>();
            LoadUsersCampaigns();
        }

        public async void DeleteButtonPressed()
        {
            MessageDialog deleteMessageHelper = new MessageDialog("The campaign will be permanently deleted",
                "Are you sure you want to delete this campaign?");
            await deleteMessageHelper.ShowAsync();
            //deleteMessageHelper.Commands
        }

        public void LoadUsersCampaigns()
        {
            foreach (var campaign in CampaignDBPersistency.LoadCampaigns().Result)
            {
                Campaigns.Add(campaign);
            }
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
