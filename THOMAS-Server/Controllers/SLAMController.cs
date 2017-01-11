using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THOMASServer.Actors;
using THOMASServer.Attributes;

namespace THOMASServer.Controllers
{
    [Requirement(typeof(LidarActor))]
    public class SLAMController : IController
    {
        public bool IsEnabled { get; private set; } = false;

        public string Name => "SLAM";

        private readonly LidarActor _lidarActor;

        public SLAMController(LidarActor lidarActor)
        {
            _lidarActor = lidarActor;
            lidarActor.StartScan(1);

            //lidarActor.SaveScansToFile("scans.csv", 1, 10);
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
