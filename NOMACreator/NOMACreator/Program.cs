using MySql.Data.Entity;
using NOMACreator.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOMACreator
{
   class Program
   {
      static void Main(string[] args)
      {
         AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
         {
            DBZugriff.CloseDB();
         };


         DBZugriff.InitDB();


         //CODE
         Schule s = new Schule()
         {
            Bez = "TestSchule",
         };
         s.Speichern();

         DBZugriff.CloseDB();


      }
   }
}
