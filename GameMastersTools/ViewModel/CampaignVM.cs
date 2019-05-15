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
        private string _name;
        private string _description;
        private static Campaign _selectedCampaign;
        private static string _selectedCampaignName;

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
                SelectedCampaignId = _selectedCampaign.CampaignId;
                _selectedCampaignName = _selectedCampaign.CampaignName;
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

        public static int SelectedCampaignId { get; set; }

        public static string SelectedCampaignName
        {
            get => _selectedCampaignName;
            set => _selectedCampaignName = value;
        }

        #endregion

        #region Constructor

        public CampaignVM()
        {
            Campaigns = new ObservableCollection<Campaign>();
            AddCommand = new RelayCommand(AddCampaign);
            DeleteCommand = new RelayCommand(DeleteCampaign);
            //UserViewModel.LoggedInUserId = 1;
            LoadUsersCampaigns();
            _selectedCampaign = null;
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

            ClearNameAndDescription();
        }


        public void DeleteCampaign()
        {
            if (SelectedCampaign == null)
            {
                new MessageDialog("Please select a campaign to delete", "No campaign selected");
            }

            try
            {
                CampaignDBPersistency.DeleteCampaign(SelectedCampaign);
                Campaigns = new ObservableCollection<Campaign>();
                LoadUsersCampaigns();
            }
            catch (Exception e)
            {
                new MessageDialog(e.Message);
            }
        }

        

        public void LoadUsersCampaigns()
        {
            foreach (var campaign in CampaignDBPersistency.LoadCampaigns().Result)
            {
                Campaigns.Add(campaign);
            }
        }

        private void ClearNameAndDescription()
        {
            Name = null;
            Description = null;
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
