using System;

namespace Sitecore.Glimpse.Model
{
    public class Controller : ControllerBase
    {
        public ControllerType ControllerType { get; private set; }

        public Controller(string name, ControllerType controllerType, string definition, Csrf csrfProtection, bool authorise)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (definition == null) throw new ArgumentNullException("definition");

            Name = name;
            ControllerType = controllerType;
            Definition = definition;
            CsrfProtection = csrfProtection;
            Authorise = authorise;
        }
    }
}