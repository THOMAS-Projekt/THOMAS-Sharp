using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THOMASServer.Actors;
using THOMASServer.Attributes;
using THOMASServer.Controllers;
using THOMASServer.Drivers;
using THOMASServer.Pools;
using THOMASServer.Sensors;
using THOMASServer.Utils;

namespace THOMASServer
{
    public class ModuleManager
    {
        private Pool<IDriver> _drivers = new Pool<IDriver>();
        private Pool<ISensor> _sensors = new Pool<ISensor>();
        private Pool<IActor> _actors = new Pool<IActor>();
        private Pool<IController> _controllers = new Pool<IController>();

        public void Initialize()
        {
            Instantiate(typeof(RemoteController));
            //Instantiate(typeof(SLAMController));
        }

        private IThomasElement Instantiate(Type elementType)
        {            
            // Ist bereits eine Instanz vorhanden?
            if (_drivers.Select(e => e.GetType() == elementType).Count() >= 1) return _drivers.First(e => e.GetType() == elementType);
            else if (_sensors.Select(e => e.GetType() == elementType).Count() >= 1) return _sensors.First(e => e.GetType() == elementType);
            else if (_actors.Select(e => e.GetType() == elementType).Count() >= 1) return _actors.First(e => e.GetType() == elementType);
            else if (_controllers.Select(e => e.GetType() == elementType).Count() >= 1) return _controllers.First(e => e.GetType() == elementType);

            // Instanzen sammeln, die diese Instanz verlangt
            List<IThomasElement> requiredInstances = new List<IThomasElement>();

            RequirementAttribute requirementAttribute = (RequirementAttribute)elementType.GetCustomAttributes(typeof(RequirementAttribute), true).FirstOrDefault();
            if (requirementAttribute != null)
            {
                foreach (Type subElementType in requirementAttribute.RequiredElements)
                {
                    IThomasElement requiredInstance = Instantiate(subElementType);

                    if (requiredInstance == null)
                        return null;

                    requiredInstances.Add(requiredInstance);
                }
            }

            OptionalAttribute optionalAttribute = (OptionalAttribute)elementType.GetCustomAttributes(typeof(OptionalAttribute), true).FirstOrDefault();
            if (optionalAttribute != null)
            {
                foreach (Type subElementType in optionalAttribute.OptionalElements)
                {
                    IThomasElement requiredInstance = Instantiate(subElementType);

                    requiredInstances.Add(requiredInstance);
                }
            }

            // Instanz erstellen
            object instance;
            if (typeof(IDriver).IsAssignableFrom(elementType))
            {
                instance = Activator.CreateInstance(elementType);

                if (!((IDriver)instance).Initialize()) // TODO: Debug Meldungen implementieren
                    return null;
            }
            else
            {
                instance = Activator.CreateInstance(elementType, requiredInstances.Cast<object>().ToArray());
            }

            // Instanz zu einem der Pools hinzufügen
            if (instance is IDriver) _drivers.Add((IDriver)instance);
            else if (instance is ISensor) _sensors.Add((ISensor)instance);
            else if (instance is IActor) _actors.Add((IActor)instance);
            else if (instance is IController) _controllers.Add((IController)instance);
            else throw new ArgumentException("Ungültiger Element-Typ", nameof(elementType));

            return (IThomasElement)instance;
        }
    }
}
