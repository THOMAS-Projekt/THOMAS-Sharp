using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THOMASServer.Controllers
{
    class SLAMController : IController
    {
        public bool IsEnabled { get; private set; } = false;

        public string Name => "SLAM";

        public void Start()
        {
            IsEnabled = true;
            throw new NotImplementedException();
        }

        public void Stop()
        {
            IsEnabled = false;
            throw new NotImplementedException();
        }
    }
}
