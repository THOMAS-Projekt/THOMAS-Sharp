using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THOMASServer.Attributes
{
    public class ConfigAttribute : Attribute
    {
        public string Id { get; private set; }

        public ConfigAttribute(string id)
        {
            Id = id;
        }
    }
}
