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
    [Requirement(typeof(CameraServoActor))]
    public class RemoteController : IController
    {
        public bool IsEnabled { get; private set; } = false;

        public string Name => "Fernsteuerung";

        public RemoteController(CameraServoActor cameraServoActor)
        {

        }

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
