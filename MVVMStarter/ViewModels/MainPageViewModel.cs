using MVVMStarter.Models;
using MVVMStarter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMStarter.ViewModels
{
    public class MainPageViewModel : BasePageViewModel
    {
        public string LearnMore { get; set; }
        public string URL { get; set; }

        private IPlanetService _planetService;
        private IStarService _starService;

        public MainPageViewModel()
        {

        }
        public MainPageViewModel(IPlanetService planetService, IStarService starService)
        {

            _planetService = planetService;
            _starService = starService;

            var planets = _planetService.GetThePlanets();
            var stars = _starService.GetTheStars();

            SessionInfo.Instance.PlanetsAndStars.AddRange(planets);
            SessionInfo.Instance.PlanetsAndStars.AddRange(stars);
        }
    }
}
