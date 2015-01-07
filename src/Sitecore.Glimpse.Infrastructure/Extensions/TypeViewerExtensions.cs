using Sitecore.Glimpse.Infrastructure.Reflection;
using Sitecore.Glimpse.Model;

namespace Sitecore.Glimpse.Infrastructure.Extensions
{
    public static class TypeViewerExtensions
    {
        public static bool CheckForAuthorise(this TypeViewer typeViewer, ControllerType controllerType)
        {
            var webApiAuthorizeAttributeName = typeof(System.Web.Http.AuthorizeAttribute).Name;

            var mvcAuthorizeAttributeName = typeof(System.Web.Mvc.AuthorizeAttribute).Name;

            var attributeToDetect = (controllerType == ControllerType.MVC)
                                    ? mvcAuthorizeAttributeName
                                    : webApiAuthorizeAttributeName;

            var classValidation = typeViewer.HasClassAttribute(attributeToDetect);
            var methodValidation = typeViewer.HasMethodAttribute(attributeToDetect);

            return classValidation || methodValidation;
        }

        public static Csrf CheckForMitigations(this TypeViewer typeViewer, ControllerType controllerType)
        {
            const string webApiAntiForgeryAttributeName = "ValidateHttpAntiForgeryTokenAttribute";

            var mvcAntiForgeryAttributeName = typeof(System.Web.Mvc.ValidateAntiForgeryTokenAttribute).Name;

            var attributeToDetect = (controllerType == ControllerType.MVC)
                        ? mvcAntiForgeryAttributeName
                        : webApiAntiForgeryAttributeName;

            var classValidation = typeViewer.HasClassAttribute(attributeToDetect);
            var methodValidation = typeViewer.HasMethodAttribute(attributeToDetect);

            return classValidation ? Csrf.Class 
                : methodValidation ? Csrf.Method 
                    : Csrf.None;
        }
    }
}