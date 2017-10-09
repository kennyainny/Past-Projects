using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using NaijaHandyManAPI.Models;



namespace NaijaHandyManAPI.Controllers
{
    public class ArtisanController : ApiController
    {
        private HandyManEntities1 db = new HandyManEntities1();

        #region Encryption and Decryption Methods

        
        private string stringEncryptionKey = "TangoQuando".PadRight(32);
        private byte[] InitializationVector = new byte[] { 2, 4, 6, 8, 10, 12, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

        //this method is used to encrypt a registering users password
        //before storing it in the database

        private string Encrypt(string RawPassword)
        {
            byte[] BytesToEncrypt = Encoding.UTF8.GetBytes(RawPassword);
            byte[] EncryptionKey = Encoding.UTF8.GetBytes(stringEncryptionKey);
            string EncryptedPassword = null;
            Aes AESAlgorithm = Aes.Create();
            AESAlgorithm.Key = EncryptionKey;
            AESAlgorithm.IV = InitializationVector;

            using (MemoryStream StreamIntoMemory = new MemoryStream())
            {
                using (CryptoStream EncryptionStream = new CryptoStream(StreamIntoMemory, AESAlgorithm.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    EncryptionStream.Write(BytesToEncrypt, 0, BytesToEncrypt.Length);
                }

                byte[] EncryptedBytes = StreamIntoMemory.ToArray();
                EncryptedPassword = Convert.ToBase64String(EncryptedBytes);
            }

            return EncryptedPassword;

        }

        private string Decrypt(string EncryptedPassword)
        {
            byte[] BytesToDecrypt = Convert.FromBase64String(EncryptedPassword);
            byte[] EncryptionKey = Encoding.UTF8.GetBytes(stringEncryptionKey);
            string RawPassword = null;
            Aes AESAlgorithm = Aes.Create();
            AESAlgorithm.Key = EncryptionKey;
            AESAlgorithm.IV = InitializationVector;
            using (MemoryStream StreamIntoMemory = new MemoryStream())
            {
                using (CryptoStream DecryptionStream = new CryptoStream(StreamIntoMemory, AESAlgorithm.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    DecryptionStream.Write(BytesToDecrypt, 0, BytesToDecrypt.Length);
                }
                byte[] DecryptedBytes = StreamIntoMemory.ToArray();
                string DecryptedString = Encoding.UTF8.GetString(DecryptedBytes);
                RawPassword = DecryptedString;
            }

            return RawPassword;
        }

        #endregion

        //POST api/Artisan
        [HttpPost]
        public HttpResponseMessage RegisterArtisan(Artisan artisan)
        {
            if (ModelState.IsValid)
            {
                var query = from a in db.Artisans
                            where (((a.Surname).ToLower() == (artisan.Surname).ToLower())
                            && ((a.FirstName).ToLower() == (artisan.FirstName).ToLower())
                            && ((a.MiddleName).ToLower() == (artisan.MiddleName).ToLower())
                            && (a.JobID == artisan.JobID)
                            && (a.PhoneNumber == artisan.PhoneNumber)
                            && ((a.State).ToLower() == (artisan.State).ToLower())
                            && ((a.FullAddress).ToLower() == (a.FullAddress).ToLower()))
                            select a;
                List<Artisan> Artisans = new List<Artisan>();

                Artisans = query.ToList<Artisan>();

                if (Artisans == null || Artisans.Count == 0)
                {
                    try
                    {
                        artisan.Password = Encrypt(artisan.Password);
                        db.Artisans.Add(artisan);
                        db.SaveChanges();

                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, artisan);
                        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = artisan.ArtisanID }));
                        return response;
                    }
                    catch
                    {
                        //an exception occured, so the user was not saved in the db
                        return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    }
                }
                else
                {
                    //the artisan already exists in the database
                    return Request.CreateResponse(HttpStatusCode.Conflict);
                }
            }
            else
            {
                //the artisan object POSTED is invalid
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
        }


        #region This Region Contains GET actions

        //GET api/Artisan/id
        public Artisan[] GetArtisanByJobID(int id)
        {
            var query = from member in db.Artisans
                        where member.JobID == id
                        orderby member.Rating
                        descending
                        select member;

            List<Artisan> ArtisanList = query.ToList<Artisan>();
            if (ArtisanList == null || ArtisanList.Count == 0)
            {
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                return null;
                
            }
            else
            //returns a list of artisans with a particular jobtype
            return ArtisanList.ToArray();
        }

