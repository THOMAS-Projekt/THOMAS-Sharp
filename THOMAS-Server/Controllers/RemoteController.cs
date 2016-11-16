using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THOMASServer.Actors;
using THOMASServer.Attributes;
using THOMASServer.Utils;

namespace THOMASServer.Controllers
{
    [Optional(typeof(CameraServoActor))]
    public class RemoteController : IController
    {
        public bool IsEnabled => _isEnabled;

        public string Name => "Fernsteuerung";

        private bool _isEnabled = false;

        public RemoteController(CameraServoActor cameraServoActor)
        {

        }

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
