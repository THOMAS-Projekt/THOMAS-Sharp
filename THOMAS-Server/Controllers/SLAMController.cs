using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THOMASServer.Controllers
{
    class SLAMController : IController
    {
        public bool IsEnabled => _isEnabled;

        public string Name => "SLAM";

        private bool _isEnabled = false;

        public void Start()
        {
            _isEnabled = true;
            throw new NotImplementedException();
        }

        public void Stop()
        {
            _isEnabled = false;
            throw new NotImplementedException();
        }
    }
}
