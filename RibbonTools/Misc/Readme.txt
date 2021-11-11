Was aufgefallen ist in Verbindung mit dem Ribbon mit WinForms:

ApplicationModes in Verbindung mit Button, SplitButton, DropDownButton sind nur im ApplicationMenu (Left Side) zulässig.
Das wird im RibbonDesigner nicht berücksichtigt.
Im Original RibbonDesigner sind ein paar Bugs, die in den RibbonTools korrigiert wurden.

Wenn ApplicationModes auf Groups oder Tabs verwendet werden, dann müssen alle! Tabs und Groups eine
Definition von ApplicationModes enthalten.
Ansonsten wird bei Ändern des Ribbon.SetModes ein Beenden der App durchgeführt.
Hier vermute ich einen Bug in der Microsoft Com Implementierung.
Die UICC.exe stellt keinen Fehler fest.

Im RibbonDesigner wäre es einfacher, wenn in der View auch ein Command vergeben werden könnte
mit allen notwendigen Eingaben für Texte und Images. Oder auch ein Aufruf bei vorhandenem Command für die Eingabe von Text, Images
=> einfacher Wechsel zum Command möglich mit Rückkehr an die verlassene View

Copy Funktion für SizeDefinition (Large  nach Medium oder Small)

Microsoft Bug in UICC.exe mit SizeDefinition ButtonGroupsAndInputs, Beispiel aus Dokumentation funktioniert nicht.
Entfernen der ControlGroup mit Button10 akzeptiert UICC, aber der Button3 erscheint dann an falscher Stelle