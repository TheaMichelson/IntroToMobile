using MVVMStarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMStarter.ViewModels
{
    public class PlanetsPageViewModel
    {
        public List<Planet> Planets => SessionInfo.Instance.Planets;

        public PlanetsPageViewModel()
        {
        }
    }
}