        //GET api/Artisan
        public Artisan[] GetAllArtisans()
        {
            var query = from member in db.Artisans
                        select member;

            List<Artisan> ArtisanList = query.ToList<Artisan>();
            if (ArtisanList == null || ArtisanList.Count == 0)
            {
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                return null;
            }
            else
            {
                Artisan[] artisanArr = new Artisan[ArtisanList.Count];

                for (int i = 0; i < ArtisanList.Count; i++)
                {
                    artisanArr[i] = new Artisan();
                    artisanArr[i].ArtisanID = ArtisanList[i].ArtisanID;
                    artisanArr[i].Surname = ArtisanList[i].Surname;
                    artisanArr[i].FirstName = ArtisanList[i].FirstName;
                    artisanArr[i].MiddleName = ArtisanList[i].MiddleName;
                    artisanArr[i].Password = ArtisanList[i].Password;
                    artisanArr[i].OfficialWorkingHours = ArtisanList[i].OfficialWorkingHours;
                    artisanArr[i].Picture = ArtisanList[i].Picture;
                    artisanArr[i].CPCRegistrationNumber = ArtisanList[i].CPCRegistrationNumber;
                    artisanArr[i].JobID = ArtisanList[i].JobID;
                    artisanArr[i].City = ArtisanList[i].City;
                    artisanArr[i].State = ArtisanList[i].State;
                    artisanArr[i].FullAddress = ArtisanList[i].FullAddress;
                    artisanArr[i].PhoneNumber = ArtisanList[i].PhoneNumber;
                    artisanArr[i].AddressLatitude = ArtisanList[i].AddressLatitude;
                    artisanArr[i].AddressLongitude = ArtisanList[i].AddressLongitude;
                    artisanArr[i].Rating = ArtisanList[i].Rating;
                    artisanArr[i].Email = ArtisanList[i].Email;
                    artisanArr[i].JobCore = ArtisanList[i].JobCore;
                    artisanArr[i].YearsOfExperience = ArtisanList[i].YearsOfExperience;
                    artisanArr[i].RegDate = ArtisanList[i].RegDate;
                    artisanArr[i].NoOfRecommendations = ArtisanList[i].NoOfRecommendations;
                }

                return artisanArr;//.ToArray();
            }
        }

        //GET api/Artisan/State/City/JobId/Locality
        public Artisan[] GetArtisan(string State, string City, int JobId, string Locality)
        {
            var query = from member in db.Artisans
                        where (member.State).ToLower() == (State).ToLower()
                        && (member.City).ToLower() == City.ToLower()
                        && member.JobID == JobId
                        select member;

            List<Artisan> ArtisanList = query.ToList<Artisan>();

            if (ArtisanList == null || ArtisanList.Count == 0)
            {
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                return null;
            }
            else
            {
                if (Locality == null || Locality == "")
                {
                    Artisan[] artisanArr = new Artisan[ArtisanList.Count];

                    for (int i = 0; i < ArtisanList.Count; i++)
                    {
                        artisanArr[i] = new Artisan();
                        artisanArr[i].ArtisanID = ArtisanList[i].ArtisanID;
                        artisanArr[i].Surname = ArtisanList[i].Surname;
                        artisanArr[i].FirstName = ArtisanList[i].FirstName;
                        artisanArr[i].MiddleName = ArtisanList[i].MiddleName;
                        artisanArr[i].Password = ArtisanList[i].Password;
                        artisanArr[i].OfficialWorkingHours = ArtisanList[i].OfficialWorkingHours;
                        artisanArr[i].Picture = ArtisanList[i].Picture;
                        artisanArr[i].CPCRegistrationNumber = ArtisanList[i].CPCRegistrationNumber;
                        artisanArr[i].JobID = ArtisanList[i].JobID;
                        artisanArr[i].City = ArtisanList[i].City;
                        artisanArr[i].State = ArtisanList[i].State;
                        artisanArr[i].FullAddress = ArtisanList[i].FullAddress;
                        artisanArr[i].PhoneNumber = ArtisanList[i].PhoneNumber;
                        artisanArr[i].AddressLatitude = ArtisanList[i].AddressLatitude;
                        artisanArr[i].AddressLongitude = ArtisanList[i].AddressLongitude;
                        artisanArr[i].Rating = ArtisanList[i].Rating;
                        artisanArr[i].Email = ArtisanList[i].Email;
                        artisanArr[i].JobCore = ArtisanList[i].JobCore;
                        artisanArr[i].YearsOfExperience = ArtisanList[i].YearsOfExperience;
                        artisanArr[i].RegDate = ArtisanList[i].RegDate;
                        artisanArr[i].NoOfRecommendations = ArtisanList[i].NoOfRecommendations;
                    }

                    return artisanArr;//.ToArray();

                    // return ArtisanList.ToArray();
                }
                else
                {
                    foreach (Artisan artisan in ArtisanList)
                    {
                        if (!artisan.FullAddress.ToLower().Contains(Locality.ToLower()))
                            ArtisanList.Remove(artisan); //bad logic
                    }


                    if (ArtisanList == null || ArtisanList.Count == 0)
                    {
                        //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                        return null;
                    }
                    else
                    {
                        Artisan[] artisanArr = new Artisan[ArtisanList.Count];

                        for (int i = 0; i < ArtisanList.Count; i++)
                        {
                            artisanArr[i] = new Artisan();
                            artisanArr[i].ArtisanID = ArtisanList[i].ArtisanID;
                            artisanArr[i].Surname = ArtisanList[i].Surname;
                            artisanArr[i].FirstName = ArtisanList[i].FirstName;
                            artisanArr[i].MiddleName = ArtisanList[i].MiddleName;
                            artisanArr[i].Password = ArtisanList[i].Password;
                            artisanArr[i].OfficialWorkingHours = ArtisanList[i].OfficialWorkingHours;
                            artisanArr[i].Picture = ArtisanList[i].Picture;
                            artisanArr[i].CPCRegistrationNumber = ArtisanList[i].CPCRegistrationNumber;
                            artisanArr[i].JobID = ArtisanList[i].JobID;
                            artisanArr[i].City = ArtisanList[i].City;
                            artisanArr[i].State = ArtisanList[i].State;
                            artisanArr[i].FullAddress = ArtisanList[i].FullAddress;
                            artisanArr[i].PhoneNumber = ArtisanList[i].PhoneNumber;
                            artisanArr[i].AddressLatitude = ArtisanList[i].AddressLatitude;
                            artisanArr[i].AddressLongitude = ArtisanList[i].AddressLongitude;
                            artisanArr[i].Rating = ArtisanList[i].Rating;
                            artisanArr[i].Email = ArtisanList[i].Email;
                            artisanArr[i].JobCore = ArtisanList[i].JobCore;
                            artisanArr[i].YearsOfExperience = ArtisanList[i].YearsOfExperience;
                            artisanArr[i].RegDate = ArtisanList[i].RegDate;
                            artisanArr[i].NoOfRecommendations = ArtisanList[i].NoOfRecommendations;
                        }

                        return artisanArr;//.ToArray();
                        //return ArtisanList.ToArray();
                    }
                }
            }
        }

