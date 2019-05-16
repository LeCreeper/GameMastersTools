using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMastersTools.Model
{
    class CampaignPC
    {
        public int CampaignPCId { get; set; }
        public int CampaignId { get; set; }
        public int PCId { get; set; }

        public CampaignPC(int campaignId, int pcId)
        {
            CampaignId = campaignId;
            PCId = pcId;
        }
    }
}
