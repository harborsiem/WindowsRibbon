Namespace _15_ContextPopup
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
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel3 = New System.Windows.Forms.Panel()
            Me._ribbon = New RibbonLib.Ribbon()
            Me.panel3.SuspendLayout()
            Me.SuspendLayout()
            '
            'panel2
            '
            Me.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.panel2.Location = New System.Drawing.Point(318, 16)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(124, 106)
            Me.panel2.TabIndex = 2
            '
            'panel1
            '
            Me.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.panel1.Location = New System.Drawing.Point(20, 16)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(121, 106)
            Me.panel1.TabIndex = 1
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.panel2)
            Me.panel3.Controls.Add(Me.panel1)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panel3.Location = New System.Drawing.Point(0, 100)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(501, 177)
            Me.panel3.TabIndex = 1
            '
            '_ribbon
            '
            Me._ribbon.Location = New System.Drawing.Point(0, 0)
            Me._ribbon.Minimized = False
            Me._ribbon.Name = "_ribbon"
            Me._ribbon.ResourceName = "RibbonMarkup.ribbon"
            Me._ribbon.ShortcutTableResourceName = Nothing
            Me._ribbon.Size = New System.Drawing.Size(501, 100)
            Me._ribbon.TabIndex = 5
            '
            'Form1
            '
            Me.ClientSize = New System.Drawing.Size(501, 277)
            Me.Controls.Add(Me.panel3)
            Me.Controls.Add(Me._ribbon)
            Me.Name = "Form1"
            Me.Text = "Form1"
            Me.panel3.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

		#End Region

		Private panel1 As Panel
		Private panel2 As Panel
		Private panel3 As Panel
		Private _ribbon As RibbonLib.Ribbon
	End Class
End Namespace

