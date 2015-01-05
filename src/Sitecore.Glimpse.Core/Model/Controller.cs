using System;

namespace Sitecore.Glimpse.Model
{
    public class Controller
    {
        public string Name { get; private set; }
        public ControllerType ControllerType { get; private set; }
        public string Definition { get; private set; }
        public Csrf CsrfProtection { get; private set; }

        public Controller(string name, ControllerType controllerType, string definition, Csrf csrfProtection)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (definition == null) throw new ArgumentNullException("definition");

            Name = name;
            ControllerType = controllerType;
            Definition = definition;
            CsrfProtection = csrfProtection;
        }
    }
}