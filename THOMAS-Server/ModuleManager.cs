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

        private List<Type> _failedDrivers = new List<Type>();

        private ApplicationConfig _applicationConfig;

        public ModuleManager(ApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig;
        }

        public void Initialize()
        {
            Instantiate(typeof(RemoteController));
            //Instantiate(typeof(SLAMController));
        }

        private IThomasElement Instantiate(Type elementType)
        {
            // Ist bereits eine Instanz vorhanden?
           
            if (_drivers.Any(e => e.GetType () == elementType)) return _drivers.First(e => e.GetType() == elementType);
            else if (_sensors.Any(e => e.GetType() == elementType)) return _sensors.First(e => e.GetType() == elementType);
            else if (_actors.Any(e => e.GetType() == elementType)) return _actors.First(e => e.GetType() == elementType);
            else if (_controllers.Any(e => e.GetType() == elementType)) return _controllers.First(e => e.GetType() == elementType);

            // Parameter, die der neuen Instanz übergeben werden
            List<object> instanceParameters = new List<object>();

            RequirementAttribute requirementAttribute = (RequirementAttribute)elementType.GetCustomAttributes(typeof(RequirementAttribute), true).FirstOrDefault();
            if (requirementAttribute != null)
            {
                foreach (Type subElementType in requirementAttribute.RequiredElements)
                {
                    IThomasElement requiredInstance = Instantiate(subElementType);

                    if (requiredInstance == null)
                    {
                        Logger.Warning($"Abhängigkeit {subElementType.Name} von {elementType.Name} konnte nicht erfüllt werden.");
                        return null;
                    }

                    instanceParameters.Add(requiredInstance);
                }
            }

            OptionalAttribute optionalAttribute = (OptionalAttribute)elementType.GetCustomAttributes(typeof(OptionalAttribute), true).FirstOrDefault();
            if (optionalAttribute != null)
            {
                foreach (Type subElementType in optionalAttribute.OptionalElements)
                {
                    IThomasElement requiredInstance = Instantiate(subElementType);

                    if (requiredInstance == null)
                        Logger.Warning($"Optionale Abhängigkeit {subElementType.Name} von {elementType.Name} konnte nicht erfüllt werden.");

                    instanceParameters.Add(requiredInstance);
                }
            }

            ConfigAttribute configAttribute = (ConfigAttribute)elementType.GetCustomAttributes(typeof(ConfigAttribute), true).FirstOrDefault();
            if(configAttribute != null)
            {
                if(_applicationConfig.ModuleConfigs.ContainsKey(configAttribute.Id))
                {
                    instanceParameters.Add(_applicationConfig.ModuleConfigs[configAttribute.Id]);
                }
            }

            // Instanz erstellen
            object instance;

            if (instanceParameters.Count > 0)
               instance = Activator.CreateInstance(elementType, instanceParameters.ToArray());
            else
                instance = Activator.CreateInstance(elementType);

            if (typeof(IDriver).IsAssignableFrom(elementType))
            {
                if (_failedDrivers.Contains(elementType))
                    return null;

                if (!((IDriver)instance).Initialize())
                {
                    Logger.Warning($"Treiber {((IDriver) instance).Name} konnte nicht initalisiert werden.");
                    _failedDrivers.Add(elementType);

                    return null;
                }
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
