using MVVMStarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMStarter.Services
{
    public interface IStarService
    {
        List<Star> GetTheStars();

    }
}
