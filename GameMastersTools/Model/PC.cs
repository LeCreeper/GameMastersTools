using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMastersTools.Model
{
    class PC
    {
        //TODO Image property
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

        public PC(string name, string description, int userId)
        {
            Name = name;
            Description = description;
            UserId = userId;

        }
    }
}
