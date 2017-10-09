using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyManClient
{
    class Artisan
    {
        public int ArtisanID { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Password { get; set; }
        public string OfficialWorkingHours { get; set; }
        public byte[] Picture { get; set; }
        public string CPCRegistrationNumber { get; set; }
        public int JobID { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string FullAddress { get; set; }
        public string PhoneNumber { get; set; }
        public double AddressLongitude { get; set; }
        public double AddressLatitude { get; set; }
        public double Rating { get; set; }
        public string Email { get; set; }
        public string JobCore { get; set; }
        public int YearsOfExperience { get; set; }
        public string RegDate { get; set; }
        public int NoOfRecommendations { get; set; }
    }
}
