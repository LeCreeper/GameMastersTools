using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMastersTools.Model
{
    class Campaign : BaseProperties
    {
        /// <summary> This collection is a list of the PCs (Playable Characters) within the campaign.  </summary>
        public ObservableCollection<PC> PCs;

        /// <summary> This collection is a list of the chapters existing in the campaign. </summary>
        public ObservableCollection<Chapter> Chapters;

        public int CampaignId { get; set; }

        public Campaign(string name)
        {
            Name = name;
            PCs = new ObservableCollection<PC>();
            Chapters = new ObservableCollection<Chapter>();

        }
    }
}
