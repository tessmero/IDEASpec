Partial Class DMK_v1
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents List1 As System.Windows.Forms.ListBox
    Public CommonDialog1Open As System.Windows.Forms.OpenFileDialog
	Public CommonDialog1Save As System.Windows.Forms.SaveFileDialog
    Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Script_Label As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Pesgo1 As Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray
	Public WithEvents Load_Script As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents dmk_exit As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents dmk_file As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents Run_DMK_Script As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents halt As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents Set_Actinic As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents LEDS_Off As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents skip_wait As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough(), System.Diagnostics.DebuggerNonUserCode()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DMK_v1))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.List1 = New System.Windows.Forms.ListBox()
        Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Script_Label = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Pesgo1 = New Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.dmk_file = New System.Windows.Forms.ToolStripMenuItem()
        Me.Load_Script = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestScriptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetbasefileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetAutoRunFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.dmk_exit = New System.Windows.Forms.ToolStripMenuItem()
        Me.Run_DMK_Script = New System.Windows.Forms.ToolStripMenuItem()
        Me.halt = New System.Windows.Forms.ToolStripMenuItem()
        Me.Set_Actinic = New System.Windows.Forms.ToolStripMenuItem()
        Me.LEDS_Off = New System.Windows.Forms.ToolStripMenuItem()
        Me.skip_wait = New System.Windows.Forms.ToolStripMenuItem()
        Me.TakeFluorBaselineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NotesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TakenoteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RecordeventsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ONToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OFFToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StirerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OnToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.OffToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SpecialHardwareToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FilterwheelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HomewheelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LaserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.servoform = New System.Windows.Forms.ToolStripMenuItem()
        Me.MonochromatorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CustomdataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Cd1ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.zg1 = New ZedGraph.ZedGraphControl()
        Me.zg3 = New ZedGraph.ZedGraphControl()
        Me.zg2 = New ZedGraph.ZedGraphControl()
        Me.zg4 = New ZedGraph.ZedGraphControl()
        Me.zg5 = New ZedGraph.ZedGraphControl()
        Me.zg6 = New ZedGraph.ZedGraphControl()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.Button10 = New System.Windows.Forms.Button()
        Me.SerialPort2 = New System.IO.Ports.SerialPort(Me.components)
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ProgressBar2 = New System.Windows.Forms.ProgressBar()
        Me.ProgressBar3 = New System.Windows.Forms.ProgressBar()
        Me.sample_gain_label = New System.Windows.Forms.Label()
        Me.reference_gain_label = New System.Windows.Forms.Label()
        Me.actinic_label = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.SerialPort3 = New System.IO.Ports.SerialPort(Me.components)
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lambdasettext = New System.Windows.Forms.TextBox()
        Me.LambdaText = New System.Windows.Forms.TextBox()
        Me.Button13 = New System.Windows.Forms.Button()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.mddir = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.movelin = New System.Windows.Forms.Button()
        Me.ProgressBar4 = New System.Windows.Forms.ProgressBar()
        Me.ProgressBar5 = New System.Windows.Forms.ProgressBar()
        Me.outfile_label = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        CType(Me.Pesgo1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainMenu1.SuspendLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'List1
        '
        Me.List1.BackColor = System.Drawing.SystemColors.Window
        Me.List1.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.List1, "List1")
        Me.List1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.List1.FormattingEnabled = True
        Me.List1.Name = "List1"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Name = "Label3"
        '
        'Script_Label
        '
        Me.Script_Label.BackColor = System.Drawing.SystemColors.Control
        Me.Script_Label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Script_Label.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.Script_Label, "Script_Label")
        Me.Script_Label.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Script_Label.Name = "Script_Label"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Name = "Label5"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Name = "Label2"
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.dmk_file, Me.Run_DMK_Script, Me.halt, Me.Set_Actinic, Me.LEDS_Off, Me.skip_wait, Me.TakeFluorBaselineToolStripMenuItem, Me.NotesToolStripMenuItem, Me.StirerToolStripMenuItem, Me.SpecialHardwareToolStripMenuItem, Me.CustomdataToolStripMenuItem})
        resources.ApplyResources(Me.MainMenu1, "MainMenu1")
        Me.MainMenu1.Name = "MainMenu1"
        '
        'dmk_file
        '
        Me.dmk_file.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Load_Script, Me.TestScriptToolStripMenuItem, Me.SetbasefileToolStripMenuItem, Me.SetAutoRunFileToolStripMenuItem, Me.dmk_exit})
        Me.dmk_file.Name = "dmk_file"
        resources.ApplyResources(Me.dmk_file, "dmk_file")
        '
        'Load_Script
        '
        Me.Load_Script.Name = "Load_Script"
        resources.ApplyResources(Me.Load_Script, "Load_Script")
        '
        'TestScriptToolStripMenuItem
        '
        Me.TestScriptToolStripMenuItem.Name = "TestScriptToolStripMenuItem"
        resources.ApplyResources(Me.TestScriptToolStripMenuItem, "TestScriptToolStripMenuItem")
        '
        'SetbasefileToolStripMenuItem
        '
        Me.SetbasefileToolStripMenuItem.Name = "SetbasefileToolStripMenuItem"
        resources.ApplyResources(Me.SetbasefileToolStripMenuItem, "SetbasefileToolStripMenuItem")
        '
        'SetAutoRunFileToolStripMenuItem
        '
        Me.SetAutoRunFileToolStripMenuItem.Name = "SetAutoRunFileToolStripMenuItem"
        resources.ApplyResources(Me.SetAutoRunFileToolStripMenuItem, "SetAutoRunFileToolStripMenuItem")
        '
        'dmk_exit
        '
        Me.dmk_exit.Name = "dmk_exit"
        resources.ApplyResources(Me.dmk_exit, "dmk_exit")
        '
        'Run_DMK_Script
        '
        Me.Run_DMK_Script.Name = "Run_DMK_Script"
        resources.ApplyResources(Me.Run_DMK_Script, "Run_DMK_Script")
        '
        'halt
        '
        Me.halt.Name = "halt"
        resources.ApplyResources(Me.halt, "halt")
        '
        'Set_Actinic
        '
        Me.Set_Actinic.Checked = True
        Me.Set_Actinic.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Set_Actinic.Name = "Set_Actinic"
        resources.ApplyResources(Me.Set_Actinic, "Set_Actinic")
        '
        'LEDS_Off
        '
        Me.LEDS_Off.Name = "LEDS_Off"
        resources.ApplyResources(Me.LEDS_Off, "LEDS_Off")
        '
        'skip_wait
        '
        Me.skip_wait.Name = "skip_wait"
        resources.ApplyResources(Me.skip_wait, "skip_wait")
        '
        'TakeFluorBaselineToolStripMenuItem
        '
        Me.TakeFluorBaselineToolStripMenuItem.Name = "TakeFluorBaselineToolStripMenuItem"
        resources.ApplyResources(Me.TakeFluorBaselineToolStripMenuItem, "TakeFluorBaselineToolStripMenuItem")
        '
        'NotesToolStripMenuItem
        '
        Me.NotesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TakenoteToolStripMenuItem, Me.RecordeventsToolStripMenuItem})
        Me.NotesToolStripMenuItem.Name = "NotesToolStripMenuItem"
        resources.ApplyResources(Me.NotesToolStripMenuItem, "NotesToolStripMenuItem")
        '
        'TakenoteToolStripMenuItem
        '
        Me.TakenoteToolStripMenuItem.Name = "TakenoteToolStripMenuItem"
        resources.ApplyResources(Me.TakenoteToolStripMenuItem, "TakenoteToolStripMenuItem")
        '
        'RecordeventsToolStripMenuItem
        '
        Me.RecordeventsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ONToolStripMenuItem, Me.OFFToolStripMenuItem})
        Me.RecordeventsToolStripMenuItem.Name = "RecordeventsToolStripMenuItem"
        resources.ApplyResources(Me.RecordeventsToolStripMenuItem, "RecordeventsToolStripMenuItem")
        '
        'ONToolStripMenuItem
        '
        Me.ONToolStripMenuItem.Name = "ONToolStripMenuItem"
        resources.ApplyResources(Me.ONToolStripMenuItem, "ONToolStripMenuItem")
        '
        'OFFToolStripMenuItem
        '
        Me.OFFToolStripMenuItem.Name = "OFFToolStripMenuItem"
        resources.ApplyResources(Me.OFFToolStripMenuItem, "OFFToolStripMenuItem")
        '
        'StirerToolStripMenuItem
        '
        Me.StirerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem1, Me.OffToolStripMenuItem1})
        Me.StirerToolStripMenuItem.Name = "StirerToolStripMenuItem"
        resources.ApplyResources(Me.StirerToolStripMenuItem, "StirerToolStripMenuItem")
        '
        'OnToolStripMenuItem1
        '
        Me.OnToolStripMenuItem1.Name = "OnToolStripMenuItem1"
        resources.ApplyResources(Me.OnToolStripMenuItem1, "OnToolStripMenuItem1")
        '
        'OffToolStripMenuItem1
        '
        Me.OffToolStripMenuItem1.Name = "OffToolStripMenuItem1"
        resources.ApplyResources(Me.OffToolStripMenuItem1, "OffToolStripMenuItem1")
        '
        'SpecialHardwareToolStripMenuItem
        '
        Me.SpecialHardwareToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilterwheelToolStripMenuItem, Me.HomewheelToolStripMenuItem, Me.LaserToolStripMenuItem, Me.servoform, Me.MonochromatorToolStripMenuItem})
        Me.SpecialHardwareToolStripMenuItem.Name = "SpecialHardwareToolStripMenuItem"
        resources.ApplyResources(Me.SpecialHardwareToolStripMenuItem, "SpecialHardwareToolStripMenuItem")
        '
        'FilterwheelToolStripMenuItem
        '
        Me.FilterwheelToolStripMenuItem.Name = "FilterwheelToolStripMenuItem"
        resources.ApplyResources(Me.FilterwheelToolStripMenuItem, "FilterwheelToolStripMenuItem")
        '
        'HomewheelToolStripMenuItem
        '
        Me.HomewheelToolStripMenuItem.Name = "HomewheelToolStripMenuItem"
        resources.ApplyResources(Me.HomewheelToolStripMenuItem, "HomewheelToolStripMenuItem")
        '
        'LaserToolStripMenuItem
        '
        Me.LaserToolStripMenuItem.Name = "LaserToolStripMenuItem"
        resources.ApplyResources(Me.LaserToolStripMenuItem, "LaserToolStripMenuItem")
        '
        'servoform
        '
        Me.servoform.Name = "servoform"
        resources.ApplyResources(Me.servoform, "servoform")
        '
        'MonochromatorToolStripMenuItem
        '
        Me.MonochromatorToolStripMenuItem.Name = "MonochromatorToolStripMenuItem"
        resources.ApplyResources(Me.MonochromatorToolStripMenuItem, "MonochromatorToolStripMenuItem")
        '
        'CustomdataToolStripMenuItem
        '
        Me.CustomdataToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Cd1ToolStripMenuItem, Me.MatToolStripMenuItem})
        Me.CustomdataToolStripMenuItem.Name = "CustomdataToolStripMenuItem"
        resources.ApplyResources(Me.CustomdataToolStripMenuItem, "CustomdataToolStripMenuItem")
        '
        'Cd1ToolStripMenuItem
        '
        Me.Cd1ToolStripMenuItem.Name = "Cd1ToolStripMenuItem"
        resources.ApplyResources(Me.Cd1ToolStripMenuItem, "Cd1ToolStripMenuItem")
        '
        'MatToolStripMenuItem
        '
        Me.MatToolStripMenuItem.Name = "MatToolStripMenuItem"
        resources.ApplyResources(Me.MatToolStripMenuItem, "MatToolStripMenuItem")
        '
        'zg1
        '
        Me.zg1.EditButtons = System.Windows.Forms.MouseButtons.Left
        resources.ApplyResources(Me.zg1, "zg1")
        Me.zg1.Name = "zg1"
        Me.zg1.PanModifierKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg1.ScrollGrace = 0.0R
        Me.zg1.ScrollMaxX = 0.0R
        Me.zg1.ScrollMaxY = 0.0R
        Me.zg1.ScrollMaxY2 = 0.0R
        Me.zg1.ScrollMinX = 0.0R
        Me.zg1.ScrollMinY = 0.0R
        Me.zg1.ScrollMinY2 = 0.0R
        '
        'zg3
        '
        Me.zg3.EditButtons = System.Windows.Forms.MouseButtons.Left
        resources.ApplyResources(Me.zg3, "zg3")
        Me.zg3.Name = "zg3"
        Me.zg3.PanModifierKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg3.ScrollGrace = 0.0R
        Me.zg3.ScrollMaxX = 0.0R
        Me.zg3.ScrollMaxY = 0.0R
        Me.zg3.ScrollMaxY2 = 0.0R
        Me.zg3.ScrollMinX = 0.0R
        Me.zg3.ScrollMinY = 0.0R
        Me.zg3.ScrollMinY2 = 0.0R
        '
        'zg2
        '
        Me.zg2.EditButtons = System.Windows.Forms.MouseButtons.Left
        resources.ApplyResources(Me.zg2, "zg2")
        Me.zg2.Name = "zg2"
        Me.zg2.PanModifierKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg2.ScrollGrace = 0.0R
        Me.zg2.ScrollMaxX = 0.0R
        Me.zg2.ScrollMaxY = 0.0R
        Me.zg2.ScrollMaxY2 = 0.0R
        Me.zg2.ScrollMinX = 0.0R
        Me.zg2.ScrollMinY = 0.0R
        Me.zg2.ScrollMinY2 = 0.0R
        '
        'zg4
        '
        Me.zg4.EditButtons = System.Windows.Forms.MouseButtons.Left
        resources.ApplyResources(Me.zg4, "zg4")
        Me.zg4.Name = "zg4"
        Me.zg4.PanModifierKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg4.ScrollGrace = 0.0R
        Me.zg4.ScrollMaxX = 0.0R
        Me.zg4.ScrollMaxY = 0.0R
        Me.zg4.ScrollMaxY2 = 0.0R
        Me.zg4.ScrollMinX = 0.0R
        Me.zg4.ScrollMinY = 0.0R
        Me.zg4.ScrollMinY2 = 0.0R
        '
        'zg5
        '
        Me.zg5.EditButtons = System.Windows.Forms.MouseButtons.Left
        resources.ApplyResources(Me.zg5, "zg5")
        Me.zg5.Name = "zg5"
        Me.zg5.PanModifierKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg5.ScrollGrace = 0.0R
        Me.zg5.ScrollMaxX = 0.0R
        Me.zg5.ScrollMaxY = 0.0R
        Me.zg5.ScrollMaxY2 = 0.0R
        Me.zg5.ScrollMinX = 0.0R
        Me.zg5.ScrollMinY = 0.0R
        Me.zg5.ScrollMinY2 = 0.0R
        '
        'zg6
        '
        Me.zg6.EditButtons = System.Windows.Forms.MouseButtons.Left
        resources.ApplyResources(Me.zg6, "zg6")
        Me.zg6.Name = "zg6"
        Me.zg6.PanModifierKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg6.ScrollGrace = 0.0R
        Me.zg6.ScrollMaxX = 0.0R
        Me.zg6.ScrollMaxY = 0.0R
        Me.zg6.ScrollMaxY2 = 0.0R
        Me.zg6.ScrollMinX = 0.0R
        Me.zg6.ScrollMinY = 0.0R
        Me.zg6.ScrollMinY2 = 0.0R
        '
        'Button7
        '
        resources.ApplyResources(Me.Button7, "Button7")
        Me.Button7.Name = "Button7"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'TextBox1
        '
        resources.ApplyResources(Me.TextBox1, "TextBox1")
        Me.TextBox1.Name = "TextBox1"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'Button8
        '
        resources.ApplyResources(Me.Button8, "Button8")
        Me.Button8.Name = "Button8"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'TrackBar1
        '
        resources.ApplyResources(Me.TrackBar1, "TrackBar1")
        Me.TrackBar1.Maximum = 500
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Value = 230
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'Button9
        '
        resources.ApplyResources(Me.Button9, "Button9")
        Me.Button9.Name = "Button9"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button10
        '
        resources.ApplyResources(Me.Button10, "Button10")
        Me.Button10.Name = "Button10"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        resources.ApplyResources(Me.ProgressBar1, "ProgressBar1")
        Me.ProgressBar1.Maximum = 8
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Value = 2
        '
        'ProgressBar2
        '
        resources.ApplyResources(Me.ProgressBar2, "ProgressBar2")
        Me.ProgressBar2.Maximum = 8
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.Value = 1
        '
        'ProgressBar3
        '
        Me.ProgressBar3.ForeColor = System.Drawing.Color.Red
        resources.ApplyResources(Me.ProgressBar3, "ProgressBar3")
        Me.ProgressBar3.Maximum = 255
        Me.ProgressBar3.Name = "ProgressBar3"
        '
        'sample_gain_label
        '
        resources.ApplyResources(Me.sample_gain_label, "sample_gain_label")
        Me.sample_gain_label.Name = "sample_gain_label"
        '
        'reference_gain_label
        '
        resources.ApplyResources(Me.reference_gain_label, "reference_gain_label")
        Me.reference_gain_label.Name = "reference_gain_label"
        '
        'actinic_label
        '
        resources.ApplyResources(Me.actinic_label, "actinic_label")
        Me.actinic_label.Name = "actinic_label"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        '
        'Button11
        '
        resources.ApplyResources(Me.Button11, "Button11")
        Me.Button11.Name = "Button11"
        Me.Button11.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'TextBox2
        '
        resources.ApplyResources(Me.TextBox2, "TextBox2")
        Me.TextBox2.Name = "TextBox2"
        '
        'TextBox3
        '
        resources.ApplyResources(Me.TextBox3, "TextBox3")
        Me.TextBox3.Name = "TextBox3"
        '
        'Button12
        '
        resources.ApplyResources(Me.Button12, "Button12")
        Me.Button12.Name = "Button12"
        Me.Button12.UseVisualStyleBackColor = True
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.Name = "Label12"
        '
        'lambdasettext
        '
        resources.ApplyResources(Me.lambdasettext, "lambdasettext")
        Me.lambdasettext.Name = "lambdasettext"
        '
        'LambdaText
        '
        resources.ApplyResources(Me.LambdaText, "LambdaText")
        Me.LambdaText.Name = "LambdaText"
        '
        'Button13
        '
        resources.ApplyResources(Me.Button13, "Button13")
        Me.Button13.Name = "Button13"
        Me.Button13.UseVisualStyleBackColor = True
        '
        'Button14
        '
        resources.ApplyResources(Me.Button14, "Button14")
        Me.Button14.Name = "Button14"
        Me.Button14.UseVisualStyleBackColor = True
        '
        'mddir
        '
        resources.ApplyResources(Me.mddir, "mddir")
        Me.mddir.Name = "mddir"
        '
        'Label13
        '
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.Name = "Label13"
        '
        'movelin
        '
        resources.ApplyResources(Me.movelin, "movelin")
        Me.movelin.Name = "movelin"
        Me.movelin.UseVisualStyleBackColor = True
        '
        'ProgressBar4
        '
        resources.ApplyResources(Me.ProgressBar4, "ProgressBar4")
        Me.ProgressBar4.Maximum = 8
        Me.ProgressBar4.Name = "ProgressBar4"
        Me.ProgressBar4.Value = 1
        '
        'ProgressBar5
        '
        resources.ApplyResources(Me.ProgressBar5, "ProgressBar5")
        Me.ProgressBar5.Maximum = 8
        Me.ProgressBar5.Name = "ProgressBar5"
        Me.ProgressBar5.Value = 2
        '
        'outfile_label
        '
        Me.outfile_label.BackColor = System.Drawing.SystemColors.Control
        Me.outfile_label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.outfile_label.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.outfile_label, "outfile_label")
        Me.outfile_label.ForeColor = System.Drawing.SystemColors.ControlText
        Me.outfile_label.Name = "outfile_label"
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.Name = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox4
        '
        resources.ApplyResources(Me.TextBox4, "TextBox4")
        Me.TextBox4.Name = "TextBox4"
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.Name = "Label14"
        '
        'DMK_v1
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.outfile_label)
        Me.Controls.Add(Me.ProgressBar4)
        Me.Controls.Add(Me.ProgressBar5)
        Me.Controls.Add(Me.movelin)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.mddir)
        Me.Controls.Add(Me.Button14)
        Me.Controls.Add(Me.Button13)
        Me.Controls.Add(Me.LambdaText)
        Me.Controls.Add(Me.lambdasettext)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Button12)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Button11)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.actinic_label)
        Me.Controls.Add(Me.reference_gain_label)
        Me.Controls.Add(Me.sample_gain_label)
        Me.Controls.Add(Me.ProgressBar3)
        Me.Controls.Add(Me.ProgressBar2)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TrackBar1)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.zg6)
        Me.Controls.Add(Me.zg5)
        Me.Controls.Add(Me.zg4)
        Me.Controls.Add(Me.zg2)
        Me.Controls.Add(Me.zg3)
        Me.Controls.Add(Me.zg1)
        Me.Controls.Add(Me.List1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Script_Label)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "DMK_v1"
        CType(Me.Pesgo1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents zg1 As ZedGraph.ZedGraphControl
    Friend WithEvents zg3 As ZedGraph.ZedGraphControl
    Friend WithEvents zg2 As ZedGraph.ZedGraphControl
    Friend WithEvents zg4 As ZedGraph.ZedGraphControl
    Friend WithEvents zg5 As ZedGraph.ZedGraphControl
    Friend WithEvents zg6 As ZedGraph.ZedGraphControl
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents SerialPort2 As System.IO.Ports.SerialPort
    Friend WithEvents TakeFluorBaselineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents ProgressBar2 As System.Windows.Forms.ProgressBar
    Friend WithEvents ProgressBar3 As System.Windows.Forms.ProgressBar
    Friend WithEvents NotesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TakenoteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetbasefileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RecordeventsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ONToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OFFToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sample_gain_label As System.Windows.Forms.Label
    Friend WithEvents reference_gain_label As System.Windows.Forms.Label
    Friend WithEvents actinic_label As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents StirerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SpecialHardwareToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterwheelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HomewheelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LaserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents servoform As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TestScriptToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents SetAutoRunFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents CustomdataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Cd1ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MatToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents SerialPort3 As System.IO.Ports.SerialPort
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lambdasettext As System.Windows.Forms.TextBox
    Friend WithEvents LambdaText As System.Windows.Forms.TextBox
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents mddir As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents movelin As System.Windows.Forms.Button
    Friend WithEvents ProgressBar4 As System.Windows.Forms.ProgressBar
    Friend WithEvents ProgressBar5 As System.Windows.Forms.ProgressBar
    Public WithEvents outfile_label As System.Windows.Forms.Label
    Friend WithEvents MonochromatorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
#End Region
End Class