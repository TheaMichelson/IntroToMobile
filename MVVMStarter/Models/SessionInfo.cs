using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MVVMStarter.Models
{
    public class SessionInfo
    {
        private static SessionInfo _instance;

        private SessionInfo() { }

        public static SessionInfo Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SessionInfo();
                return _instance;
            }
        }

        private List<CelestialObject> _planetsAndStars;

        public List<CelestialObject> PlanetsAndStars
        {
            get
            {
                if (_planetsAndStars == null)
                    _planetsAndStars = new List<CelestialObject>();

                return _planetsAndStars;
            }
        }

        public List<Planet> Planets => PlanetsAndStars.Where(ps => ps is Planet).Cast<Planet>().ToList();
        public List<Star> Stars => PlanetsAndStars.Where(ps => ps is Star).Cast<Star>().ToList();
    }
}
