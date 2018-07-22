using Notenmanager.Model;
using Notenmanager.ViewModel.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notenmanager.ViewModel
{
   public class NotenPageVM : BaseViewModel
   {

      #region Properties

      private List<Schule> _lstSchulen;
      private Schule _currentSchule;
      private Klasse _currentKlasse;

      public List<Schule> LstSchulen
      {
         get
         {
            return _lstSchulen;
         }

         set
         {
            _lstSchulen = value;
            OnPropertyChanged();
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
            _currentSchule = value;
            OnPropertyChanged();
         }
      }
      public List<Klasse> LstKlassen
      {
         get
         {
            return DBZugriff.Current.Select<Klasse>(x => x.Schule == CurrentSchule && x.SJ == CURRENTSJ);
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
               CurrentKlasseChanged?.Invoke(this,new EventArgs());
         }
      }
      public List<Lehrer> LstLehrer
      {
         get
         {
            return DBZugriff.Current.Select<Lehrer>();
         }
      }

      #endregion Properties

      #region Commands
      public ICommand SchuelerZuweisenClickCmd { get; set; }
      public ICommand ZeugnisFaecherClickCmd { get; set; }
      public ICommand MassenErfassungClickCmd { get; set; }

      #endregion Commands

      public event EventHandler CurrentKlasseChanged; 
      public NotenPageVM()
      {
         LstSchulen = DBZugriff.Current.Select<Schule>();

         BuildDataGridColumns();

         SchuelerZuweisenClickCmd = new ActionCommand(OnSchuelerZuweisenClick);
         ZeugnisFaecherClickCmd = new ActionCommand(OnZeugnisFaecherClick);
      }



      #region UIMethods
      private void OnSchuelerZuweisenClick(object obj)
      {

      }

      private void OnZeugnisFaecherClick(object obj)
      {

      }

      private void OnMassenErfassungClick(object obj)
      {

      }



      #endregion UIMethods

      #region Noten
      public List<Tuple<Leistungsart, int>> BuildDataGridColumns()
      {

         //Endausgabe 
         List<Tuple<Leistungsart, int>> lstlaanz = new List<Tuple<Leistungsart, int>>();
         foreach (Leistungsart la in DBZugriff.Current.Select<Leistungsart>())
            lstlaanz.Add(new Tuple<Leistungsart, int>(la, 0));


         //alle Kombinationen von UFach, LA, anz
         List<Unterrichtsfach> lstcufs = DBZugriff.Current.Select<Unterrichtsfach>(x => x.Zeugnisfach.Klasse == CurrentKlasse);
         List<Tuple<Unterrichtsfach, Leistungsart, int>> lstufleicount = new List<Tuple<Unterrichtsfach, Leistungsart, int>>();
         foreach (Tuple<Leistungsart, int> tmp in lstlaanz)
         {
            foreach (Unterrichtsfach uf in lstcufs)
               lstufleicount.Add(new Tuple<Unterrichtsfach, Leistungsart, int>(uf, tmp.Item1, tmp.Item2));
         }

         //Hauptaufbau
         foreach (Leistung l in DBZugriff.Current.Select<Leistung>(x => x.SchuelerKlasse.Klasse == CurrentKlasse))
         {
            Tuple<Unterrichtsfach, Leistungsart, int> tmp = lstufleicount.Find(x => x.Item1 == l.UFachLehrer.Unterrichtsfach && x.Item2 == l.Leistungsart);
            if (tmp == null)
               continue;

            int i = lstufleicount.IndexOf(tmp);
            lstufleicount[i] = new Tuple<Unterrichtsfach, Leistungsart, int>(tmp.Item1, tmp.Item2, tmp.Item3 + 1);
         }

         //Extrahieren
         foreach (Tuple<Unterrichtsfach, Leistungsart, int> t in lstufleicount)
         {
            Tuple<Leistungsart, int> tmp = lstlaanz.Find(x => x.Item1 == t.Item2);
            if (tmp == null)
               continue;


            if (t.Item3 > tmp.Item2)
            {
               int i = lstlaanz.IndexOf(tmp);
               lstlaanz[i] = new Tuple<Leistungsart, int>(tmp.Item1, t.Item3);
            }
         }

         lstlaanz.RemoveAll(x => x.Item2 == 0);

         return lstlaanz.OrderByDescending(x => x.Item1.Gruppe.Gewicht).ThenBy(x => x.Item1.Gruppe.Id).ThenByDescending(x => x.Item1.Gewichtung).ToList();

      }




      public List<Zeugnisfach> GetZFs()
      {
         return DBZugriff.Current.Select<Zeugnisfach>(x => x.Klasse == CurrentKlasse);
      }

      public List<Schueler> GetSchueler()
      {
         List<Schueler> re = new List<Schueler>();

         foreach (SchuelerKlasse sk in CurrentKlasse.SchuelerKlassen.Where(x => x.Active == true).ToList())
            re.Add(sk.Schueler);


         return re.OrderBy(x => x.Nachname).ThenBy(x => x.Vorname).ToList();
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
         if (teiler == 0)
            return 0;
         return summe / teiler;
      }

      public double CalcNoteDoubleZF(List<Tuple<Unterrichtsfach,double>> ufNoten)
      {
         double sum = 0, teiler = 0;
         foreach (Tuple<Unterrichtsfach, double> t in ufNoten)
         {
            sum += t.Item2 * t.Item1.Stunden;
            teiler += t.Item1.Stunden;
         }
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
         return sum / teiler;
      }

      public List<Leistung> GetNoten(Schueler s)
      {
         return DBZugriff.Current.Select<Leistung>().Where(x => x.SchuelerKlasse.Klasse == CurrentKlasse && x.SchuelerKlasse.Schueler == s).ToList();
      }

      #endregion Noten

      //von 2017/18 --> 17
      public static readonly int CURRENTSJ = GetCurrentSJ();
      private static int GetCurrentSJ()
      {
         int re = DateTime.Now.Year;
         if (DateTime.Now.Month >= 9) //ab September
            return re;
         else
            return --re;
      }

      
   }
}
