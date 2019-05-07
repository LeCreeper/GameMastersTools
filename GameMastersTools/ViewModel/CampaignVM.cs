using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GameMastersTools.Annotations;
using GameMastersTools.Model;

namespace GameMastersTools.ViewModel
{
    class CampaignVM : INotifyPropertyChanged
    {
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

        public string Name { get; set; }
        public string Description { get; set; }

        public CampaignVM()
        {
            Campaigns = new ObservableCollection<Campaign>();
            AddCampaignsTest();

        }

        public void AddCampaignsTest()
        {
            Campaigns.Add(new Campaign("Lystfiskeren"){Description = "Der var engang en fiskermand..."});
            Campaigns.Add(new Campaign("Giganternes flyvemaskine") { Description = "En gruppe giants har planer om at udvikle den første flyvemaskine..." });
            Campaigns.Add(new Campaign("Tronspillet") { Description = "Go go, John Snow. Beat them walkers! You can do it!" });
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
