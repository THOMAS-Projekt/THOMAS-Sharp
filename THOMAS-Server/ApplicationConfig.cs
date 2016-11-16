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
            XElement thomasConfig = XDocument.Load(filename).Element("thomas");
            XElement applicationConfig = thomasConfig.Element("application");
            XElement modulesConfig = thomasConfig.Element("modules");

            Debug = bool.Parse(applicationConfig.Element("debug").Value);

            foreach (XElement moduleConfig in modulesConfig.Elements("module"))
            {
                string id = moduleConfig.Attribute("id").Value;
                XElement config = moduleConfig.Element("config");

                if (config == null)
                    continue;

                ModuleConfigs[id] = moduleConfig.Element("config");
            }
        }
    }
}
