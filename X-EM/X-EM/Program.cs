using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace X_EM
{
   class Program
   {
      static void Main(string[] args)
      {
         DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
      }
   }
}
