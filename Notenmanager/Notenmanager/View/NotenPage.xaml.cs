using Notenmanager.Model;
using Notenmanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notenmanager.View
{
   /// <summary>
   /// Interaktionslogik für NotenPage.xaml
   /// </summary>
   public partial class NotenPage : Page
   {
      private const int HEADER_ROWS = 3;
      private const int DESC_COLUMNS = 3;

      private List<ColumnSpecification> lstcspec = new List<ColumnSpecification>();

      private NotenPageVM _vm;
      public NotenPage()
      {
         InitializeComponent();

         _vm = FindResource("NotenPageVM") as NotenPageVM;
         if (_vm == null)
            throw new Exception("Konnte VM nicht finden!");

         _vm.CurrentSelectionChanged += (s, e) =>
         {
            UpdateNotenGrid();
         };
         _vm.RunMassenerfassung += (s, e) =>
         {
            new LeistungMassenerfassungWindow().ShowDialog();
            UpdateNotenGrid();
         };
         this.Loaded += (s, e) =>
         {
            UpdateNotenGrid();
         };
         this.Unloaded += (s, e) =>
         {
            if (_vm.BeendenCmd.CanExecute(null))
               _vm.BeendenCmd.Execute(null);
         };
      }

      //Eventuell irgendwann async?
      private void UpdateNotenGrid()
      {
         //DateTime start = DateTime.Now;
         gNoten.Children.Clear();

         BuildHeader();
         BuildContent();

         //Trace.WriteLine("GUIBauzeit: "+(DateTime.Now - start).Milliseconds + "ms");

         // *** DURCHSCHNITTLICHE GUIBauzeit(2 Schüler, ~10 Noten): 22ms *** 
      }

      /// <summary>
      /// Baut die Spaltenköpfe des Notengrids und befüllt "ColumSpecifiation"-Liste
      /// </summary>
      private void BuildHeader()
      {
         //Rücksetzten
         gNoten.ColumnDefinitions.Clear();
         gNoten.RowDefinitions.Clear();
         lstcspec.Clear();



         int currentcolumn = 0;

         //ersten Zeilendefinitionen für die Kopfzeile
         for (int i = 0; i < HEADER_ROWS; i++)
            gNoten.RowDefinitions.Add(new RowDefinition()
            {
               Height = new GridLength(1, GridUnitType.Auto)
            });

         //Einrückungen für die Basis links oben
         for (int i = 0; i < DESC_COLUMNS; i++)
            gNoten.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(10, GridUnitType.Auto), MinWidth = 10 });
         AddTextBlock("", 0, 0, HEADER_ROWS, DESC_COLUMNS);
         currentcolumn += DESC_COLUMNS;

         int lastlacolumn = currentcolumn;
         int lastlgcolumn = currentcolumn;
         Leistungsgruppe lastlg = null;

         foreach (NotenPageVM.GridColumHelperClass gchc in _vm.BuildGridColumns())
         {
            lastlacolumn = currentcolumn;
            //Leistungsgruppe-Textblock
            if (lastlg != gchc.Leistungsart.Gruppe)
            {
               if (lastlg != null)
                  AddTextBlock(lastlg.Bez + " (" + lastlg.Gewicht + ")", 0, lastlgcolumn, 1, currentcolumn - lastlgcolumn);
               lastlg = gchc.Leistungsart.Gruppe;
               lastlgcolumn = currentcolumn;
            }
            //Anzahl an max. Leistungen - Spalten + Textblock
            for (int i = 0; i < gchc.Anz; i++)
            {
               AddDefaultColumnDef();
               lstcspec.Add(new ColumnSpecification()
               {
                  ColumnBez = i + 1,
                  ColumnIndex = currentcolumn,
                  Lg = lastlg,
                  La = gchc.Leistungsart,
               });
               AddTextBlock(i + 1, HEADER_ROWS - 1, currentcolumn);
               currentcolumn++;
            }
            //Gesamt-Leistungsart-Spalte + Textblock
            AddDefaultColumnDef();
            AddTextBlock("G", HEADER_ROWS - 1, currentcolumn);
            lstcspec.Add(new ColumnSpecification()
            {
               ColumnBez = null,
               ColumnIndex = currentcolumn,
               Lg = lastlg,
               La = gchc.Leistungsart,
            });
            currentcolumn++;

            //Leistungsart-Textblock
            AddTextBlock(gchc.Leistungsart.Bez + " (" + gchc.Leistungsart.Gewichtung + ")", 1, lastlacolumn, 1, currentcolumn - lastlacolumn);
         }
         //Abschließender Leistungsgruppe-Textblock
         if (lastlg != null)
            AddTextBlock(lastlg.Bez + " (" + lastlg.Gewicht + ")", 0, lastlgcolumn, 1, currentcolumn - lastlgcolumn);

         //Endgültige Gesamt-Spalte
         AddDefaultColumnDef();
         AddTextBlock("G", 0, currentcolumn, HEADER_ROWS);
         currentcolumn++;
      }
      private void AddDefaultColumnDef()
      {
         gNoten.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(10, GridUnitType.Auto) });
      }

      /// <summary>
      /// Baut den Inhalt des Notengrids auf
      /// WARNUNG: Methode ist nicht optimiert! Bei vielen Einträgen kann es eventl zu UI-Verzögerungen kommen, da nur der UI-Thread genutzt wird
      /// </summary>
      private void BuildContent()
      {
         List<Zeugnisfach> lstzfs = _vm.GetZFs();

         int currentrow = HEADER_ROWS - 1;
         foreach (Schueler s in _vm.GetSchueler())
         {
            gNoten.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            currentrow++;
            AddTextBlock(s.Nachname + " " + s.Vorname, currentrow, 0, 1, DESC_COLUMNS);


            int currentrowschueler = currentrow;
            List<Leistung> schuelerleistungen = _vm.GetNoten(s);
            List<int> zfNoten = new List<int>();

            foreach (Zeugnisfach zf in lstzfs)
            {
               gNoten.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
               currentrow++;
               AddTextBlock(zf.Bez, currentrow, 1, 1, DESC_COLUMNS - 1);
               int currentrowzf = currentrow;
               List<Tuple<Unterrichtsfach, double>> ufNoten = new List<Tuple<Unterrichtsfach, double>>();

               foreach (Unterrichtsfach uf in zf.Unterrichtsfaecher.Where(x => x.Active == true).OrderBy(x => x.Bez).ToList())
               {
                  gNoten.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
                  currentrow++;
                  AddTextBlock(uf.Bez, currentrow, 2, 1, DESC_COLUMNS - 2);

                  List<Leistung> lstleistungenuf = new List<Leistung>();

                  foreach (ColumnSpecification cs in lstcspec.Where(x => x.ColumnBez != null)
                     .OrderBy(x => x.ColumnIndex)
                     .ToList())
                  {
                     //Einzelne Noten
                     Leistung l = schuelerleistungen.FindAll(x => x.Leistungsart == cs.La && x.UFachLehrer.Unterrichtsfach == uf).
                        OrderBy(x => x.Erhebungsdatum).ToList().ElementAtOrDefault((int)cs.ColumnBez - 1);
                     if (l == null) //Neu anlegbar
                     {
                        AddNotenTextBox(null, currentrow, cs.ColumnIndex, false, new CellEditorTag()
                        {
                           Mode = DialogMode.Neu,
                           Leistungsart = cs.La,
                           Unterrichtsfach = uf,
                           SchuelerKlasse = s.SchuelerKlassen.Where(x => x.Active == true && x.Klasse == _vm.CurrentKlasse).FirstOrDefault(),
                        }, true);
                        continue;
                     }

                     AddNotenTextBox(l, currentrow, cs.ColumnIndex, false, new CellEditorTag()
                     {
                        Mode = DialogMode.Aendern,
                        Leistung = l,
                        SchuelerKlasse = s.SchuelerKlassen.Where(x => x.Active == true && x.Klasse == _vm.CurrentKlasse).FirstOrDefault(),
                     }, true);


                     lstleistungenuf.Add(l);
                  }
                  //TeilNoten Leistungsarten
                  foreach (ColumnSpecification cs in lstcspec.Where(x => x.ColumnBez == null))
                  {
                     List<Leistung> lstlei = schuelerleistungen.FindAll(x => x.Leistungsart == cs.La && x.UFachLehrer.Unterrichtsfach == uf).ToList();
                     if (lstlei.Count == 0)
                        continue;
                     AddNotenTextBox(_vm.CalcNote(lstlei), currentrow, cs.ColumnIndex, true);
                  }

                  //GesamtNote UF
                  double ufnote = _vm.CalcNoteDouble(lstleistungenuf);
                  AddNotenTextBox(ufnote, currentrow, gNoten.ColumnDefinitions.Count - 1, true);

                  if (ufnote != 0)
                     ufNoten.Add(new Tuple<Unterrichtsfach, double>(uf, ufnote));
               }

               //Gesamtnote ZF
               double zfgesnote = _vm.CalcNoteDoubleZF(ufNoten);
               AddNotenTextBox(zfgesnote, currentrowzf, gNoten.ColumnDefinitions.Count - 1, true);

               if (zfgesnote != 0)
                  zfNoten.Add((int)Math.Round(zfgesnote));
            }

            //GesamtNote Schüler
            AddNotenTextBox(_vm.CalcNoteDoubleSchueler(zfNoten), currentrowschueler, gNoten.ColumnDefinitions.Count - 1, true);
         }
      }


      public const int MARGIN = 3;

      private void AddTextBlock(object text, int row, int column, int rowspan = 1, int columnspan = 1)
      {
         TextBlock tb = new TextBlock();
         tb.Text = text.ToString();
         tb.TextAlignment = TextAlignment.Center;
         tb.VerticalAlignment = VerticalAlignment.Center;
         tb.Margin = new Thickness(MARGIN);


         Border obj = new Border();
         obj.BorderThickness = new Thickness(1);
         obj.BorderBrush = Brushes.Black;
         obj.Child = tb;

         gNoten.Children.Add(obj);

         Grid.SetRow(obj, row);
         Grid.SetColumn(obj, column);
         Grid.SetRowSpan(obj, rowspan);
         Grid.SetColumnSpan(obj, columnspan);
      }

      private void AddNotenTextBox(Leistung l, int row, int column, bool italic = false, CellEditorTag tag = null, bool useeditor = false)
      {
         TextBox tb = new TextBox();
         if (l == null)
            tb.Text = "";
         else
         {
            tb.Text = l.Notenstufe.ToString();
            tb.ToolTip = $"Erhoben am: {l.Erhebungsdatum} \r\n" +
               (l.LetzteÄnderung != null ? $"Letzte Änderung: {l.LetzteÄnderung} \r\n" : "");
         }
         tb.TextAlignment = TextAlignment.Center;
         tb.VerticalAlignment = VerticalAlignment.Center;
         tb.Margin = new Thickness(MARGIN);
         tb.IsReadOnly = true;

         if (tag != null)
         {
            tb.Tag = tag;
            if (useeditor)
               tb.MouseDoubleClick += (s, e) =>
               {
                  TextBox sender = s as TextBox;
                  if (sender == null)
                     return;

                  CellEditorTag ctag = sender.Tag as CellEditorTag;
                  if (ctag == null)
                     return;

                  if (new LeistungsEditor(ctag).ShowDialog() == true)
                     UpdateNotenGrid();
               };
         }

         if (italic)
            tb.FontStyle = FontStyles.Italic;

         gNoten.Children.Add(tb);

         Grid.SetRow(tb, row);
         Grid.SetColumn(tb, column);
      }

      private void AddNotenTextBox(double note, int row, int column, bool bold = false)
      {
         TextBox tb = new TextBox();
         tb.Text = note.ToString("#0.00");
         tb.TextAlignment = TextAlignment.Center;
         tb.VerticalAlignment = VerticalAlignment.Center;
         tb.Margin = new Thickness(1);
         tb.IsReadOnly = true;

         if (bold)
            tb.FontWeight = FontWeights.Bold;

         gNoten.Children.Add(tb);

         Grid.SetRow(tb, row);
         Grid.SetColumn(tb, column);
      }

      class ColumnSpecification
      {
         public int ColumnIndex { get; set; }

         public int? ColumnBez { get; set; } //1,2,3.. null=G

         public Leistungsart La { get; set; }

         public Leistungsgruppe Lg { get; set; }
      }

      private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
      {

      }
   }
}
