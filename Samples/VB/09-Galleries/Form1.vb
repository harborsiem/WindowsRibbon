Imports RibbonLib
Imports RibbonLib.Controls
Imports RibbonLib.Controls.Events
Imports RibbonLib.Interop

Namespace _09_Galleries
	Public Enum RibbonMarkupCommands As UInteger
		 cmdTabMain = 1000
		 cmdGroupDropDownGallery = 1001
		 cmdDropDownGallery = 1002
		 cmdCommandSpace = 1003
		 cmdGroupSplitButtonGallery = 1004
		 cmdSplitButtonGallery = 1005
		 cmdGroupInRibbonGallery = 1006
		 cmdInRibbonGallery = 1007
	End Enum

	Partial Public Class Form1
		Inherits Form
		Private _dropDownGallery As RibbonDropDownGallery
		Private _splitButtonGallery As RibbonSplitButtonGallery
		Private _buttons() As RibbonButton
		Private _inRibbonGallery As RibbonInRibbonGallery

		Public Sub New()
			InitializeComponent()

			_dropDownGallery = New RibbonDropDownGallery(_ribbon, CUInt(RibbonMarkupCommands.cmdDropDownGallery))
			_splitButtonGallery = New RibbonSplitButtonGallery(_ribbon, CUInt(RibbonMarkupCommands.cmdSplitButtonGallery))
			_inRibbonGallery = New RibbonInRibbonGallery(_ribbon, CUInt(RibbonMarkupCommands.cmdInRibbonGallery))

            AddHandler _dropDownGallery.ItemsSourceReady, AddressOf _dropDownGallery_ItemsSourceReady
            AddHandler _splitButtonGallery.CategoriesReady, AddressOf _splitButtonGallery_CategoriesReady
            AddHandler _splitButtonGallery.ItemsSourceReady, AddressOf _splitButtonGallery_ItemsSourceReady
            AddHandler _inRibbonGallery.ItemsSourceReady, AddressOf _inRibbonGallery_ItemsSourceReady

			AddHandler _dropDownGallery.ExecuteEvent, AddressOf _dropDownGallery_ExecuteEvent
			AddHandler _dropDownGallery.PreviewEvent, AddressOf _dropDownGallery_OnPreview
			AddHandler _dropDownGallery.CancelPreviewEvent, AddressOf _dropDownGallery_OnCancelPreview
		End Sub

        Private Sub _dropDownGallery_ItemsSourceReady()
            ' set label
            _dropDownGallery.Label = "Size"

            ' set _dropDownGallery items
            Dim itemsSource As IUICollection = _dropDownGallery.ItemsSource
            itemsSource.Clear()
            For Each image_Renamed As Image In imageListLines.Images
                itemsSource.Add(New GalleryItemPropertySet() With {.ItemImage = _ribbon.ConvertToUIImage(CType(image_Renamed, Bitmap))})

            Next image_Renamed
        End Sub

        Private Sub _splitButtonGallery_CategoriesReady()
            ' set _splitButtonGallery categories
            Dim categories As IUICollection = _splitButtonGallery.Categories
            categories.Clear()
            categories.Add(New GalleryItemPropertySet() With {.Label = "Category 1", .CategoryID = 1})
        End Sub

        Private Sub _splitButtonGallery_ItemsSourceReady()
            ' set label
            _splitButtonGallery.Label = "Brushes"

            ' prepare helper classes for commands
            _buttons = New RibbonButton(imageListBrushes.Images.Count - 1) {}
            Dim i As UInteger
            For i = 0 To _buttons.Length - 1
                _buttons(i) = New RibbonButton(_ribbon, 2000 + i) With {.Label = "Label " & i.ToString(), .LargeImage = _ribbon.ConvertToUIImage(CType(imageListBrushes.Images(CInt(Fix(i))), Bitmap))}
            Next i

            ' set _splitButtonGallery items
            Dim itemsSource As IUICollection = _splitButtonGallery.ItemsSource
            itemsSource.Clear()
            i = 0
            For Each image_Renamed As Image In imageListBrushes.Images
                itemsSource.Add(New GalleryCommandPropertySet() With {.CommandID = 2000 + i, .CommandType = CommandType.Action, .CategoryID = 1})
                i += 1
            Next image_Renamed
        End Sub

        Private Sub _inRibbonGallery_ItemsSourceReady()
            ' set _inRibbonGallery items
            Dim itemsSource As IUICollection = _inRibbonGallery.ItemsSource
            itemsSource.Clear()
            For Each image_Renamed As Image In imageListShapes.Images
                itemsSource.Add(New GalleryItemPropertySet() With {.ItemImage = _ribbon.ConvertToUIImage(CType(image_Renamed, Bitmap))})
            Next image_Renamed
        End Sub

		Private Sub _dropDownGallery_OnCancelPreview(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			Debug.WriteLine("DropDownGallery::OnCancelPreview")
		End Sub

		Private Sub _dropDownGallery_OnPreview(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			Debug.WriteLine("DropDownGallery::OnPreview")
		End Sub

		Private Sub _dropDownGallery_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			Debug.WriteLine("DropDownGallery::ExecuteEvent")
		End Sub

    End Class
End Namespace
