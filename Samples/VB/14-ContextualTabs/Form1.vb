Imports RibbonLib
Imports RibbonLib.Controls
Imports RibbonLib.Controls.Events
Imports RibbonLib.Interop

Namespace _14_ContextualTabs
	Public Enum RibbonMarkupCommands As UInteger
		cmdTabMain = 1001
		cmdGroupMain = 1002
		cmdTabGroupTableTools = 1003
		cmdTabDesign = 1004
		cmdTabLayout = 1005
		cmdGroupDesign = 1006
		cmdGroupLayout = 1007
		cmdButtonSelect = 1008
		cmdButtonUnselect = 1009
		cmdButtonDesign1 = 1010
		cmdButtonDesign2 = 1011
		cmdButtonDesign3 = 1012
		cmdButtonLayout1 = 1013
		cmdButtonLayout2 = 1014
	End Enum

	Partial Public Class Form1
		Inherits Form
		Private _tabMain As RibbonTab
		Private _groupMain As RibbonGroup
		Private _tabGroupTableTools As RibbonTabGroup
		Private _tabDesign As RibbonTab
		Private _tabLayout As RibbonTab
		Private _groupDesign As RibbonGroup
		Private _groupLayout As RibbonGroup
		Private _buttonSelect As RibbonButton
		Private _buttonUnselect As RibbonButton
		Private _buttonDesign1 As RibbonButton
		Private _buttonDesign2 As RibbonButton
		Private _buttonDesign3 As RibbonButton
		Private _buttonLayout1 As RibbonButton
		Private _buttonLayout2 As RibbonButton

		Public Sub New()
			InitializeComponent()

			_tabMain = New RibbonTab(_ribbon, CUInt(RibbonMarkupCommands.cmdTabMain))
			_groupMain = New RibbonGroup(_ribbon, CUInt(RibbonMarkupCommands.cmdGroupMain))
			_tabGroupTableTools = New RibbonTabGroup(_ribbon, CUInt(RibbonMarkupCommands.cmdTabGroupTableTools))
			_tabDesign = New RibbonTab(_ribbon, CUInt(RibbonMarkupCommands.cmdTabDesign))
			_tabLayout = New RibbonTab(_ribbon, CUInt(RibbonMarkupCommands.cmdTabLayout))
			_groupDesign = New RibbonGroup(_ribbon, CUInt(RibbonMarkupCommands.cmdGroupDesign))
			_groupLayout = New RibbonGroup(_ribbon, CUInt(RibbonMarkupCommands.cmdGroupLayout))
			_buttonSelect = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonSelect))
			_buttonUnselect = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonUnselect))
			_buttonDesign1 = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDesign1))
			_buttonDesign2 = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDesign2))
			_buttonDesign3 = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDesign3))
			_buttonLayout1 = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonLayout1))
			_buttonLayout2 = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonLayout2))

			AddHandler _buttonSelect.ExecuteEvent, AddressOf _buttonSelect_ExecuteEvent
			AddHandler _buttonUnselect.ExecuteEvent, AddressOf _buttonUnselect_ExecuteEvent
		End Sub

		Private Sub _buttonSelect_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			_tabGroupTableTools.ContextAvailable = ContextAvailability.Active
		End Sub

		Private Sub _buttonUnselect_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			_tabGroupTableTools.ContextAvailable = ContextAvailability.NotAvailable
		End Sub
	End Class
End Namespace
