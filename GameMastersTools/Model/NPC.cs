using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMastersTools.Model
{
    class NPC : BaseProperties
    {
        public string Race { get; set; }

        //TODO 2 pictures

        public NPC(string name, string race)
        {
            Name = name;
            Race = race;
        }
        
        
    }
}
