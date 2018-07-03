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
         DbConfiguration.SetConfiguration(new MySqlEFConfiguration());


         using (var context = new ModelContext())
         {
            Schule s = new Schule()
            {
               Bez = "TestSchule",
            };
            context.SchuleSet.Add(s);
            context.SaveChanges();
         }


      }
   }
}
