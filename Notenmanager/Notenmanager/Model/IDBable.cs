using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notenmanager.Model
{
   ///<summary>
   ///Interface für Standardmethoden einer datenbankfähigen Fachklasse
   ///</summary>
   public interface IDBable
   {
      int Id { get; }

      bool Active { get; set; }

      bool Speichern();
      bool Loeschen();

      void Reload();
   }
}
