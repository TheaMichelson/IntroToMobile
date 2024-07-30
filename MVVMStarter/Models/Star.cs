using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMStarter.Models
{
    public class Star : CelestialObject
    {
        public string Distance { get; set; }
        public float Magnitude { get; set; }

        public Star()
        {
            IsPlanet = false;
        }
    }
}
