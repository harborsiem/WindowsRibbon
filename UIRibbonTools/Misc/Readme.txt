Was aufgefallen ist in Verbindung mit dem Ribbon mit WinForms:

ApplicationModes in Verbindung mit Button, SplitButton, DropDownButton sind nur im ApplicationMenu (Left Side) zulässig.
Das wird im RibbonDesigner nicht berücksichtigt.

Wenn ApplicationModes auf Groups oder Tabs verwendet werden, dann müssen alle! Tabs und Groups eine
Definition von ApplicationModes enthalten.
Ansonsten wird bei Ändern des Ribbon.SetModes ein Beenden der App durchgeführt.
Hier vermute ich einen Bug in der Microsoft Com Implementierung.
Die UICC.exe stellt keinen Fehler fest.

Im RibbonDesigner wäre es einfacher, wenn in der View auch ein Command vergeben werden könnte
mit allen notwendigen Eingaben für Texte und Images. Oder auch ein Aufruf bei vorhandenem Command für die Eingabe von Text, Images

Copy Funktion für SizeDefinition (Large  nach Medium oder Small)