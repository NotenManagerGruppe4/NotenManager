private void wbrMain_LoadCompleted(object sender, NavigationEventArgs e)
        {
            //mshtml.HTMLDocument doc = (mshtml.HTMLDocument)wbrMain.Document;
            //if (doc.all.item("Seitenname").value == "Dateiimport")
            //{
            //    HTMLAnchorElement liDateiimport = (HTMLAnchorElement)doc.all.item("liDateiimport");
            //    mshtml.HTMLAnchorEvents2_Event liDateiimportEvent = liDateiimport as mshtml.HTMLAnchorEvents2_Event;
            //    liDateiimportEvent.onclick += new mshtml.HTMLAnchorEvents2_onclickEventHandler(liDateiimportMenu);
            //}
        }
        private bool liDateiimportMenu(IHTMLEventObj pEvtObj)
        {
            //MessageBox.Show("Dateiimport");
            wbrMain.Navigate(@"C:\Users\krausy\Desktop\Gruppenprojekt\Projekt\Zweitfenster.html");
            return true;
        }

        private bool ClickEventHandler2(IHTMLEventObj pEvtObj)
        {
            MessageBox.Show("Link");
            wbrMain.Navigate(@"H:\BFI11a\Gruppenprojekt\Test\Test\Hauptfenster\Zweitfenster.html");
            return true;
        }
