Imports RibbonLib
Imports RibbonLib.Controls
Imports RibbonLib.Controls.Events
Imports RibbonLib.Interop

Namespace _04_TabGroupHelp
	Public Enum RibbonMarkupCommands As UInteger
		 cmdButtonNew = 1001
		 cmdButtonOpen = 1002
		 cmdButtonSave = 1003
		 cmdButtonExit = 1004
		 cmdButtonDropA = 1008
		 cmdButtonDropB = 1009
		 cmdButtonDropC = 1010
		 cmdTabMain = 1011
		 cmdTabDrop = 1012
		 cmdGroupFileActions = 1013
		 cmdGroupExit = 1014
		 cmdGroupDrop = 1015
		 cmdHelpButton = 1016
	End Enum

	Partial Public Class Form1
		Inherits Form
		Private _exitButton As RibbonButton
		Private _helpButton As RibbonHelpButton

		Public Sub New()
			InitializeComponent()

			_exitButton = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonExit))
			_helpButton = New RibbonHelpButton(_ribbon, CUInt(RibbonMarkupCommands.cmdHelpButton))

			AddHandler _exitButton.ExecuteEvent, AddressOf _exitButton_ExecuteEvent
			AddHandler _helpButton.ExecuteEvent, AddressOf _helpButton_ExecuteEvent
		End Sub

		Private Sub _exitButton_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			' Close form asynchronously since we are in a ribbon event 
			' handler, so the ribbon is still in use, and calling Close 
			' will eventually call _ribbon.DestroyFramework(), which is 
			' a big no-no, if you still use the ribbon.
			Me.BeginInvoke(New MethodInvoker(AddressOf Me.Close))
		End Sub

		Private Sub _helpButton_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			MessageBox.Show("Help button pressed")
		End Sub
	End Class
End Namespace
