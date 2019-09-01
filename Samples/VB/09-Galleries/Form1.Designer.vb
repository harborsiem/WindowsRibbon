Namespace _09_Galleries
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
            Me.imageListLines = New System.Windows.Forms.ImageList(Me.components)
            Me.imageListBrushes = New System.Windows.Forms.ImageList(Me.components)
            Me.imageListShapes = New System.Windows.Forms.ImageList(Me.components)
            Me._ribbon = New RibbonLib.Ribbon()
            Me.SuspendLayout()
            '
            'imageListLines
            '
            Me.imageListLines.ImageStream = CType(resources.GetObject("imageListLines.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageListLines.TransparentColor = System.Drawing.Color.Gray
            Me.imageListLines.Images.SetKeyName(0, "line01.bmp")
            Me.imageListLines.Images.SetKeyName(1, "line02.bmp")
            Me.imageListLines.Images.SetKeyName(2, "line03.bmp")
            Me.imageListLines.Images.SetKeyName(3, "line04.bmp")
            Me.imageListLines.Images.SetKeyName(4, "line05.bmp")
            Me.imageListLines.Images.SetKeyName(5, "line06.bmp")
            Me.imageListLines.Images.SetKeyName(6, "line07.bmp")
            Me.imageListLines.Images.SetKeyName(7, "line08.bmp")
            Me.imageListLines.Images.SetKeyName(8, "line09.bmp")
            Me.imageListLines.Images.SetKeyName(9, "line10.bmp")
            Me.imageListLines.Images.SetKeyName(10, "line11.bmp")
            Me.imageListLines.Images.SetKeyName(11, "line12.bmp")
            '
            'imageListBrushes
            '
            Me.imageListBrushes.ImageStream = CType(resources.GetObject("imageListBrushes.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageListBrushes.TransparentColor = System.Drawing.Color.Gray
            Me.imageListBrushes.Images.SetKeyName(0, "brush1.bmp")
            Me.imageListBrushes.Images.SetKeyName(1, "brush2.bmp")
            Me.imageListBrushes.Images.SetKeyName(2, "brush3.bmp")
            Me.imageListBrushes.Images.SetKeyName(3, "brush4.bmp")
            Me.imageListBrushes.Images.SetKeyName(4, "brush5.bmp")
            Me.imageListBrushes.Images.SetKeyName(5, "brush6.bmp")
            Me.imageListBrushes.Images.SetKeyName(6, "brush7.bmp")
            Me.imageListBrushes.Images.SetKeyName(7, "brush8.bmp")
            Me.imageListBrushes.Images.SetKeyName(8, "brush9.bmp")
            '
            'imageListShapes
            '
            Me.imageListShapes.ImageStream = CType(resources.GetObject("imageListShapes.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageListShapes.TransparentColor = System.Drawing.Color.Gray
            Me.imageListShapes.Images.SetKeyName(0, "shape01.bmp")
            Me.imageListShapes.Images.SetKeyName(1, "shape02.bmp")
            Me.imageListShapes.Images.SetKeyName(2, "shape03.bmp")
            Me.imageListShapes.Images.SetKeyName(3, "shape04.bmp")
            Me.imageListShapes.Images.SetKeyName(4, "shape05.bmp")
            Me.imageListShapes.Images.SetKeyName(5, "shape06.bmp")
            Me.imageListShapes.Images.SetKeyName(6, "shape07.bmp")
            Me.imageListShapes.Images.SetKeyName(7, "shape08.bmp")
            Me.imageListShapes.Images.SetKeyName(8, "shape09.bmp")
            Me.imageListShapes.Images.SetKeyName(9, "shape10.bmp")
            Me.imageListShapes.Images.SetKeyName(10, "shape11.bmp")
            Me.imageListShapes.Images.SetKeyName(11, "shape12.bmp")
            Me.imageListShapes.Images.SetKeyName(12, "shape13.bmp")
            Me.imageListShapes.Images.SetKeyName(13, "shape14.bmp")
            Me.imageListShapes.Images.SetKeyName(14, "shape15.bmp")
            Me.imageListShapes.Images.SetKeyName(15, "shape16.bmp")
            Me.imageListShapes.Images.SetKeyName(16, "shape17.bmp")
            Me.imageListShapes.Images.SetKeyName(17, "shape18.bmp")
            Me.imageListShapes.Images.SetKeyName(18, "shape19.bmp")
            Me.imageListShapes.Images.SetKeyName(19, "shape20.bmp")
            Me.imageListShapes.Images.SetKeyName(20, "shape21.bmp")
            Me.imageListShapes.Images.SetKeyName(21, "shape22.bmp")
            Me.imageListShapes.Images.SetKeyName(22, "shape23.bmp")
            '
            '_ribbon
            '
            Me._ribbon.Location = New System.Drawing.Point(0, 0)
            Me._ribbon.Minimized = False
            Me._ribbon.Name = "_ribbon"
            Me._ribbon.ResourceName = "RibbonMarkup.ribbon"
            Me._ribbon.ShortcutTableResourceName = Nothing
            Me._ribbon.Size = New System.Drawing.Size(501, 100)
            Me._ribbon.TabIndex = 4
            '
            'Form1
            '
            Me.ClientSize = New System.Drawing.Size(501, 428)
            Me.Controls.Add(Me._ribbon)
            Me.Name = "Form1"
            Me.Text = "Form1"
            Me.ResumeLayout(False)

        End Sub

		#End Region

        Private imageListBrushes As ImageList
		Private imageListShapes As ImageList
        Private _ribbon As RibbonLib.Ribbon
        Private WithEvents imageListLines As System.Windows.Forms.ImageList
    End Class
End Namespace

