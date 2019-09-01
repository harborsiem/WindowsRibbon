Imports RibbonLib
Imports RibbonLib.Controls
Imports RibbonLib.Controls.Events
Imports RibbonLib.Interop

Namespace _12_FontControl
	Public Enum RibbonMarkupCommands As UInteger
		 cmdTabMain = 1001
		 cmdGroupRichFont = 1002
		 cmdRichFont = 1003
	End Enum

	Partial Public Class Form1
		Inherits Form
		Private _richFont As RibbonFontControl

		Public Sub New()
			InitializeComponent()

			_richFont = New RibbonFontControl(_ribbon, CUInt(RibbonMarkupCommands.cmdRichFont))

			AddHandler _richFont.ExecuteEvent, AddressOf _richFont_ExecuteEvent
			AddHandler _richFont.PreviewEvent, AddressOf _richFont_OnPreview
			AddHandler _richFont.CancelPreviewEvent, AddressOf _richFont_OnCancelPreview
		End Sub

		Private Sub _richFont_ExecuteEvent(ByVal sender As Object, ByVal e As ExecuteEventArgs)
#If DEBUG Then
			PrintFontControlProperties(_richFont)
			PrintChangedProperties(e.CommandExecutionProperties)
#End If
			' skip if selected font is not valid
			If (_richFont.Family Is Nothing) OrElse (_richFont.Family.Trim() = String.Empty) OrElse (_richFont.Size = 0) Then
				Return
			End If

			' prepare font style
			Dim fontStyle_Renamed As FontStyle = FontStyle.Regular
			If _richFont.Bold = FontProperties.Set Then
				fontStyle_Renamed = fontStyle_Renamed Or FontStyle.Bold
			End If
			If _richFont.Italic = FontProperties.Set Then
				fontStyle_Renamed = fontStyle_Renamed Or FontStyle.Italic
			End If
			If _richFont.Underline = FontUnderline.Set Then
				fontStyle_Renamed = fontStyle_Renamed Or FontStyle.Underline
			End If
			If _richFont.Strikethrough = FontProperties.Set Then
				fontStyle_Renamed = fontStyle_Renamed Or FontStyle.Strikeout
			End If

			' set selected font
			' creating a new font can't fail if the font doesn't support the requested style
			' or if the font family name doesn't exist
			Try
				richTextBox1.SelectionFont = New Font(_richFont.Family, CSng(_richFont.Size), fontStyle_Renamed)
			Catch e1 As ArgumentException
			End Try

			' set selected colors
			richTextBox1.SelectionColor = _richFont.ForegroundColor
			richTextBox1.SelectionBackColor = _richFont.BackgroundColor

			' set subscript / superscript
			Select Case _richFont.VerticalPositioning
				Case FontVerticalPosition.NotSet, FontVerticalPosition.NotAvailable
					richTextBox1.SelectionCharOffset = 0

				Case FontVerticalPosition.SuperScript
					richTextBox1.SelectionCharOffset = 10

				Case FontVerticalPosition.SubScript
					richTextBox1.SelectionCharOffset = -10
			End Select
		End Sub

		Private Sub _richFont_OnPreview(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			Dim propChangesProperties As PropVariant
			e.CommandExecutionProperties.GetValue(RibbonProperties.FontProperties_ChangedProperties, propChangesProperties)
			Dim changedProperties As IPropertyStore = CType(propChangesProperties.Value, IPropertyStore)

			UpdateRichTextBox(changedProperties)
		End Sub

		Private Sub _richFont_OnCancelPreview(ByVal sender As Object, ByVal e As ExecuteEventArgs)
			Dim fontProperties As IPropertyStore = CType(e.CurrentValue.PropVariant.Value, IPropertyStore)

			UpdateRichTextBox(fontProperties)
		End Sub

		Private Shared Sub PrintFontControlProperties(ByVal fontControl As RibbonFontControl)
			Debug.WriteLine("")
			Debug.WriteLine("FontControl current properties:")
			Debug.WriteLine("Family: " & fontControl.Family)
			Debug.WriteLine("Size: " & fontControl.Size.ToString())
			Debug.WriteLine("Bold: " & fontControl.Bold.ToString())
			Debug.WriteLine("Italic: " & fontControl.Italic.ToString())
			Debug.WriteLine("Underline: " & fontControl.Underline.ToString())
			Debug.WriteLine("Strikethrough: " & fontControl.Strikethrough.ToString())
			Debug.WriteLine("ForegroundColor: " & fontControl.ForegroundColor.ToString())
			Debug.WriteLine("BackgroundColor: " & fontControl.BackgroundColor.ToString())
			Debug.WriteLine("VerticalPositioning: " & fontControl.VerticalPositioning.ToString())
		End Sub

		Private Shared Sub PrintChangedProperties(ByVal commandExecutionProperties As IUISimplePropertySet)
			Dim propChangesProperties As PropVariant
			commandExecutionProperties.GetValue(RibbonProperties.FontProperties_ChangedProperties, propChangesProperties)
			Dim changedProperties As IPropertyStore = CType(propChangesProperties.Value, IPropertyStore)
			Dim changedPropertiesNumber As UInteger
			changedProperties.GetCount(changedPropertiesNumber)

			Debug.WriteLine("")
			Debug.WriteLine("FontControl changed properties:")
			For i As UInteger = 0 To changedPropertiesNumber - 1
				Dim propertyKey_Renamed As PropertyKey
				changedProperties.GetAt(i, propertyKey_Renamed)
				Debug.WriteLine(RibbonProperties.GetPropertyKeyName(propertyKey_Renamed))
			Next i
		End Sub

		Private Sub UpdateRichTextBox(ByVal propertyStore As IPropertyStore)
			Dim fontPropertyStore_Renamed As New FontPropertyStore(propertyStore)
			Dim propValue As PropVariant

			Dim fontStyle_Renamed As FontStyle
			Dim family As String
			Dim size As Single

			If richTextBox1.SelectionFont IsNot Nothing Then
				fontStyle_Renamed = richTextBox1.SelectionFont.Style
				family = richTextBox1.SelectionFont.FontFamily.Name
				size = richTextBox1.SelectionFont.Size
			Else
				fontStyle_Renamed = FontStyle.Regular
				family = String.Empty
				size = 0
			End If

			If propertyStore.GetValue(RibbonProperties.FontProperties_Family, propValue) = HRESULT.S_OK Then
				family = fontPropertyStore_Renamed.Family
			End If
			If propertyStore.GetValue(RibbonProperties.FontProperties_Size, propValue) = HRESULT.S_OK Then
				size = CSng(fontPropertyStore_Renamed.Size)
			End If

			' creating a new font can't fail if the font doesn't support the requested style
			' or if the font family name doesn't exist
			Try
				richTextBox1.SelectionFont = New Font(family, size, fontStyle_Renamed)
			Catch e1 As ArgumentException
			End Try
		End Sub

		Private Sub richTextBox1_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles richTextBox1.SelectionChanged
			' update font control font
			If richTextBox1.SelectionFont IsNot Nothing Then
				_richFont.Family = richTextBox1.SelectionFont.FontFamily.Name
				_richFont.Size = CDec(richTextBox1.SelectionFont.Size)
				_richFont.Bold = If(richTextBox1.SelectionFont.Bold, FontProperties.Set, FontProperties.NotSet)
				_richFont.Italic = If(richTextBox1.SelectionFont.Italic, FontProperties.Set, FontProperties.NotSet)
				_richFont.Underline = If(richTextBox1.SelectionFont.Underline, FontUnderline.Set, FontUnderline.NotSet)
				_richFont.Strikethrough = If(richTextBox1.SelectionFont.Strikeout, FontProperties.Set, FontProperties.NotSet)
			Else
				_richFont.Family = String.Empty
				_richFont.Size = 0
				_richFont.Bold = FontProperties.NotAvailable
				_richFont.Italic = FontProperties.NotAvailable
				_richFont.Underline = FontUnderline.NotAvailable
				_richFont.Strikethrough = FontProperties.NotAvailable
			End If

			' update font control colors
			_richFont.ForegroundColor = richTextBox1.SelectionColor
			_richFont.BackgroundColor = richTextBox1.SelectionBackColor

			' update font control vertical positioning
			Select Case richTextBox1.SelectionCharOffset
				Case 0
					_richFont.VerticalPositioning = FontVerticalPosition.NotSet

				Case 10
					_richFont.VerticalPositioning = FontVerticalPosition.SuperScript

				Case -10
					_richFont.VerticalPositioning = FontVerticalPosition.SubScript
			End Select
		End Sub
	End Class
End Namespace
