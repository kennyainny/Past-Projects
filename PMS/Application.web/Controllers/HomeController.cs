using Application.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Web.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDatabaseContext _db = new ApplicationDatabaseContext();

        int pageSize = 1;
        int pageIndex = 1;
        string searchValue;
        string sortFilterOld;

        //public HomeController()
        //{
        //    _db = new ApplicationDatabaseContext();
        //}

        public ActionResult Index(int? page)
        {
            ViewBag.placement = "data-placement";

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                        
            IPagedList<MinisterProfile> ministers = _db.MinisterProfiles
                .OrderBy(m => m.DistrictId)
                .ThenBy(n => n.Surname)
                .ToPagedList(pageIndex, 1);

            return View(ministers);
        }

        [ChildActionOnly]
        public PartialViewResult MinisterTable()
        {
            //sortOrder: District, College, Church, -lastname: constant
            //Search: Any value

            ViewBag.CurrentSort = "DistrictId";

            IPagedList<MinisterProfile> ministers = _db.MinisterProfiles
                            .OrderBy(m => m.DistrictId)
                            //.ThenBy(m => m.ChurchId)
                            .ThenBy(m => m.Surname)
                            .ToPagedList(pageIndex, pageSize);

            return PartialView("_MinisterTable", ministers);
        }

        [ChildActionOnly]
        public PartialViewResult MinisterTableFilter(string sortOrder)
        {
            //sortOrder: District, College, Church, -lastname: constant
            //Search: Any value

            sortFilterOld = ViewBag.CurrentSort;

            sortOrder = String.IsNullOrEmpty(sortOrder) ? "DistrictId" : sortOrder;

            IPagedList<MinisterProfile> ministers = null;

            switch (sortOrder)
            {
                case "DistrictId":
                    if (sortOrder.Equals(sortFilterOld))
                        ministers = _db.MinisterProfiles
                            .OrderByDescending(m => m.DistrictId).ThenBy(n => n.Surname)
                            .ToPagedList(pageIndex, pageSize);
                    else
                        ministers = _db.MinisterProfiles.OrderBy
                                (m => m.DistrictId).ThenBy(n => n.Surname)
                                .ToPagedList(pageIndex, pageSize);
                    break;
                //case "ChurchId":
                //    if (sortOrder.Equals(sortFilterOld))
                //        ministers = _db.MinisterProfiles.OrderByDescending
                //                (m => m.ChurchId).ThenBy(n => n.LastName)
                //                .ToPagedList(pageIndex, pageSize);
                //    else
                //        ministers = _db.MinisterProfiles.OrderBy
                //                (m => m.ChurchId).ThenBy(n => n.LastName)
                //                .ToPagedList(pageIndex, pageSize);
                //    break;

                // Add sorting statements for other columns

                case "Default":
                    ministers = _db.MinisterProfiles.OrderBy
                            (m => m.DistrictId).ThenBy(n => n.Surname)
                            .ToPagedList(pageIndex, pageSize);
                    break;
            }

            ViewBag.CurrentSort = sortOrder;

            return PartialView("_MinisterTable", ministers);
        }

        [ChildActionOnly]
        public PartialViewResult MinisterList(int? page, int? pagesize)
        {
            //sortOrder: District, College, Church, -lastname: constant
            //Search: Any value

            pageSize = pagesize.HasValue ? Convert.ToInt32(pagesize) : 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            sortFilterOld = String.IsNullOrEmpty(ViewBag.CurrentSort) ? "DistrictId" : ViewBag.CurrentSort;

            IPagedList<MinisterProfile> ministers = null;

            switch (sortFilterOld)
            {
                case "DistrictId":
                    ministers = _db.MinisterProfiles.OrderBy
                            (m => m.DistrictId).ThenBy(n => n.Surname)
                            .ToPagedList(pageIndex, pageSize);
                    break;
                //case "ChurchId":
                //    ministers = _db.MinisterProfiles.OrderBy
                //            (m => m.ChurchId).ThenBy(n => n.LastName)
                //            .ToPagedList(pageIndex, pageSize);
                //    break;

                // Add sorting statements for other columns

                case "Default":
                    ministers = _db.MinisterProfiles.OrderBy
                            (m => m.DistrictId).ThenBy(n => n.Surname)
                            .ToPagedList(pageIndex, pageSize);
                    break;
            }

            return PartialView("_MinisterTable", ministers);

            //var cat_model = db.CategoryModels.ToList();
            //return PartialView("_SideBar", cat_model);
        }

        [ChildActionOnly]
        public PartialViewResult SearchMinister(string searchvalue)
        {
            //sortOrder: District, College, Church, -lastname: constant
            //Search: Any value

            searchValue = String.IsNullOrEmpty(searchvalue) ? "Nill" : searchvalue;

            sortFilterOld = String.IsNullOrEmpty(ViewBag.CurrentSort) ? "DistrictId" : ViewBag.CurrentSort;

            IPagedList<MinisterProfile> ministers = null;

            if (!String.IsNullOrEmpty(searchvalue))
            {

                switch (sortFilterOld)
                {
                    case "DistrictId":
                        ministers = _db.MinisterProfiles.OrderBy
                                (m => m.DistrictId).ThenBy(n => n.Surname)
                                .ToPagedList(pageIndex, pageSize);
                        break;
                    //case "ChurchId":
                    //    ministers = _db.MinisterProfiles.OrderBy
                    //            (m => m.ChurchId).ThenBy(n => n.LastName)
                    //            .ToPagedList(pageIndex, pageSize);
                    //    break;

                    // Add sorting statements for other columns

                    case "Default":
                        ministers = _db.MinisterProfiles.OrderBy
                                (m => m.DistrictId).ThenBy(n => n.Surname)
                                .ToPagedList(pageIndex, pageSize);
                        break;
                }
            }
            else
            {
                ministers = _db.MinisterProfiles.OrderBy
                               (m => m.DistrictId).ThenBy(n => n.Surname)
                               .ToPagedList(pageIndex, pageSize);
            }

            return PartialView("_MinisterTable", ministers);
        }


        [ChildActionOnly]
        public PartialViewResult MinisterTableSample(string sortOrder, string currentFilter,
            string searchvalue, int? page, int? pagesize)
        {
            //sortOrder: District, College, Church, -lastname: constant
            //Search: Any value

            pageSize = pagesize.HasValue ? Convert.ToInt32(pagesize) : 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            searchValue = String.IsNullOrEmpty(searchvalue) ? "Nill" : searchvalue;

            ViewBag.CurrentSort = sortOrder;

            sortOrder = String.IsNullOrEmpty(sortOrder) ? "DistrictId" : sortOrder;

            IPagedList<MinisterProfile> ministers = null;

            switch (sortOrder)
            {
                case "DistrictId":
                    if (sortOrder.Equals(currentFilter))
                        ministers = _db.MinisterProfiles
                            .OrderByDescending(m => m.DistrictId).ThenBy(n => n.Surname)
                            .ToPagedList(pageIndex, pageSize);
                    else
                        ministers = _db.MinisterProfiles.OrderBy
                                (m => m.DistrictId).ThenBy(n => n.Surname)
                                .ToPagedList(pageIndex, pageSize);
                    break;
                //case "ChurchId":
                //    if (sortOrder.Equals(currentFilter))
                //        ministers = _db.MinisterProfiles.OrderByDescending
                //                (m => m.ChurchId).ThenBy(n => n.LastName)
                //                .ToPagedList(pageIndex, pageSize);
                //    else
                //        ministers = _db.MinisterProfiles.OrderBy
                //                (m => m.ChurchId).ThenBy(n => n.LastName)
                //                .ToPagedList(pageIndex, pageSize);
                //    break;

                // Add sorting statements for other columns

                case "Default":
                    ministers = _db.MinisterProfiles.OrderBy
                            (m => m.DistrictId).ThenBy(n => n.Surname)
                            .ToPagedList(pageIndex, pageSize);
                    break;
            }

            return PartialView("_MinisterTable", ministers);

            //var cat_model = db.CategoryModels.ToList();
            //return PartialView("_SideBar", cat_model);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
