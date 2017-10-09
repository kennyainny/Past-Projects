using System.Web;
using System.Web.Optimization;

namespace Application.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725

        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new StyleBundle("~/Content/css").Include(
               "~/Content/Gridmvc.css",
                "~/Content/bootstrap/css/bootstrap.css",
               "~/Content/theme.css",
               "~/Content/themes/base/jquery-ui.css",
               "~/Content/themes/base/jquery.ui.dialog.css"));
               //"~/Content/font-awesome/css/font-awesome.css",
               //"~/Content/font-awesome-4/css/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.10.4.js",
                        "~/Scripts/gridmvc.js"));
            //"~/Scripts/gridmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                        "~/Content/bootstrap/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/customjs").Include(
                        "~/Scripts/CustomScripts.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/jquerydatatable").Include(
                       "~/Content/datatables/jquery.dataTables.js"));
                        //"~/Content/DataTables-10/js/jquery.dataTables.js"));
                       //"~/Content/datatables/DT_bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/datatablecss").Include(               
                "~/Content/datatables/css/jquery.dataTables.css"));
                //"~/Content/DataTables-10/css/jquery.dataTables.css"));
                //"~/Content/datatables/css/DT_bootstrap.css"));


            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));
                        
            //bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
            //                        "~/Scripts/knockout-{version}.js"));

           

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
           
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}