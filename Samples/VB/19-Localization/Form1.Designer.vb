Namespace _19_Localization
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
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.label1 = New System.Windows.Forms.Label()
            Me._ribbonControl = New RibbonLib.Ribbon()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.label1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panel1.Location = New System.Drawing.Point(0, 118)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(501, 310)
            Me.panel1.TabIndex = 3
            '
            'label1
            '
            Me.label1.AutoSize = True
            Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.label1.Location = New System.Drawing.Point(0, 0)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(321, 13)
            Me.label1.TabIndex = 1
            Me.label1.Text = "Change CurrentThread Culture in Program.cs to specify localization"
            '
            '_ribbonControl
            '
            Me._ribbonControl.Location = New System.Drawing.Point(0, 0)
            Me._ribbonControl.Minimized = False
            Me._ribbonControl.Name = "_ribbonControl"
            Me._ribbonControl.ResourceName = "RibbonMarkup.ribbon"
            Me._ribbonControl.ShortcutTableResourceName = "RibbonMarkup.shortcuts"
            Me._ribbonControl.Size = New System.Drawing.Size(501, 118)
            Me._ribbonControl.TabIndex = 2
            '
            'Form1
            '
            Me.ClientSize = New System.Drawing.Size(501, 428)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me._ribbonControl)
            Me.Name = "Form1"
            Me.Text = "Form1"
            Me.panel1.ResumeLayout(False)
            Me.panel1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

		#End Region

		Private _ribbonControl As RibbonLib.Ribbon
		Private panel1 As Panel
		Private label1 As Label

	End Class
End Namespace

