using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THOMASServer.Utils;

namespace THOMASServer.Pools
{
    /// <summary>
    /// Definiert einen Pool für Programmkomponenten.
    /// </summary>
    /// <typeparam name="TE">Interfacetyp der Komponente.</typeparam>
    public class Pool<TE> : List<TE> where TE : IThomasElement
    {
        /// <summary>
        /// Gibt, falls vorhanden, die Instanz der angefragten Komponente zurück.
        /// </summary>
        /// <typeparam name="T">Typ der gesuchten Instanz.</typeparam>
        /// <returns>Die gesuchte Instanz oder Null.</returns>
        public T GetInstance<T>() where T : TE
        {
            return (T)this.FirstOrDefault(e => e is T);
        }

        /// <summary>
        /// Gibt, falls vorhanden, die Instanz der angefragten Komponente zurück.
        /// </summary>
        /// <param name="elementType">Der Typ der gesuchten Instanz.</param>
        /// <returns>Die gesuchte Instanz oder Null.</returns>
        public IThomasElement GetInstance(Type elementType)
        {
            return this.FirstOrDefault(e => e.GetType() == elementType);
        }

        /// <summary>
        /// Prüft, ob der Pool eine Instanz des angegebenen Typen enthält. Unabhängig vom Basis-Typen. 
        /// </summary>
        /// <param name="elementType">Der Typ der gesuchten Instanz.</param>
        /// <returns>True, falls die Instanz gefunden wurde, sonst False.</returns>
        public bool ContainsInstance(Type elementType)
        {
            return this.Any(e => e.GetType() == elementType);
        }

        /// <summary>
        /// Fügt die übergebene Instanz dem Pool hinzu, sofern deren Typen in den Pools passt.
        /// </summary>
        /// <param name="instance">Die hinzuzufügene Instanz.</param>
        /// <returns>Gibt an, ob die Instanz zum Pool hinzugefügt wurde.</returns>
        public bool AddIfMAtches(IThomasElement instance)
        {
            if (!(instance is TE))
                return false;

            Add((TE)instance);

            return true;
        }
    }
}
