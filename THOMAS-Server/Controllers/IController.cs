using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THOMASServer.Utils;

namespace THOMASServer.Controllers
{
    public interface IController : IThomasElement
    {
        bool IsEnabled { get;}

        void Start();
        void Stop();
    }
}
