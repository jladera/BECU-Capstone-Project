using DatabaseLibrary.Models;
using System.Collections.Generic;

namespace Locator.Models
{
    public class CleanLocationViewModel
    {
        public List<CleanLocationModel> CleanLocationList = new List<CleanLocationModel>();

        public CleanLocationViewModel(List<Locations> data)
        {
            foreach (var item in data)
            {
                CleanLocationList.Add(new CleanLocationModel(item));
            }
        }
    }
}
