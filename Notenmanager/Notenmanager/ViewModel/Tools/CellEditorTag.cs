using Notenmanager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notenmanager.ViewModel
{
   public class CellEditorTag
   {
      public DialogMode Mode { get; set; }

      public Leistung Leistung { get; set; } = null;

      public Leistungsart Leistungsart { get; set; } = null;
      public Unterrichtsfach Unterrichtsfach { get; set; } = null;
      public Klasse Klasse { get; set; } = null;
   }
}
