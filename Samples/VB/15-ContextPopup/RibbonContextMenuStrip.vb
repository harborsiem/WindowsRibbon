Imports System.Text
Imports System.ComponentModel
Imports RibbonLib

Namespace _15_ContextPopup
	Public Class RibbonContextMenuStrip
		Inherits ContextMenuStrip
		Private _contextPopupID As UInteger
		Private _ribbon As Ribbon

		Public Sub New(ByVal ribbon_Renamed As Ribbon, ByVal contextPopupID As UInteger)
			MyBase.New()
			_contextPopupID = contextPopupID
			_ribbon = ribbon_Renamed
		End Sub

		Protected Overrides Sub OnOpening(ByVal e As CancelEventArgs)
			_ribbon.ShowContextPopup(_contextPopupID, Cursor.Position.X, Cursor.Position.Y)
			e.Cancel = True
		End Sub
	End Class
End Namespace
