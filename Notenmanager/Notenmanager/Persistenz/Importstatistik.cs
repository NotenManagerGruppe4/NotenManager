using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notenmanager.Persistenz
{
    public class Importstatistik
    {
        public string Bez { get; set; } = "";

        public int OkCount { get; set; }

        public List<Exception> Fehler { get; set; } = new List<Exception>();

        public List<Exception> SchuelerKlassenFehler { get; set; } = new List<Exception>();

        public override string ToString()
        {
            return $"Importergebniss:\r\n" +
            $"{OkCount} {Bez} ok und gespeichert\r\n" +
            (SchuelerKlassenFehler.Count > 0 ?
            $"\tdavon konnten bei {SchuelerKlassenFehler.Count} keine Schülerklassen erstellt werden:\r\n" : "") +
            (Fehler.Count > 0 ?
            $"{Fehler.Count} {Bez} fehlerhaft und nicht importiert:\r\n" : "");
        }
    }
}
