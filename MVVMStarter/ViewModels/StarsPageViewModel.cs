using MVVMStarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMStarter.ViewModels
{
    public class StarsPageViewModel
    {
        public List<Star> Stars => SessionInfo.Instance.Stars;

        public StarsPageViewModel()
        {
        }
    }
}
