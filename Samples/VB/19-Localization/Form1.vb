Imports System.Text
Imports RibbonLib
Imports RibbonLib.Controls
Imports RibbonLib.Controls.Events
Imports RibbonLib.Interop
Imports System.IO

Namespace _19_Localization
	Public Enum RibbonMarkupCommands As UInteger
		 cmdTab = 1012
		 cmdGroup = 1015
		 cmdButtonOne = 1008
		 cmdButtonTwo = 1009
		 cmdButtonThree = 1010
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

			_tabDrop = New RibbonTab(_ribbonControl, CUInt(RibbonMarkupCommands.cmdTab))
			_buttonDropA = New RibbonButton(_ribbonControl, CUInt(RibbonMarkupCommands.cmdButtonOne))
			_buttonDropB = New RibbonButton(_ribbonControl, CUInt(RibbonMarkupCommands.cmdButtonTwo))
			_buttonDropC = New RibbonButton(_ribbonControl, CUInt(RibbonMarkupCommands.cmdButtonThree))

			AddHandler _buttonDropA.ExecuteEvent, AddressOf _buttonDropA_ExecuteEvent
			AddHandler _buttonDropB.ExecuteEvent, AddressOf _buttonDropB_ExecuteEvent
		End Sub

		Private Sub _buttonDropA_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			' load bitmap from file
			Dim bitmap_Renamed As Bitmap = GetResourceBitmap("Drop32.bmp")
			bitmap_Renamed.MakeTransparent()

			' set large image property
			_buttonDropA.LargeImage = _ribbonControl.ConvertToUIImage(bitmap_Renamed)
		End Sub

		Private Sub _buttonDropB_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			Dim supportedImageSizes As New List(Of Integer)() From {32, 48, 64}

			Dim bitmapFileName As New StringBuilder()

			Dim selectedImageSize As Integer
			If supportedImageSizes.Contains(SystemInformation.IconSize.Width) Then
				selectedImageSize = SystemInformation.IconSize.Width
			Else
				selectedImageSize = 32
			End If

			exitOn = Not exitOn
			Dim exitStatus As String = If(exitOn, "On", "Off")

			Dim bitmap_Renamed = GetResourceBitmap(String.Format("Exit{0}{1}.bmp", exitStatus, selectedImageSize))
			bitmap_Renamed.MakeTransparent()

			_buttonDropB.LargeImage = _ribbonControl.ConvertToUIImage(bitmap_Renamed)
		End Sub

		Private Function GetResourceBitmap(ByVal name As String) As Bitmap
			Dim resourceName As String = String.Format("_19_Localization.Res.{0}", name)
			Using stream = Me.GetType().Assembly.GetManifestResourceStream(resourceName)
				Dim bitmap_Renamed = New Bitmap(stream)
				Return bitmap_Renamed
			End Using
		End Function

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		End Sub

		Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles MyBase.FormClosed
		End Sub

	End Class
End Namespace
