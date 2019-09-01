Imports RibbonLib

Namespace _07_RibbonColor
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			' set ribbon colors
			_ribbon.SetColors(Color.Wheat, Color.IndianRed, Color.BlueViolet)
		End Sub
	End Class
End Namespace
