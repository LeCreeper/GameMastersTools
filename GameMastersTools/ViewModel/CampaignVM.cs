using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
            CampaignDBPersistency.PostCampaigns(new Campaign(Name, Description, UserViewModel.LoggedInUserId));
            Campaigns = new ObservableCollection<Campaign>();
            LoadUsersCampaigns();
        }

        public void DeleteCampaign()
        {
            CampaignDBPersistency.DeleteCampaign(SelectedCampaign);
            Campaigns = new ObservableCollection<Campaign>();
            LoadUsersCampaigns();
        }

        public void LoadUsersCampaigns()
        {
            foreach (var campaign in CampaignDBPersistency.LoadCampaigns().Result)
            {
                Campaigns.Add(campaign);
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
