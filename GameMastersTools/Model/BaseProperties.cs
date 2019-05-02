using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMastersTools.Model
{
    abstract class BaseProperties
    {
        /// <summary>  This property is the name of the object made from the class.  </summary>
        public string Name { get; set; }

        /// <summary>  This property is the description of the object made from the class </summary>
        public string Description { get; set; }

        public int Id { get; set; }
    }
}
