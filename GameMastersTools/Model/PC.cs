using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GameMastersTools.Annotations;

namespace GameMastersTools.Model
{
    public class PC 
    {
        private string _pcDescription;

        //TODO Image property
        public int PcId { get; set; }
        public string PcName { get; set; }

        public string PcDescription
        {
            get => _pcDescription;
            set => _pcDescription = value;
        }

        public int UserId { get; set; }

        public PC(string pcName, string pcDescription, int userId)
        {
            PcName = pcName;
            PcDescription = pcDescription;
            UserId = userId;

        }

    }
}
