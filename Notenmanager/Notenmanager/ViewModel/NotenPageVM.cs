﻿using Notenmanager.Model;
using Notenmanager.ViewModel.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Notenmanager.ViewModel
{
   public enum Periode { Gesamt, Halbjahr1, Halbjahr2 };


   public class NotenPageVM : BaseViewModel
   {
      #region Properties

      private Schule _currentSchule;
      private Klasse _currentKlasse;
      private Schueler _currentSchueler;

      private bool _alleSchueler = false;

      public List<Schule> LstSchulen
      {
         get
         {
            return DBZugriff.Current.Select<Schule>();
         }
      }
      public Schule CurrentSchule
      {
         get
         {
            if (_currentSchule == null && LstSchulen.Count > 0)
               _currentSchule = LstSchulen[0];
            return _currentSchule;
         }

         set
         {
            bool changed = (_currentSchule != value);
            _currentSchule = value;
            OnPropertyChanged();

            if (changed)
            {
               //Updaten
               ResetKlasse();
            }
         }
      }
      public List<Klasse> LstKlassen
      {
         get
         {
            return DBZugriff.Current.Select<Klasse>(x => x.Schule == CurrentSchule && x.SJ == Tool.CURRENTSJ);
         }
      }
      public Klasse CurrentKlasse
      {
         get
         {
            if (_currentKlasse == null && LstKlassen.Count > 0)
               CurrentKlasse = LstKlassen[0];
            return _currentKlasse;
         }

         set
         {
            bool changed = (_currentKlasse != value);
            _currentKlasse = value;
            OnPropertyChanged();

            if (changed)
            {
               //Updaten
               ResetSchueler();
            }
         }
      }

      public void ResetKlasse()
      {
         OnPropertyChanged(nameof(LstKlassen));
         if (LstKlassen.Count > 0)
            CurrentKlasse = LstKlassen[0];
         else
            CurrentKlasse = null;
      }

      public List<Schueler> LstSchueler
      {
         get
         {
            List<Schueler> re = new List<Schueler>();

            foreach (SchuelerKlasse sk in CurrentKlasse?.SchuelerKlassen?.Where(x => x.Active == true).ToList() ?? new List<SchuelerKlasse>())
               re.Add(sk.Schueler);


            return re.OrderBy(x => x.Nachname).ThenBy(x => x.Vorname).ToList();
         }
      }

      public Schueler CurrentSchueler
      {
         get
         {
            if (_currentSchueler == null && LstSchueler.Count > 0)
               CurrentSchueler = LstSchueler[0];
            return _currentSchueler;
         }
         set
         {
            bool changed = (_currentSchueler != value);
            _currentSchueler = value;
            OnPropertyChanged();

            if (changed)
               CurrentSelectionChanged?.Invoke(this, new EventArgs());
         }
      }

      private void ResetSchueler()
      {
         OnPropertyChanged(nameof(LstSchueler));
         if (LstSchueler.Count > 0)
            CurrentSchueler = LstSchueler[0];
         else
            CurrentSchueler = null;
      }

      public bool AlleSchueler
      {
         get
         {
            return _alleSchueler;
         }
         set
         {
            bool changed = (_alleSchueler != value);
            _alleSchueler = value;

            OnPropertyChanged();
            OnPropertyChanged(nameof(NurEinSchueler));

            if(changed)
               CurrentSelectionChanged?.Invoke(this, new EventArgs());
         }
      }
      public bool NurEinSchueler { get => !AlleSchueler; }




      public List<Lehrer> LstLehrer
      {
         get
         {
            return DBZugriff.Current.Select<Lehrer>();
         }
      }

      public List<Periode> LstPerioden
      {
         get
         {
            return Enum.GetValues(typeof(Periode)).OfType<Periode>().ToList();
         }
      }
      private Periode _currentPeriode = Periode.Gesamt;
      public Periode CurrentPeriode
      {
         get
         {
            return _currentPeriode;
         }
         set
         {
            bool changed = (_currentPeriode != value);
            _currentPeriode = value;
            OnPropertyChanged();

            if (changed)
               CurrentSelectionChanged?.Invoke(this, new EventArgs());
         }
      }

      #endregion Properties

      #region Commands
      //public ICommand SchuelerZuweisenClickCmd { get; set; }
      public ICommand ZeugnisFaecherClickCmd { get; set; }
      public ICommand MassenErfassungClickCmd { get; set; }
      public ICommand SaveClickCmd { get; set; }
      public ICommand BeendenClickCmd { get; set; }
      public ICommand BeendenCmd { get; set; }

      #endregion Commands

      public event EventHandler CurrentSelectionChanged;
      public event EventHandler RunMassenerfassung;

      public NotenPageVM()
      {
         //SchuelerZuweisenClickCmd = new ActionCommand(OnSchuelerZuweisenClick);
         ZeugnisFaecherClickCmd = new ActionCommand(OnZeugnisFaecherClick);
         MassenErfassungClickCmd = new ActionCommand(OnMassenErfassungClick);
         SaveClickCmd = new ActionCommand(OnSaveClick);
         BeendenClickCmd = new ActionCommand(OnBeendenClick);
         BeendenCmd = new ActionCommand(OnBeenden);
      }



      #region UIMethods
      //private void OnSchuelerZuweisenClick(object obj)
      //{
      //}

      private void OnZeugnisFaecherClick(object obj)
      {
         Navigator.Instance.NavigateTo("FaecherVerwaltenPage");
      }

      private void OnMassenErfassungClick(object obj)
      {
         RunMassenerfassung?.Invoke(this, new EventArgs());
      }

      private void OnSaveClick(object obj)
      {
         try
         {
            CurrentKlasse.Speichern();
         }
         catch (Exception e)
         {
            Trace.WriteLine("[NotenPage] Speichern: " + e.ToString());
         }
      }

      private void OnBeendenClick(object obj)
      {
         Navigator.Instance.NavigateTo("MainPage");
      }

      private void OnBeenden(object obj)
      {
         try
         {
            DBZugriff.Current.Reload(CurrentKlasse);
         }
         catch (Exception e)
         {
            Trace.WriteLine("[NotenPage] Reload: " + e.ToString());
         }
      }

      #endregion UIMethods

      #region Notenbau

      public bool CheckIfIsInCurrentPeriod(Leistung l)
      {
         if (l.Erhebungsdatum == null)
         {
            Trace.WriteLine("[CheckIfIsInCu...] WARN: Erhebungsdatum null!");
            return false;
         }

         if (Tool.GetSJ(l.Erhebungsdatum) != Tool.CURRENTSJ) //Nicht im SJ
            return false;

         if (CurrentPeriode == Periode.Gesamt)
            return true;

         if (CurrentPeriode == Periode.Halbjahr1) //1. HJ
            return l.Erhebungsdatum <= Tool.HALBJAHRESDATUM;
         else //2. HJ
            return l.Erhebungsdatum > Tool.HALBJAHRESDATUM;


      }

      //Baut die Spalten des Notengrids
      public List<GridColumHelperClass> BuildGridColumns()
      {

         //Endausgabe 
         List<GridColumHelperClass> lstlaanz = new List<GridColumHelperClass>();


         //alle Kombinationen von UFach, LA, anz
         List<Unterrichtsfach> lstcufs = DBZugriff.Current.Select<Unterrichtsfach>(x => x.Zeugnisfach.Klasse == CurrentKlasse);
         List<Tuple<Schueler,Unterrichtsfach, Leistungsart, int>> lstufleicount = new List<Tuple<Schueler,Unterrichtsfach, Leistungsart, int>>();
         foreach (Leistungsart la in DBZugriff.Current.Select<Leistungsart>())
         {
            lstlaanz.Add(new GridColumHelperClass()
            {
               Leistungsart = la,
            });
            foreach (Unterrichtsfach uf in lstcufs)
               foreach(Schueler s in LstSchueler)
                  lstufleicount.Add(new Tuple<Schueler,Unterrichtsfach, Leistungsart, int>(s, uf, la, 0));
         }

         //Hauptaufbau
         foreach (Leistung l in DBZugriff.Current.Select<Leistung>(x => x.SchuelerKlasse.Klasse == CurrentKlasse && CheckIfIsInCurrentPeriod(x) && (AlleSchueler==true ? true : x.SchuelerKlasse.Schueler == CurrentSchueler)))
         {
            Tuple<Schueler,Unterrichtsfach, Leistungsart, int> tmp = lstufleicount.Find(x => x.Item1 == l.SchuelerKlasse.Schueler && x.Item2 == l.UFachLehrer.Unterrichtsfach && x.Item3 == l.Leistungsart);
            if (tmp == null)
               continue;

            int i = lstufleicount.IndexOf(tmp);
            lstufleicount[i] = new Tuple<Schueler,Unterrichtsfach, Leistungsart, int>(tmp.Item1 ,tmp.Item2, tmp.Item3, tmp.Item4 + 1);
         }

         //Extrahieren und vergleichen
         foreach (Tuple<Schueler,Unterrichtsfach, Leistungsart, int> t in lstufleicount)
         {
            GridColumHelperClass tmp = lstlaanz.Find(x => x.Leistungsart == t.Item3);
            if (tmp == null)
               continue;

            //Vergleichen und ggf vergrößeren
            if (t.Item4 > tmp.Anz)
               tmp.Anz = t.Item4;
         }

         lstlaanz.ForEach(x => x.Anz += 1);

         return lstlaanz.OrderByDescending(x => x.Leistungsart.Gruppe.Gewicht).ThenBy(x => x.Leistungsart.Gruppe.Id).ThenByDescending(x => x.Leistungsart.Gewichtung).ToList();

      }


      public List<Zeugnisfach> GetZFs()
      {
         return DBZugriff.Current.Select<Zeugnisfach>(x => x.Klasse == CurrentKlasse);
      }

      public List<Schueler> GetSchueler()
      {
         if (AlleSchueler)
            return LstSchueler;
         else if (CurrentSchueler != null)
            return new List<Schueler>() { CurrentSchueler };
         else
            return new List<Schueler>();
      }


      public int CalcNote(List<Leistung> leistungen)
      {
         return (int)Math.Round(CalcNoteDouble(leistungen), 0);
      }

      public double CalcNoteDouble(List<Leistung> leistungen)
      {
         double summe = 0;
         double teiler = 0;
         foreach (Leistung l in leistungen)
         {
            summe += l.Notenstufe * l.Leistungsart.Gewichtung;
            teiler += l.Leistungsart.Gewichtung;
         }
         if (teiler == 0 || teiler == 0)
            return 0;
         return summe / teiler;
      }

      public double CalcNoteDoubleZF(List<Tuple<Unterrichtsfach, double>> ufNoten)
      {
         double sum = 0, teiler = 0;
         foreach (Tuple<Unterrichtsfach, double> t in ufNoten)
         {
            sum += t.Item2 * t.Item1.Stunden;
            teiler += t.Item1.Stunden;
         }
         if (sum == 0 || teiler == 0)
            return 0;
         return sum / teiler;
      }

      public double CalcNoteDoubleSchueler(List<int> zfNoten)
      {
         double sum = 0, teiler = 0;
         foreach (int n in zfNoten)
         {
            sum += n;
            teiler++;
         }
         if (sum == 0 || teiler == 0)
            return 0;

         return sum / teiler;
      }

      public List<Leistung> GetNoten(Schueler s)
      {
         return DBZugriff.Current.Select<Leistung>().Where(x => x.SchuelerKlasse.Klasse == CurrentKlasse && x.SchuelerKlasse.Schueler == s && CheckIfIsInCurrentPeriod(x)).ToList();
      }

      #endregion Notenbau


      public class GridColumHelperClass
      {
         public Leistungsart Leistungsart { get; set; }
         public int Anz { get; set; } = 0;
      }
   }

}
