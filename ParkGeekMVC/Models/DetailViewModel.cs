using ParkGeek.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkGeekMVC.Models
{
    public class DetailViewModel
    {
        public Park Park { get; set; }
        public IList<Weather> Forecast { get; set; }

        public bool PrefersFarenheit { get; set; } = true;
    }
}
