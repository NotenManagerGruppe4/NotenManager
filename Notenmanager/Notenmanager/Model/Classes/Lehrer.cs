using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notenmanager.Model
{
   [Table("lehrer")]
   public class Lehrer : IDBable
   {
      public const int KUERZEL_MAXLENGTH = 5;

      public Lehrer()
      { }

      public int Id { get; private set; }
      public int SID { get; set; } = 0;

      public bool Active { get; set; } = true;

      [Required,MaxLength(KUERZEL_MAXLENGTH)]
      public string Kürzel { get; set; }

      [Required, MaxLength(DBZugriff.STRING_MAXLENGTH)]
      public string Nachname { get; set; }

      [Required, MaxLength(DBZugriff.STRING_MAXLENGTH)]
      public string Vorname { get; set; }

      [MaxLength(DBZugriff.STRING_MAXLENGTH)]
      public string Dienstbezeichnung { get; set; } = null;

      public virtual List<UFachLehrer> UFaecherLehrer { get; set; } = new List<UFachLehrer>();

      public virtual List<Klasse> Klassenleiter { get; set; } = new List<Klasse>();
      public virtual List<Klasse> StellvertretenderKlassenleiter { get; set; } = new List<Klasse>();

      public bool Speichern()
      {
         return DBZugriff.Current.Speichern(this);
      }

      public bool Loeschen()
      {
         return DBZugriff.Current.Loeschen(this);
      }

      public void Reload()
      {
         DBZugriff.Current.Reload(this);
      }

   }
}
