using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THOMASServer.Utils;

namespace THOMASServer.Attributes
{
    public class RequirementAttribute : Attribute
    {
        public Type[] RequiredElements { get; private set; }

        public RequirementAttribute(params Type[] requiredElements)
        {
            RequiredElements = requiredElements;
        }
    }
}
