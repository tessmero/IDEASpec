Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class Intensity_Form
	Inherits System.Windows.Forms.Form
	Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click

        Call send_intensity()
     
    End Sub
    Private Sub send_intensity()
        Dim aint, far, blue, XeTest As Short

        Do_Intensity = True
        aint = Val(INtensity_Text.Text)
        If aint > 255 Or aint < 0 Then
            aint = 0
            INtensity_Text.Text = "0"
        End If

        If blue_actinic_check.Checked = True Then
            blue = 1
        Else
            blue = 0
        End If
        If far_red_check.Checked = True Then
            far = 1
        Else
            far = 0
        End If

        

        'Label1.Text = Str$(blue)
        Call DMK_v1.Out_Intensity(aint, blue, far, 0)


    End Sub
	
	Private Sub Command2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command2.Click
        INtensity_Text.Text = "0"
        far_red_check.Checked = False
        blue_actinic_check.Checked = False
        Call DMK_v1.Out_Intensity(0, 0, 0, 0)
	End Sub
	
	Private Sub Command3_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command3.Click
		
		Me.Hide()
	End Sub
	
    Private Sub Command4_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Time_Out_Time As Integer = 10000
        Dim Timed_Out As Object
        Dim CurIndex As Integer
        Dim CurCount As Integer
        Dim ss As Short
        Dim BoardNum As Short

        Dim ulstat As Short
        'Dim c As Object
        'Dim bip10v As Object
        Dim options As Integer

        'UPGRADE_WARNING: Couldn't resolve default property of object options. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        options = BACKGROUND + SINGLEIO

        'UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        'UPGRADE_WARNING: Couldn't resolve default property of object bip10v. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        ulstat = cbAInScan(0, 2, 2, 100, 100, BIP10VOLTS, MemHandle, options)

        'CBCount&, CBRate&, Gain, MemHandle&, options)
        ulstat = cbGetStatus(BoardNum, ss, CurCount, CurIndex, AIFUNCTION)

        'UPGRADE_WARNING: Couldn't resolve default property of object Timed_Out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        Timed_Out = False
        'UPGRADE_WARNING: Couldn't resolve default property of object Time_Out_Time. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        Time_Out_Time = VB.Timer() + Time_Out_Time


        'UPGRADE_WARNING: Couldn't resolve default property of object Timed_Out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        While ss = RUNNING And Timed_Out = False
            'UPGRADE_WARNING: Couldn't resolve default property of object Time_Out_Time. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Timed_Out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            If VB.Timer() > Time_Out_Time Then Timed_Out = True

            System.Windows.Forms.Application.DoEvents()
            ulstat = cbGetStatus(BoardNum, ss, CurCount, CurIndex, AIFUNCTION)
            'Label1.Caption = Str(CurCount&)
        End While
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        INtensity_Text.Text = TextBox1.Text
        Call send_intensity()

    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        INtensity_Text.Text = TextBox2.Text
        Call send_intensity()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        '  Call DMK_v1.Out_Intensity(0, 0, 0, 1)
        '  Call DMK_v1.Hold_on_There(1)
        '  Call DMK_v1.Out_Intensity(0, 0, 0, 0)
        DMK_v1.testSaturationLight()
    End Sub

    'Private Sub Gain_Test_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Gain_Test_Button.Click
    '    Call DMK_v1.do_auto_gain_ref(1)
    'End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call mass_flow_set()
        
    End Sub
    Public Sub mass_flow_set()
        'Dim test As Long
        '32000=2000 ccm/min = 5V

        ' test = cbAOut(1, 2, UNI10VOLTS, (23 * Val(TextBox4.Text)))
        ' test = cbAOut(1, 3, UNI10VOLTS, (23 * Val(TextBox5.Text)))

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class