Imports RibbonLib
Imports RibbonLib.Controls
Imports RibbonLib.Controls.Events
Imports RibbonLib.Interop

Namespace _06_ComboBox
    Public Enum RibbonMarkupCommands As UInteger
        cmdButtonDropA = 1008
        cmdButtonDropB = 1009
        cmdButtonDropC = 1010
        cmdButtonDropD = 1011
        cmdButtonDropE = 1012
        cmdButtonDropF = 1013
        cmdTabDrop = 1014
        cmdGroupDrop = 1015
        cmdGroupMore = 1017
        cmdComboBox1 = 1018
        cmdComboBox2 = 1019
        cmdTabSecond = 1020
        cmdGroupSecond = 1021
        cmdComboBox3 = 1022
    End Enum

	Partial Public Class Form1
		Inherits Form
		Private _buttonDropA As RibbonButton
		Private _buttonDropB As RibbonButton
		Private _buttonDropC As RibbonButton
		Private _buttonDropD As RibbonButton
		Private _buttonDropE As RibbonButton
		Private _buttonDropF As RibbonButton
		Private _comboBox1 As RibbonComboBox
        Private _comboBox2 As RibbonComboBox
        Private _comboBox3 As RibbonComboBox

		Private _uiCollectionChangedEvent As UICollectionChangedEvent

		Public Sub New()
			InitializeComponent()

			_buttonDropA = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDropA))
			_buttonDropB = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDropB))
			_buttonDropC = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDropC))
			_buttonDropD = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDropD))
			_buttonDropE = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDropE))
			_buttonDropF = New RibbonButton(_ribbon, CUInt(RibbonMarkupCommands.cmdButtonDropF))
			_comboBox1 = New RibbonComboBox(_ribbon, CUInt(RibbonMarkupCommands.cmdComboBox1))
            _comboBox2 = New RibbonComboBox(_ribbon, CUInt(RibbonMarkupCommands.cmdComboBox2))
            _comboBox3 = New RibbonComboBox(_ribbon, CUInt(RibbonMarkupCommands.cmdComboBox3))
            _uiCollectionChangedEvent = New UICollectionChangedEvent()

			AddHandler _buttonDropA.ExecuteEvent, AddressOf _buttonDropA_ExecuteEvent
			AddHandler _buttonDropB.ExecuteEvent, AddressOf _buttonDropB_ExecuteEvent
			AddHandler _buttonDropC.ExecuteEvent, AddressOf _buttonDropC_ExecuteEvent
			AddHandler _buttonDropD.ExecuteEvent, AddressOf _buttonDropD_ExecuteEvent
			AddHandler _buttonDropE.ExecuteEvent, AddressOf _buttonDropE_ExecuteEvent
			AddHandler _buttonDropF.ExecuteEvent, AddressOf _buttonDropF_ExecuteEvent

			InitComboBoxes()
		End Sub

		Private Sub _buttonDropA_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			' get selected item index from combo box 1
			Dim selectedItemIndex As UInteger = _comboBox1.SelectedItem

			If selectedItemIndex = Constants.UI_Collection_InvalidIndex Then
				MessageBox.Show("No item is selected in simple combo")
			Else
				Dim selectedItem As Object
				_comboBox1.ItemsSource.GetItem(selectedItemIndex, selectedItem)
				Dim uiItem As IUISimplePropertySet = CType(selectedItem, IUISimplePropertySet)
				Dim itemLabel As PropVariant
				uiItem.GetValue(RibbonProperties.Label, itemLabel)
				MessageBox.Show("Selected item in simple combo is: " & CStr(itemLabel.Value))
			End If
		End Sub

		Private Sub _buttonDropB_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			' get string value from combo box 2
			Dim stringValue As String = _comboBox2.StringValue
			MessageBox.Show("String value in advanced combo is: " & stringValue)
		End Sub

		Private Sub _buttonDropC_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			' enumerate over items
			Dim itemsSource As IEnumUnknown = CType(_comboBox1.ItemsSource, IEnumUnknown)
			itemsSource.Reset()
			Dim items(0) As Object
			Dim fetchedItem As UInteger
			Do While itemsSource.Next(1, items, fetchedItem) = HRESULT.S_OK
				Dim uiItem As IUISimplePropertySet = CType(items(0), IUISimplePropertySet)
				Dim itemLabel As PropVariant
				uiItem.GetValue(RibbonProperties.Label, itemLabel)
				MessageBox.Show("Label = " & CStr(itemLabel.Value))
			Loop
		End Sub

		Private Sub _buttonDropD_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			_uiCollectionChangedEvent.Attach(_comboBox1.ItemsSource)
			AddHandler _uiCollectionChangedEvent.ChangedEvent, AddressOf _uiCollectionChangedEvent_ChangedEvent
		End Sub

		Private Sub _buttonDropE_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			Dim itemsSource1 As IUICollection = _comboBox1.ItemsSource
			Dim count As UInteger
			itemsSource1.GetCount(count)
			count += 1
			itemsSource1.Add(New GalleryItemPropertySet() With {.Label = "Label " & count.ToString(), .CategoryID = Constants.UI_Collection_InvalidIndex})
		End Sub

		Private Sub _buttonDropF_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			RemoveHandler _uiCollectionChangedEvent.ChangedEvent, AddressOf _uiCollectionChangedEvent_ChangedEvent
			_uiCollectionChangedEvent.Detach()
		End Sub

		Private Sub _uiCollectionChangedEvent_ChangedEvent(ByVal sender As Object, ByVal e As UICollectionChangedEventArgs)
			MessageBox.Show("Got ChangedEvent. Action = " & e.Action.ToString())
		End Sub

        Private Sub InitComboBoxes()
            _comboBox1.RepresentativeString = "Label 1"
            _comboBox2.RepresentativeString = "XXXXXXXXXXX"
            _comboBox3.RepresentativeString = "XXXXXXXXXXX"

            _comboBox1.Label = "Simple Combo"
            _comboBox2.Label = "Advanced Combo"
            _comboBox3.Label = "Another Combo"

            AddHandler _comboBox1.ItemsSourceReady, AddressOf _comboBox1_ItemsSourceReady

            AddHandler _comboBox2.ItemsSourceReady, AddressOf _comboBox2_CategoriesReady
            AddHandler _comboBox2.ItemsSourceReady, AddressOf _comboBox2_ItemsSourceReady

            AddHandler _comboBox3.ItemsSourceReady, AddressOf _comboBox3_ItemsSourceReady
        End Sub

        Private Sub _comboBox1_ItemsSourceReady()
            ' set combobox1 items
            Dim itemsSource1 As IUICollection = _comboBox1.ItemsSource
            itemsSource1.Clear()
            itemsSource1.Add(New GalleryItemPropertySet() With {.Label = "Label 1", .CategoryID = Constants.UI_Collection_InvalidIndex})
            itemsSource1.Add(New GalleryItemPropertySet() With {.Label = "Label 2", .CategoryID = Constants.UI_Collection_InvalidIndex})
            itemsSource1.Add(New GalleryItemPropertySet() With {.Label = "Label 3", .CategoryID = Constants.UI_Collection_InvalidIndex})
        End Sub

        Private Sub _comboBox2_CategoriesReady()
            ' set _comboBox2 categories
            Dim categories2 As IUICollection = _comboBox2.Categories
            categories2.Clear()
            categories2.Add(New GalleryItemPropertySet() With {.Label = "Category 1", .CategoryID = 1})
            categories2.Add(New GalleryItemPropertySet() With {.Label = "Category 2", .CategoryID = 2})
        End Sub

        Private Sub _comboBox2_ItemsSourceReady()
            ' set _comboBox2 items
            Dim itemsSource2 As IUICollection = _comboBox2.ItemsSource
            itemsSource2.Clear()
            itemsSource2.Add(New GalleryItemPropertySet() With {.Label = "Label 1", .CategoryID = 1})
            itemsSource2.Add(New GalleryItemPropertySet() With {.Label = "Label 2", .CategoryID = 1})
            itemsSource2.Add(New GalleryItemPropertySet() With {.Label = "Label 3", .CategoryID = 2})
        End Sub

        Private Sub _comboBox3_ItemsSourceReady()
            ' set combobox3 items
            Dim itemsSource3 As IUICollection = _comboBox3.ItemsSource
            itemsSource3.Clear()
            itemsSource3.Add(New GalleryItemPropertySet() With {.Label = "Label 1", .CategoryID = Constants.UI_Collection_InvalidIndex})
            itemsSource3.Add(New GalleryItemPropertySet() With {.Label = "Label 2", .CategoryID = Constants.UI_Collection_InvalidIndex})
            itemsSource3.Add(New GalleryItemPropertySet() With {.Label = "Label 3", .CategoryID = Constants.UI_Collection_InvalidIndex})
        End Sub

    End Class
End Namespace
