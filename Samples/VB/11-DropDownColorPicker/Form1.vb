Imports RibbonLib
Imports RibbonLib.Controls
Imports RibbonLib.Controls.Events
Imports RibbonLib.Interop

Namespace _11_DropDownColorPicker
	Public Enum RibbonMarkupCommands As UInteger
		 cmdTab = 999
		 cmdButtonsGroup = 1000
		 cmdDropDownColorPickerGroup = 1001
		 cmdDropDownColorPickerThemeColors = 1002
		 cmdDropDownColorPickerStandardColors = 1003
		 cmdDropDownColorPickerHighlightColors = 1004
		 cmdButtonListColors = 1006
	End Enum

	Partial Public Class Form1
		Inherits Form
		Private _groupButtons As RibbonGroup
		Private _groupColors As RibbonGroup
		Private _buttonListColors As RibbonButton
		Private _themeColors As RibbonDropDownColorPicker
		Private _standardColors As RibbonDropDownColorPicker
		Private _highlightColors As RibbonDropDownColorPicker

		Public Sub New()
			InitializeComponent()

			_groupButtons = New RibbonGroup(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonsGroup))
			_groupColors = New RibbonGroup(_ribbon, CUInt(RibbonMarkupCommands.cmdDropDownColorPickerGroup))
			_buttonListColors = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonListColors))
			_themeColors = New RibbonDropDownColorPicker(_ribbon, CUInt(RibbonMarkupCommands.cmdDropDownColorPickerThemeColors))
			_standardColors = New RibbonDropDownColorPicker(_ribbon, CUInt(RibbonMarkupCommands.cmdDropDownColorPickerStandardColors))
			_highlightColors = New RibbonDropDownColorPicker(_ribbon, CUInt(RibbonMarkupCommands.cmdDropDownColorPickerHighlightColors))

			AddHandler _buttonListColors.ExecuteEvent, AddressOf _buttonListColors_ExecuteEvent
		End Sub

		Private Sub _buttonListColors_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			Dim colors() As Color = _themeColors.ThemeColors
			Dim colorsTooltips() As String = _themeColors.ThemeColorsTooltips

			Dim stringBuilder As New System.Text.StringBuilder()

			For i As Integer = 0 To colors.Length - 1
				stringBuilder.AppendFormat("{0} = {1}" & vbLf, colorsTooltips(i), colors(i).ToString())
			Next i

			MessageBox.Show(stringBuilder.ToString())
		End Sub

		Private Sub InitDropDownColorPickers()
			' common properties
			_themeColors.Label = "Theme Colors"
			AddHandler _themeColors.ExecuteEvent, AddressOf _themeColors_ExecuteEvent

			' set labels
			_themeColors.AutomaticColorLabel = "My Automatic"
			_themeColors.MoreColorsLabel = "My More Colors"
			_themeColors.NoColorLabel = "My No Color"
			_themeColors.RecentColorsCategoryLabel = "My Recent Colors"
			_themeColors.StandardColorsCategoryLabel = "My Standard Colors"
			_themeColors.ThemeColorsCategoryLabel = "My Theme Colors"

			' set colors
			_themeColors.ThemeColorsTooltips = New String() { "yellow", "green", "red", "blue" }
			_themeColors.ThemeColors = New Color() { Color.Yellow, Color.Green, Color.Red, Color.Blue }
		End Sub

		Private Sub _themeColors_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			MessageBox.Show("Selected color is " & _themeColors.Color.ToString())
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			InitDropDownColorPickers()
		End Sub
	End Class
End Namespace
