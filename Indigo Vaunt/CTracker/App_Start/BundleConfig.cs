using System.Web;
using System.Web.Optimization;

namespace CTracker
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = false;

            #region Bootstrap Ref

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js",
                       "~/Scripts/jquery-ui-{version}.js"));
                       //,
                       //"~/Scripts/DataTables-1.10.7/jquery-2.1.1,js",
                       //"~/Scripts/DataTables-1.10.7/jquery.min.js",
                       //"~/Scripts/DataTables-1.10.7/media/js/jquery.dataTables.js",
                       //"~/Scripts/DataTables-1.10.7/integration/bootstrap/3/dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/metisMenu").Include(
                      "~/Scripts/metisMenu/dist/metisMenu.js",
                //"~/Scripts/raphael/raphael.js",
                //"~/Scripts/morrisjs/morris.js",
                //"~/Scripts/js/morris-data.js",
                      "~/Scripts/js/iv.js"));

            bundles.Add(new StyleBundle("~/Content/metis").Include(
                "~/Content/bootstrap.css",
               // "~/Scripts/DataTables-1.10.7/integration/bootstrap/3/dataTables.bootstrap.css",
            "~/Content/metisMenu/metisMenu.css",
            //"~/Content/timeline/timeline.css",
            "~/Content/iv.css",
            //"~/Content/morrisjs/morris.css",
            "~/Content/font-awesome/css/font-awesome.css",
                "~/Content/ace.css"
            ));

            bundles.Add(new StyleBundle("~/Script/dataTables").Include(
                "~/Scripts/DataTables-1.10.7/media/css/jquery.dataTables.css",
                "~/Scripts/DataTables-1.10.7/integration/bootstrap/3/dataTables.bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                "~/Scripts/DataTables-1.10.7/media/js/jquery.dataTables.js",
                "~/Scripts/DataTables-1.10.7/integration/bootstrap/3/dataTables.bootstrap.js"));

           
            //bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
            //          "~/Scripts/datatables/jquery.dataTables.js",
            //    "~/Scripts/datatables/jquery.dataTables.bootstrap.min.js",
            //    "~/Scripts/datatables/dataTables.tableTools.min.js",
            //    //"~/Scripts/datatables/dataTables.colVis.min.js",
            //    "~/Scripts/datatables/dataTables.colVis.min.js"
            //    ));


            #endregion

            #region Old References
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive*",
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

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
            #endregion
        }
    }
}