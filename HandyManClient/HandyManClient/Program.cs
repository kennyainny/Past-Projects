using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;


namespace HandyManClient
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/naijahandyman/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //var user = new User();

            //user.UserName = "kelechi";
            //user.Password = "beans";
            //user.Email = "nwosu.kelechukwu@gmail.com";

            //var response = client.PostAsJsonAsync("api/login", user).Result;
            
            Artisan artisan = new Artisan();
            artisan.Surname = "Kelechukwu";
            artisan.FirstName = "Nwosu";
            artisan.MiddleName = "Chukwuemeka";
            artisan.JobID = 2;
            artisan.City = "Shomolu";
            artisan.State = "lagos";
            artisan.FullAddress = "18,Sholanke Street,Shomolu,Lagos";
            artisan.PhoneNumber = "080136631297";
            artisan.Password = "ewa";

             var response = client.PostAsJsonAsync("api/artisan", artisan).Result;
            RateArtisanRequest RatingRequest = new RateArtisanRequest() { USERID = 3, ARTISAN_ID = 3, RATING = 5 };

             response = client.PostAsJsonAsync("api/RateArtisan", RatingRequest).Result;
        }
    }
}
