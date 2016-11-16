using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THOMASServer.Utils;

namespace THOMASServer.Sensors
{
    public interface ISensor : IThomasElement
    {
        string ValueString { get; }
    }
}
