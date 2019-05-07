<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form3
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim SerialPort1 As System.IO.Ports.SerialPort
        Me.comPort_ComboBox = New System.Windows.Forms.ComboBox()
        Me.moveSteps = New System.Windows.Forms.TextBox()
        Me.moveButton = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.posLabel = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lambdaLabel = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.moveTo = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.engagedCheck = New System.Windows.Forms.CheckBox()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.linPosNow = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.linPosToDo = New System.Windows.Forms.Button()
        Me.linPosTo = New System.Windows.Forms.TextBox()
        Me.linMoveRelDo = New System.Windows.Forms.Button()
        Me.linMoveRel = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.setLinIndex = New System.Windows.Forms.TextBox()
        Me.setLinIndexDo = New System.Windows.Forms.Button()
        Me.LinMoveToIndexDo = New System.Windows.Forms.Button()
        Me.linMoveToIndexIndex = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.set_mono_pos = New System.Windows.Forms.TextBox()
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.SuspendLayout()
        '
        'SerialPort1
        '
        SerialPort1.DiscardNull = True
        '
        'comPort_ComboBox
        '
        Me.comPort_ComboBox.FormattingEnabled = True
        Me.comPort_ComboBox.Location = New System.Drawing.Point(106, 88)
        Me.comPort_ComboBox.Name = "comPort_ComboBox"
        Me.comPort_ComboBox.Size = New System.Drawing.Size(91, 21)
        Me.comPort_ComboBox.TabIndex = 1
        '
        'moveSteps
        '
        Me.moveSteps.Location = New System.Drawing.Point(85, 251)
        Me.moveSteps.Name = "moveSteps"
        Me.moveSteps.Size = New System.Drawing.Size(54, 20)
        Me.moveSteps.TabIndex = 13
        Me.moveSteps.Text = "0"
        '
        'moveButton
        '
        Me.moveButton.Location = New System.Drawing.Point(18, 247)
        Me.moveButton.Name = "moveButton"
        Me.moveButton.Size = New System.Drawing.Size(59, 27)
        Me.moveButton.TabIndex = 14
        Me.moveButton.Text = "move"
        Me.moveButton.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(145, 254)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(126, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "steps (+ for cw, - for ccw)"
        '
        'posLabel
        '
        Me.posLabel.AutoSize = True
        Me.posLabel.Location = New System.Drawing.Point(145, 285)
        Me.posLabel.Name = "posLabel"
        Me.posLabel.Size = New System.Drawing.Size(13, 13)
        Me.posLabel.TabIndex = 17
        Me.posLabel.Text = "0"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 285)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "position (steps) :"
        '
        'lambdaLabel
        '
        Me.lambdaLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.lambdaLabel.AutoSize = True
        Me.lambdaLabel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.lambdaLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lambdaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lambdaLabel.Location = New System.Drawing.Point(85, 19)
        Me.lambdaLabel.Name = "lambdaLabel"
        Me.lambdaLabel.Size = New System.Drawing.Size(100, 39)
        Me.lambdaLabel.TabIndex = 19
        Me.lambdaLabel.Text = "500.0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(14, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 29)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "now:"
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(13, 143)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(86, 33)
        Me.Button2.TabIndex = 22
        Me.Button2.Text = "move to:"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'moveTo
        '
        Me.moveTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.moveTo.Location = New System.Drawing.Point(106, 141)
        Me.moveTo.Name = "moveTo"
        Me.moveTo.Size = New System.Drawing.Size(84, 35)
        Me.moveTo.TabIndex = 23
        Me.moveTo.Text = "500.0"
        Me.moveTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.moveTo.UseWaitCursor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(196, 279)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(160, 25)
        Me.Button3.TabIndex = 24
        Me.Button3.Text = "set monochromator position to:"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'engagedCheck
        '
        Me.engagedCheck.AutoSize = True
        Me.engagedCheck.Location = New System.Drawing.Point(18, 197)
        Me.engagedCheck.Name = "engagedCheck"
        Me.engagedCheck.Size = New System.Drawing.Size(159, 17)
        Me.engagedCheck.TabIndex = 25
        Me.engagedCheck.Text = "disengage when not moving"
        Me.engagedCheck.UseVisualStyleBackColor = True
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(191, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 29)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "nm"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(9, 86)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 20)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "connection:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(196, 143)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 29)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "nm"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(308, 75)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(48, 30)
        Me.Button1.TabIndex = 29
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(307, 134)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(65, 41)
        Me.Button4.TabIndex = 30
        Me.Button4.Text = "Button4"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'linPosNow
        '
        Me.linPosNow.AutoSize = True
        Me.linPosNow.BackColor = System.Drawing.SystemColors.Highlight
        Me.linPosNow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.linPosNow.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linPosNow.Location = New System.Drawing.Point(685, 19)
        Me.linPosNow.Name = "linPosNow"
        Me.linPosNow.Size = New System.Drawing.Size(109, 39)
        Me.linPosNow.TabIndex = 31
        Me.linPosNow.Text = "        0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(485, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(184, 29)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = "current position:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(808, 25)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(71, 29)
        Me.Label9.TabIndex = 33
        Me.Label9.Text = "steps"
        '
        'linPosToDo
        '
        Me.linPosToDo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linPosToDo.Location = New System.Drawing.Point(486, 100)
        Me.linPosToDo.Name = "linPosToDo"
        Me.linPosToDo.Size = New System.Drawing.Size(183, 33)
        Me.linPosToDo.TabIndex = 34
        Me.linPosToDo.Text = "move to position:"
        Me.linPosToDo.UseVisualStyleBackColor = True
        '
        'linPosTo
        '
        Me.linPosTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linPosTo.Location = New System.Drawing.Point(689, 100)
        Me.linPosTo.Name = "linPosTo"
        Me.linPosTo.Size = New System.Drawing.Size(84, 35)
        Me.linPosTo.TabIndex = 35
        Me.linPosTo.Text = "0"
        Me.linPosTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.linPosTo.UseWaitCursor = True
        '
        'linMoveRelDo
        '
        Me.linMoveRelDo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linMoveRelDo.Location = New System.Drawing.Point(486, 200)
        Me.linMoveRelDo.Name = "linMoveRelDo"
        Me.linMoveRelDo.Size = New System.Drawing.Size(181, 43)
        Me.linMoveRelDo.TabIndex = 36
        Me.linMoveRelDo.Text = "move steps relative"
        Me.linMoveRelDo.UseVisualStyleBackColor = True
        '
        'linMoveRel
        '
        Me.linMoveRel.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linMoveRel.Location = New System.Drawing.Point(689, 197)
        Me.linMoveRel.Name = "linMoveRel"
        Me.linMoveRel.Size = New System.Drawing.Size(84, 35)
        Me.linMoveRel.TabIndex = 37
        Me.linMoveRel.Text = "0"
        Me.linMoveRel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(810, 100)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 29)
        Me.Label10.TabIndex = 38
        Me.Label10.Text = "steps"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(801, 200)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(71, 29)
        Me.Label11.TabIndex = 39
        Me.Label11.Text = "steps"
        '
        'setLinIndex
        '
        Me.setLinIndex.Location = New System.Drawing.Point(692, 278)
        Me.setLinIndex.Name = "setLinIndex"
        Me.setLinIndex.Size = New System.Drawing.Size(41, 20)
        Me.setLinIndex.TabIndex = 40
        Me.setLinIndex.Text = "0"
        '
        'setLinIndexDo
        '
        Me.setLinIndexDo.Location = New System.Drawing.Point(486, 272)
        Me.setLinIndexDo.Name = "setLinIndexDo"
        Me.setLinIndexDo.Size = New System.Drawing.Size(200, 31)
        Me.setLinIndexDo.TabIndex = 41
        Me.setLinIndexDo.Text = "Set Current Position as index"
        Me.setLinIndexDo.UseVisualStyleBackColor = True
        '
        'LinMoveToIndexDo
        '
        Me.LinMoveToIndexDo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinMoveToIndexDo.Location = New System.Drawing.Point(486, 145)
        Me.LinMoveToIndexDo.Name = "LinMoveToIndexDo"
        Me.LinMoveToIndexDo.Size = New System.Drawing.Size(183, 33)
        Me.LinMoveToIndexDo.TabIndex = 42
        Me.LinMoveToIndexDo.Text = "move to index:"
        Me.LinMoveToIndexDo.UseVisualStyleBackColor = True
        '
        'linMoveToIndexIndex
        '
        Me.linMoveToIndexIndex.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linMoveToIndexIndex.Location = New System.Drawing.Point(689, 145)
        Me.linMoveToIndexIndex.Name = "linMoveToIndexIndex"
        Me.linMoveToIndexIndex.Size = New System.Drawing.Size(84, 35)
        Me.linMoveToIndexIndex.TabIndex = 43
        Me.linMoveToIndexIndex.Text = "0"
        Me.linMoveToIndexIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.linMoveToIndexIndex.UseWaitCursor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(810, 151)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(140, 29)
        Me.Label7.TabIndex = 44
        Me.Label7.Text = "(index 0-19)"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(739, 278)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(150, 20)
        Me.Label12.TabIndex = 45
        Me.Label12.Text = "(index number 0-19)"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(303, 232)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(102, 20)
        Me.TextBox1.TabIndex = 46
        '
        'set_mono_pos
        '
        Me.set_mono_pos.Location = New System.Drawing.Point(362, 282)
        Me.set_mono_pos.Name = "set_mono_pos"
        Me.set_mono_pos.Size = New System.Drawing.Size(75, 20)
        Me.set_mono_pos.TabIndex = 47
        Me.set_mono_pos.Text = "500.0"
        Me.set_mono_pos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Timer3
        '
        Me.Timer3.Interval = 1000
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1003, 315)
        Me.Controls.Add(Me.set_mono_pos)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.linMoveToIndexIndex)
        Me.Controls.Add(Me.LinMoveToIndexDo)
        Me.Controls.Add(Me.setLinIndexDo)
        Me.Controls.Add(Me.setLinIndex)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.linMoveRel)
        Me.Controls.Add(Me.linMoveRelDo)
        Me.Controls.Add(Me.linPosTo)
        Me.Controls.Add(Me.linPosToDo)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.linPosNow)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.engagedCheck)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.moveTo)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lambdaLabel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.posLabel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.moveButton)
        Me.Controls.Add(Me.moveSteps)
        Me.Controls.Add(Me.comPort_ComboBox)
        Me.Name = "Form3"
        Me.Text = "Lambda Controller"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents comPort_ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents moveSteps As System.Windows.Forms.TextBox
    Friend WithEvents moveButton As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents posLabel As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents moveTo As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents engagedCheck As System.Windows.Forms.CheckBox
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents linPosNow As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents linPosToDo As System.Windows.Forms.Button
    Friend WithEvents linPosTo As System.Windows.Forms.TextBox
    Friend WithEvents linMoveRelDo As System.Windows.Forms.Button
    Friend WithEvents linMoveRel As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Protected WithEvents lambdaLabel As System.Windows.Forms.Label
    Friend WithEvents setLinIndex As System.Windows.Forms.TextBox
    Friend WithEvents setLinIndexDo As System.Windows.Forms.Button
    Friend WithEvents LinMoveToIndexDo As System.Windows.Forms.Button
    Friend WithEvents linMoveToIndexIndex As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents set_mono_pos As System.Windows.Forms.TextBox
    Friend WithEvents Timer3 As System.Windows.Forms.Timer
End Class
