Imports RibbonLib
Imports RibbonLib.Controls
Imports RibbonLib.Controls.Events
Imports RibbonLib.Interop

Namespace _02_ApplicationMenuButton
	Public Enum RibbonMarkupCommands As UInteger
		 cmdApplicationMenu = 1000
		 cmdButtonNew = 1001
		 cmdButtonOpen = 1002
		 cmdButtonSave = 1003
		 cmdButtonExit = 1004
	End Enum

	Partial Public Class Form1
		Inherits Form
		Private _applicationMenu As RibbonApplicationMenu
		Private _buttonNew As RibbonButton
		Private _buttonOpen As RibbonButton
		Private _buttonSave As RibbonButton
		Private _buttonExit As RibbonButton

		Public Sub New()
			InitializeComponent()

			_applicationMenu = New RibbonApplicationMenu(_ribbon, CUInt(RibbonMarkupCommands.cmdApplicationMenu))
			_buttonNew = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonNew))
			_buttonOpen = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonOpen))
			_buttonSave = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonSave))
			_buttonExit = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonExit))

			_applicationMenu.TooltipTitle = "Menu"
			_applicationMenu.TooltipDescription = "Application main menu"

			AddHandler _buttonNew.ExecuteEvent, AddressOf _buttonNew_ExecuteEvent
		End Sub

		Private Sub _buttonNew_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			MessageBox.Show("new button pressed")
		End Sub
	End Class
End Namespace
