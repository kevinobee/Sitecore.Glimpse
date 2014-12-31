using System;

namespace Sitecore.Glimpse.Model
{
    public enum ControllerType
    {
        MVC,
        WebAPI
    }

    public class Controller
    {
        public string Name { get; set; }
        public ControllerType ControllerType { get; set; }
        public string Definition { get; set; }

        public Controller(string name, ControllerType controllerType, string definition)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (definition == null) throw new ArgumentNullException("definition");

            Name = name;
            ControllerType = controllerType;
            Definition = definition;
        }
    }
}