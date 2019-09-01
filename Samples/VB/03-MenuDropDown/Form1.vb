Imports RibbonLib
Imports RibbonLib.Controls
Imports RibbonLib.Controls.Events
Imports RibbonLib.Interop

Namespace _03_MenuDropDown
	Public Enum RibbonMarkupCommands As UInteger
		 cmdButtonNew = 1001
		 cmdButtonOpen = 1002
		 cmdButtonSave = 1003
		 cmdButtonExit = 1004
		 cmdMenuGroupFile = 1005
		 cmdMenuGroupExit = 1006
		 cmdDropDownButton = 1007
		 cmdButtonDropA = 1008
		 cmdButtonDropB = 1009
		 cmdButtonDropC = 1010
	End Enum

	Partial Public Class Form1
		Inherits Form
		Private _buttonDropB As RibbonButton

		Public Sub New()
			InitializeComponent()

			_buttonDropB = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDropB))

			AddHandler _buttonDropB.ExecuteEvent, AddressOf _buttonDropB_ExecuteEvent
		End Sub

		Private Sub _buttonDropB_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			MessageBox.Show("drop B button pressed")
		End Sub
	End Class
End Namespace
