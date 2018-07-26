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
   public class LeistungMassenerfassungWindowVM : BaseViewModel
   {
      public ICommand SpeichernCMD { get; set; }
      public ICommand BeendenCMD { get; set; }

      public LeistungMassenerfassungWindowVM()
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
            if (Mode == DialogMode.Aendern)
               DBZugriff.Current.Reload(CLeistung);
         }
         catch (Exception e)
         {
            Trace.WriteLine("[LeiEdit] Beenden: " + e.ToString());
         }
      }

      //Config
      public void Init(Klasse k, Leistungsart la, UFachLehrer ufl)
      {
         Erhebungsdatum = DateTime.Now;
         Klasse = k;
         Leistungsart = la;
         UFachLehrer = ufl;


         OnPropertyChanged("LstUnterrichtsfachLehrer");
         OnPropertyChanged("LstKlassen");


      }

      //Lists
      public List<Klasse> LstKlassen
      {
         get
         {
            return DBZugriff.Current.Select<Klasse>();
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


      //Props
      private Klasse _klasse;
      public Klasse Klasse
      {
         get
         {
            if (_klasse == null && LstKlassen.Count > 0)
               Klasse = LstKlassen[0];
            return _klasse;
         }
         set
         {
            _klasse = value;
            OnPropertyChanged();
            SaveAbleChanged();
         }
      }

      private Leistungsart _la;
      public Leistungsart Leistungsart
      {
         get
         {
            if (_la == null && LstLeistungsarten.Count > 0)
               Leistungsart = LstLeistungsarten[0];
            return _la;
         }
         set
         {
            _la = value;
            OnPropertyChanged();
            SaveAbleChanged();
         }
      }

      private UFachLehrer _uf;
      public UFachLehrer UFachLehrer
      {
         get
         {
            if (_uf == null && LstUnterrichtsfachLehrer.Count > 0)
               UFachLehrer = LstUnterrichtsfachLehrer[0];
            return _uf;
         }
         set
         {
            _uf = value;
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


      private DateTime _erhebungsdatum = DateTime.Now;
      public DateTime Erhebungsdatum
      {
         get
         {
            return _erhebungsdatum;
         }
         set
         {
            _erhebungsdatum = value;
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
