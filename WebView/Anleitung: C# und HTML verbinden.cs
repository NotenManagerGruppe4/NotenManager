//Vor dem Verwenden von mshtml:
//Im Projektmappenexplorer: Verweise -> Verweis hinzufügen -> COM -> Microsoft HTML Object Library.
using mshtml

//WICHTIG: Hier wird nicht geprüft ob unser Webbrowser (wbrMain) nicht NULL ist.

private void wbrMain_LoadCompleted(object sender, NavigationEventArgs e)
{
    //HTMLDocument "doc" erstellen. Dies ist die aktuell geladene seite des Webbrowsers (hier wbrMain).
    mshtml.HTMLDocument doc = (mshtml.HTMLDocument)wbrMain.Document;
    
    //Nachfolgend: Button Events (HTML submit & button knöpfe)
    //Nun wird ein Button aus dem HTML code ausgelesen. Dieser hat die id & den Namen "testknopf".
    HTMLButtonElement myButton = (HTMLButtonElement)doc.all.item("testknopf");

    //Speichern des Textes aus dem Testknopf im string test.
    string test = myButton.value;

    //Erstellen eines Events mit mshtml.
    mshtml.HTMLButtonElementEvents_Event iEvent = myButton as mshtml.HTMLButtonElementEvents_Event;

    //Verbinden des Events mit einer Methode, die bei auslösen aufgerufen wird.
    iEvent.onclick += new mshtml.HTMLButtonElementEvents_onclickEventHandler(ClickEventHandler);

    //Nachfolgend: Anchor Events (HTML links)
    //Der Link wird aus dem HTML code ausgelesen. Dieser hat die id & den Namen "Link2".
    HTMLAnchorElement myLink = (HTMLAnchorElement)doc.all.item("Link2");
    
    //Erstellen eines Events mit mshtml.
    mshtml.HTMLAnchorEvents2_Event linkEvent = myLink as mshtml.HTMLAnchorEvents2_Event;
    
    //Verbinden des Events mit einer Methode, die bei auslösen aufgerufen wird.
    linkEvent.onclick += new mshtml.HTMLAnchorEvents2_onclickEventHandler(ClickEventHandler2);
}

//Methode die bei Knopfdruck aufgerufen wird.
private bool ClickEventHandler()
{
    //Die Methode leitet den Webbrowser (wbrMain) auf eine andere Seite weiter.
    wbrMain.Navigate(@"H:\BFI11a\Gruppenprojekt\Test\Test\Hauptfenster\Zweitfenster.html");
    return true;
}

//Methode die bei Linkklick aufgerufen wird.
private bool ClickEventHandler2(IHTMLEventObj pEvtObj)
{
    //Die Methode leitet den Webbrowser (wbrMain) auf eine andere Seite weiter.
    wbrMain.Navigate(@"H:\BFI11a\Gruppenprojekt\Test\Test\Hauptfenster\Zweitfenster.html");
    return true;
}
