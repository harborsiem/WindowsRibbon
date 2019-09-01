Namespace _06_ComboBox
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
            Me._ribbon = New RibbonLib.Ribbon()
            Me.SuspendLayout()
            '
            '_ribbon
            '
            Me._ribbon.Location = New System.Drawing.Point(0, 0)
            Me._ribbon.Minimized = False
            Me._ribbon.Name = "_ribbon"
            Me._ribbon.ResourceName = "RibbonMarkup.ribbon"
            Me._ribbon.ShortcutTableResourceName = Nothing
            Me._ribbon.Size = New System.Drawing.Size(712, 100)
            Me._ribbon.TabIndex = 2
            '
            'Form1
            '
            Me.ClientSize = New System.Drawing.Size(712, 428)
            Me.Controls.Add(Me._ribbon)
            Me.Name = "Form1"
            Me.Text = "Form1"
            Me.ResumeLayout(False)

        End Sub

		#End Region

		Private _ribbon As RibbonLib.Ribbon

	End Class
End Namespace

