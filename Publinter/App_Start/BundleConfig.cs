using System.Web;
using System.Web.Optimization;

namespace Publinter
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region 'Bundles Admin Alteir'

            bundles.Add(new StyleBundle("~/Content/components").Include(
                "~/Content/Admin/bower_components/uikit/css/uikit.almost-flat.min.css",
                "~/Content/Admin/assets/css/style_switcher.css",
                "~/Content/Admin/assets/css/main.min.css",
                "~/Content/Admin/assets/skins/jquery.fancytree/ui.fancytree.min.css",

                "~/Content/Admin/plugins/daterangepicker/daterangepicker.css",
                "~/Content/bootstrap-datetimepicker.min.css",
                "~/Content/bootstrap-datetimepicker-standalone.css",
                "~/Content/Admin/plugins/select2/select2.css"));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                "~/Content/Admin/assets/js/common.min.js",
                "~/Content/Admin/assets/js/uikit_custom.min.js",
                "~/Content/Admin/bower_components/jquery-ui/jquery-ui.min.js",
                "~/Content/Admin/assets/js/altair_admin_common.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                "~/Content/Admin/bower_components/moment/moment.min.js",
                "~/Content/Admin/bower_components/moment/min/moment-with-locales.min.js",
                "~/Content/Admin/plugins/daterangepicker/daterangepicker.js",
                "~/Content/Admin/bower_components/jquery.fancytree/dist/jquery.fancytree-all-deps.min.js",
                "~/Content/Admin/bower_components/jquery-rotate/jQueryRotate.js",
                "~/Content/Admin/bower_components/MProgress.js-master/mprogress.min.js",
                "~/Content/Admin/assets/js/pages/plugins_tree.js",
                "~/Content/Admin/plugins/select2/select2.full.js",
                "~/Content/Admin/bower_components/ToggleSwitch/ToggleSwitch.js",
                "~/Scripts/bootstrap-datetimepicker.min.js",
                "~/Scripts/d3.js",
                "~/Scripts/c3.min.js",
                "~/Content/Admin/bower_components/shortcut/shortcut.js"
            ));

            bundles.Add(new StyleBundle("~/bundles/datatables/css").Include(
                "~/Content/Admin/bower_components/datatables/datatables.min.css",
                "~/ContentAdmin/bower_components/datatables/DataTables-1.10.13/css/dataTables.foundation.mini.css",
                "~/ContentAdmin/bower_components/datatables/DataTables-1.10.13/css/dataTables.jqueryui.min.css",
                "~/ContentAdmin/bower_components/datatables/DataTables-1.10.13/css/jquery.dataTables.min.css",
                "~/Content/Admin/bower_components/datatables/Responsive-2.1.1/css/responsive.dataTables.min.css",
                "~/Content/Admin/bower_components/datatables/Scroller-1.4.2/css/scroller.dataTables.min.css",
                "~/Content/Admin/bower_components/MProgress.js-master/build/css/mprogress.min.css",
                "~/Content/Admin/bower_components/ToggleSwitch/ToggleSwitch.css"
               ));

            bundles.Add(new ScriptBundle("~/bundles/datatables/js").Include(
                "~/Content/Admin/bower_components/datatables/DataTables-1.10.13/js/jquery.dataTables.min.js",
                "~/Content/Admin/bower_components/datatables/Responsive-2.1.1/js/dataTables.responsive.min.js",
                "~/Content/Admin/bower_components/datatables/Scroller-1.4.2/js/dataTables.scroller.min.js"
                ));

            //bundles.Add(new ScriptBundle("~/bundles/C3").Include(
            //    "~/Scripts/d3.min.js",
            //    "~/Scripts/c3.min.js"
            //    ));


            #endregion
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
