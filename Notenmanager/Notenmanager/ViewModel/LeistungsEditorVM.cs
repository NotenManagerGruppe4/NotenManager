using Notenmanager.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notenmanager.ViewModel
{
   public class LeistungsEditorVM : BaseViewModel
   {

      public ICommand SpeichernCMD { get; set; }
      public ICommand BeendenCMD { get; set; }

      public LeistungsEditorVM()
      {
         BeendenCMD = new ActionCommand(Beenden);
         SpeichernCMD = new ActionCommand(Speichern);
      }

      public event EventHandler CloseAfterSaveRequesting;

      private void Speichern(object obj)
      {
         try
         {
            CLeistung.LetzteÄnderung = DateTime.Now;
            CLeistung.Speichern();

            CloseAfterSaveRequesting?.Invoke(this, new EventArgs());
         }
         catch (Exception e)
         {
            Trace.WriteLine("[LeiEdit] Speichern: " + e.ToString());
         }
      }

      private void Beenden(object obj)
      {
         try
         {
            DBZugriff.Current.Reload(CLeistung);
         }
         catch (Exception e)
         {
            Trace.WriteLine("[LeiEdit] Beenden: " + e.ToString());
         }
      }

      //Config
      public DialogMode Mode { get; private set; }
      public void SetMode(DialogMode mode, Leistungsart la, Unterrichtsfach uf, Klasse k)
      {
         CurrentKlasse = k;

         if (Mode == DialogMode.Neu)
         {

            CLeistung = new Leistung();

            if (la != null)
               Leistungsart = la;
            else
               Leistungsart = LstLeistungsarten.FirstOrDefault();
            SchuelerKlasse = LstSchuelerKlassen.FirstOrDefault();
            UFachLehrer = LstUnterrichtsfachLehrer.FirstOrDefault();
            if (uf != null)
            {
               UFachLehrer ufl = uf.UFaecherLehrer.Where(x => x.Active == true).FirstOrDefault();
               if (ufl != null)
                  UFachLehrer = ufl;
            }

            Notenstufe = 1;
            Erhebungsdatum = DateTime.Now;


            
            
         }
      }


      public Klasse CurrentKlasse { get; set; } = null;

      //Lists
      public List<SchuelerKlasse> LstSchuelerKlassen
      {
         get
         {

            List<SchuelerKlasse> re = DBZugriff.Current.Select<SchuelerKlasse>();
            if (CurrentKlasse == null)
               return re;
            else
               return re.Where(x => x.Klasse == CurrentKlasse).ToList();
         }
      }

      public List<Leistungsart> LstLeistungsarten
      {
         get
         {
            return DBZugriff.Current.Select<Leistungsart>();
         }
      }

      public List<UFachLehrer> LstUnterrichtsfachLehrer
      {
         get
         {
            return DBZugriff.Current.Select<UFachLehrer>();
         }
      }


      //Props
      private Leistung _leistung;
      public Leistung CLeistung
      {
         get
         {
            return _leistung;
         }

         set
         {
            _leistung = value;
            OnPropertyChanged();
            OnPropertyChanged("SchuelerKlasse");
            OnPropertyChanged("Leistungsart");
            OnPropertyChanged("UFachLehrer");
            OnPropertyChanged("Notenstufe");
            OnPropertyChanged("Erhebungsdatum");
            SaveAbleChanged();
         }
      }

      public SchuelerKlasse SchuelerKlasse
      {
         get
         {
            return CLeistung?.SchuelerKlasse;
         }
         set
         {
            CLeistung.SchuelerKlasse = value;
            OnPropertyChanged();
            SaveAbleChanged();
         }
      }

      public Leistungsart Leistungsart
      {
         get
         {
            return CLeistung?.Leistungsart;
         }
         set
         {
            CLeistung.Leistungsart = value;
            OnPropertyChanged();
            SaveAbleChanged();
         }
      }

      public UFachLehrer UFachLehrer
      {
         get
         {
            return CLeistung?.UFachLehrer;
         }
         set
         {
            CLeistung.UFachLehrer = value;
            OnPropertyChanged();
            SaveAbleChanged();
         }
      }

      public List<int> Notenstufen
      {
         get
         {
            return new List<int>()
            {
               1,2,3,4,5,6
            };
         }
      }
      public int Notenstufe
      {
         get
         {
            if (CLeistung == null)
               return 0;
            return CLeistung.Notenstufe;
         }
         set
         {
            if (value < 1 || value > 6)
               throw new ArgumentOutOfRangeException();

            CLeistung.Notenstufe = value;
            OnPropertyChanged();
            SaveAbleChanged();
         }
      }

      public DateTime Erhebungsdatum
      {
         get
         {
            if (CLeistung == null)
               return DateTime.MinValue;
            return CLeistung.Erhebungsdatum;
         }
         set
         {
            CLeistung.Erhebungsdatum = value;
            OnPropertyChanged();
            SaveAbleChanged();
         }
      }


      private void SaveAbleChanged()
      {
         OnPropertyChanged("SaveAble");
      }

      public bool SaveAble
      {
         get
         {
            if (CLeistung == null)
               return false;

            return (CLeistung.Leistungsart != null &&
               CLeistung.SchuelerKlasse != null &&
               CLeistung.UFachLehrer != null &&
               CLeistung.Notenstufe >= 1 && CLeistung.Notenstufe <= 6 &&
               CLeistung.Erhebungsdatum != null);
         }
      }
   }
}
