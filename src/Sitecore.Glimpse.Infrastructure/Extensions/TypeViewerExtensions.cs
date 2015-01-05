using Sitecore.Glimpse.Infrastructure.Reflection;
using Sitecore.Glimpse.Model;

namespace Sitecore.Glimpse.Infrastructure.Extensions
{
    public static class TypeViewerExtensions
    {
        public static Csrf CheckForMitigations(this TypeViewer typeViewer, ControllerType controllerType)
        {
            const string webApiAntiForgeryAttributeName = "ValidateHttpAntiForgeryTokenAttribute";

            var mvcAntiForgeryAttributeName = typeof(System.Web.Mvc.ValidateAntiForgeryTokenAttribute).Name;


            var classValidation =
                typeViewer.HasClassAttribute(controllerType == ControllerType.MVC
                    ? mvcAntiForgeryAttributeName
                    : webApiAntiForgeryAttributeName);

            var methodValidation =
                typeViewer.HasMethodAttribute(controllerType == ControllerType.MVC
                    ? mvcAntiForgeryAttributeName
                    : webApiAntiForgeryAttributeName);

            return classValidation ? Csrf.Class 
                : methodValidation ? Csrf.Method 
                    : Csrf.None;
        }
    }
}