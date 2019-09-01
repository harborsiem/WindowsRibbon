Imports RibbonLib
Imports RibbonLib.Controls
Imports RibbonLib.Interop

Namespace _15_ContextPopup
	Public Enum RibbonMarkupCommands As UInteger
		cmdButtonNew = 1001
		cmdButtonOpen = 1002
		cmdButtonSave = 1003
		cmdButtonExit = 1004
		cmdContextMap = 1005
		cmdFontControl = 1006
		cmdDropDownColorPicker = 1007
	End Enum

	Partial Public Class Form1
		Inherits Form
		Private _ribbonContextMenuStrip As RibbonContextMenuStrip

		Public Sub New()
			InitializeComponent()

			_ribbonContextMenuStrip = New RibbonContextMenuStrip(_ribbon, CUInt(RibbonMarkupCommands.cmdContextMap))

			' recommended way
			AddHandler panel2.MouseClick, AddressOf panel2_MouseClick

			' convenient way
			panel1.ContextMenuStrip = _ribbonContextMenuStrip
		End Sub

		Private Sub panel2_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs)
			If e.Button = MouseButtons.Right Then
				Dim p As Point = panel2.PointToScreen(e.Location)
				_ribbon.ShowContextPopup(CUInt(RibbonMarkupCommands.cmdContextMap), p.X, p.Y)
			End If
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			_ribbon.Viewable = False
		End Sub
	End Class
End Namespace
