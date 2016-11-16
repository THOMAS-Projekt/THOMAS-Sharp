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
    /// <typeparam name="E">Interfacetyp der Komponente.</typeparam>
    public class Pool<E> : List<E> where E : IThomasElement
    {
        /// <summary>
        /// Gibt, falls vorhanden, die Instanz der angefragten Komponente zurück.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetInstance<T>() where T : E
        {
            return (T)this.FirstOrDefault(e => e is T);
        }
    }
}
