using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notenmanager.ViewModel
{
   public class Tool
   {
      //Muss genau, wie der NotenSchluss irgendwo gespeichert sein (--> DB)
      [Obsolete]
      public static readonly DateTime HALBJAHRESDATUM = new DateTime(2017, 04, 28, 23, 59, 59);

      //von 2017/18 --> 17
      public static readonly int CURRENTSJ = GetCurrentSJ();
      private static int GetCurrentSJ()
      {
         return GetSJ(DateTime.Now);
      }

      public static int GetSJ(DateTime d)
      {
         int re = d.Year;
         if (d.Month >= 9) //ab September
            return re;
         else
            return --re;
      }
   }
}
