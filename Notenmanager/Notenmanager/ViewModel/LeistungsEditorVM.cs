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

            try
            {
               CloseAfterSaveRequesting?.Invoke(this, new EventArgs());
            }
            catch (Exception e)
            {
               Trace.WriteLine("[LeiEdit] InvokeFailed: " + e.ToString());
            }
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
            if(Mode == DialogMode.Aendern)
               DBZugriff.Current.Reload(CLeistung);
         }
         catch (Exception e)
         {
            Trace.WriteLine("[LeiEdit] Beenden: " + e.ToString());
         }
      }

      //Config
      public DialogMode Mode { get; private set; }
      public void SetMode(CellEditorTag tag)
      {
         CLeistung = tag.Leistung;
         Mode = tag.Mode;
         
         if (Mode == DialogMode.Neu)
         {
            CLeistung = new Leistung();

            if (tag.Leistungsart != null)
               Leistungsart = tag.Leistungsart;
            else
               Leistungsart = LstLeistungsarten.FirstOrDefault();

            SchuelerKlasse = LstSchuelerKlassen.FirstOrDefault();
            UFachLehrer = LstUnterrichtsfachLehrer.FirstOrDefault();

            if (tag.Unterrichtsfach != null)
            {
               UFachLehrer ufl = DBZugriff.Current.SelectFirstOrDefault<UFachLehrer>(x => x.Unterrichtsfach == tag.Unterrichtsfach);
               if (ufl != null)
                  UFachLehrer = ufl;
            }

            Notenstufe = 1;
            Erhebungsdatum = DateTime.Now;

            //Updaten
            OnPropertyChanged(nameof(LstUnterrichtsfachLehrer));
            OnPropertyChanged(nameof(LstSchuelerKlassen));
         }
            
      }

      //Lists
      public List<SchuelerKlasse> LstSchuelerKlassen
      {
         get
         {

            List<SchuelerKlasse> re = DBZugriff.Current.Select<SchuelerKlasse>();
            if (CLeistung?.SchuelerKlasse == null)
               return re;
            else
               return re.Where(x => x.Klasse == CLeistung.SchuelerKlasse.Klasse).ToList();
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
            List<UFachLehrer> lstuf = DBZugriff.Current.Select<UFachLehrer>();

            if (CLeistung?.UFachLehrer?.Unterrichtsfach?.Zeugnisfach == null)
               return lstuf;

            return lstuf.Where(x => x.Unterrichtsfach.Zeugnisfach == CLeistung.UFachLehrer.Unterrichtsfach.Zeugnisfach).ToList();
         }
      }


      #region Properties
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
            OnPropertyChanged(nameof(SchuelerKlasse));
            OnPropertyChanged(nameof(Leistungsart));
            OnPropertyChanged(nameof(UFachLehrer));
            OnPropertyChanged(nameof(Notenstufe));
            OnPropertyChanged(nameof(Erhebungsdatum));
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
         OnPropertyChanged(nameof(SaveAble));
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

#endregion Properties
   }
}
