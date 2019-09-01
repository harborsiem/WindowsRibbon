Imports RibbonLib
Imports RibbonLib.Controls
Imports RibbonLib.Controls.Events
Imports RibbonLib.Interop

Namespace _05_Spinner
	Public Enum RibbonMarkupCommands As UInteger
		 cmdButtonDropA = 1008
		 cmdButtonDropB = 1009
		 cmdButtonDropC = 1010
		 cmdTabDrop = 1012
		 cmdGroupDrop = 1015
		 cmdGroupMore = 1017
		 cmdSpinner = 1018
	End Enum

	Partial Public Class Form1
		Inherits Form
		Private _buttonDropA As RibbonButton
		Private _spinner As RibbonSpinner

		Public Sub New()
			InitializeComponent()

			_buttonDropA = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDropA))
			_spinner = New RibbonSpinner(_ribbon, CUInt(RibbonMarkupCommands.cmdSpinner))

			AddHandler _buttonDropA.ExecuteEvent, AddressOf _buttonDropA_ExecuteEvent
		End Sub

		Private Sub _buttonDropA_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			InitSpinner()
		End Sub

		Private Sub InitSpinner()
			_spinner.DecimalPlaces = 2
			_spinner.DecimalValue = 1.8D
			_spinner.TooltipTitle = "Height"
			_spinner.TooltipDescription = "Enter height in meters."
			_spinner.MaxValue = 2.5D
			_spinner.MinValue = 0
			_spinner.Increment = 0.01D
			_spinner.FormatString = " m"
			_spinner.RepresentativeString = "2.50 m"
			_spinner.Label = "Height:"
		End Sub
	End Class
End Namespace
