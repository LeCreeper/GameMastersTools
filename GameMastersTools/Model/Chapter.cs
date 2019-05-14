using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMastersTools.Model
{
    class Chapter
    {
        public int CampaignId { get; set; }

        public int ChapterId { get; set; }

        public string ChapterName { get; set; }

        public string ChapterDescription { get; set; }


        public Chapter(string name, string description, int campaignId)
        {
            ChapterName = name;
            ChapterDescription = description;
            CampaignId = campaignId;

        }
    }
}
