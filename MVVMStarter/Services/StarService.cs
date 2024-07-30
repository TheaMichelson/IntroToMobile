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
    public class StarService : IStarService
    {
        public StarService()
        {
        }

        public List<Star> GetTheStars()
        {
            var assembly = typeof(StarService).GetTypeInfo().Assembly;

            var names = assembly.GetManifestResourceNames();



            Stream stream = assembly.GetManifestResourceStream("MVVMStarter.stars.json");

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                try
                {
                    var data = JsonConvert.DeserializeObject<Root>(json);
                    return data.stars;
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
