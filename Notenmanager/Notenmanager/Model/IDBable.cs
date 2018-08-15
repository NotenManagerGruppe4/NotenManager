using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notenmanager.Model
{
   ///<summary>
   ///Interface für eine datenbankfähige Fachklasse
   ///</summary>
   public interface IDBable
   {
      int Id { get; }

      bool Active { get; set; }

      void Speichern();
      void Loeschen();

      void Reload();
   }
}
