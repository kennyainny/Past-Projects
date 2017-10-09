using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NaijaHandyManAPI.Models;

namespace NaijaHandyManAPI
{
    public class RateArtisanRequest
    {
        public int ARTISAN_ID { get; set; }
        public int USERID { get; set; }
        public int RATING { get; set; }
    }
}