        //GET api/Artisan/StateId/CityId/JobId
        public Artisan[] GetArtisanWithoutLocality(string State, string City, int JobId)
        {
            var query = from member in db.Artisans
                        where member.State == State
                        && (member.City).ToLower() == City.ToLower()
                        && member.JobID == JobId
                        select member;

            List<Artisan> ArtisanList = query.ToList<Artisan>();

            if (ArtisanList == null || ArtisanList.Count == 0)
            {
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                return null;
            }
            else
            {
                Artisan[] artisanArr = new Artisan[ArtisanList.Count];

                for (int i = 0; i < ArtisanList.Count; i++)
                {
                    artisanArr[i] = new Artisan();
                    artisanArr[i].ArtisanID = ArtisanList[i].ArtisanID;
                    artisanArr[i].Surname = ArtisanList[i].Surname;
                    artisanArr[i].FirstName = ArtisanList[i].FirstName;
                    artisanArr[i].MiddleName = ArtisanList[i].MiddleName;
                    artisanArr[i].Password = ArtisanList[i].Password;
                    artisanArr[i].OfficialWorkingHours = ArtisanList[i].OfficialWorkingHours;
                    artisanArr[i].Picture = ArtisanList[i].Picture;
                    artisanArr[i].CPCRegistrationNumber = ArtisanList[i].CPCRegistrationNumber;
                    artisanArr[i].JobID = ArtisanList[i].JobID;
                    artisanArr[i].City = ArtisanList[i].City;
                    artisanArr[i].State = ArtisanList[i].State;
                    artisanArr[i].FullAddress = ArtisanList[i].FullAddress;
                    artisanArr[i].PhoneNumber = ArtisanList[i].PhoneNumber;
                    artisanArr[i].AddressLatitude = ArtisanList[i].AddressLatitude;
                    artisanArr[i].AddressLongitude = ArtisanList[i].AddressLongitude;
                    artisanArr[i].Rating = ArtisanList[i].Rating;
                    artisanArr[i].Email = ArtisanList[i].Email;
                    artisanArr[i].JobCore = ArtisanList[i].JobCore;
                    artisanArr[i].YearsOfExperience = ArtisanList[i].YearsOfExperience;
                    artisanArr[i].RegDate = ArtisanList[i].RegDate;
                    artisanArr[i].NoOfRecommendations = ArtisanList[i].NoOfRecommendations;
                }

                return artisanArr;//.ToArray();
                // return ArtisanList.ToArray();
            }
        }

        #endregion



    }
}
