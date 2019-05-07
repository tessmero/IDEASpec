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
	Public WithEvents servoform As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough(), System.Diagnostics.DebuggerNonUserCode()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.List1 = New System.Windows.Forms.ListBox
        Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog
        Me.Label3 = New System.Windows.Forms.Label
        Me.Script_Label = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Pesgo1 = New Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip
        Me.dmk_file = New System.Windows.Forms.ToolStripMenuItem
        Me.Load_Script = New System.Windows.Forms.ToolStripMenuItem
        Me.SetbasefileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.dmk_exit = New System.Windows.Forms.ToolStripMenuItem
        Me.Run_DMK_Script = New System.Windows.Forms.ToolStripMenuItem
        Me.halt = New System.Windows.Forms.ToolStripMenuItem
        Me.Set_Actinic = New System.Windows.Forms.ToolStripMenuItem
        Me.LEDS_Off = New System.Windows.Forms.ToolStripMenuItem
        Me.skip_wait = New System.Windows.Forms.ToolStripMenuItem
        Me.servoform = New System.Windows.Forms.ToolStripMenuItem
        Me.HomewheelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LaserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterwheelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TakeFluorBaselineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NotesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TakenoteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RecordeventsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ONToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OFFToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StirerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.zg1 = New ZedGraph.ZedGraphControl
        Me.zg3 = New ZedGraph.ZedGraphControl
        Me.zg2 = New ZedGraph.ZedGraphControl
        Me.zg4 = New ZedGraph.ZedGraphControl
        Me.zg5 = New ZedGraph.ZedGraphControl
        Me.zg6 = New ZedGraph.ZedGraphControl
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.Button7 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Button8 = New System.Windows.Forms.Button
        Me.TrackBar1 = New System.Windows.Forms.TrackBar
        Me.Label6 = New System.Windows.Forms.Label
        Me.Button9 = New System.Windows.Forms.Button
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.Button10 = New System.Windows.Forms.Button
        Me.SerialPort2 = New System.IO.Ports.SerialPort(Me.components)
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.ProgressBar2 = New System.Windows.Forms.ProgressBar
        Me.ProgressBar3 = New System.Windows.Forms.ProgressBar
        Me.sample_gain_label = New System.Windows.Forms.Label
        Me.reference_gain_label = New System.Windows.Forms.Label
        Me.actinic_label = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.gain_data_list = New System.Windows.Forms.ListBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        CType(Me.Pesgo1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainMenu1.SuspendLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'List1
        '
        Me.List1.BackColor = System.Drawing.SystemColors.Window
        Me.List1.Cursor = System.Windows.Forms.Cursors.Default
        Me.List1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.List1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.List1.FormattingEnabled = True
        Me.List1.ItemHeight = 14
        Me.List1.Location = New System.Drawing.Point(753, 695)
        Me.List1.Name = "List1"
        Me.List1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.List1.Size = New System.Drawing.Size(263, 46)
        Me.List1.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 13.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(567, 698)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(81, 33)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Waiting:"
        '
        'Script_Label
        '
        Me.Script_Label.BackColor = System.Drawing.SystemColors.Control
        Me.Script_Label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Script_Label.Cursor = System.Windows.Forms.Cursors.Default
        Me.Script_Label.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Script_Label.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Script_Label.Location = New System.Drawing.Point(0, 707)
        Me.Script_Label.Name = "Script_Label"
        Me.Script_Label.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Script_Label.Size = New System.Drawing.Size(264, 25)
        Me.Script_Label.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(-3, 685)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(153, 17)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Current Script (click to load):"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(654, 696)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(65, 33)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "0"
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.dmk_file, Me.Run_DMK_Script, Me.halt, Me.Set_Actinic, Me.LEDS_Off, Me.skip_wait, Me.servoform, Me.HomewheelToolStripMenuItem, Me.LaserToolStripMenuItem, Me.FilterwheelToolStripMenuItem, Me.TakeFluorBaselineToolStripMenuItem, Me.NotesToolStripMenuItem, Me.StirerToolStripMenuItem})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(1175, 24)
        Me.MainMenu1.TabIndex = 11
        '
        'dmk_file
        '
        Me.dmk_file.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Load_Script, Me.SetbasefileToolStripMenuItem, Me.dmk_exit})
        Me.dmk_file.Name = "dmk_file"
        Me.dmk_file.Size = New System.Drawing.Size(35, 20)
        Me.dmk_file.Text = "File"
        '
        'Load_Script
        '
        Me.Load_Script.Name = "Load_Script"
        Me.Load_Script.Size = New System.Drawing.Size(138, 22)
        Me.Load_Script.Text = "Load_Script"
        '
        'SetbasefileToolStripMenuItem
        '
        Me.SetbasefileToolStripMenuItem.Name = "SetbasefileToolStripMenuItem"
        Me.SetbasefileToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
        Me.SetbasefileToolStripMenuItem.Text = "set_base_file"
        '
        'dmk_exit
        '
        Me.dmk_exit.Name = "dmk_exit"
        Me.dmk_exit.Size = New System.Drawing.Size(138, 22)
        Me.dmk_exit.Text = "Exit"
        '
        'Run_DMK_Script
        '
        Me.Run_DMK_Script.Name = "Run_DMK_Script"
        Me.Run_DMK_Script.Size = New System.Drawing.Size(68, 20)
        Me.Run_DMK_Script.Text = "Run Script"
        '
        'halt
        '
        Me.halt.Name = "halt"
        Me.halt.Size = New System.Drawing.Size(44, 20)
        Me.halt.Text = "HALT"
        '
        'Set_Actinic
        '
        Me.Set_Actinic.Checked = True
        Me.Set_Actinic.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Set_Actinic.Name = "Set_Actinic"
        Me.Set_Actinic.Size = New System.Drawing.Size(85, 20)
        Me.Set_Actinic.Text = "SET_ACTINIC"
        '
        'LEDS_Off
        '
        Me.LEDS_Off.Name = "LEDS_Off"
        Me.LEDS_Off.Size = New System.Drawing.Size(63, 20)
        Me.LEDS_Off.Text = "LEDs OFf"
        '
        'skip_wait
        '
        Me.skip_wait.Name = "skip_wait"
        Me.skip_wait.Size = New System.Drawing.Size(63, 20)
        Me.skip_wait.Text = "skip_wait"
        '
        'servoform
        '
        Me.servoform.Name = "servoform"
        Me.servoform.Size = New System.Drawing.Size(71, 20)
        Me.servoform.Text = "test_serve"
        '
        'HomewheelToolStripMenuItem
        '
        Me.HomewheelToolStripMenuItem.Name = "HomewheelToolStripMenuItem"
        Me.HomewheelToolStripMenuItem.Size = New System.Drawing.Size(79, 20)
        Me.HomewheelToolStripMenuItem.Text = "home_wheel"
        '
        'LaserToolStripMenuItem
        '
        Me.LaserToolStripMenuItem.Name = "LaserToolStripMenuItem"
        Me.LaserToolStripMenuItem.Size = New System.Drawing.Size(42, 20)
        Me.LaserToolStripMenuItem.Text = "laser"
        '
        'FilterwheelToolStripMenuItem
        '
        Me.FilterwheelToolStripMenuItem.Name = "FilterwheelToolStripMenuItem"
        Me.FilterwheelToolStripMenuItem.Size = New System.Drawing.Size(75, 20)
        Me.FilterwheelToolStripMenuItem.Text = "filter_wheel"
        '
        'TakeFluorBaselineToolStripMenuItem
        '
        Me.TakeFluorBaselineToolStripMenuItem.Name = "TakeFluorBaselineToolStripMenuItem"
        Me.TakeFluorBaselineToolStripMenuItem.Size = New System.Drawing.Size(100, 20)
        Me.TakeFluorBaselineToolStripMenuItem.Text = "toggle_f_shutter"
        '
        'NotesToolStripMenuItem
        '
        Me.NotesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TakenoteToolStripMenuItem, Me.RecordeventsToolStripMenuItem})
        Me.NotesToolStripMenuItem.Name = "NotesToolStripMenuItem"
        Me.NotesToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.NotesToolStripMenuItem.Text = "Notes"
        '
        'TakenoteToolStripMenuItem
        '
        Me.TakenoteToolStripMenuItem.Name = "TakenoteToolStripMenuItem"
        Me.TakenoteToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.TakenoteToolStripMenuItem.Text = "take_note"
        '
        'RecordeventsToolStripMenuItem
        '
        Me.RecordeventsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ONToolStripMenuItem, Me.OFFToolStripMenuItem})
        Me.RecordeventsToolStripMenuItem.Name = "RecordeventsToolStripMenuItem"
        Me.RecordeventsToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.RecordeventsToolStripMenuItem.Text = "record_events"
        '
        'ONToolStripMenuItem
        '
        Me.ONToolStripMenuItem.Name = "ONToolStripMenuItem"
        Me.ONToolStripMenuItem.Size = New System.Drawing.Size(94, 22)
        Me.ONToolStripMenuItem.Text = "ON"
        '
        'OFFToolStripMenuItem
        '
        Me.OFFToolStripMenuItem.Name = "OFFToolStripMenuItem"
        Me.OFFToolStripMenuItem.Size = New System.Drawing.Size(94, 22)
        Me.OFFToolStripMenuItem.Text = "OFF"
        '
        'StirerToolStripMenuItem
        '
        Me.StirerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem1, Me.OffToolStripMenuItem1})
        Me.StirerToolStripMenuItem.Name = "StirerToolStripMenuItem"
        Me.StirerToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.StirerToolStripMenuItem.Text = "stirer"
        '
        'OnToolStripMenuItem1
        '
        Me.OnToolStripMenuItem1.Name = "OnToolStripMenuItem1"
        Me.OnToolStripMenuItem1.Size = New System.Drawing.Size(152, 22)
        Me.OnToolStripMenuItem1.Text = "on"
        '
        'OffToolStripMenuItem1
        '
        Me.OffToolStripMenuItem1.Name = "OffToolStripMenuItem1"
        Me.OffToolStripMenuItem1.Size = New System.Drawing.Size(152, 22)
        Me.OffToolStripMenuItem1.Text = "off"
        '
        'zg1
        '
        Me.zg1.EditButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg1.EditModifierKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg1.IsAutoScrollRange = False
        Me.zg1.IsEnableHEdit = False
        Me.zg1.IsEnableHPan = True
        Me.zg1.IsEnableHZoom = True
        Me.zg1.IsEnableVEdit = False
        Me.zg1.IsEnableVPan = True
        Me.zg1.IsEnableVZoom = True
        Me.zg1.IsPrintFillPage = True
        Me.zg1.IsPrintKeepAspectRatio = True
        Me.zg1.IsScrollY2 = False
        Me.zg1.IsShowContextMenu = True
        Me.zg1.IsShowCopyMessage = True
        Me.zg1.IsShowCursorValues = False
        Me.zg1.IsShowHScrollBar = False
        Me.zg1.IsShowPointValues = False
        Me.zg1.IsShowVScrollBar = False
        Me.zg1.IsSynchronizeXAxes = False
        Me.zg1.IsSynchronizeYAxes = False
        Me.zg1.IsZoomOnMouseCenter = False
        Me.zg1.LinkButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg1.LinkModifierKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg1.Location = New System.Drawing.Point(0, 27)
        Me.zg1.Name = "zg1"
        Me.zg1.PanButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg1.PanButtons2 = System.Windows.Forms.MouseButtons.Middle
        Me.zg1.PanModifierKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg1.PanModifierKeys2 = System.Windows.Forms.Keys.None
        Me.zg1.PointDateFormat = "g"
        Me.zg1.PointValueFormat = "G"
        Me.zg1.ScrollMaxX = 0
        Me.zg1.ScrollMaxY = 0
        Me.zg1.ScrollMaxY2 = 0
        Me.zg1.ScrollMinX = 0
        Me.zg1.ScrollMinY = 0
        Me.zg1.ScrollMinY2 = 0
        Me.zg1.Size = New System.Drawing.Size(487, 405)
        Me.zg1.TabIndex = 12
        Me.zg1.ZoomButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg1.ZoomButtons2 = System.Windows.Forms.MouseButtons.None
        Me.zg1.ZoomModifierKeys = System.Windows.Forms.Keys.None
        Me.zg1.ZoomModifierKeys2 = System.Windows.Forms.Keys.None
        Me.zg1.ZoomStepFraction = 0.1
        '
        'zg3
        '
        Me.zg3.EditButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg3.EditModifierKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg3.IsAutoScrollRange = False
        Me.zg3.IsEnableHEdit = False
        Me.zg3.IsEnableHPan = True
        Me.zg3.IsEnableHZoom = True
        Me.zg3.IsEnableVEdit = False
        Me.zg3.IsEnableVPan = True
        Me.zg3.IsEnableVZoom = True
        Me.zg3.IsPrintFillPage = True
        Me.zg3.IsPrintKeepAspectRatio = True
        Me.zg3.IsScrollY2 = False
        Me.zg3.IsShowContextMenu = True
        Me.zg3.IsShowCopyMessage = True
        Me.zg3.IsShowCursorValues = False
        Me.zg3.IsShowHScrollBar = False
        Me.zg3.IsShowPointValues = False
        Me.zg3.IsShowVScrollBar = False
        Me.zg3.IsSynchronizeXAxes = False
        Me.zg3.IsSynchronizeYAxes = False
        Me.zg3.IsZoomOnMouseCenter = False
        Me.zg3.LinkButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg3.LinkModifierKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg3.Location = New System.Drawing.Point(0, 446)
        Me.zg3.Name = "zg3"
        Me.zg3.PanButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg3.PanButtons2 = System.Windows.Forms.MouseButtons.Middle
        Me.zg3.PanModifierKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg3.PanModifierKeys2 = System.Windows.Forms.Keys.None
        Me.zg3.PointDateFormat = "g"
        Me.zg3.PointValueFormat = "G"
        Me.zg3.ScrollMaxX = 0
        Me.zg3.ScrollMaxY = 0
        Me.zg3.ScrollMaxY2 = 0
        Me.zg3.ScrollMinX = 0
        Me.zg3.ScrollMinY = 0
        Me.zg3.ScrollMinY2 = 0
        Me.zg3.Size = New System.Drawing.Size(278, 243)
        Me.zg3.TabIndex = 13
        Me.zg3.ZoomButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg3.ZoomButtons2 = System.Windows.Forms.MouseButtons.None
        Me.zg3.ZoomModifierKeys = System.Windows.Forms.Keys.None
        Me.zg3.ZoomModifierKeys2 = System.Windows.Forms.Keys.None
        Me.zg3.ZoomStepFraction = 0.1
        '
        'zg2
        '
        Me.zg2.EditButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg2.EditModifierKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg2.IsAutoScrollRange = False
        Me.zg2.IsEnableHEdit = False
        Me.zg2.IsEnableHPan = True
        Me.zg2.IsEnableHZoom = True
        Me.zg2.IsEnableVEdit = False
        Me.zg2.IsEnableVPan = True
        Me.zg2.IsEnableVZoom = True
        Me.zg2.IsPrintFillPage = True
        Me.zg2.IsPrintKeepAspectRatio = True
        Me.zg2.IsScrollY2 = False
        Me.zg2.IsShowContextMenu = True
        Me.zg2.IsShowCopyMessage = True
        Me.zg2.IsShowCursorValues = False
        Me.zg2.IsShowHScrollBar = False
        Me.zg2.IsShowPointValues = False
        Me.zg2.IsShowVScrollBar = False
        Me.zg2.IsSynchronizeXAxes = False
        Me.zg2.IsSynchronizeYAxes = False
        Me.zg2.IsZoomOnMouseCenter = False
        Me.zg2.LinkButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg2.LinkModifierKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg2.Location = New System.Drawing.Point(494, 27)
        Me.zg2.Name = "zg2"
        Me.zg2.PanButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg2.PanButtons2 = System.Windows.Forms.MouseButtons.Middle
        Me.zg2.PanModifierKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg2.PanModifierKeys2 = System.Windows.Forms.Keys.None
        Me.zg2.PointDateFormat = "g"
        Me.zg2.PointValueFormat = "G"
        Me.zg2.ScrollMaxX = 0
        Me.zg2.ScrollMaxY = 0
        Me.zg2.ScrollMaxY2 = 0
        Me.zg2.ScrollMinX = 0
        Me.zg2.ScrollMinY = 0
        Me.zg2.ScrollMinY2 = 0
        Me.zg2.Size = New System.Drawing.Size(522, 405)
        Me.zg2.TabIndex = 14
        Me.zg2.ZoomButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg2.ZoomButtons2 = System.Windows.Forms.MouseButtons.None
        Me.zg2.ZoomModifierKeys = System.Windows.Forms.Keys.None
        Me.zg2.ZoomModifierKeys2 = System.Windows.Forms.Keys.None
        Me.zg2.ZoomStepFraction = 0.1
        '
        'zg4
        '
        Me.zg4.EditButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg4.EditModifierKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg4.IsAutoScrollRange = False
        Me.zg4.IsEnableHEdit = False
        Me.zg4.IsEnableHPan = True
        Me.zg4.IsEnableHZoom = True
        Me.zg4.IsEnableVEdit = False
        Me.zg4.IsEnableVPan = True
        Me.zg4.IsEnableVZoom = True
        Me.zg4.IsPrintFillPage = True
        Me.zg4.IsPrintKeepAspectRatio = True
        Me.zg4.IsScrollY2 = False
        Me.zg4.IsShowContextMenu = True
        Me.zg4.IsShowCopyMessage = True
        Me.zg4.IsShowCursorValues = False
        Me.zg4.IsShowHScrollBar = False
        Me.zg4.IsShowPointValues = False
        Me.zg4.IsShowVScrollBar = False
        Me.zg4.IsSynchronizeXAxes = False
        Me.zg4.IsSynchronizeYAxes = False
        Me.zg4.IsZoomOnMouseCenter = False
        Me.zg4.LinkButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg4.LinkModifierKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg4.Location = New System.Drawing.Point(284, 446)
        Me.zg4.Name = "zg4"
        Me.zg4.PanButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg4.PanButtons2 = System.Windows.Forms.MouseButtons.Middle
        Me.zg4.PanModifierKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg4.PanModifierKeys2 = System.Windows.Forms.Keys.None
        Me.zg4.PointDateFormat = "g"
        Me.zg4.PointValueFormat = "G"
        Me.zg4.ScrollMaxX = 0
        Me.zg4.ScrollMaxY = 0
        Me.zg4.ScrollMaxY2 = 0
        Me.zg4.ScrollMinX = 0
        Me.zg4.ScrollMinY = 0
        Me.zg4.ScrollMinY2 = 0
        Me.zg4.Size = New System.Drawing.Size(264, 243)
        Me.zg4.TabIndex = 15
        Me.zg4.ZoomButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg4.ZoomButtons2 = System.Windows.Forms.MouseButtons.None
        Me.zg4.ZoomModifierKeys = System.Windows.Forms.Keys.None
        Me.zg4.ZoomModifierKeys2 = System.Windows.Forms.Keys.None
        Me.zg4.ZoomStepFraction = 0.1
        '
        'zg5
        '
        Me.zg5.EditButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg5.EditModifierKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg5.IsAutoScrollRange = False
        Me.zg5.IsEnableHEdit = False
        Me.zg5.IsEnableHPan = True
        Me.zg5.IsEnableHZoom = True
        Me.zg5.IsEnableVEdit = False
        Me.zg5.IsEnableVPan = True
        Me.zg5.IsEnableVZoom = True
        Me.zg5.IsPrintFillPage = True
        Me.zg5.IsPrintKeepAspectRatio = True
        Me.zg5.IsScrollY2 = False
        Me.zg5.IsShowContextMenu = True
        Me.zg5.IsShowCopyMessage = True
        Me.zg5.IsShowCursorValues = False
        Me.zg5.IsShowHScrollBar = False
        Me.zg5.IsShowPointValues = False
        Me.zg5.IsShowVScrollBar = False
        Me.zg5.IsSynchronizeXAxes = False
        Me.zg5.IsSynchronizeYAxes = False
        Me.zg5.IsZoomOnMouseCenter = False
        Me.zg5.LinkButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg5.LinkModifierKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg5.Location = New System.Drawing.Point(554, 445)
        Me.zg5.Name = "zg5"
        Me.zg5.PanButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg5.PanButtons2 = System.Windows.Forms.MouseButtons.Middle
        Me.zg5.PanModifierKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg5.PanModifierKeys2 = System.Windows.Forms.Keys.None
        Me.zg5.PointDateFormat = "g"
        Me.zg5.PointValueFormat = "G"
        Me.zg5.ScrollMaxX = 0
        Me.zg5.ScrollMaxY = 0
        Me.zg5.ScrollMaxY2 = 0
        Me.zg5.ScrollMinX = 0
        Me.zg5.ScrollMinY = 0
        Me.zg5.ScrollMinY2 = 0
        Me.zg5.Size = New System.Drawing.Size(264, 243)
        Me.zg5.TabIndex = 16
        Me.zg5.ZoomButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg5.ZoomButtons2 = System.Windows.Forms.MouseButtons.None
        Me.zg5.ZoomModifierKeys = System.Windows.Forms.Keys.None
        Me.zg5.ZoomModifierKeys2 = System.Windows.Forms.Keys.None
        Me.zg5.ZoomStepFraction = 0.1
        '
        'zg6
        '
        Me.zg6.EditButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg6.EditModifierKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg6.IsAutoScrollRange = False
        Me.zg6.IsEnableHEdit = False
        Me.zg6.IsEnableHPan = True
        Me.zg6.IsEnableHZoom = True
        Me.zg6.IsEnableVEdit = False
        Me.zg6.IsEnableVPan = True
        Me.zg6.IsEnableVZoom = True
        Me.zg6.IsPrintFillPage = True
        Me.zg6.IsPrintKeepAspectRatio = True
        Me.zg6.IsScrollY2 = False
        Me.zg6.IsShowContextMenu = True
        Me.zg6.IsShowCopyMessage = True
        Me.zg6.IsShowCursorValues = False
        Me.zg6.IsShowHScrollBar = False
        Me.zg6.IsShowPointValues = False
        Me.zg6.IsShowVScrollBar = False
        Me.zg6.IsSynchronizeXAxes = False
        Me.zg6.IsSynchronizeYAxes = False
        Me.zg6.IsZoomOnMouseCenter = False
        Me.zg6.LinkButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg6.LinkModifierKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg6.Location = New System.Drawing.Point(824, 445)
        Me.zg6.Name = "zg6"
        Me.zg6.PanButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg6.PanButtons2 = System.Windows.Forms.MouseButtons.Middle
        Me.zg6.PanModifierKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)
        Me.zg6.PanModifierKeys2 = System.Windows.Forms.Keys.None
        Me.zg6.PointDateFormat = "g"
        Me.zg6.PointValueFormat = "G"
        Me.zg6.ScrollMaxX = 0
        Me.zg6.ScrollMaxY = 0
        Me.zg6.ScrollMaxY2 = 0
        Me.zg6.ScrollMinX = 0
        Me.zg6.ScrollMinY = 0
        Me.zg6.ScrollMinY2 = 0
        Me.zg6.Size = New System.Drawing.Size(276, 244)
        Me.zg6.TabIndex = 17
        Me.zg6.ZoomButtons = System.Windows.Forms.MouseButtons.Left
        Me.zg6.ZoomButtons2 = System.Windows.Forms.MouseButtons.None
        Me.zg6.ZoomModifierKeys = System.Windows.Forms.Keys.None
        Me.zg6.ZoomModifierKeys2 = System.Windows.Forms.Keys.None
        Me.zg6.ZoomStepFraction = 0.1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(441, 27)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(46, 25)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "DPlot"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(971, 27)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(45, 20)
        Me.Button2.TabIndex = 19
        Me.Button2.Text = "DPlot"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(237, 448)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(41, 22)
        Me.Button3.TabIndex = 20
        Me.Button3.Text = "Dplot"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(505, 445)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(43, 21)
        Me.Button4.TabIndex = 21
        Me.Button4.Text = "DPlot"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(775, 447)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(43, 23)
        Me.Button5.TabIndex = 22
        Me.Button5.Text = "DPlot"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(1050, 450)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(41, 21)
        Me.Button6.TabIndex = 23
        Me.Button6.Text = "DPlot"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(83, 65)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(30, 29)
        Me.Button7.TabIndex = 24
        Me.Button7.Text = "Button7"
        Me.Button7.UseVisualStyleBackColor = True
        Me.Button7.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(133, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 14)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Label1"
        Me.Label1.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(98, 121)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(103, 20)
        Me.TextBox1.TabIndex = 26
        Me.TextBox1.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(225, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 14)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Label4"
        Me.Label4.Visible = False
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(493, 688)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(68, 31)
        Me.Button8.TabIndex = 28
        Me.Button8.Text = "Xe sim"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(290, 698)
        Me.TrackBar1.Maximum = 500
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(154, 42)
        Me.TrackBar1.TabIndex = 29
        Me.TrackBar1.Value = 230
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(441, 706)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 24)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "230"
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(494, 725)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(67, 25)
        Me.Button9.TabIndex = 31
        Me.Button9.Text = "LASER"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(76, 185)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(50, 57)
        Me.Button10.TabIndex = 32
        Me.Button10.Text = "Button10"
        Me.Button10.UseVisualStyleBackColor = True
        Me.Button10.Visible = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(1046, 65)
        Me.ProgressBar1.Maximum = 8
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(78, 20)
        Me.ProgressBar1.TabIndex = 33
        Me.ProgressBar1.Value = 2
        '
        'ProgressBar2
        '
        Me.ProgressBar2.Location = New System.Drawing.Point(1046, 112)
        Me.ProgressBar2.Maximum = 8
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.Size = New System.Drawing.Size(78, 20)
        Me.ProgressBar2.TabIndex = 34
        Me.ProgressBar2.Value = 1
        '
        'ProgressBar3
        '
        Me.ProgressBar3.ForeColor = System.Drawing.Color.Red
        Me.ProgressBar3.Location = New System.Drawing.Point(1050, 389)
        Me.ProgressBar3.Maximum = 255
        Me.ProgressBar3.Name = "ProgressBar3"
        Me.ProgressBar3.Size = New System.Drawing.Size(78, 20)
        Me.ProgressBar3.TabIndex = 35
        '
        'sample_gain_label
        '
        Me.sample_gain_label.AutoSize = True
        Me.sample_gain_label.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sample_gain_label.Location = New System.Drawing.Point(1144, 65)
        Me.sample_gain_label.Name = "sample_gain_label"
        Me.sample_gain_label.Size = New System.Drawing.Size(17, 18)
        Me.sample_gain_label.TabIndex = 36
        Me.sample_gain_label.Text = "0"
        '
        'reference_gain_label
        '
        Me.reference_gain_label.AutoSize = True
        Me.reference_gain_label.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.reference_gain_label.Location = New System.Drawing.Point(1144, 112)
        Me.reference_gain_label.Name = "reference_gain_label"
        Me.reference_gain_label.Size = New System.Drawing.Size(17, 18)
        Me.reference_gain_label.TabIndex = 37
        Me.reference_gain_label.Text = "0"
        '
        'actinic_label
        '
        Me.actinic_label.AutoSize = True
        Me.actinic_label.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.actinic_label.Location = New System.Drawing.Point(1150, 389)
        Me.actinic_label.Name = "actinic_label"
        Me.actinic_label.Size = New System.Drawing.Size(17, 18)
        Me.actinic_label.TabIndex = 38
        Me.actinic_label.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(1043, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 14)
        Me.Label7.TabIndex = 39
        Me.Label7.Text = "sample gain:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(1043, 95)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 14)
        Me.Label8.TabIndex = 40
        Me.Label8.Text = "reference gain:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(1047, 372)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(63, 14)
        Me.Label9.TabIndex = 41
        Me.Label9.Text = "actinic light:"
        '
        'gain_data_list
        '
        Me.gain_data_list.FormattingEnabled = True
        Me.gain_data_list.ItemHeight = 14
        Me.gain_data_list.Location = New System.Drawing.Point(1046, 162)
        Me.gain_data_list.Name = "gain_data_list"
        Me.gain_data_list.Size = New System.Drawing.Size(121, 158)
        Me.gain_data_list.TabIndex = 42
        Me.gain_data_list.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(1133, 78)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(16, 17)
        Me.Label10.TabIndex = 43
        Me.Label10.Text = "2"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(1131, 125)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(16, 17)
        Me.Label11.TabIndex = 44
        Me.Label11.Text = "2"
        '
        'DMK_v1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1175, 746)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.gain_data_list)
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
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
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
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(11, 49)
        Me.Name = "DMK_v1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "2"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
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
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents HomewheelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents LaserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterwheelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents gain_data_list As System.Windows.Forms.ListBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents StirerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
#End Region
End Class