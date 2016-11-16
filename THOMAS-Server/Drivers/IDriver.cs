using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THOMASServer.Utils;

namespace THOMASServer.Drivers
{
    public interface IDriver : IThomasElement
    {
        /// <summary>
        /// Versucht den Treiber zu Initailisieren (Verbindungsaufbau etc.).
        /// </summary>
        /// <returns>Gibt zurück, ob der Treiber initialisiert werden konnte.</returns>
        bool Initialize();
    }
}
