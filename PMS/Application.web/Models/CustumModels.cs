using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Application.Web.Models
{
    public class ExitedMinistersModel
    {
        public List<MinisterProfile> SelectedMinistersList { get; set; }
    }
    public class Country
    {
        public int countryId { get; set; }
        public bool CheckedStatus { get; set; }
        public string CountryName { get; set; }
    }

}