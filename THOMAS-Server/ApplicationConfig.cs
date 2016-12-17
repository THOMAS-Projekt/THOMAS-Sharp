using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace THOMASServer
{
    public class ApplicationConfig
    {
        public bool Debug { get; private set; }

        public Dictionary<string, XElement> ModuleConfigs { get; private set; } = new Dictionary<string, XElement>();

        public ApplicationConfig(string filename)
        {
            XElement thomasConfig = XDocument.Load(filename).Element("Thomas");
            XElement applicationConfig = thomasConfig.Element("Application");
            XElement modulesConfig = thomasConfig.Element("Modules");

            Debug = bool.Parse(applicationConfig.Element("Debug").Value);

            foreach (XElement moduleConfig in modulesConfig.Elements("Module"))
            {
                string id = moduleConfig.Attribute("id").Value;
                XElement config = moduleConfig.Element("Config");

                if (config == null)
                    continue;

                ModuleConfigs[id] = config;
            }
        }
    }
}
