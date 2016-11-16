using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THOMASServer.Attributes
{
    public class OptionalAttribute : Attribute
    {
        public Type[] OptionalElements { get; private set; }

        public OptionalAttribute(params Type[] optionalElements)
        {
            OptionalElements = optionalElements;
        }
    }
}
