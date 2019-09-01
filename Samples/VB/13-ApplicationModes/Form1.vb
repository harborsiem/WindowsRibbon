Imports RibbonLib
Imports RibbonLib.Controls
Imports RibbonLib.Controls.Events
Imports RibbonLib.Interop

Namespace _13_ApplicationModes
	Public Enum RibbonMarkupCommands As UInteger
		cmdTabMain = 1001
		cmdGroupCommon = 1002
		cmdGroupSimple = 1003
		cmdGroupAdvanced = 1004
		cmdButtonNew = 1005
		cmdButtonOpen = 1006
		cmdButtonSave = 1007
		cmdButtonDropA = 1008
		cmdButtonDropB = 1009
		cmdButtonDropC = 1010
		cmdButtonSwitchToAdvanced = 1011
		cmdButtonSwitchToSimple = 1012
	End Enum

	Partial Public Class Form1
		Inherits Form
		Private _tabMain As RibbonTab
		Private _groupCommon As RibbonGroup
		Private _groupSimple As RibbonGroup
		Private _groupAdvanced As RibbonGroup
		Private _buttonNew As RibbonButton
		Private _buttonOpen As RibbonButton
		Private _buttonSave As RibbonButton
		Private _buttonDropA As RibbonButton
		Private _buttonDropB As RibbonButton
		Private _buttonDropC As RibbonButton
		Private _buttonSwitchToAdvanced As RibbonButton
		Private _buttonSwitchToSimple As RibbonButton

		Public Sub New()
			InitializeComponent()

			_tabMain = New RibbonTab(_ribbon, CUInt(RibbonMarkupCommands.cmdTabMain))
			_groupCommon = New RibbonGroup(_ribbon, CUInt(RibbonMarkupCommands.cmdGroupCommon))
			_groupSimple = New RibbonGroup(_ribbon, CUInt(RibbonMarkupCommands.cmdGroupSimple))
			_groupAdvanced = New RibbonGroup(_ribbon, CUInt(RibbonMarkupCommands.cmdGroupAdvanced))
			_buttonNew = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonNew))
			_buttonOpen = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonOpen))
			_buttonSave = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonSave))
			_buttonDropA = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDropA))
			_buttonDropB = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDropB))
			_buttonDropC = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDropC))
			_buttonSwitchToAdvanced = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonSwitchToAdvanced))
			_buttonSwitchToSimple = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonSwitchToSimple))

			AddHandler _buttonSwitchToAdvanced.ExecuteEvent, AddressOf _buttonSwitchToAdvanced_ExecuteEvent
			AddHandler _buttonSwitchToSimple.ExecuteEvent, AddressOf _buttonSwitchToSimple_ExecuteEvent
		End Sub

		Private Sub _buttonSwitchToAdvanced_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			_ribbon.SetModes(1)
		End Sub

		Private Sub _buttonSwitchToSimple_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			_ribbon.SetModes(0)
		End Sub
	End Class
End Namespace
