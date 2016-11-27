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
        private readonly Pool<IDriver> _drivers = new Pool<IDriver>();
        private readonly Pool<ISensor> _sensors = new Pool<ISensor>();
        private readonly Pool<IActor> _actors = new Pool<IActor>();
        private readonly Pool<IController> _controllers = new Pool<IController>();

        private readonly List<Type> _failedDrivers = new List<Type>();

        private readonly ApplicationConfig _applicationConfig;

        public ModuleManager(ApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig;
        }

        public void Initialize()
        {
            Instantiate(typeof(RemoteController));
            Instantiate(typeof(SLAMController));
        }

        private IThomasElement Instantiate(Type elementType)
        {
            // Ist bereits eine Instanz vorhanden?
            if (_drivers.ContainsInstance(elementType)) return _drivers.GetInstance(elementType);
            if (_sensors.ContainsInstance(elementType)) return _sensors.GetInstance(elementType);
            if (_actors.ContainsInstance(elementType)) return _actors.GetInstance(elementType);
            if (_controllers.ContainsInstance(elementType)) return _controllers.GetInstance(elementType);

            // Parameter, die der neuen Instanz übergeben werden
            List<object> instanceParameters = new List<object>();

            // Erforderliche Abhängigkeiten. Sollte eine nicht verfügbar sein, ist dieses Element auch nicht verfügbar.
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

            // Optionale Abhängigkeiten. Falls verfügbar, werden diese Komponenten dem aktuellen Element mit übergeben.
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

            // Falls ein Konfigurationselement angefordert wird, gib dieses, falls vorhanden zurück. Sollte es angefordert werden, aber nicht vorhanden sein, gib Null zurück.
            ConfigAttribute configAttribute = (ConfigAttribute)elementType.GetCustomAttributes(typeof(ConfigAttribute), true).FirstOrDefault();
            if (configAttribute != null)
                instanceParameters.Add(_applicationConfig.ModuleConfigs.ContainsKey(configAttribute.Id) ? _applicationConfig.ModuleConfigs[configAttribute.Id] : null);

            // Instanz erstellen
            IThomasElement instance = (IThomasElement)(instanceParameters.Count > 0 ? Activator.CreateInstance(elementType, instanceParameters.ToArray()) : Activator.CreateInstance(elementType));

            // Sonderbehandlung von Treibern, da diese aufgrund von fehlender Hardware nicht initialisiert werden könnten.
            if (typeof(IDriver).IsAssignableFrom(elementType))
            {
                if (_failedDrivers.Contains(elementType))
                    return null;

                if (!((IDriver)instance).Initialize())
                {
                    Logger.Warning($"Treiber {((IDriver)instance).Name} konnte nicht initalisiert werden.");
                    _failedDrivers.Add(elementType);

                    return null;
                }
            }

            // Instanz zu einem der Pools hinzufügen
            if (!_drivers.AddIfMAtches(instance) && !_sensors.AddIfMAtches(instance) && !_actors.AddIfMAtches(instance) && !_controllers.AddIfMAtches(instance))
                throw new ArgumentException("Ungültiger Element-Typ", nameof(elementType));

            return instance;
        }
    }
}
