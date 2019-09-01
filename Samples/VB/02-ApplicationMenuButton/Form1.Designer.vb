Namespace _02_ApplicationMenuButton
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
            Me.button3 = New System.Windows.Forms.Button()
            Me.button2 = New System.Windows.Forms.Button()
            Me.button1 = New System.Windows.Forms.Button()
            Me._ribbon = New RibbonLib.Ribbon()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'button3
            '
            Me.button3.Location = New System.Drawing.Point(165, 3)
            Me.button3.Name = "button3"
            Me.button3.Size = New System.Drawing.Size(75, 23)
            Me.button3.TabIndex = 2
            Me.button3.Text = "button3"
            Me.button3.UseVisualStyleBackColor = True
            '
            'button2
            '
            Me.button2.Location = New System.Drawing.Point(84, 3)
            Me.button2.Name = "button2"
            Me.button2.Size = New System.Drawing.Size(75, 23)
            Me.button2.TabIndex = 1
            Me.button2.Text = "button2"
            Me.button2.UseVisualStyleBackColor = True
            '
            'button1
            '
            Me.button1.Location = New System.Drawing.Point(3, 3)
            Me.button1.Name = "button1"
            Me.button1.Size = New System.Drawing.Size(75, 23)
            Me.button1.TabIndex = 0
            Me.button1.Text = "button1"
            Me.button1.UseVisualStyleBackColor = True
            '
            '_ribbon
            '
            Me._ribbon.Location = New System.Drawing.Point(0, 0)
            Me._ribbon.Minimized = False
            Me._ribbon.Name = "_ribbon"
            Me._ribbon.ResourceName = "RibbonMarkup.ribbon"
            Me._ribbon.ShortcutTableResourceName = Nothing
            Me._ribbon.Size = New System.Drawing.Size(407, 100)
            Me._ribbon.TabIndex = 2
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.button3)
            Me.panel1.Controls.Add(Me.button1)
            Me.panel1.Controls.Add(Me.button2)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panel1.Location = New System.Drawing.Point(0, 100)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(407, 281)
            Me.panel1.TabIndex = 3
            '
            'Form1
            '
            Me.ClientSize = New System.Drawing.Size(407, 381)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me._ribbon)
            Me.Name = "Form1"
            Me.Text = "Form1"
            Me.panel1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

		#End Region

		Private button3 As Button
		Private button2 As Button
		Private button1 As Button
		Private _ribbon As RibbonLib.Ribbon
		Private panel1 As Panel
	End Class
End Namespace

