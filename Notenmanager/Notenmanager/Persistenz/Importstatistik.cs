using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notenmanager.Persistenz
{
   public class Importstatistik
   {
      public int OkCount { get; set; }

      public List<Exception> Fehler { get; set; } = new List<Exception>();

      public List<Exception> SchuelerKlassenFehler { get; set; } = new List<Exception>();
   }
}
