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
    public class CampaignVM : INotifyPropertyChanged
    {
        #region Backingfields

        private ObservableCollection<Campaign> _campaigns;
        private string _name;
        private string _description;
        private static Campaign _selectedCampaign;
        private static string _selectedCampaignName;

        #endregion

        #region TestProperties

        public bool NameAlreadyExists { get; set; }
        public bool NameIsTooLong { get; set; }
        public bool AddIsSuccessful { get; set; }
        public bool DeleteIsSuccessful { get; set; }

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

                try
                {
                    SelectedCampaignId = _selectedCampaign.CampaignId;
                }
                catch (Exception e)
                {

                }
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

        public static int SelectedCampaignId { get; set; }

        #endregion

        #region Constructor

        public CampaignVM()
        {
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
            ResetTestBools();

            if (Name.Length > 60)
            {
                NameIsTooLong = true;
            }

            if (NameIsTooLong)
            {
                //MessageDialogHelper.Show("Please choose a name with less than 60 characters", "Campaign name is too long");
            }
            else
            {
                foreach (var campaign in Campaigns)
                {
                    if (Name == campaign.CampaignName)
                    {
                        NameAlreadyExists = true;
                    }
                }

                if (!NameAlreadyExists)
                {
                    GenericDbPersistency<Campaign>.PostObj(new Campaign(Name, Description, UserViewModel.LoggedInUserId), "api/Campaigns");
                    LoadUsersCampaigns();
                    AddIsSuccessful = true;
                }

                else
                {
                    MessageDialogHelper.Show(
                        "You already have a campaign with this name. Please choose a unique name for your campaign.",
                        "Invalid campaign name");
                }
            }

            ClearNameAndDescription();
        }


        public void DeleteCampaign()
        {
            if (SelectedCampaign == null)
            {
                new MessageDialog("Please select a campaign to delete", "No campaign selected");
            }

            else try
            {
                GenericDbPersistency<Campaign>.DeleteObj("api/Campaigns/", SelectedCampaignId);
                LoadUsersCampaigns();
                DeleteIsSuccessful = true;
                SelectedCampaign = null;
            }
            catch (Exception e)
            {
                new MessageDialog(e.Message);
            }
        }

        

        public void LoadUsersCampaigns()
        {
            Campaigns = new ObservableCollection<Campaign>();
            foreach (var campaign in GenericDbPersistency<Campaign>.GetObj("api/Campaigns").Result)
            {
                if (campaign.UserId == UserViewModel.LoggedInUserId)
                {
                    Campaigns.Add(campaign);
                }           
            }
        }

        private void ClearNameAndDescription()
        {
            Name = null;
            Description = null;
        }

        private void ResetTestBools()
        {
            NameIsTooLong = false;
            NameAlreadyExists = false;
            AddIsSuccessful = false;
            DeleteIsSuccessful = false;
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
