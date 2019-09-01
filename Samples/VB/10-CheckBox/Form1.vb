Imports RibbonLib
Imports RibbonLib.Controls
Imports RibbonLib.Controls.Events
Imports RibbonLib.Interop

Namespace _10_CheckBox
	Public Enum RibbonMarkupCommands As UInteger
		 cmdButton = 1001
		 cmdToggleButton = 1002
		 cmdCheckBox = 1003
		 cmdDropDown = 1004
		 cmdTabMain = 1011
		 cmdGroupButtons = 1013
		 cmdGroupCheckBox = 1014
	End Enum

	Partial Public Class Form1
		Inherits Form
		Private _button As RibbonButton
		Private _toggleButton As RibbonToggleButton
		Private _checkBox As RibbonCheckBox

		Public Sub New()
			InitializeComponent()

			_button = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButton))
			_toggleButton = New RibbonToggleButton(_ribbon, CUInt(RibbonMarkupCommands.cmdToggleButton))
			_checkBox = New RibbonLib.Controls.RibbonCheckBox(_ribbon, CUInt(RibbonMarkupCommands.cmdCheckBox))

			AddHandler _button.ExecuteEvent, AddressOf _button_ExecuteEvent
		End Sub

		Private Sub _button_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			MessageBox.Show("checkbox check status is: " & _checkBox.BooleanValue.ToString())
		End Sub
	End Class
End Namespace
