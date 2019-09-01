Imports RibbonLib
Imports RibbonLib.Controls
Imports RibbonLib.Interop
Imports RibbonLib.Controls.Events
Imports System.IO

Namespace _17_QuickAccessToolbar
	Public Enum RibbonMarkupCommands As UInteger
		cmdButtonNew = 1001
		cmdButtonOpen = 1002
		cmdButtonSave = 1003
		cmdTabMain = 1004
		cmdGroupFileActions = 1005
		cmdQAT = 1006
		cmdCustomizeQAT = 1007
	End Enum

	Partial Public Class Form1
		Inherits Form
		Private _buttonNew As RibbonButton
		Private _buttonOpen As RibbonButton
		Private _buttonSave As RibbonButton
		Private _tabMain As RibbonTab
		Private _groupFileActions As RibbonGroup
		Private _ribbonQuickAccessToolbar As RibbonQuickAccessToolbar

		Private _stream As Stream

		Public Sub New()
			InitializeComponent()

			_buttonNew = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonNew))
			_buttonOpen = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonOpen))
			_buttonSave = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonSave))
			_tabMain = New RibbonTab(_ribbon, CUInt(RibbonMarkupCommands.cmdTabMain))
			_groupFileActions = New RibbonGroup(_ribbon, CUInt(RibbonMarkupCommands.cmdGroupFileActions))
			_ribbonQuickAccessToolbar = New RibbonQuickAccessToolbar(_ribbon, CUInt(RibbonMarkupCommands.cmdQAT), CUInt(RibbonMarkupCommands.cmdCustomizeQAT))

			AddHandler _buttonNew.ExecuteEvent, AddressOf _buttonNew_ExecuteEvent
			AddHandler _buttonSave.ExecuteEvent, AddressOf _buttonSave_ExecuteEvent
			AddHandler _buttonOpen.ExecuteEvent, AddressOf _buttonOpen_ExecuteEvent

			' register to the QAT customize button
			AddHandler _ribbonQuickAccessToolbar.ExecuteEvent, AddressOf _ribbonQuickAccessToolbar_ExecuteEvent
		End Sub

		Private Sub _buttonNew_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			' changing QAT commands list 
			Dim itemsSource As IUICollection = _ribbonQuickAccessToolbar.ItemsSource
			itemsSource.Clear()
			itemsSource.Add(New GalleryCommandPropertySet() With {.CommandID = CUInt(RibbonMarkupCommands.cmdButtonNew)})
			itemsSource.Add(New GalleryCommandPropertySet() With {.CommandID = CUInt(RibbonMarkupCommands.cmdButtonOpen)})
			itemsSource.Add(New GalleryCommandPropertySet() With {.CommandID = CUInt(RibbonMarkupCommands.cmdButtonSave)})
		End Sub

		Private Sub _buttonSave_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			' save ribbon QAT settings 
			_stream = New MemoryStream()
			_ribbon.SaveSettingsToStream(_stream)
		End Sub

		Private Sub _buttonOpen_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			If _stream Is Nothing Then
				Return
			End If

			' load ribbon QAT settings 
			_stream.Position = 0
			_ribbon.LoadSettingsFromStream(_stream)
		End Sub

		Private Sub _ribbonQuickAccessToolbar_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			MessageBox.Show("Open customize commands dialog..")
		End Sub
	End Class
End Namespace
