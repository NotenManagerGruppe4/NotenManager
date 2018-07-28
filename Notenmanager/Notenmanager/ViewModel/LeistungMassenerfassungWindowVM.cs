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
      #region Commands
      public ICommand SpeichernCMD { get; set; }
      public ICommand BeendenCMD { get; set; }
      #endregion Commands

      public LeistungMassenerfassungWindowVM()
      {
         BeendenCMD = new ActionCommand(Beenden);
         SpeichernCMD = new ActionCommand(Speichern);
      }

      public event EventHandler CloseAfterSaveRequesting;
      public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;


      private void Speichern(object obj)
      {
         try
         {
            NotenSaveAble();


            foreach (Leistung l in LstLeistungen)
            {
               l.LetzteÄnderung = DateTime.Now;
               DBZugriff.Current.Speichern(l, false);
            }
            DBZugriff.Current.Save();

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
            MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs(new Action<System.Windows.MessageBoxResult>((MessageBoxResult) => { }),

               "Fehler beim Speichern:" + e.Message,
               "Fehler",
               System.Windows.MessageBoxButton.OK,
               System.Windows.MessageBoxImage.Error));

            Trace.WriteLine("[LeiEdit] Speichern: " + e.ToString());
         }
      }

      private void Beenden(object obj)
      {
      }

      //Config
      bool init = true;
      public void Init(Klasse k)
      {
         //Set
         init = true;

         Erhebungsdatum = DateTime.Now;
         Klasse = k;

         init = false;

         //Build
         ReBuildLstLeistungenHard();

         //Refresh
         LstUnterrichtsfachLehrer = null;
      }

      //Nur wenn andere Klasse selektiert
      private void ReBuildLstLeistungenHard()
      {
         if (init)
            return;

         List<Leistung> lsttmp = new List<Leistung>();
         foreach (SchuelerKlasse sk in DBZugriff.Current.Select<SchuelerKlasse>(x => x.Klasse == Klasse && x.Klasse.SJ == Tool.CURRENTSJ).OrderBy(x => x.Schueler.Nachname).ThenBy(x => x.Schueler.Vorname).ToList())
         {
            lsttmp.Add(new Leistung()
            {
               Erhebungsdatum = Erhebungsdatum,
               Leistungsart = Leistungsart,
               UFachLehrer = UFachLehrer,
               LetzteÄnderung = DateTime.Now,
               SchuelerKlasse = sk,
               Notenstufe = 0,
            });
         }
         LstLeistungen = lsttmp;
      }

      //Nur wenn NICHT andere Klasse selektiert
      private void ReBuildLstLeistungenSoft()
      {
         if (init)
            return;

         foreach (Leistung l in LstLeistungen)
         {
            l.Erhebungsdatum = Erhebungsdatum;
            l.Leistungsart = Leistungsart;
            l.UFachLehrer = UFachLehrer;
            l.LetzteÄnderung = DateTime.Now;
         }
         OnPropertyChanged("LstLeistungen");
      }

      #region Properties
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

            return lstuf.Where(x => x.Unterrichtsfach.Zeugnisfach.Klasse == Klasse).ToList();
         }
         private set
         {
            OnPropertyChanged();
         }
      }



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
            ReBuildLstLeistungenHard();

            OnPropertyChanged();
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
            ReBuildLstLeistungenSoft();

            OnPropertyChanged();
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
            ReBuildLstLeistungenSoft();

            OnPropertyChanged();
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
            ReBuildLstLeistungenSoft();

            OnPropertyChanged();
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

      private List<Leistung> _lstLeistungen = new List<Leistung>();
      public List<Leistung> LstLeistungen
      {
         get
         {
            return _lstLeistungen;
         }
         set
         {
            _lstLeistungen = value;
            OnPropertyChanged();
         }
      }
      #endregion Properties

      //true = OK, false = Nicht OK
      private void NotenSaveAble()
      {
         try
         {
            foreach (Leistung l in LstLeistungen)
            {
               if (l.Notenstufe < 1 || l.Notenstufe > 6)
                  InternalThrowEx(l, "Notenstufe", l.Notenstufe);
               else
                  if (l.SchuelerKlasse?.Klasse != Klasse)
                  InternalThrowEx(l, "Klasse", l.SchuelerKlasse?.Klasse);
               else if (l.UFachLehrer != UFachLehrer)
                  InternalThrowEx(l, "UFachLehrer", l.UFachLehrer);
               else if (l.Leistungsart != Leistungsart)
                  InternalThrowEx(l, "Leistungsart", l.Leistungsart);
               else if (l.Erhebungsdatum != Erhebungsdatum)
                  InternalThrowEx(l, "Erhebungsdatum", l.Erhebungsdatum);

            }
         }
         catch (Exception e)
         {
            throw e;
         }
      }

      private void InternalThrowEx(Leistung l, string attr, object attrvalue)
      {
         throw new ArgumentException($"Fehler der Codierung von Leistung SchülerName['{l?.SchuelerKlasse?.Schueler?.Nachname} {l?.SchuelerKlasse?.Schueler?.Vorname}']: Fehlerhaftes Attribut '{attr}'='{attrvalue}'");
      }


   }
}
