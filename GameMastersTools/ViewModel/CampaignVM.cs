using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GameMastersTools.Annotations;
using GameMastersTools.Common;
using GameMastersTools.Model;
using GameMastersTools.Persistency;

namespace GameMastersTools.ViewModel
{
    class CampaignVM : INotifyPropertyChanged
    {
        #region Properties

        private ObservableCollection<Campaign> _campaigns;
        private static Campaign _selectedCampaign;

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

        public List<Campaign> X { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public CampaignVM()
        {
            Campaigns = new ObservableCollection<Campaign>();
            AddCommand = new RelayCommand(AddCampaign);
            DeleteCommand = new RelayCommand(DeleteCampaign);
            //UserViewModel.LoggedInUserId = 1;
            LoadUsersCampaigns();

        }

        //public void AddCampaignsTest()
        //{
        //    Campaigns.Add(new Campaign("Lystfiskeren"){Description = "Der var engang en fiskermand..."});
        //    Campaigns.Add(new Campaign("Giganternes flyvemaskine") { Description = "En gruppe giants har planer om at udvikle den første flyvemaskine..." });
        //    Campaigns.Add(new Campaign("Tronspillet") { Description = "Go go, John Snow. Beat them walkers! You can do it!" });
        //}


        public void AddCampaign()
        {
            CampaignDBPersistency.PostCampaigns(new Campaign(Name, Description, 1));
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
