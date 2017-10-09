using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using NaijaHandyManAPI.Models;
using System.Web.Http;

namespace NaijaHandyManAPI.Controllers
{
    public class RateArtisanController : ApiController
    {
        private HandyManEntities1 db = new HandyManEntities1();

        //POST api/RateArtisan
        public int Post(RateArtisanRequest request)
        {
            int userID = request.USERID;
            int ArtisanID = request.ARTISAN_ID;
            int Rating = request.RATING;
            var query = from c in db.ArtisanRatings
                        where ((c.UserID == userID) && (c.ArtisanID == ArtisanID))
                        select c;
            List<ArtisanRating> ArtisanRatingList = query.ToList<ArtisanRating>();

            //get the artiasn object with a speicified ID
            var ArtisanQuery = from c in db.Artisans
                               where c.ArtisanID == ArtisanID
                               select c;

            //get the User Object with a specified userID
            var UserQuery = from c in db.Users
                            where c.UserID == userID
                            select c;

            if (ArtisanRatingList.Count > 0)
                return 1; //because that particular user has already rated this artisan
            else
            {
                ArtisanRating NewRating = new ArtisanRating();
                NewRating.User = UserQuery.First();
                NewRating.UserID = userID;

                NewRating.Artisan = ArtisanQuery.First();
                NewRating.ArtisanID = ArtisanID;

                NewRating.PersonalRating = Rating;
                db.ArtisanRatings.Add(NewRating);
                db.SaveChanges();

                //rating algorithm goes here
                var Ratingquery = from Row in db.ArtisanRatings
                                  where Row.ArtisanID == ArtisanID
                                  select Row;

                var ArtisanRatingsList = Ratingquery.ToList<ArtisanRating>();

                double cummulativeRating = 0;

                foreach (ArtisanRating R in ArtisanRatingsList)
                {
                    cummulativeRating += (double)R.PersonalRating;
                }


                var AnotherQuery = from a in db.Artisans
                                   where a.ArtisanID == ArtisanID
                                   select a;
                Artisan UpdateArtisan = AnotherQuery.First();
                UpdateArtisan.Rating = cummulativeRating / ArtisanRatingsList.Count;
                db.SaveChanges();


                return 2;

            }
        }
    }
}
