using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunnyAPI.Model
{
    public class Series
    {
        public string Name { get; set; }
        public List<Episodio> eps {get;set;}
        public string cover { get; set; }
    }
}
