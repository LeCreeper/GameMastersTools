using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMastersTools.Model
{
    public class PC
    {
        //TODO Image property
        public int PcId { get; set; }
        public string PcName { get; set; }
        public string PcDescription { get; set; }
        public int UserId { get; set; }

        public PC(string pcName, string pcDescription, int userId)
        {
            PcName = pcName;
            PcDescription = pcDescription;
            UserId = userId;

        }
    }
}
