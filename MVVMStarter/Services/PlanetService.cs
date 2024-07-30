using MVVMStarter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVVMStarter.Services
{
    public class PlanetService : IPlanetService
    {
        public PlanetService()
        {
        }

        public List<Planet> GetThePlanets()
        {
            var assembly = typeof(PlanetService).GetTypeInfo().Assembly;

            var names = assembly.GetManifestResourceNames();



            Stream stream = assembly.GetManifestResourceStream("MVVMStarter.planets.json");

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                try
                {
                    var data = JsonConvert.DeserializeObject<Root>(json);
                    return data.planets;
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                    return null;
                }
            }
        }
    }
}
