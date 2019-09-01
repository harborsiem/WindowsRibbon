Imports RibbonLib
Imports RibbonLib.Controls
Imports RibbonLib.Interop
Imports RibbonLib.Controls.Events

Namespace _16_RecentItems
	Public Enum RibbonMarkupCommands As UInteger
		cmdApplicationMenu = 1000
		cmdButtonNew = 1001
		cmdButtonOpen = 1002
		cmdButtonSave = 1003
		cmdButtonExit = 1004
		cmdRecentItems = 1005
	End Enum

	Partial Public Class Form1
		Inherits Form
		Private _ribbonRecentItems As RibbonRecentItems

		Private _recentItems As List(Of RecentItemsPropertySet)

		Public Sub New()
			InitializeComponent()

			_ribbonRecentItems = New RibbonRecentItems(_ribbon, CUInt(RibbonMarkupCommands.cmdRecentItems))

			AddHandler _ribbonRecentItems.ExecuteEvent, AddressOf _recentItems_ExecuteEvent
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			InitRecentItems()
		End Sub

		Private Sub InitRecentItems()
			' prepare list of recent items
			_recentItems = New List(Of RecentItemsPropertySet)()
			_recentItems.Add(New RecentItemsPropertySet() With {.Label = "Recent item 1", .LabelDescription = "Recent item 1 description", .Pinned = True})
			_recentItems.Add(New RecentItemsPropertySet() With {.Label = "Recent item 2", .LabelDescription = "Recent item 2 description", .Pinned = False})

			_ribbonRecentItems.RecentItems = _recentItems
		End Sub

		Private Sub _recentItems_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			If e.Key.PropertyKey = RibbonProperties.RecentItems Then
				' go over recent items
				Dim objectArray() As Object = CType(e.CurrentValue.PropVariant.Value, Object())
				For i As Integer = 0 To objectArray.Length - 1
					Dim propertySet As IUISimplePropertySet = TryCast(objectArray(i), IUISimplePropertySet)

					If propertySet IsNot Nothing Then
						Dim propLabel As PropVariant
						propertySet.GetValue(RibbonProperties.Label, propLabel)
						Dim label As String = CStr(propLabel.Value)

						Dim propLabelDescription As PropVariant
						propertySet.GetValue(RibbonProperties.LabelDescription, propLabelDescription)
						Dim labelDescription As String = CStr(propLabelDescription.Value)

						Dim propPinned As PropVariant
						propertySet.GetValue(RibbonProperties.Pinned, propPinned)
						Dim pinned As Boolean = CBool(propPinned.Value)

						' update pinned value
						_recentItems(i).Pinned = pinned
					End If
				Next i
			ElseIf e.Key.PropertyKey = RibbonProperties.SelectedItem Then
				' get selected item index
				Dim selectedItem As UInteger = CUInt(e.CurrentValue.PropVariant.Value)

				' get selected item label
				Dim propLabel As PropVariant
				e.CommandExecutionProperties.GetValue(RibbonProperties.Label, propLabel)
				Dim label As String = CStr(propLabel.Value)

				' get selected item label description
				Dim propLabelDescription As PropVariant
				e.CommandExecutionProperties.GetValue(RibbonProperties.LabelDescription, propLabelDescription)
				Dim labelDescription As String = CStr(propLabelDescription.Value)

				' get selected item pinned value
				Dim propPinned As PropVariant
				e.CommandExecutionProperties.GetValue(RibbonProperties.Pinned, propPinned)
				Dim pinned As Boolean = CBool(propPinned.Value)
			End If
		End Sub
	End Class
End Namespace
