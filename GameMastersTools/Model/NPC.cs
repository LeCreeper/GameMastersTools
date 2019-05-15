using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMastersTools.Model
{
    class NPC : BaseProperties
    {
        public int NPCId { get; set; }

        public string NPCName { get; set; }

        public string NPCDescription { get; set; }

        public int UserId { get; set; }

        public NPC(string npcName, string npcDescription, int userId)
        {
            NPCName = npcName;
            NPCDescription = npcDescription;
            UserId = userId;
        }

        //TODO 2 pictures

        public override string ToString()
        {
            return $"{nameof(NPCName)}: {NPCName}, {nameof(NPCDescription)}: {NPCDescription}, {nameof(UserId)}: {UserId}";
        }
    }
}
