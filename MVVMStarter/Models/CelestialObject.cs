using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMStarter.Models
{
    public class CelestialObject
    {
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public bool IsPlanet { get; set; }
        public bool IsStar => !IsPlanet;

        public CelestialObject()
        {
        }
    }
}
