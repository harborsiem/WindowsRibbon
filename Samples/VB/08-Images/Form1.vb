Imports System.Text
Imports RibbonLib
Imports RibbonLib.Controls
Imports RibbonLib.Controls.Events
Imports RibbonLib.Interop

Namespace _08_Images
	Public Enum RibbonMarkupCommands As UInteger
		 cmdTabDrop = 1012
		 cmdGroupDrop = 1015
		 cmdButtonDropA = 1008
		 cmdButtonDropB = 1009
		 cmdButtonDropC = 1010
	End Enum

	Partial Public Class Form1
		Inherits Form
		Private exitOn As Boolean = False
		Private _tabDrop As RibbonTab
		Private _buttonDropA As RibbonButton
		Private _buttonDropB As RibbonButton
		Private _buttonDropC As RibbonButton

		Public Sub New()
			InitializeComponent()

			_tabDrop = New RibbonTab(_ribbon, CUInt(RibbonMarkupCommands.cmdTabDrop))
			_buttonDropA = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDropA))
			_buttonDropB = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDropB))
			_buttonDropC = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDropC))

			AddHandler _buttonDropA.ExecuteEvent, AddressOf _buttonDropA_ExecuteEvent
			AddHandler _buttonDropB.ExecuteEvent, AddressOf _buttonDropB_ExecuteEvent
		End Sub

		Private Sub _buttonDropA_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			' load bitmap from file
			Dim bitmap_Renamed As Bitmap = New Bitmap("..\..\Res\Drop32.bmp")
			bitmap_Renamed.MakeTransparent()

			' set large image property
			_buttonDropA.LargeImage = _ribbon.ConvertToUIImage(bitmap_Renamed)
		End Sub

		Private Sub _buttonDropB_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			Dim supportedImageSizes As New List(Of Integer)() From {32, 48, 64}

			Dim bitmap_Renamed As Bitmap
			Dim bitmapFileName As New StringBuilder()

			Dim selectedImageSize As Integer
			If supportedImageSizes.Contains(SystemInformation.IconSize.Width) Then
				selectedImageSize = SystemInformation.IconSize.Width
			Else
				selectedImageSize = 32
			End If

			exitOn = Not exitOn
			Dim exitStatus As String = If(exitOn, "on", "off")

			bitmapFileName.AppendFormat("..\..\Res\Exit{0}{1}.bmp", exitStatus, selectedImageSize)

			bitmap_Renamed = New Bitmap(bitmapFileName.ToString())
			bitmap_Renamed.MakeTransparent()

			_buttonDropB.LargeImage = _ribbon.ConvertToUIImage(bitmap_Renamed)
		End Sub
	End Class
End Namespace
