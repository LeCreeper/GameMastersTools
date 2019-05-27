using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMastersTools.Model
{
    public class Campaign
    {
        #region Properties

        public int CampaignId { get; set; }

        public int UserId { get; set; }

        public string CampaignName { get; set; }

        public string CampaignDescription { get; set; }

        #endregion

        #region Constructor

        public Campaign(string name, string description, int userId)
        {
            CampaignName = name;
            CampaignDescription = description;
            UserId = userId;

        }

        #endregion
    }
}
