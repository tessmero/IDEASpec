Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports ZedGraph


Friend Class DMK_v1
    Inherits System.Windows.Forms.Form
    Const Blue_Filter As Short = 110
    Const IR_Filter As Short = 165
    Dim Halt_Script As Short
    Dim Script() As String
    Dim record_files As Boolean = False
    Dim Script_Counter As Short
    Dim C_Script As String
    Public Graph_Name() As String = {"0", "1", "2", "3", "4", "5", "6"}
    Dim fluorescence_shutter_status As Integer
    Dim Shutter_delay As Integer = 3
    Const BoardNum As Short = 0 ' Board number
    Dim Trace_protocol() As Integer = {0, 0, 0} ' associates a protocol to a trace, such that the trace will automatically pick it's protocol when m_trace is called (deprecated)
    Dim Xe_intensity_value As Single
    Dim M_Primary_Gain(,) As Short = {{0, 0, 0}, {0, 0, 0}}
    Dim DefaultPicFile As String = "C:\Documents and Settings\All Users\Documents\My Pictures\Sample Pictures\hangar1.jpg"
    Dim script_file As String
    Dim s_Plot_File_Name(10) As String
    Dim s_Plot_File_Type(10) As Integer
    Dim D_Out_Mask As Integer
    Dim laser As Integer = 0
    Dim Current_Wheel_Position As Single
    Dim view_laser As Integer
    Dim servo_position(8, 30) As Integer
    Dim current_servo, number_servo_positions, current_servo_position As Integer
    Dim sub_name As String
    Dim Sub_Return_Pointer(36) As Integer 'where to return after a subroutine
    Dim Sub_Return_Level As Integer
    Dim number_variables As Integer
    Dim user_variable(100, 100) As Single
    Dim user_variable_name(1000) As String
    Dim user_variable_index(100) As Integer
    Dim Base_File_Name, Base_Base_File_Name As String
    Dim run_another_script As Integer = False
    Dim dice_length As Integer
    Dim take_notes As Integer
    Dim stir_status As Integer = 0
    Dim Current_Trace As Short
    Dim pre_pulse_light As Short

    'from pblast"
    Dim stemp As Single
    'Dim Count_time_num$
    Dim temp1 As Short
    Dim temp2 As Short
    Dim temp3 As Short
    Dim PB_multiplier As Single
    Dim PB_freq As Double
    Dim PB_Numm As Object
    Dim PB_pulselen As Single
    Dim ptb1, ptb0, ptb2 As Short 'changed from object
    Dim ptb3 As Short
    Dim PB_cycles As Double
    Dim pb_flags As Integer

    Dim ccc(10) As Integer
    Dim tttemp As Short
    Dim PBStatt As Integer
    Dim pb_port As Short



    Dim cblaster As New ChrisBlasterIO("Nexys2")


    Public Sub dmkversion1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'This sub places a default image in the pictureboxes to populate the pixels

        Dim XSize As Integer = 320
        Dim YSize As Integer = 240
        'Dim MyPic As String = "C:\Documents and Settings\All Users\Documents\My Pictures\Sample Pictures\hangar1.jpg"
        'Dim MyImage As New Bitmap(MyPic)
        'Dim s_Plot_File_name(10) As String

        ' Stretches the image to fit the pictureBoxes. 
        Dim i As Integer

        For i = 1 To 6
            Graph_Name(i) = "GRAPH: " & i
        Next i
        Button8.Visible = False
        Button9.Visible = False
        TrackBar1.Visible = False
        Label6.Visible = False

        view_laser = False


    End Sub
    Private Sub parsenum(ByRef num As String)
        'back up copy of code inserted into Private Sub P_blast

        Freq = 10 ^ 8
        pulselen = 1 / Freq

        StdDelay = 5 'standard 5 cycle delay with this board (or 50 nsec)

        num = Trim(num)

        If VB.Right(num, 1) = "u" Then
            multiplier = 0.000001
        ElseIf VB.Right(num, 1) = "m" Then
            multiplier = 0.001
        ElseIf VB.Right(num, 1) = "n" Then
            multiplier = 0.000000001
        Else
            multiplier = 1
        End If

        'seconds = multiplier * numm

        numm = Val(num)
        Time_Interval_in_Seconds = numm * multiplier

        'DMK_v1.List1.AddItem "n " & num$ & " TIS " & Time_Interval_in_Seconds & " m " & multiplier

        System.Windows.Forms.Application.DoEvents()

        cycles = Int(multiplier * numm / pulselen) - StdDelay

        'DMK_v1.List1.AddItem "cycles " & cycles

        System.Windows.Forms.Application.DoEvents()

        ptb0 = cycles And &HFFS
        cycles = (cycles - ptb0) / 256

        ptb1 = cycles And &HFFS
        cycles = (cycles - ptb1) / 256

        ptb2 = cycles And &HFFS
        cycles = (cycles - ptb2) / 256

        ptb3 = cycles And &HFFS
        'DMK_v1.List1.AddItem "0: " & ptb0 & " 1: " & ptb1 & " 2: " & ptb2 & " 3: " & ptb3

    End Sub
    Sub outt(ByRef port As Short, ByRef dt As Short)
        '   Dim pb_version As String = "pci"
        'Dim pbservion As Object
        'Dim pbversion As Object
        Dim PBStat As Short

           PBStat = pb_outp(port, dt)


  
    End Sub
    Sub outt_old(ByRef port As Short, ByRef dt As Short)
        Dim pb_version As String = "pci"
        'Dim pbservion As Object
        'Dim pbversion As Object
        Dim PBStat As Short

        If pb_version = "isa" Then

            'Me.PortIO1.OutputDataWidth = PORTIO32Lib.enumdatawidth.bit8
            'Me.PortIO1.OutputAddr = port
            'Me.PortIO1.OutputData = dt
            'Me.PortIO1.Output()

        ElseIf pb_version = "usb" Then


        ElseIf pb_version = "pci" Then
            PBStat = pb_outp(port, dt)


        End If

    End Sub
    Private Sub Multi_Trace(ByRef Current_Protocol As Short, ByRef Number_Loops() As Short, ByRef Intensity(,) As Short, ByRef Number_Pulses(,) As Short, ByRef Number_Measuring_Lights() As Short, ByRef Measuring_Light(,) As Short, ByRef Measuring_Interval(,,) As String, ByRef Primary_Gain(,) As Short, ByRef reference_gain(,) As Short, ByRef Measuring_Pulse_Duration() As String, ByRef Gain As Integer, ByRef In_Channel As Short, ByRef points As Integer, ByRef data_time() As Single, ByRef Xe_Flash(,) As Short, ByRef q_switch(,) As Short, ByVal M_Far_Red(,) As Short, ByVal M_Blue_actinic(,) As Short, ByVal pre_pulse(,) As Short, ByVal pre_pulse_time(,) As String, ByVal Pre_Delay(,) As String, ByVal m_take_data(,) As Short)
        Dim ulstat As Short
        Dim Start_of_Wait, end_of_wait As Single

        'ulstat = cbDOut(BoardNum, AUXPORT, 0)

        Trace_Running = True

        Dim Time_In_S As Single
        Dim PP_Line As Short = 0
        Dim i, j As Object
        Dim ii As Integer
        Dim k As Short
        Dim AD_Trigger As Short
        Dim Loop_Return_Index() As Short
        'Dim Points&
        Dim CBRate As Integer = 50000
        Dim CBCount As Integer
        Dim LowChan, HighChan As Short
        Dim Options As Integer
        'Dim ulstat%
        Dim ss As Short
        Dim CurCount, CurIndex As Integer
        Dim Time_Accumulator As Single = 0
        Dim Measuring_Pulse_Number As Integer = 0
        Dim Timed_Out As Short
        Dim Time_Out_Time As Single = 1
        Dim Each_Measuring_Pulse_Interval() As Single
        Dim Current_Measuring_Point As Integer = 0
        Dim Give_Saturating_Pulse As Short
        Dim SH_State As Integer
        'Dim TTT1, TTT2 As Single
        Dim Raw_Data() As Short

        ReDim Loop_Return_Index(Number_Loops(Current_Protocol))
        ReDim Each_Measuring_Pulse_Interval(Number_Measuring_Lights(Current_Protocol))
        Dim pbstat As Integer
        ' set up pulseblaster
        pbstat = pb_start_programming(PULSE_PROGRAM)

        '        Call outt(BaseAddress, 0)
        '        Call outt(BaseAddress + 2, &HAS)
        '        Call outt(BaseAddress + 3, 0)
        '        Call outt(BaseAddress + 4, 0) '&H55S)

        points = 0

        For i = 1 To Number_Loops(Current_Protocol)
            If m_take_data(Current_Protocol, i) > 0 Then
                points = points + (Number_Measuring_Lights(Current_Protocol) * Number_Pulses(Current_Protocol, i))
            End If
        Next i

        ReDim data_time(points * 4 + 10)
        ReDim Data_Volts(points + 10, 4)
        ReDim Ref_Data_Volts(points * 4 + 10)
        ReDim Raw_Data(points * 4 + 10) ' dimension an array to hold the input values
        Start_of_Wait = VB.Timer()
        'Dim time_temp As String

        ' insert a 1 s delay
        SH_State = 0 'hold mode

        ProgressBar1.Value = Primary_Gain(Current_Protocol, 1)
        sample_gain_label.Text = Primary_Gain(Current_Protocol, 1)
        ProgressBar2.Value = reference_gain(Current_Protocol, 1)
        reference_gain_label.Text = reference_gain(Current_Protocol, 1)
        ProgressBar3.Value = Intensity(Current_Protocol, 1)
        actinic_label.Text = Intensity(Current_Protocol, 1)

        '2-15-removed next two to see if it will speed things up
        '       Call P_Blast(M_Primary_Gain(Current_Protocol, 1), reference_gain(Current_Protocol, 1), 0, 0, 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, 1) And 255), 0, 0, "10u", Time_In_S, SH_State, 0, (M_Far_Red(Current_Protocol, 1)), (M_Blue_actinic(Current_Protocol, 1)))

        '      PP_Line = PP_Line + 1

        For i = 1 To Number_Loops(Current_Protocol)

            Loop_Return_Index(i) = PP_Line

            ' insert loop_begin code with
            ' 1 in delay count
            ' 2 in op code
            ' number_pulses(i)-1 in the data field
            ' intensity(i) in the output field

            If Intensity(Current_Protocol, i) = 256 Then
                Give_Saturating_Pulse = 1
            Else
                Give_Saturating_Pulse = 0
            End If

            SH_State = 0



            'pre_pulse(Current_Protocol, i) = 0
            'laser = 0
            If pre_pulse(Current_Protocol, i) > 0 Then


                If Pre_Delay(Current_Protocol, i) <> "" Then

                    Call P_Blast(M_Primary_Gain(Current_Protocol, 1), reference_gain(Current_Protocol, 1), 0, 0, 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 2, Number_Pulses(Current_Protocol, i) - 1, Pre_Delay(Current_Protocol, i), Time_In_S, SH_State, Xe_Flash(Current_Protocol, i), (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))

                    'List1.Items.Add("l: " & Number_Pulses(Current_Protocol, i) - 1)
                    PP_Line = PP_Line + 1
                    Call P_Blast(M_Primary_Gain(Current_Protocol, 1), reference_gain(Current_Protocol, 1), 0, 0, 0, (pre_pulse(Current_Protocol, i) And 1), (Intensity(Current_Protocol, i) And 255), 0, Number_Pulses(Current_Protocol, i) - 1, pre_pulse_time(Current_Protocol, i), Time_In_S, SH_State, Xe_Flash(Current_Protocol, i), (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))

                Else

                    SH_State = 0 'hold mode
                    Call P_Blast(M_Primary_Gain(Current_Protocol, 1), reference_gain(Current_Protocol, 1), 0, 0, 0, ((pre_pulse_light And 2) / 2), (Intensity(Current_Protocol, i) And 255), 2, Number_Pulses(Current_Protocol, i) - 1, pre_pulse_time(Current_Protocol, i), Time_In_S, SH_State, Xe_Flash(Current_Protocol, i), ((pre_pulse_light And 4) / 4), (pre_pulse_light And 1))
                    '                    Call P_Blast(M_Primary_Gain(Current_Protocol, 1), reference_gain(Current_Protocol, 1), 0, 0, 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 2, Number_Pulses(Current_Protocol, i) - 1, "1u", Time_In_S, SH_State, Xe_Flash(Current_Protocol, i), (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))


                    '                    Call P_Blast(M_Primary_Gain(Current_Protocol, 1), reference_gain(Current_Protocol, 1), 0, 0, 0, (pre_pulse(Current_Protocol, i) And 1), (Intensity(Current_Protocol, i) And 255), 2, Number_Pulses(Current_Protocol, i) - 1, pre_pulse_time(Current_Protocol, i), Time_In_S, SH_State, Xe_Flash(Current_Protocol, i), 1, (M_Blue_actinic(Current_Protocol, i)))
                End If

                PP_Line = PP_Line + 1


                Call P_Blast(M_Primary_Gain(Current_Protocol, 1), reference_gain(Current_Protocol, 1), 0, 0, Xe_Flash(Current_Protocol, i), Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 0, 0, "1u", Time_In_S, SH_State, 0, 0, 0)

                PP_Line = PP_Line + 1

                '            ElseIf laser = 1 Then

                '               Call P_Blast(M_Primary_Gain(Current_Protocol, i), reference_gain(Current_Protocol, i), 0, 0, 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 2, Number_Pulses(Current_Protocol, i) - 1, "5u", Time_In_S, SH_State, Xe_Flash(Current_Protocol, i), (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))

                'List1.Items.Add("l: " & Number_Pulses(Current_Protocol, i) - 1)
                '              PP_Line = PP_Line + 1
                'Xe_Flash(Current_Protocol, i)

                '             Call P_Blast(M_Primary_Gain(Current_Protocol, i), reference_gain(Current_Protocol, i), 0, 0, Xe_Flash(Current_Protocol, i), Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 0, 0, "230u", Time_In_S, SH_State, 0, 0, 0)

                '           PP_Line = PP_Line + 1
                '            'for q-switch

                '          Call P_Blast(M_Primary_Gain(Current_Protocol, i), reference_gain(Current_Protocol, i), 0, 0, Xe_Flash(Current_Protocol, i), Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 0, 0, "10u", Time_In_S, SH_State, 0, q_switch(Current_Protocol, i), 0)

                '         PP_Line = PP_Line + 1

            Else
                Call P_Blast(M_Primary_Gain(Current_Protocol, 1), reference_gain(Current_Protocol, 1), 0, 0, 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 2, Number_Pulses(Current_Protocol, i) - 1, "1u", Time_In_S, SH_State, Xe_Flash(Current_Protocol, i), (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))

                'List1.Items.Add("l: " & Number_Pulses(Current_Protocol, i) - 1)
                PP_Line = PP_Line + 1

            End If


            For k = 1 To Number_Measuring_Lights(Current_Protocol)

                ' insert continue with:
                ' Measuring_Interval(k) in delay count
                ' 0 in op code
                ' 0 in data field
                ' intensity(i) in output field
                SH_State = 0 'hold mode

                Call P_Blast(M_Primary_Gain(Current_Protocol, k), reference_gain(Current_Protocol, k), 0, 0, 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 0, 0, Measuring_Interval(Current_Protocol, k, i), Time_In_S, SH_State, 0, (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))

                Each_Measuring_Pulse_Interval(k) = Time_In_S

                Time_Out_Time = Time_Out_Time + (Time_In_S * Number_Pulses(Current_Protocol, i))

                PP_Line = PP_Line + 1

                ' insert continue with:
                ' measuring_Pulse_Duration in delay count
                ' 0 in op code
                ' 0 in data field
                ' intensity(i) and
                ' measuring_light(k)
                ' in output field
                'List1.AddItem "gain: " & k & " " & Primary_Gain(Current_Protocol, k)
                SH_State = 1 'sample mode

                If m_take_data(Current_Protocol, i) > 0 Then
                    Call P_Blast(M_Primary_Gain(Current_Protocol, k), reference_gain(Current_Protocol, k), 0, Measuring_Light(Current_Protocol, k), 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 0, 0, Measuring_Pulse_Duration(Current_Protocol), Time_In_S, SH_State, 0, (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))
                Else  'do not give measuring pulse
                    Call P_Blast(M_Primary_Gain(Current_Protocol, k), reference_gain(Current_Protocol, k), 0, 0, 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 0, 0, Measuring_Pulse_Duration(Current_Protocol), Time_In_S, SH_State, 0, (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))

                End If

                PP_Line = PP_Line + 1

                ' insert continue with:
                ' 5u in delay count
                ' 0 in op code
                ' 0 in data field
                ' intensity(i) and
                ' measuring_light(k)
                ' trigger
                ' in output field


                AD_Trigger = 0

                SH_State = 0 'change to hold mode, allow delay for settle, measuring light still on
                If m_take_data(Current_Protocol, i) > 0 Then

                    Call P_Blast(M_Primary_Gain(Current_Protocol, k), reference_gain(Current_Protocol, k), AD_Trigger, Measuring_Light(Current_Protocol, k), 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 0, 0, "10u", Time_In_S, SH_State, Xe_Flash(Current_Protocol, i), (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))
                Else
                    Call P_Blast(M_Primary_Gain(Current_Protocol, k), reference_gain(Current_Protocol, k), AD_Trigger, 0, 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 0, 0, "10u", Time_In_S, SH_State, Xe_Flash(Current_Protocol, i), (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))
                End If

                'Call P_Blast(Primary_Gain(Current_Protocol, k), AD_Trigger, Measuring_Light(Current_Protocol, k), 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 0, 0, "50u", Time_In_S)
                PP_Line = PP_Line + 1


                ' insert continue with:
                ' measuring_Pulse_Duration in delay count
                ' 0 in op code
                ' 0 in data field
                ' intensity(i) and
                ' measuring_light(k)
                ' trigger
                ' in output field

                If m_take_data(Current_Protocol, i) > 0 Then

                    AD_Trigger = 1
                    ' measuring light off, trigger adc
                    Call P_Blast(M_Primary_Gain(Current_Protocol, k), reference_gain(Current_Protocol, k), AD_Trigger, Measuring_Light(Current_Protocol, k), 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 0, 0, "5u", Time_In_S, SH_State, Xe_Flash(Current_Protocol, i), (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))
                Else
                    Call P_Blast(M_Primary_Gain(Current_Protocol, k), reference_gain(Current_Protocol, k), AD_Trigger, 0, 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 0, 0, "5u", Time_In_S, SH_State, Xe_Flash(Current_Protocol, i), (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))
                End If

                '                Call P_Blast(Primary_Gain(Current_Protocol, k), AD_Trigger, Measuring_Light(Current_Protocol, k), 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 0, 0, "1u", Time_In_S, SH_State)
                'Call P_Blast(Primary_Gain(Current_Protocol, k), AD_Trigger, Measuring_Light(Current_Protocol, k), 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 0, 0, "50u", Time_In_S)
                PP_Line = PP_Line + 1


                ' insert continue with:
                ' measuring_Pulse_Duration in delay count
                ' 0 in op code
                ' 0 in data field
                ' intensity(i) and
                ' measuring_light(k)
                ' trigger =0
                ' in output field

                AD_Trigger = 0
                If m_take_data(Current_Protocol, i) > 0 Then

                    Call P_Blast(M_Primary_Gain(Current_Protocol, k), reference_gain(Current_Protocol, k), AD_Trigger, Measuring_Light(Current_Protocol, k), 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 0, 0, "2u", Time_In_S, SH_State, Xe_Flash(Current_Protocol, i), (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))
                Else
                    Call P_Blast(M_Primary_Gain(Current_Protocol, k), reference_gain(Current_Protocol, k), AD_Trigger, 0, 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 0, 0, "2u", Time_In_S, SH_State, Xe_Flash(Current_Protocol, i), (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))
                End If
                PP_Line = PP_Line + 1
                'next turn off the measuring pulse
                Call P_Blast(M_Primary_Gain(Current_Protocol, k), reference_gain(Current_Protocol, k), AD_Trigger, 0, 0, Give_Saturating_Pulse, (Intensity(Current_Protocol, i) And 255), 0, 0, "1u", Time_In_S, SH_State, Xe_Flash(Current_Protocol, i), (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))
                PP_Line = PP_Line + 1


            Next k


            For j = 1 To Number_Pulses(Current_Protocol, i)
                For k = 1 To Number_Measuring_Lights(Current_Protocol)
                    Current_Measuring_Point = Current_Measuring_Point + 1
                    Time_Accumulator = Time_Accumulator + Each_Measuring_Pulse_Interval(k)
                    data_time(Current_Measuring_Point - 1) = Time_Accumulator
                    'List1.AddItem Current_Measuring_Point & Time_Accumulator

                Next k
            Next j

            ' insert loop end with:

            ' measuring_Pulse_Duration in delay count
            ' 3 in op code
            ' loop_return_index in data field
            ' intensity(i) and
            ' measuring_light(k)
            ' trigger
            ' in output field


            Call P_Blast(M_Primary_Gain(Current_Protocol, k - 1), reference_gain(Current_Protocol, k - 1), 0, 0, 0, 0, Intensity(Current_Protocol, i), 3, (Loop_Return_Index(i)), "100n", Time_In_S, SH_State, Xe_Flash(Current_Protocol, i), (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))


            PP_Line = PP_Line + 1

        Next i


        ' insert STOP


        ' 1 in op code
        ' intensity(i) and


        Call P_Blast(M_Primary_Gain(Current_Protocol, k - 1), reference_gain(Current_Protocol, k - 1), 0, 0, 0, 0, Intensity(Current_Protocol, i - 1), 1, 0, "100n", Time_In_S, SH_State, Xe_Flash(Current_Protocol, i), (M_Far_Red(Current_Protocol, i)), (M_Blue_actinic(Current_Protocol, i)))
        'Midnight bug fix:
        Dim time_now As Double = VB.Timer
        If (time_now < Start_of_Wait) Then
            end_of_wait = Start_of_Wait - time_now - (24 * 60 * 60) ' midnight adjusted code
        Else
            end_of_wait = Start_of_Wait - VB.Timer() ' original code
        End If



        ' end of Pulse_Blaster program

        'Call outt(BaseAddress% + 6, 0)
        'Call outt(BaseAddress% + 7, 7)


        'List1.AddItem "points:" & Str$(Points&)

        '************************

        ' Collect the values with cbAInScan%()
        ' Parameters:
        '   BoardNum%   :the number used by CB.CFG to describe this board
        '   LowChan%    :the first channel of the scan
        '   HighChan%   :the last channel of the scan
        '   CBCount&    :the total number of A/D samples to collect
        '   CBRate&     :sample rate
        '   Gain        :the gain for the board
        '   ADData%     :the array for the collected data values
        '   Options     :data collection options


        Time_Out_Time = Time_Out_Time * 5


        'CBRate = 3 ' sampling rate (samples per second)

        LowChan = 0 'In_Channel '0                   ' first channel to acquire
        HighChan = 3 'In_Channel ' 0                  ' last channel to acquire

        Const FirstPoint As Integer = 0 ' set first element in buffer to transfer to array

        ' total number of data points to collect

        CBCount = points * 4  'hughchannel-lowchannel
        'If points > 10000 Then

        MemHandle = cbWinBufAlloc(points * 4 + 10) ' set aside memory to hold data
        'End If
        '      MemHandle = cbWinBufAlloc(CBCount) ' set aside memory to hold data
        ulstat = cbSetTrigger(BoardNum, TRIGPOSEDGE, 0, 0.5)  'changes high from 1 to 2

        'Options = EXTTRIGGER + BACKGROUND + NOCONVERTDATA '+ SINGLEIO
        Options = EXTCLOCK + BACKGROUND + NOCONVERTDATA '+ SINGLEIO

        ' return data as 16-bit values
        ' collect data in BACKGROUND mode
        ' use NOCONVERTDATA if using 16 bit board


        '        List1.Items.Add(" trace started")
        System.Windows.Forms.Application.DoEvents()

        If MemHandle = 0 Then Stop ' check that a handle to a memory buffer exists

        ulstat = cbAInScan(BoardNum, LowChan, HighChan, CBCount, CBRate, Gain, MemHandle, Options)


        'Call Hold_on_There(3)

        'Call outt(BaseAddress + 6, 0)  'not needed for this version of PB?
        'Call outt(BaseAddress + 7, 0) '7)
        pbstat = pb_stop_programming()

        'put the device in run mode
        'Call outt(BaseAddress + 1, 0) '7)
        pbstat = pb_start()

        ulstat = cbGetStatus(BoardNum, ss, CurCount, CurIndex, AIFUNCTION)

        Timed_Out = False
        Time_Out_Time = VB.Timer() + Time_Out_Time
        'Midnight bug fix:
        ' First, find the correct ending wait time
        Dim midnight_mark As Boolean = False
        If (Time_Out_Time > (24 * 60 * 60)) Then
            Time_Out_Time = Time_Out_Time - (24 * 60 * 60)
            midnight_mark = True ' tell following code not to trigger "timed out" until after midnight
        End If


        While CurCount < CBCount And Timed_Out = False
            If (VB.Timer() > Time_Out_Time And midnight_mark = False) Then Timed_Out = True ' don't time-out before midnight
            If (midnight_mark = True And VB.Timer() < 60) Then midnight_mark = False ' indicate that midnight has passed

            System.Windows.Forms.Application.DoEvents()
            ulstat = cbGetStatus(BoardNum, ss, CurCount, CurIndex, AIFUNCTION)
            Label4.Text = CurCount & " / " & CBCount
            System.Windows.Forms.Application.DoEvents()
        End While

        Label4.Text = CurCount & " * " & CBCount
        System.Windows.Forms.Application.DoEvents()
        'List1.Items.Add(CBCount & "CC:" & CurCount)
        'ulstat = cbDOut(BoardNum, AUXPORT, &HFFS)

        If Timed_Out = True Then
            ulstat = cbStopBackground(BoardNum, AIFUNCTION)
            MessageBox.Show("Timed out!")
            System.Windows.Forms.Application.DoEvents()

        Else
            List1.Items.Add(" trace done")
            'Scrolling text box fix: make the box jump to the bottom after adding an item
            List1.TopIndex = List1.Items.Count - 1
        End If


        ulstat = cbStopBackground(BoardNum, AIFUNCTION) 'clears variables and flags after normal termination

        'Transfer the data from the memory buffer set up by Windows to an array for use by Visual Basic

        ulstat = cbWinBufToArray(MemHandle, Raw_Data(0), FirstPoint, CBCount)

        'If ulstat% <> 0 Then Stop
        ' convert RAW data into VOLTS

        '        For i = 0 To (points - 1) * 2 Step 2
        ' ulstat = cbToEngUnits(BoardNum, Gain, Raw_Data(i), TTT1)
        'ulstat = cbToEngUnits(BoardNum, Gain, Raw_Data(i + 1), TTT2)
        'Data_Volts(i / 2) = (TTT1 / TTT2)
        'Next i

        For i = 0 To (points - 1)
            For ii = 0 To 3
                ulstat = cbToEngUnits(BoardNum, Gain, Raw_Data((i * 4) + ii), Data_Volts(i, ii))
            Next ii
        Next i


        'Set the time values

        'Time_Accumulator = 0

        'Call parsenum(Baseline_Measuring_Interval)

        'For i% = 1 To Baseline_Number_Points
        '    Time_Accumulator = Time_Accumulator + Time_Interval_in_Seconds
        '    data_time(i%) = Time_Accumulator
        'Next i%

        'Call parsenum(Dirk_Measuring_Interval)

        'For i% = (Baseline_Number_Points + 1) To ((Number_Dirk_Repeats * Dirk_Number_Points) + Baseline_Number_Points)
        '    Time_Accumulator = Time_Accumulator + Time_Interval_in_Seconds
        '    data_time(i%) = Time_Accumulator
        'Next i%

        'Call parsenum(Recovery_Measuring_Interval)

        'For i% = (Baseline_Number_Points + (Number_Dirk_Repeats * Dirk_Number_Points) + 1) To (Baseline_Number_Points + (Number_Dirk_Repeats * Dirk_Number_Points) + Recovery_Number_Points)
        '    Time_Accumulator = Time_Accumulator + Time_Interval_in_Seconds
        '    data_time(i%) = Time_Accumulator
        'Next i%

        'List1.Clear
        'For i = 0 To points& - 1
        'List1.AddItem data_time(i) & " " & data_volts(i)
        'Next i


        'put the Pulse_Blaster in run mode
        '*****Call outt(BaseAddress% + 1, 7)

        Trace_Running = False

    End Sub
    Private Sub P_Blast_newwer(ByRef Primary_Gain As Short, ByVal Reference_Gain As Short, ByRef AD_Trigger As Short, ByRef Measuring_Light As Short, ByRef Xe_Pulse As Short, ByRef Saturating_Pulse As Short, ByRef Actinic_Intensity As Short, ByRef Op_Code As Short, ByRef Data_field As Integer, ByRef Count_Time_Num As String, ByRef Time_In_S As Single, ByRef SH_State As Integer, ByVal Xe_flash As Integer, ByVal m_far_red As Integer, ByVal m_blue_actinic As Integer)
        Dim pbb As Integer = 0
        'Saturating_Pulse = 1

        'm_far_red = Xe_flash

        'List1.Items.Add("Xe: " & Xe_flash & "Xe p: " & Xe_Pulse)
        pb_flags = ((128 * SH_State) + (64 * Xe_flash) + (Reference_Gain) + (16 * Saturating_Pulse) + 8 * m_far_red + 32 * m_blue_actinic)
        pb_flags = pb_flags + (&H100 * ((AD_Trigger + (Measuring_Light * 2) + (Primary_Gain * &H20S))))
        pb_flags = pb_flags + &H10000 * Actinic_Intensity
        '        Call outt(BaseAddress + 6, ((128 * SH_State) + (64 * Xe_flash) + Reference_Gain) + (16 * Saturating_Pulse) + 8 * m_far_red + 32 * m_blue_actinic)
        'Call outt(BaseAddress + 6, AD_Trigger + (Measuring_Light * 2) + (Primary_Gain * &H20S)) '+ (Xe_Pulse * &H20) + (Saturating_Pulse * &H80)) 'bit 8
        '        Call outt(BaseAddress + 6, Actinic_Intensity) 'bit 7


        '      stemp = Data_field * &H10S + Op_Code
        '
        '       temp1 = stemp And &HFFS

        '      stemp = stemp - temp1

        '     temp2 = CShort(stemp And &HFF00S) / &H100S

        '    stemp = stemp - temp2

        '   temp3 = CShort(stemp And &HFF0000) / &H10000

        '  Call outt(BaseAddress + 6, temp3) 'bit 6
        ' Call outt(BaseAddress + 6, temp2) 'bit 5
        'Call outt(BaseAddress + 6, temp1) 'bit 4

        PB_freq = 100000000.0#
        PB_pulselen = 1 / PB_freq

        Count_Time_Num = Trim(Count_Time_Num)

        If VB.Right(Count_Time_Num, 1) = "u" Then
            PB_multiplier = 0.000001
        ElseIf VB.Right(Count_Time_Num, 1) = "m" Then
            PB_multiplier = 0.001
        ElseIf VB.Right(Count_Time_Num, 1) = "n" Then
            PB_multiplier = 0.000000001
        Else
            PB_multiplier = 1
        End If



        PB_Numm = Val(Count_Time_Num)


        Time_In_S = PB_multiplier * PB_Numm


        PB_cycles = Time_In_S * 10 ^ 9
        'Int(PB_multiplier * PB_Numm / PB_pulselen) '-5  '5 cycle correction


        '        ptb0 = PB_cycles And &HFFS


        '       PB_cycles = (PB_cycles - ptb0) / 256


        '      ptb1 = PB_cycles And &HFFS


        '     PB_cycles = (PB_cycles - ptb1) / 256


        '    ptb2 = PB_cycles And &HFFS


        '   PB_cycles = (PB_cycles - ptb2) / 256

        '  ptb3 = PB_cycles And &HFFS

        ' Call outt(BaseAddress + 6, (ptb3)) 'bit 3
        'Call outt(BaseAddress + 6, (ptb2)) 'bit 2
        'Call outt(BaseAddress + 6, (ptb1)) 'bit 1
        'Call outt(BaseAddress + 6, (ptb0)) 'bit 0
        '        Call pb_inst(pb_flags, Op_Code, Data_field, PB_cycles)

        Call pb_inst_pbonly(pb_flags, Op_Code, Data_field, PB_cycles)

    End Sub

    Private Sub P_Blast(ByRef Primary_Gain As Short, ByVal Reference_Gain As Short, ByRef AD_Trigger As Short, ByRef Measuring_Light As Short, ByRef Xe_Pulse As Short, ByRef Saturating_Pulse As Short, ByRef Actinic_Intensity As Short, ByRef Op_Code As Short, ByRef Data_field As Single, ByRef Count_Time_Num As String, ByRef Time_In_S As Single, ByRef SH_State As Integer, ByVal Xe_flash As Integer, ByVal m_far_red As Integer, ByVal m_blue_actinic As Integer)

        'Saturating_Pulse = 1

        'm_far_red = Xe_flash

        'List1.Items.Add("Xe: " & Xe_flash & "Xe p: " & Xe_Pulse)

 
        pb_port = BaseAddress + 6
        stemp = Data_field * &H10S + Op_Code

        temp1 = stemp And &HFFS

        stemp = stemp - temp1

        temp2 = CShort(stemp And &HFF00S) / &H100S

        stemp = stemp - temp2

        temp3 = CShort(stemp And &HFF0000) / &H10000


        PB_freq = 100000000.0#
        PB_pulselen = 1 / PB_freq

        Count_Time_Num = Trim(Count_Time_Num)

        If VB.Right(Count_Time_Num, 1) = "u" Then
            PB_multiplier = 0.000001
        ElseIf VB.Right(Count_Time_Num, 1) = "m" Then
            PB_multiplier = 0.001
        ElseIf VB.Right(Count_Time_Num, 1) = "n" Then
            PB_multiplier = 0.000000001
        Else
            PB_multiplier = 1
        End If



        PB_Numm = Val(Count_Time_Num)


        Time_In_S = PB_multiplier * PB_Numm


        PB_cycles = Int(PB_multiplier * PB_Numm / PB_pulselen) '-5  '5 cycle correction


        ptb0 = PB_cycles And &HFFS


        PB_cycles = (PB_cycles - ptb0) / 256


        ptb1 = PB_cycles And &HFFS


        PB_cycles = (PB_cycles - ptb1) / 256


        ptb2 = PB_cycles And &HFFS


        PB_cycles = (PB_cycles - ptb2) / 256

        ptb3 = PB_cycles And &HFFS

        tttemp = (128 * SH_State) + (64 * Xe_flash) + Reference_Gain + (16 * Saturating_Pulse) + 8 * m_far_red + 32 * m_blue_actinic
        PBStatt = pb_outp(pb_port, tttemp)

        'Call outt(BaseAddress + 6, tttemp)
        tttemp = AD_Trigger + (Measuring_Light * 2) + (Primary_Gain * &H20S)
        'Call outt(BaseAddress + 6, tttemp) '+ (Xe_Pulse * &H20) + (Saturating_Pulse * &H80)) 'bit 8
        PBStatt = pb_outp(pb_port, tttemp)

        tttemp = Actinic_Intensity
        PBStatt = pb_outp(pb_port, tttemp)

        'Call outt(BaseAddress + 6, tttemp) 'bit 7
        PBStatt = pb_outp(pb_port, temp3)
        PBStatt = pb_outp(pb_port, temp2)
        PBStatt = pb_outp(pb_port, temp1)

        ' outt(BaseAddress + 6, temp3) 'bit 6
        'Call outt(BaseAddress + 6, temp2) 'bit 5
        'Call outt(BaseAddress + 6, temp1) 'bit 4
        PBStatt = pb_outp(pb_port, ptb3)
        PBStatt = pb_outp(pb_port, ptb2)
        PBStatt = pb_outp(pb_port, ptb1)
        PBStatt = pb_outp(pb_port, ptb0)

        '        Call outt(BaseAddress + 6, (ptb3)) 'bit 3
        '        Call outt(BaseAddress + 6, (ptb2)) 'bit 2
        '        Call outt(BaseAddress + 6, (ptb1)) 'bit 1
        '        Call outt(BaseAddress + 6, (ptb0)) 'bit 0

    End Sub
    Private Sub P_Blastx(ByRef Primary_Gain As Short, ByVal Reference_Gain As Short, ByRef AD_Trigger As Short, ByRef Measuring_Light As Short, ByRef Xe_Pulse As Short, ByRef Saturating_Pulse As Short, ByRef Actinic_Intensity As Short, ByRef Op_Code As Short, ByRef Data_field As Single, ByRef Count_Time_Num As String, ByRef Time_In_S As Single, ByRef SH_State As Integer, ByVal Xe_flash As Integer, ByVal m_far_red As Integer, ByVal m_blue_actinic As Integer)

        'Saturating_Pulse = 1

        'm_far_red = Xe_flash

        'List1.Items.Add("Xe: " & Xe_flash & "Xe p: " & Xe_Pulse)


        Call outt(BaseAddress + 6, ((128 * SH_State) + (64 * Xe_flash) + Reference_Gain) + (16 * Saturating_Pulse) + 8 * m_far_red + 32 * m_blue_actinic)
        Call outt(BaseAddress + 6, AD_Trigger + (Measuring_Light * 2) + (Primary_Gain * &H20S)) '+ (Xe_Pulse * &H20) + (Saturating_Pulse * &H80)) 'bit 8
        Call outt(BaseAddress + 6, Actinic_Intensity) 'bit 7


        stemp = Data_field * &H10S + Op_Code

        temp1 = stemp And &HFFS

        stemp = stemp - temp1

        temp2 = CShort(stemp And &HFF00S) / &H100S

        stemp = stemp - temp2

        temp3 = CShort(stemp And &HFF0000) / &H10000

        Call outt(BaseAddress + 6, temp3) 'bit 6
        Call outt(BaseAddress + 6, temp2) 'bit 5
        Call outt(BaseAddress + 6, temp1) 'bit 4

        PB_freq = 100000000.0#
        PB_pulselen = 1 / PB_freq

        Count_Time_Num = Trim(Count_Time_Num)

        If VB.Right(Count_Time_Num, 1) = "u" Then
            PB_multiplier = 0.000001
        ElseIf VB.Right(Count_Time_Num, 1) = "m" Then
            PB_multiplier = 0.001
        ElseIf VB.Right(Count_Time_Num, 1) = "n" Then
            PB_multiplier = 0.000000001
        Else
            PB_multiplier = 1
        End If



        PB_Numm = Val(Count_Time_Num)


        Time_In_S = PB_multiplier * PB_Numm


        PB_cycles = Int(PB_multiplier * PB_Numm / PB_pulselen) '-5  '5 cycle correction


        ptb0 = PB_cycles And &HFFS


        PB_cycles = (PB_cycles - ptb0) / 256


        ptb1 = PB_cycles And &HFFS


        PB_cycles = (PB_cycles - ptb1) / 256


        ptb2 = PB_cycles And &HFFS


        PB_cycles = (PB_cycles - ptb2) / 256

        ptb3 = PB_cycles And &HFFS

        Call outt(BaseAddress + 6, (ptb3)) 'bit 3
        Call outt(BaseAddress + 6, (ptb2)) 'bit 2
        Call outt(BaseAddress + 6, (ptb1)) 'bit 1
        Call outt(BaseAddress + 6, (ptb0)) 'bit 0

    End Sub
    Public Sub dmk_exit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles dmk_exit.Click
        End
    End Sub

    Private Sub DMK_v1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim ulstat As Short
        'Dim i, ii As Short
        Dim i As Short
        Dim PBstat As Short

        PBstat = pb_init()
        PBstat = pb_set_clock(100.0)


        BaseAddress = 0

        '  declare revision level of Universal Library

        ulstat = cbDeclareRevision(CURRENTREVNUM)

        ' Initiate error handling
        '  activating error handling will trap errors like
        '  bad channel numbers and non-configured conditions.
        '  Parameters:
        '    PRINTALL    :all warnings and errors encountered will be printed
        '    DONTSTOP    :if an error is encountered, the program will not stop,
        '                  errors must be handled locally

        ulstat = cbErrHandling(PRINTALL, STOPALL)
        If ulstat <> 0 Then Stop

        ' If cbErrHandling% is set for STOPALL or STOPFATAL during the program
        ' design stage, Visual Basic will be unloaded when an error is encountered.
        ' We suggest trapping errors locally until the program is ready for compiling
        ' to avoid losing unsaved data during program design.  This can be done by
        ' setting cbErrHandling options as above and checking the value of ULStat%
        ' after a call to the library. If it is not equal to 0, an error has occurred.

        'FileOpen(1, "c:\testx.dat", OpenMode.Output)

        'For i = 1 To 100
        ' ii = 1
        ' Print(1, i & Chr(9) & System.Math.Sin(i * ii / 100) & Chr(9) & System.Math.Cos(i * ii / 100))
        ' For ii = 2 To 5
        ' Print(1, Chr(9) & i & Chr(9) & System.Math.Sin(i * ii / 100) & Chr(9) & System.Math.Cos(i * ii / 100))
        ' Next ii
        ' PrintLine(1, "")
        ' Next i
        ' FileClose(1)

        For i = 1 To 6
            'List1.Items.Add(Graph_Name(i))
            Call Plot_File("c:\test.dat", (i), (i And 1), False, 0)
        Next i


        FileOpen(1, "c:\temp.dat", OpenMode.Output)

        PrintLine(1, "")
        FileClose(1)

        '        ulstat = cbDConfigPort(BoardNum, AUXPORT, DIGITALOUT)
        '        D_Out_Mask = 0
        '        ulstat = cbDOut(BoardNum, AUXPORT, D_Out_Mask)


    End Sub


    Sub Hold_on_There(ByRef Wait_Time As Single)
        Dim Start_of_Wait As Single

        Start_of_Wait = VB.Timer()
        Bug_Out = False

        'Midnight bug fix:
        ' Adjust the waiting time by subtracting a day if the wait should end tomorrow
        If ((Wait_Time + Start_of_Wait) > (24 * 60 * 60)) Then
            Wait_Time = Wait_Time - (24 * 60 * 60)
        End If

        While VB.Timer() - Start_of_Wait < Wait_Time And Bug_Out = False And Halt_Script = False

            Label2.Text = CStr(Int(10 * (Wait_Time - (VB.Timer() - Start_of_Wait))) / 10)

            System.Windows.Forms.Application.DoEvents()

        End While

    End Sub


    Sub Plot_File(ByRef Plot_File_name As String, ByVal Plot_Window As Short, ByVal Plot_Delta As Short, ByVal add_to As Integer, ByVal linear_x As Integer) ', ByRef graph_name() As Object)

        Dim Number_Measuring_Lights As Integer
        Dim i, ii, total_points As Integer
        Dim j As String
        Dim Plot_Data_X(,) As Object
        Dim Plot_Data_Y(,) As Object
        Dim temp As Single = 0

        ' figure out how many columns
        FileOpen(1, Plot_File_name, OpenMode.Input)
        s_Plot_File_Name(Plot_Window) = Plot_File_name
        s_Plot_File_Type(Plot_Window) = Plot_Delta

        j = LineInput(1)
        FileClose(1)
        '        List1.Items.Clear()

        Number_Measuring_Lights = 1
        For i = 1 To Len(j)
            'List1.AddItem Mid$(j$, i%, 1) & Asc(Mid$(j$, i%, 1))
            If Asc(Mid(j, i, 1)) = 9 Then  'counting the number of tabs
                Number_Measuring_Lights = Number_Measuring_Lights + 1
            End If
        Next i

        Number_Measuring_Lights = Number_Measuring_Lights / 4
        FileOpen(1, Plot_File_name, OpenMode.Input)
        i = 0

        While EOF(1) = False
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            i = i + 1
        End While  'coiunt the total number of points, each has 4 data
        FileClose(1)
        total_points = i / Number_Measuring_Lights
        ReDim Plot_Data_X(Number_Measuring_Lights, i)
        ReDim Plot_Data_Y(Number_Measuring_Lights, i)

        i = 0
        FileOpen(1, Plot_File_name, OpenMode.Input)
        If Plot_Delta = 1 Then
            While EOF(1) = False
                For ii = 1 To Number_Measuring_Lights
                    Input(1, Plot_Data_X(ii, i))
                    If linear_x >= 1 Then
                        Plot_Data_X(ii, i) = i
                    End If
                    Input(1, temp)
                    Input(1, temp)
                    Input(1, Plot_Data_Y(ii, i))

                Next ii
                i = i + 1
            End While
        ElseIf Plot_Delta = 0 Then
            While EOF(1) = False
                For ii = 1 To Number_Measuring_Lights
                    Input(1, Plot_Data_X(ii, i))
                    Input(1, Plot_Data_Y(ii, i))
                    Input(1, temp)
                    Input(1, temp)

                Next ii
                i = i + 1
            End While
        ElseIf Plot_Delta = 2 Then  'plot reference
            While EOF(1) = False
                For ii = 1 To Number_Measuring_Lights
                    Input(1, Plot_Data_X(ii, i))
                    Input(1, temp)
                    Input(1, Plot_Data_Y(ii, i))
                    Input(1, temp)

                Next ii
                i = i + 1
            End While
        End If
        FileClose(1)

        'Call CreateGraph(zg1, total_points, Plot_Data_X, Plot_Data_Y)




        Select Case Plot_Window
            Case 1
                Call CreateGraph(zg1, Number_Measuring_Lights, total_points, Plot_Data_X, Plot_Data_Y, Plot_Delta, Graph_Name(1), add_to, Plot_Window)
            Case 2
                Call CreateGraph(zg2, Number_Measuring_Lights, total_points, Plot_Data_X, Plot_Data_Y, Plot_Delta, Graph_Name(2), add_to, Plot_Window)
            Case 3
                Call CreateGraph(zg3, Number_Measuring_Lights, total_points, Plot_Data_X, Plot_Data_Y, Plot_Delta, Graph_Name(3), add_to, Plot_Window)
            Case 4
                Call CreateGraph(zg4, Number_Measuring_Lights, total_points, Plot_Data_X, Plot_Data_Y, Plot_Delta, Graph_Name(4), add_to, Plot_Window)
            Case 5
                Call CreateGraph(zg5, Number_Measuring_Lights, total_points, Plot_Data_X, Plot_Data_Y, Plot_Delta, Graph_Name(5), add_to, Plot_Window)
            Case 6
                Call CreateGraph(zg6, Number_Measuring_Lights, total_points, Plot_Data_X, Plot_Data_Y, Plot_Delta, Graph_Name(6), add_to, Plot_Window)

        End Select

    End Sub

    
    Private Sub UpdateLabels(ByRef This_Graph As Integer, ByRef Low_Val As Single, ByRef High_Val As Single)

    End Sub
    Sub Delta_A_no_ref(ByRef I_File_Name As String, ByVal baseline_start As Integer, ByVal baseline_end As Integer)

        Dim Number_columns As Object
        Dim Number_Measuring_Lights As Short
        Dim i, ii As Short
        Dim total_points As Short
        Dim j As String
        Dim p, pp As Integer
        'Dim ppp, p, pp, light As Object
        'Dim Column_N As Short

        Dim Delta_Data_X() As Object
        Dim Delta_Data_Y() As Object
        Dim Delta_Data_ref() As Object
        Dim temp As Single = 0
        Dim tempp As Single
        'Dim I0_Y As Single
        Dim I0_Data_Y() As Single
        Dim av_baseline() As Single

        ' figure out how many columns are in I_FILE


        FileOpen(1, I_File_Name, OpenMode.Input)
        j = LineInput(1)
        FileClose(1)


        Number_Measuring_Lights = 1
        For i = 1 To Len(j)

            'List1.AddItem Mid$(j$, i%, 1) & Asc(Mid$(j$, i%, 1))

            If Asc(Mid(j, i, 1)) = 9 Then
                Number_Measuring_Lights = Number_Measuring_Lights + 1
            End If
        Next i

        'List1.AddItem j$
        'List1.AddItem "l: " & Len(j$)

        Number_Measuring_Lights = Number_Measuring_Lights / 4

        Number_columns = Number_Measuring_Lights


        FileOpen(1, I_File_Name, OpenMode.Input)

        i = 0

        While EOF(1) = False
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            i = i + 1
        End While
        FileClose(1)
        total_points = i
        'dimension the temporary arrays

        ReDim Delta_Data_X(i)
        ReDim Delta_Data_Y(i)
        ReDim Delta_Data_ref(i)

        ReDim I0_Data_Y(i)
        ReDim av_baseline(Number_Measuring_Lights)

        ' input the RAW data from I_FILE

        i = 0

        FileOpen(1, I_File_Name, OpenMode.Input)


        While EOF(1) = False
            Input(1, Delta_Data_X(i))
            Input(1, Delta_Data_Y(i))
            Input(1, Delta_Data_ref(i))
            Input(1, temp)
            i = i + 1
        End While

        FileClose(1)

        For pp = 0 To Number_Measuring_Lights - 1
            av_baseline(pp) = 0
        Next pp
        i = 0
        ii = 0
        For p = 0 To total_points - Number_Measuring_Lights Step Number_Measuring_Lights

            If i >= baseline_start And i <= baseline_end Then
                ii = ii + 1
                For pp = 0 To Number_Measuring_Lights - 1
                    av_baseline(pp) = av_baseline(pp) + Delta_Data_Y(i + pp)
                Next pp
            End If
            i = i + 1
        Next p

        For pp = 0 To Number_Measuring_Lights - 1
            av_baseline(pp) = av_baseline(pp) / ii
        Next pp
        ' input the RAW data from I0_FILE

        i = 0


        ' now open I0_FILE, calculate (deltaA, then resave the data into I_FILE

        FileOpen(1, I_File_Name, OpenMode.Output)

        For i = 0 To total_points - Number_columns Step Number_columns
            'Column_N = 0


            For p = 0 To Number_Measuring_Lights - 1

                If (Delta_Data_Y(i + p) / av_baseline(p)) > 0 Then
                    tempp = (Math.Log10(Delta_Data_Y(i + p) / av_baseline(p)))
                Else
                    tempp = 0
                End If
                Print(1, Delta_Data_X(i + p) & Chr(9) & Delta_Data_Y(i + p) & Chr(9) & Delta_Data_ref(i + p) & Chr(9) & tempp)
            Next p
            PrintLine(1, "")
        Next i
        FileClose(1)


    End Sub

    Sub Delta_A_WITH_ref(ByRef I_File_Name As String, ByVal baseline_start As Integer, ByVal baseline_end As Integer)

        Dim Number_columns As Object
        Dim Number_Measuring_Lights As Short
        Dim i, ii As Short
        Dim total_points As Short
        Dim j As String
        Dim p, pp As Integer
        'Dim ppp, p, pp, light As Object
        'Dim Column_N As Short

        Dim Delta_Data_X() As Object
        Dim Delta_Data_Y() As Object
        Dim Delta_Data_ref() As Object
        Dim temp As Single = 0
        Dim tempp As Single
        'Dim I0_Y As Single
        Dim I0_Data_Y() As Single
        Dim av_baseline() As Single

        ' figure out how many columns are in I_FILE


        FileOpen(1, I_File_Name, OpenMode.Input)
        j = LineInput(1)
        FileClose(1)


        Number_Measuring_Lights = 1
        For i = 1 To Len(j)

            'List1.AddItem Mid$(j$, i%, 1) & Asc(Mid$(j$, i%, 1))

            If Asc(Mid(j, i, 1)) = 9 Then
                Number_Measuring_Lights = Number_Measuring_Lights + 1
            End If
        Next i

        'List1.AddItem j$
        'List1.AddItem "l: " & Len(j$)

        Number_Measuring_Lights = Number_Measuring_Lights / 4

        Number_columns = Number_Measuring_Lights


        FileOpen(1, I_File_Name, OpenMode.Input)

        i = 0

        While EOF(1) = False
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            i = i + 1
        End While
        FileClose(1)
        total_points = i
        'dimension the temporary arrays

        ReDim Delta_Data_X(i)
        ReDim Delta_Data_Y(i)
        ReDim Delta_Data_ref(i)

        ReDim I0_Data_Y(i)
        ReDim av_baseline(Number_Measuring_Lights)

        ' input the RAW data from I_FILE

        i = 0

        FileOpen(1, I_File_Name, OpenMode.Input)


        While EOF(1) = False
            Input(1, Delta_Data_X(i))
            Input(1, Delta_Data_Y(i))
            Input(1, Delta_Data_ref(i))
            Input(1, temp)
            i = i + 1
        End While

        FileClose(1)

        For pp = 0 To Number_Measuring_Lights - 1
            av_baseline(pp) = 0
        Next pp
        i = 0
        ii = 0
        For p = 0 To total_points - Number_Measuring_Lights Step Number_Measuring_Lights

            If i >= baseline_start And i <= baseline_end Then
                ii = ii + 1
                For pp = 0 To Number_Measuring_Lights - 1

                    If Delta_Data_ref(p + pp) <> 0 Then

                        If (Delta_Data_Y(p + pp) / Delta_Data_ref(p + pp)) > 0 Then
                            av_baseline(pp) = av_baseline(pp) + (Math.Log10(Delta_Data_ref(p + pp) / Delta_Data_Y(p + pp)))
                        End If
                    End If

                Next pp
            End If
            i = i + 1
        Next p

        For pp = 0 To Number_Measuring_Lights - 1
            av_baseline(pp) = av_baseline(pp) / ii
        Next pp
        ' input the RAW data from I0_FILE

        i = 0


        ' now open I0_FILE, calculate (deltaA, then resave the data into I_FILE

        FileOpen(1, I_File_Name, OpenMode.Output)

        For i = 0 To total_points - Number_columns Step Number_columns
            'Column_N = 0


            For p = 0 To Number_Measuring_Lights - 1
                If Delta_Data_ref(i + p) <> 0 Then
                    If (Delta_Data_Y(i + p) / Delta_Data_ref(i + p)) > 0 Then
                        tempp = (Math.Log10(Delta_Data_ref(i + p) / Delta_Data_Y(i + p))) - av_baseline(p)
                    Else
                        tempp = 0
                    End If
                End If
                Print(1, Delta_Data_X(i + p) & Chr(9) & Delta_Data_Y(i + p) & Chr(9) & Delta_Data_ref(i + p) & Chr(9) & tempp & Chr(9))
            Next p
            PrintLine(1, "")
        Next i
        FileClose(1)


    End Sub

    Sub Zero_Point(ByRef I_File_Name As String, ByVal point_to_zero As Integer)

        Dim Number_columns As Object
        Dim Number_Measuring_Lights As Short
        Dim i, ii As Short
        Dim total_points As Short
        Dim j As String
        Dim p, pp As Integer
        'Dim ppp, p, pp, light As Object
        'Dim Column_N As Short

        Dim Delta_Data_X() As Object
        Dim Delta_Data_Y() As Object
        Dim Delta_Data_ref() As Object
        Dim Delta_Data_Delta() As Object
        Dim temp As Single = 0
        Dim tempp As Single
        'Dim I0_Y As Single
        Dim I0_Data_Y() As Single
        Dim av_baseline() As Single

        ' figure out how many columns are in I_FILE


        FileOpen(1, I_File_Name, OpenMode.Input)
        j = LineInput(1)
        FileClose(1)


        Number_Measuring_Lights = 1
        For i = 1 To Len(j)

            'List1.AddItem Mid$(j$, i%, 1) & Asc(Mid$(j$, i%, 1))

            If Asc(Mid(j, i, 1)) = 9 Then
                Number_Measuring_Lights = Number_Measuring_Lights + 1
            End If
        Next i

        'List1.AddItem j$
        'List1.AddItem "l: " & Len(j$)

        Number_Measuring_Lights = Number_Measuring_Lights / 4

        Number_columns = Number_Measuring_Lights


        FileOpen(1, I_File_Name, OpenMode.Input)

        i = 0

        While EOF(1) = False
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            i = i + 1
        End While
        FileClose(1)
        total_points = i

        'dimension the temporary arrays

        ReDim Delta_Data_X(i)
        ReDim Delta_Data_Y(i)
        ReDim Delta_Data_ref(i)
        ReDim Delta_Data_Delta(i)
        ReDim I0_Data_Y(i)
        ReDim av_baseline(Number_Measuring_Lights)

        ' input the RAW data from I_FILE

        i = 0

        FileOpen(1, I_File_Name, OpenMode.Input)


        While EOF(1) = False
            Input(1, Delta_Data_X(i))
            Input(1, Delta_Data_Y(i))
            Input(1, Delta_Data_ref(i))
            Input(1, Delta_Data_Delta(i))
            i = i + 1
        End While

        FileClose(1)

        i = 0
        ii = 0
        For p = 0 To total_points - Number_Measuring_Lights Step Number_Measuring_Lights
            i = i + 1
            If i = point_to_zero Then

                For pp = 0 To Number_Measuring_Lights - 1


                Next pp
            End If
            i = i + 1
        Next p

        ' input the RAW data from I0_FILE

        i = 0


        ' now open I0_FILE, calculate (deltaA, then resave the data into I_FILE

        FileOpen(1, I_File_Name, OpenMode.Output)

        For i = 0 To total_points - Number_columns Step Number_columns
            'Column_N = 0


            For p = 0 To Number_Measuring_Lights - 1

                If i = point_to_zero - 1 Then
                    tempp = 0
                Else
                    tempp = Delta_Data_Delta(i + p)
                End If

                Print(1, Delta_Data_X(i + p) & Chr(9) & Delta_Data_Y(i + p) & Chr(9) & Delta_Data_ref(i + p) & Chr(9) & tempp & Chr(9))
            Next p
            PrintLine(1, "")
        Next i
        FileClose(1)


    End Sub



    Sub delete_point(ByRef I_File_Name As String, ByVal point_to_delete As Integer)

        Dim Number_columns As Object
        Dim Number_Measuring_Lights As Short
        Dim i As Short
        'Dim i, ii As Short
        Dim total_points As Short
        Dim j As String
        'Dim p, pp As Integer
        Dim p As Integer
        'Dim ppp, p, pp, light As Object
        'Dim Column_N As Short

        Dim Delta_Data_X() As Object
        Dim Delta_Data_Y() As Object
        Dim Delta_Data_ref() As Object
        Dim temp As Single = 0
        Dim tempp As Single
        'Dim I0_Y As Single
        Dim I0_Data_Y() As Single
        Dim av_baseline() As Single

        ' figure out how many columns are in I_FILE


        FileOpen(1, I_File_Name, OpenMode.Input)
        j = LineInput(1)
        FileClose(1)


        Number_Measuring_Lights = 1
        For i = 1 To Len(j)

            'List1.AddItem Mid$(j$, i%, 1) & Asc(Mid$(j$, i%, 1))

            If Asc(Mid(j, i, 1)) = 9 Then
                Number_Measuring_Lights = Number_Measuring_Lights + 1
            End If
        Next i

        'List1.AddItem j$
        'List1.AddItem "l: " & Len(j$)

        Number_Measuring_Lights = Number_Measuring_Lights / 4

        Number_columns = Number_Measuring_Lights


        FileOpen(1, I_File_Name, OpenMode.Input)

        i = 0

        While EOF(1) = False
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            i = i + 1
        End While
        FileClose(1)
        total_points = i
        'dimension the temporary arrays

        ReDim Delta_Data_X(i)
        ReDim Delta_Data_Y(i)
        ReDim Delta_Data_ref(i)

        ReDim I0_Data_Y(i)
        ReDim av_baseline(Number_Measuring_Lights)

        ' input the RAW data from I_FILE

        i = 0

        FileOpen(1, I_File_Name, OpenMode.Input)


        While EOF(1) = False
            Input(1, Delta_Data_X(i))
            Input(1, Delta_Data_Y(i))
            Input(1, Delta_Data_ref(i))
            Input(1, temp)
            i = i + 1
        End While

        FileClose(1)



        ' now open I0_FILE, then resave the data into I_FILE

        FileOpen(1, I_File_Name, OpenMode.Output)

        For i = 0 To total_points - Number_columns Step Number_columns
            'Column_N = 0


            For p = 0 To Number_Measuring_Lights - 1

                If (Delta_Data_Y(i + p) / av_baseline(p)) > 0 Then
                    tempp = (Math.Log10(Delta_Data_Y(i + p) / av_baseline(p)))
                Else
                    tempp = 0
                End If
                Print(1, Delta_Data_X(i + p) & Chr(9) & Delta_Data_Y(i + p) & Chr(9) & Delta_Data_ref(i + p) & Chr(9) & tempp)
            Next p
            PrintLine(1, "")
        Next i
        FileClose(1)


    End Sub
    Sub Delta(ByRef I_File_Name As String, ByRef I0_File_Name As String)

        Dim Number_columns As Object
        Dim Number_Measuring_Lights As Short
        Dim i As Short
        Dim j As String
        Dim p, pp As Integer
        'Dim ppp, p, pp, light As Object
        'Dim Column_N As Short

        Dim Delta_Data_X() As Object
        Dim Delta_Data_Y() As Object
        Dim temp As Single = 0
        'Dim I0_Y As Single
        Dim I0_Data_Y() As Single

        ' figure out how many columns are in I_FILE


        FileOpen(1, I_File_Name, OpenMode.Input)
        j = LineInput(1)
        FileClose(1)
        '        List1.Items.Clear()

        Number_Measuring_Lights = 1
        For i = 1 To Len(j)

            'List1.AddItem Mid$(j$, i%, 1) & Asc(Mid$(j$, i%, 1))

            If Asc(Mid(j, i, 1)) = 9 Then
                Number_Measuring_Lights = Number_Measuring_Lights + 1
            End If
        Next i

        'List1.AddItem j$
        'List1.AddItem "l: " & Len(j$)

        Number_Measuring_Lights = Number_Measuring_Lights / 4

        Number_columns = Number_Measuring_Lights


        FileOpen(1, I_File_Name, OpenMode.Input)

        i = 0

        While EOF(1) = False
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            i = i + 1
        End While
        FileClose(1)

        'dimension the temporary arrays

        ReDim Delta_Data_X(i)
        ReDim Delta_Data_Y(i)
        ReDim I0_Data_Y(i)

        ' input the RAW data from I_FILE

        i = 0

        FileOpen(1, I_File_Name, OpenMode.Input)

        While EOF(1) = False
            Input(1, Delta_Data_X(i))
            Input(1, Delta_Data_Y(i))
            Input(1, temp)
            Input(1, temp)
            i = i + 1
        End While

        FileClose(1)


        ' input the RAW data from I0_FILE

        i = 0

        FileOpen(1, I0_File_Name, OpenMode.Input)

        While EOF(1) = False
            Input(1, temp)
            Input(1, I0_Data_Y(i))
            Input(1, temp)
            Input(1, temp)
            i = i + 1
        End While

        FileClose(1)

        ' now open I0_FILE, calculate (I-I0)/I0, then resave the data into I_FILE

        FileOpen(1, I_File_Name, OpenMode.Output)



        For pp = 0 To i - 1 Step Number_columns
            'Column_N = 0


            For p = pp To pp + Number_columns - 1

                temp = Int(1000000 * ((Delta_Data_Y(p) / I0_Data_Y(p)) - 1)) / 1000000

                If p > pp Then Print(1, Chr(9))


                Print(1, Delta_Data_X(p) & Chr(9) & Int(1000000 * Delta_Data_Y(p)) / 1000000 & Chr(9) & temp & Chr(9) & temp)
            Next p
            PrintLine(1, "")
        Next pp
        FileClose(1)


    End Sub



    Sub Dirk_Save(ByRef data_time() As Single, ByRef points As Integer, ByRef File_Name As String, ByRef Save_Mode As Short, ByRef Times_Averaged As Short, ByRef S_Time_Mode As String, ByRef Start_Time As Single, ByRef Number_columns As Short, ByRef Baseline_Start As Object, ByRef Baseline_End As Object, ByRef In_Channel As Integer)

        Dim p, pp As Object
        Dim ii As Integer
        Dim Column_N As Short
        Dim Time_Offset As Single
        Dim Baseline_Y() As Single
        'Dim j As String
        Dim junk As Single
        'Dim Reference_Data_Y() As Single

        Dim Accumulated_Data_X() As Single
        Dim Accumulated_Data_Y() As Single
        Dim Accumulated_Data_Ref() As Single

        ReDim Accumulated_Data_X(points)
        ReDim Accumulated_Data_Y(points)
        ReDim Accumulated_Data_Ref(points)
        Dim Reference_Data_Y(points) As Single

        ReDim Baseline_Y(Number_columns)

        '        If take_notes = True Then
        ' note_text = note_text & " saved data: " & File_Name & "offset-taken: " & offset
        ' FileOpen(7, Note_File_Name, OpenMode.Output)
        ' Write(7, note_text)
        ' FileClose(7)
        ' End If

        System.Windows.Forms.Application.DoEvents()

        If Baseline_Start > points Then Baseline_Start = points

        If Baseline_End > points Then Baseline_End = points

        's
        ' take offset from all raw values
        For pp = 0 To points - 1
            For ii = 0 To 3
                Data_Volts(pp, ii) = -1 * (Data_Volts(pp, ii) - offset)
            Next ii
        Next pp


        If File_Name = "" Then
            List1.Items.Add("no file name!")
            'Scrolling text box fix: make the box jump to the bottom after adding an item
            List1.TopIndex = List1.Items.Count - 1
            Exit Sub
        End If

        If S_Time_Mode = "from_zero" Then
            Time_Offset = 0
        ElseIf S_Time_Mode = "sequential" Then
            Time_Offset = Start_Time
        End If

        '        Save_Mode = File_Replace

        If Save_Mode = File_Replace Then

            ' calcluate baselines

            '            For pp = Baseline_Start * Number_columns To Baseline_End * Number_columns Step Number_columns
            'Column_N = 0
            'For p = pp To pp + Number_columns - 1
            'Column_N = Column_N + 1
            'Baseline_Y(Column_N) = Baseline_Y(Column_N) + Data_Volts(p, In_Channel)
            'Next p
            'Next pp

            'For Column_N = 1 To Number_columns
            ' Baseline_Y(Column_N) = Baseline_Y(Column_N) / (Baseline_End - Baseline_Start + 1)

            'Next Column_N

            FileOpen(1, File_Name, OpenMode.Output)
            '    Print #1, "m", Number_Columns

            For pp = 0 To points - 1 'Step Number_columns
                Column_N = 0

                For p = pp To pp + Number_columns - 1
                    'Column_N = Column_N + 1

                    If p > pp Then Print(1, Chr(9))
                    Print(1, Time_Offset + data_time(p) & Chr(9) & Data_Volts(p, 0) & Chr(9))
                    Print(1, (Data_Volts(p, 2)) & Chr(9))
                    If Math.Abs(Data_Volts(p, 2)) > 0 And (Data_Volts(p, In_Channel) / Data_Volts(p, 2)) > 0 Then
                        Print(1, -1 * Math.Log10(Data_Volts(p, In_Channel) / Data_Volts(p, 2)))
                    Else
                        Print(1, 0)
                    End If
                Next p
                PrintLine(1, "")
            Next pp
            FileClose(1)

        ElseIf Save_Mode = File_Append Then

            ' calcluate baselines

            For pp = Baseline_Start * Number_columns To Baseline_End * Number_columns Step Number_columns
                Column_N = 0
                For p = pp To pp + Number_columns - 1
                    Column_N = Column_N + 1
                    Baseline_Y(Column_N) = Baseline_Y(Column_N) + Data_Volts(p, In_Channel)
                Next p
            Next pp

            For Column_N = 1 To Number_columns

                Baseline_Y(Column_N) = Baseline_Y(Column_N) / (Baseline_End - Baseline_Start + 1)

            Next Column_N


            FileOpen(1, File_Name, OpenMode.Append)

            For pp = 0 To points - 1 Step Number_columns
                Column_N = 0

                For p = pp To pp + Number_columns - 1
                    Column_N = Column_N + 1
                    If p > pp Then Print(1, Chr(9))


                    Print(1, Time_Offset + data_time(p) & Chr(9) & Data_Volts(p, In_Channel) & Chr(9))
                    Print(1, (Data_Volts(p, 2)) & Chr(9))
                    If ref_channel = True Then

                        If Math.Abs(Data_Volts(p, 2)) > 0 And (Data_Volts(p, In_Channel) / Data_Volts(p, 2)) > 0 Then
                            Print(1, -1 * Math.Log10(Data_Volts(p, In_Channel) / Data_Volts(p, 2)))
                        Else
                            Print(1, 0)
                        End If
                    Else

                        'Print(1, Time_Offset + data_time(p) & Chr(9) & Data_Volts(p, In_Channel) & Chr(9))
                        'List1.AddItem p & " " & data_volts(p)
                        '                If Baseline_Y(Column_N) <> 0 Then
                        'Print(1, (Data_Volts(p, In_Channel) / Data_Volts(p, In_Channel + 1) - 1))
                        Print(1, ((Data_Volts(p, In_Channel) / Baseline_Y(Column_N)) - 1))
                        'Print #1, (Int(1000000 * (Data_Volts(p) / Baseline_Y(Column_N)) - 1)) / 1000000;
                        'Else
                        'Print(1, 0)
                        'End If
                        'List1.AddItem p & " " & data_volts(p)
                    End If
                Next p
                PrintLine(1, "")
            Next pp
            FileClose(1)

        ElseIf Save_Mode = Average_Into Then
            If Times_Averaged = 1 Then ' first time off, just save trace

                ' calcluate baselines


                For pp = Baseline_Start * Number_columns To Baseline_End * Number_columns Step Number_columns
                    Column_N = 0
                    For p = pp To pp + Number_columns - 1
                        Column_N = Column_N + 1
                        Baseline_Y(Column_N) = Baseline_Y(Column_N) + Data_Volts(p, In_Channel)
                    Next p
                Next pp

                For Column_N = 1 To Number_columns
                    Baseline_Y(Column_N) = Baseline_Y(Column_N) / (Baseline_End - Baseline_Start + 1)

                    'List1.AddItem "col " & Column_N & " " & Baseline_Y(Column_N)
                Next Column_N

                FileOpen(1, File_Name, OpenMode.Output)
                '       Print #1, "m", Number_Columns
                For pp = 0 To points - 1 Step Number_columns
                    Column_N = 0

                    For p = pp To pp + Number_columns - 1
                        Column_N = Column_N + 1
                        If p > pp Then Print(1, Chr(9))

                        Print(1, Time_Offset + data_time(p) & Chr(9) & Data_Volts(p, In_Channel) & Chr(9))
                        Print(1, (Data_Volts(p, 2)) & Chr(9))

                        If ref_channel = True Then
                            If Math.Abs(Data_Volts(p, 2)) > 0 And (Data_Volts(p, In_Channel) / Data_Volts(p, 2)) > 0 Then
                                Print(1, -1 * Math.Log10(Data_Volts(p, In_Channel) / Data_Volts(p, 2)))
                            Else
                                Print(1, 0)
                            End If
                        Else
                            If Math.Abs(Baseline_Y(Column_N)) > 0 And (Data_Volts(p, In_Channel) / Baseline_Y(Column_N)) > 0 Then
                                Print(1, -1 * Math.Log10(Data_Volts(p, In_Channel) / Baseline_Y(Column_N)))
                            Else : Print(1, 0)
                            End If

                        End If


                        '                        Print(1, ((Data_Volts(p, In_Channel) / Baseline_Y(Column_N)) - 1))

                        '                        Print(1, Time_Offset + data_time(p) & Chr(9) & Data_Volts(p, In_Channel) & Chr(9))

                        '                       Print(1, (Data_Volts(p, In_Channel) / Data_Volts(p, In_Channel + 1) - 1))

                        'Print(1, Int(1000000 * ((Data_Volts(p) / Baseline_Y(Column_N)) - 1)) / 1000000)
                        'Print #1, (Int(1000000 * (Data_Volts(p) / Baseline_Y(Column_N)) - 1)) / 1000000;

                        'Else
                        'Print(1, 0)
                        'End If
                        'List1.AddItem p & " " & data_volts(p)
                    Next p
                    PrintLine(1, "")
                Next pp
                FileClose(1)


            Else
                FileOpen(1, File_Name, OpenMode.Input) 'input already saved data
                For pp = 0 To points - 1 Step Number_columns

                    For p = pp To pp + Number_columns - 1
                        Input(1, junk)
                        Input(1, Accumulated_Data_Y(p))
                        Input(1, Accumulated_Data_Ref(p))
                        Input(1, junk) ' throw away the deltaI/I data
                    Next p

                Next pp
                FileClose(1)
                'next make average with current data

                For p = 0 To points - 1
                    Data_Volts(p, In_Channel) = (Data_Volts(p, In_Channel) + ((Times_Averaged - 1) * Accumulated_Data_Y(p))) / Times_Averaged
                    Data_Volts(p, 2) = (Data_Volts(p, 2) + ((Times_Averaged - 1) * Accumulated_Data_Ref(p))) / Times_Averaged
                Next p


                ' calcluate baselines

                For pp = Baseline_Start * Number_columns To Baseline_End * Number_columns Step Number_columns
                    Column_N = 0
                    For p = pp To pp + Number_columns - 1
                        Column_N = Column_N + 1
                        Baseline_Y(Column_N) = Baseline_Y(Column_N) + Data_Volts(p, In_Channel)
                    Next p
                Next pp

                For Column_N = 1 To Number_columns
                    Baseline_Y(Column_N) = Baseline_Y(Column_N) / (Baseline_End - Baseline_Start + 1)
                Next Column_N



                FileOpen(1, File_Name, OpenMode.Output)
                For pp = 0 To points - 1 Step Number_columns
                    Column_N = 0
                    For p = pp To pp + Number_columns - 1
                        Column_N = Column_N + 1
                        If p > pp Then Print(1, Chr(9))


                        Print(1, Time_Offset + data_time(p) & Chr(9) & Data_Volts(p, In_Channel) & Chr(9))
                        Print(1, (Data_Volts(p, 2)) & Chr(9))
                        'Print(1, Math.Log10(Data_Volts(p, In_Channel) / Data_Volts(p, 2)))

                        If ref_channel = True Then
                            If Math.Abs(Data_Volts(p, 2)) > 0 And (Data_Volts(p, In_Channel) / Data_Volts(p, 2)) > 0 Then
                                Print(1, -1 * Math.Log10(Data_Volts(p, In_Channel) / Data_Volts(p, 2)))
                            Else
                                Print(1, 0)
                            End If
                        Else
                            If Math.Abs(Data_Volts(p, 2)) > 0 And (Data_Volts(p, In_Channel) / Data_Volts(p, 2)) > 0 Then
                                Print(1, -1 * Math.Log10(Data_Volts(p, In_Channel) / Baseline_Y(Column_N)))
                            Else
                                Print(1, 0)
                            End If
                        End If
                        '                        Print(1, Time_Offset + data_time(p) & Chr(9) & Data_Volts(p, In_Channel) & Chr(9))
                        'List1.AddItem p & " " & data_volts(p)
                        'If Baseline_Y(Column_N) <> 0 Then
                        'Print #1, (Data_Volts(p) / Baseline_Y(Column_N)) - 1;
                        '                       Print(1, (Data_Volts(p, In_Channel) / Data_Volts(p, In_Channel + 1) - 1))
                        'Print(1, Int(1000000 * ((Data_Volts(p) / Baseline_Y(Column_N)) - 1)) / 1000000)
                        'Print #1, (Int(1000000 * (Data_Volts(p) / Baseline_Y(Column_N)) - 1)) / 1000000;
                        'Else
                        'Print(1, 0)
                        'End If
                        'List1.AddItem p & " " & data_volts(p)
                    Next p
                    PrintLine(1, "")
                Next pp
                FileClose(1)

                '        Open File_Name For Output As #1
                '        For p = 1 To Points& - 1
                '            Print #1, Accumulated_Data_X(p), Accumulated_Data_Y(p)
                '
                '        Next p
                '        Close 1

            End If
        End If


    End Sub

    Private Function Number_Dirk_Points(ByRef Baseline_Number_Points As Short, ByRef Dirk_Number_Points As Short, ByRef Recovery_Number_Points As Short) As Short
        Number_Dirk_Points = Baseline_Number_Points + Dirk_Number_Points + Recovery_Number_Points
    End Function
    Private Sub subtract_traces(ByRef File_Name_1 As String, ByRef File_Name_2 As String, ByRef File_Name_3 As String)

        Dim Accumulated_Data_X() As Single
        Dim Accumulated_Data_Y() As Single
        Dim Accumulated_Data_Ref() As Single
        Dim data1, data2 As Single
        Dim p, pp, i As Integer
        ReDim Accumulated_Data_X(points)
        ReDim Accumulated_Data_Y(points)
        ReDim Accumulated_Data_Ref(points)
        Dim Reference_Data_Y(points) As Single
        Dim j As String
        Dim temp As Single
        Dim Number_Measuring_Lights As Integer

        FileOpen(1, File_Name_1, OpenMode.Input)
        j = LineInput(1)
        FileClose(1)


        Number_Measuring_Lights = 0
        For i = 1 To Len(j)

            'List1.AddItem Mid$(j$, i%, 1) & Asc(Mid$(j$, i%, 1))

            If Asc(Mid(j, i, 1)) = 9 Then
                Number_Measuring_Lights = Number_Measuring_Lights + 1
            End If
        Next i
        Number_Measuring_Lights = Number_Measuring_Lights / 4

        FileOpen(1, File_Name_1, OpenMode.Input)

        i = 0
        temp = 0
        While EOF(1) = False
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            i = i + 1
        End While
        FileClose(1)
        points = i

        'ReDim Baseline_Y(Number_columns)

        FileOpen(1, File_Name_1, OpenMode.Input) 'input already saved data
        FileOpen(2, File_Name_2, OpenMode.Input) 'input already saved data
        FileOpen(3, File_Name_3, OpenMode.Output) 'export subtrated data




        For pp = 0 To points - 1 Step Number_Measuring_Lights

            For p = pp To pp + Number_Measuring_Lights - 1
                Input(2, data2)
                Input(1, data1)
                Print(3, data1 & Chr(9))

                Input(2, data2)
                Input(1, data1)
                Print(3, (data1 - data2) & Chr(9))

                Input(2, data2)
                Input(1, data1)
                Print(3, (data1 - data2) & Chr(9))

                Input(2, data2)
                Input(1, data1)
                Print(3, (data1 - data2) & Chr(9))

            Next p
            PrintLine(3, "")
        Next pp
        FileClose(1)
        FileClose(2)
        FileClose(3)


    End Sub
    Private Sub dice_average(ByRef File_Name_1 As String, ByRef File_Name_2 As String, ByVal dice_length As Integer)
        'list2.Items.Add(File_Name_1 & " -> " & File_Name_2)


        Dim Undiced_Data_X() As Single
        Dim Undiced_Data_Y_raw() As Single
        Dim Undiced_Data_Y_ref() As Single
        Dim Undiced_Data_Y_delta() As Single

        Dim Diced_Data_X() As Single
        Dim Diced_Data_Y_raw() As Single
        Dim Diced_Data_Y_ref() As Single
        Dim Diced_Data_Y_delta() As Single

        'Dim data1, data2 As Single
        Dim p, pp, i, ii, iii As Integer
        Dim j As String
        Dim temp As Single
        Dim Number_Measuring_Lights As Integer
        Dim number_diced As Single

        FileOpen(1, File_Name_1, OpenMode.Input)
        j = LineInput(1)
        FileClose(1)


        Number_Measuring_Lights = 0
        For i = 1 To Len(j)

            'List1.AddItem Mid$(j$, i%, 1) & Asc(Mid$(j$, i%, 1))

            If Asc(Mid(j, i, 1)) = 9 Then
                Number_Measuring_Lights = Number_Measuring_Lights + 1
            End If
        Next i
        Number_Measuring_Lights = Number_Measuring_Lights / 4

        FileOpen(1, File_Name_1, OpenMode.Input)

        i = 0
        temp = 0
        While EOF(1) = False
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            i = i + 1
        End While
        FileClose(1)
        points = i

        number_diced = (i / Number_Measuring_Lights) / dice_length

        ReDim Undiced_Data_X(points)
        ReDim Undiced_Data_Y_raw(points)
        ReDim Undiced_Data_Y_ref(points)
        ReDim Undiced_Data_Y_delta(points)

        ReDim Diced_Data_X(points)
        ReDim Diced_Data_Y_raw(points)
        ReDim Diced_Data_Y_ref(points)
        ReDim Diced_Data_Y_delta(points)

        For i = 1 To points
            Diced_Data_X(i) = 0
            Diced_Data_Y_raw(i) = 0
            Diced_Data_Y_ref(i) = 0
            Diced_Data_Y_delta(i) = 0

            Undiced_Data_X(i) = 0
            Undiced_Data_Y_raw(i) = 0
            Undiced_Data_Y_ref(i) = 0
            Undiced_Data_Y_delta(i) = 0

        Next
        'ReDim Baseline_Y(Number_columns)

        FileOpen(1, File_Name_1, OpenMode.Input) 'input already saved data


        i = 0

        For pp = 0 To points - 1 Step Number_Measuring_Lights

            If i = (dice_length) Then i = 0
            iii = 0
            For p = pp To (pp + Number_Measuring_Lights - 1)
                Input(1, Undiced_Data_X(p))
                Input(1, Undiced_Data_Y_raw(p))
                Input(1, Undiced_Data_Y_ref(p))
                Input(1, Undiced_Data_Y_delta(p))
                ii = i * Number_Measuring_Lights + iii
                Diced_Data_X(ii) = Undiced_Data_X(p)
                Diced_Data_Y_raw(ii) = Diced_Data_Y_raw(ii) + (Undiced_Data_Y_raw(p) / number_diced)
                Diced_Data_Y_ref(ii) = Diced_Data_Y_ref(ii) + (Undiced_Data_Y_ref(p) / number_diced)
                Diced_Data_Y_delta(ii) = Diced_Data_Y_delta(ii) + (Undiced_Data_Y_delta(p) / number_diced)
                iii = iii + 1
            Next p
            i = i + 1
        Next pp
        FileClose(1)

        FileOpen(3, File_Name_2, OpenMode.Output) 'export chopped data


        For pp = 0 To ((dice_length - 1) * Number_Measuring_Lights) Step Number_Measuring_Lights
            For p = pp To (pp + Number_Measuring_Lights - 1)
                Print(3, Diced_Data_X(p) & Chr(9))
                Print(3, Diced_Data_Y_raw(p) & Chr(9))
                Print(3, Diced_Data_Y_ref(p) & Chr(9))
                Print(3, Diced_Data_Y_delta(p))
            Next p
            PrintLine(3, "")

        Next pp
        FileClose(3)



    End Sub
    Private Function extract_point(ByRef File_Name_1 As String, ByRef Data_Point As Integer) As Single

        Dim Accumulated_Data_X() As Single
        Dim Accumulated_Data_Y() As Single
        Dim Accumulated_Data_Ref() As Single
        Dim data1, data2 As Single
        Dim p, pp, i As Integer
        ReDim Accumulated_Data_X(points)
        ReDim Accumulated_Data_Y(points)
        ReDim Accumulated_Data_Ref(points)
        Dim Reference_Data_Y(points) As Single
        Dim j As String
        Dim temp As Single
        Dim Number_Measuring_Lights As Integer

        FileOpen(1, File_Name_1, OpenMode.Input)
        j = LineInput(1)
        FileClose(1)


        Number_Measuring_Lights = 0
        For i = 1 To Len(j)

            'List1.AddItem Mid$(j$, i%, 1) & Asc(Mid$(j$, i%, 1))

            If Asc(Mid(j, i, 1)) = 9 Then
                Number_Measuring_Lights = Number_Measuring_Lights + 1
            End If
        Next i
        Number_Measuring_Lights = Number_Measuring_Lights / 4

        FileOpen(1, File_Name_1, OpenMode.Input)

        i = 0
        temp = 0
        While EOF(1) = False
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            i = i + 1
        End While
        FileClose(1)
        points = i

        'ReDim Baseline_Y(Number_columns)

        FileOpen(1, File_Name_1, OpenMode.Input) 'input already saved data

        i = 0

        For pp = 0 To points - 1 Step Number_Measuring_Lights

            For p = pp To pp + Number_Measuring_Lights - 1
                Input(1, data1)
                If i = (((Data_Point + 1) * 4) - 1) Then
                    data2 = data1
                End If
            Next p
            i = i + 1

        Next pp
        FileClose(1)

        Return (data2)
    End Function
    Private Function extract_point_raw(ByRef File_Name_1 As String, ByRef Data_Point As Integer) As Single

        Dim Accumulated_Data_X() As Single
        Dim Accumulated_Data_Y() As Single
        Dim Accumulated_Data_Ref() As Single
        Dim data1, data2 As Single
        Dim p, pp, i As Integer
        ReDim Accumulated_Data_X(points)
        ReDim Accumulated_Data_Y(points)
        ReDim Accumulated_Data_Ref(points)
        Dim Reference_Data_Y(points) As Single
        Dim j As String
        Dim temp As Single
        Dim Number_Measuring_Lights As Integer

        FileOpen(1, File_Name_1, OpenMode.Input)
        j = LineInput(1)
        FileClose(1)


        Number_Measuring_Lights = 0
        For i = 1 To Len(j)

            'List1.AddItem Mid$(j$, i%, 1) & Asc(Mid$(j$, i%, 1))

            If Asc(Mid(j, i, 1)) = 9 Then
                Number_Measuring_Lights = Number_Measuring_Lights + 1
            End If
        Next i
        Number_Measuring_Lights = Number_Measuring_Lights / 4

        FileOpen(1, File_Name_1, OpenMode.Input)

        i = 0
        temp = 0
        While EOF(1) = False
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            i = i + 1
        End While
        FileClose(1)
        points = i

        'ReDim Baseline_Y(Number_columns)

        FileOpen(1, File_Name_1, OpenMode.Input) 'input already saved data

        i = 0

        For pp = 0 To points - 1 Step Number_Measuring_Lights

            For p = pp To pp + Number_Measuring_Lights - 1
                Input(1, data1)
                If i = (((Data_Point + 1) * 4) - 3) Then
                    data2 = data1
                End If
            Next p
            i = i + 1

        Next pp
        FileClose(1)

        Return (data2)
    End Function

    Sub Shut_Down()

        Call outt(BaseAddress, 0) 'reset
        Call outt(BaseAddress + 2, &HAS) 'load number of BYTES/word (10)
        Call outt(BaseAddress + 3, 0) 'Select Memory Device
        Call outt(BaseAddress + 4, 0) '&H55s) 'Clear Address Counter

        'Line 0
        '***** delay one baseline measuring interval
        'Actinic_Intensity = 0

        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0)

        Call outt(BaseAddress + 6, 0)

        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0) 'continue


        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 1)

        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0) 'Actinic_Intensity)

        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 1) 'STOP


        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0)

        'Signal End of Programming
        'Call outt(BaseAddress + 6, 0)  'not needed?
        Call outt(BaseAddress + 7, 0) '7)

        'put the device in run mode
        Call outt(BaseAddress + 1, 0) '7)

        'Main.Label14.Caption = INsecs(fpoints&) + " secs"
        'Main.Label2.Caption = "Fm - fluoro"

    End Sub
    Sub Out_Intensity(ByRef Actinic_Intensity As Short, ByVal blue_actinic As Integer, ByVal far_red As Integer, ByVal saturating_pulse As Integer)

        'Actinic_Intensity = (Actinic_Intensity And 7) + ((Actinic_Intensity And 56) * 2)

        Call outt(BaseAddress, 0) 'reset
        Call outt(BaseAddress + 2, &HAS) 'load number of BYTES/word (10)
        Call outt(BaseAddress + 3, 0) 'Select Memory Device
        Call outt(BaseAddress + 4, 0) '&H55s) 'Clear Address Counter

        'Line 0
        '***** delay one baseline measuring interval
        'Actinic_Intensity = 0

        Call outt(BaseAddress + 6, (128 + 8 * far_red) + (32 * blue_actinic) + (16 * saturating_pulse))
        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, Actinic_Intensity)

        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0) 'continue


        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 1)

        '        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, (8 * far_red) + (32 * blue_actinic) + (16 * saturating_pulse))

        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, Actinic_Intensity)

        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 1) 'STOP


        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0)
        Call outt(BaseAddress + 6, 0)

        'skip:

        'Signal End of Programming
        'Call outt(BaseAddress + 6, 0) not needed?
        Call outt(BaseAddress + 7, 0) '7)

        'put the device in run mode
        Call outt(BaseAddress + 1, 0) '7)

        'Main.Label14.Caption = INsecs(fpoints&) + " secs"
        'Main.Label2.Caption = "Fm - fluoro"
        ProgressBar3.Value = Actinic_Intensity
        actinic_label.Text = Actinic_Intensity
    End Sub

    Public Sub halt_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles halt.Click

        Halt_Script = True

    End Sub

    Public Sub LEDS_Off_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles LEDS_Off.Click
        Call Shut_Down()
        ProgressBar3.Value = 0
        actinic_label.Text = 0
    End Sub

    Public Sub Load_Script_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Load_Script.Click
        Call Do_Load_Script()
    End Sub
    Private Sub Do_Load_Script()
        CommonDialog1Open.FileName = "*.txt"
        'CommonDialog1Save.FileName = "*.txt"
        'CommonDialog1Open.Filter = "Script Files|*.scr|Text Files|*.txt"
        CommonDialog1Open.Filter = "Text Files|*.txt"
        CommonDialog1Save.Title = "OPEN SCRIPT"

        CommonDialog1Save.FileName = CommonDialog1Open.FileName
        CommonDialog1Open.ShowDialog()

        'CommonDialog1Save.Filter = "Script Files|*.scr|Text Files|*.txt"
        script_file = CommonDialog1Open.FileName
        Script_Label.Text = script_file

    End Sub

    Public Sub Run_DMK_Script_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Run_DMK_Script.Click
        run_another_script = True
        'script_file = Script_Label.Text
        'Me.Text = "Kinetic Spectrophotometer: RUNNING"
        Me.BackColor = Color.Green
        While run_another_script = True
            Call run_the_script()
        End While
        'Me.Text = "Kinetic Spectrophotometer: IDLE"
        Me.BackColor = Color.Gray
    End Sub

    ' pass false to this function to make it run int test mode
    Private Sub run_the_script()
        run_the_script(True)
    End Sub
    Private Sub run_the_script(ByVal ForReal As Boolean)
        ' if ForReal is true, then the script will run normally. If ForReal is false, then the script will run in test mode
        On Error GoTo err_it
        run_another_script = False
        If Trace_Running = True Then Exit Sub

        'this sub parses the script into arrays and feeds the arrays into the Multi_Trace sub

        'Dim Max_Loops As Integer
        'Dim script_file As String
        '        
        'Dim Script() As String
        Dim temp As String = ""
        Dim i, ii As Short


        Const Max_Loops As Short = 100


        Dim L_Beg(Max_Loops) As Object
        Dim L_End(Max_Loops) As Object
        Dim L_Number(Max_Loops) As Object
        Dim L_counter(Max_Loops) As Short
        Dim Total_Script_Count As Short
        Dim Loop_Number As Short
        Dim Wait_Time As Single

        ' dimentions involving DIRK traces


        Dim Measuring_Pulse_Duration() As String = {"0", "0", "0"}
        Dim Number_Dirk_Repeats() As Short = {0, 0, 0}
        Dim Dirk_Measuring_Interval() As String = {"0", "0", "0"} ' time between measuring pulses
        Dim Baseline_Measuring_Interval() As String = {"0", "0", "0"}
        Dim Recovery_Measuring_Interval() As String = {"0", "0", "0"}
        Dim Baseline_Number_Points() As Short = {0, 0, 0}
        Dim Recovery_Number_Points() As Short = {0, 0, 0}
        Dim Dirk_Number_Points() As Short = {0, 0, 0}
        'Dim Pre_Delay_Time As String

        Dim trace1, trace2, trace3 As Integer
        Dim simmer_number As Integer
        Dim simmer_q_switch As Integer


        Dim q_switch_time As Integer

        Dim In_Channel() As Short = {0, 0, 0}
        Dim Baseline_Actinic_Intensity() As Short = {0, 0, 0}
        Dim Recovery_Actinic_Intensity() As Short = {0, 0, 0}
        Dim Dirk_Actinic_Intensity() As Short = {0, 0, 0}

        'Dim Dirk_Data_X() As Single
        'Dim Dirk_Data_Y() As Single
        'Dim Dirk_Points As Short
        Dim Time_Mode() As String = {"0", "0", "0"}
        Dim trace_note() As String = {"0", "0", "0"}

        Dim ulstat As Short
        Dim temp_string As String

        Dim Number_Protocols As Short
        Dim Save_Mode() As Short = {0, 0, 0}
        Dim Times_Averaged() As Short = {0, 0, 0}
        'Dim Fluorescence_Times_Averaged() As Short
        Dim Wavelength() As String = {"none", "none", "none"}
        Dim Gain() As Integer = {0, 0, 0}
        Dim Blocking_Filter() As String = {"0", "0", "0"}
        Dim Give_Flash() As Short = {0, 0, 0}
        Dim Wheel_Position() As Short = {0, 0, 0}
        Dim Detector_Gain() As Short = {0, 0, 0}

        Dim File_Name() As String = {"temp", "temp", "temp"}
        Dim Current_Protocol As Short

        Dim wt As Short

        Dim Number_Traces As Short


        Dim Current_Intensity As Short
        Dim Far_Red As Short
        Dim Blue_Actinic As Short

        ' END dimentions involving DIRK traces

        ' start DIm with MULTI-traces
        'Call Multi_Trae(Number_Loops, Intensity(), Number_Pulses(), Number_Measuring_Lights, Measuring_Light(), Measuring_Interval(), Primary_Gain(), Measuring_Pulse_Duration)

        Dim M_Number_Loops() As Short = {0, 0, 0}
        Dim M_Intensity(,) As Short = {{0, 0, 0}, {0, 0, 0}}
        Dim M_Far_Red(,) As Short = {{0, 0, 0}, {0, 0, 0}}
        Dim M_Blue_Actinic(,) As Short = {{0, 0, 0}, {0, 0, 0}}
        Dim Pre_Pulse(,) As Short = {{0, 0, 0}, {0, 0, 0}}
        Dim Pre_Delay(,) As String = {{"0", "0", "0"}, {"0", "0", "0"}}
        Dim Pre_Pulse_Time(,) As String = {{"0", "0", "0"}, {"0", "0", "0"}}
        Dim M_Number_Pulses(,) As Short = {{0, 0, 0}, {0, 0, 0}}
        Dim M_Number_Measuring_Lights() As Short = {0, 0, 0}
        Dim M_Measuring_Light(,) As Short = {{0, 0, 0}, {0, 0, 0}}
        Dim M_Take_Data(,) As Short = {{0, 0, 0}, {0, 0, 0}}
        Dim M_Measuring_Interval(,) As String = {{0, 0, 0}, {0, 0, 0}}
        Dim L_Measuring_Interval(,,) As String = {{{0, 0, 0}, {0, 0, 0}}, {{0, 0, 0}, {0, 0, 0}}}
        'Dim M_Primary_Gain(,) As Short = {{0, 0, 0}, {0, 0, 0}}
        Dim Xe_Flash(,) As Short = {{0, 0, 0}, {0, 0, 0}}
        Dim q_switch(,) As Short = {{0, 0, 0}, {0, 0, 0}}
        Dim M_Reference_Gain(,) As Short = {{0, 0, 0}, {0, 0, 0}}
        Dim Baseline_Start() As Short = {0, 0, 0}
        Dim Baseline_End() As Short = {0, 0, 0}
        Dim iii As Short
        Dim M_Temp As Short
        Dim Max_Number_Loops As Short

        Max_Number_Loops = 100

        If ForReal Then
            List1.Items.Clear()
        Else
            List1.Items.Add("reading script file in test mode...")
            List1.TopIndex = List1.Items.Count - 1
        End If

        'Measuring_Pulse_Duration
        'Intensity()
        'Dim Max_Volts As Single
        'Dim gain_temp As Single
        Dim gain_slop As Single = 0.5
        'Dim gain_set_temp As Integer
        Dim Start_Time As Single
        Dim Zero_Time As Single
        Dim gain_data(10) As Single
        Dim Array_Value(10, 100) As Object
        Dim Array_Number As Short
        Dim Array_Max_Index(10) As Object
        Dim Array_Index(10) As Short
        Dim points, temp_s As Integer
        Dim Last_Servo As String = ""
        Dim iv As Short
        Dim smooth() As Short
        'Dim offset As Single

        Dim Wheel(25) As Single 'contains the positions of the wheel openings for the servo
        Dim word_hold As String

        'FileOpen(3, "c:\wheel_position.dat", OpenMode.Input)
        'For iii = 0 To 25
        ' Input(3, Wheel(iii))
        ' Next iii
        'FileClose(3)

        Zero_Time = VB.Timer() 'indicates the start of the trace
        Halt_Script = False


        'File_Name = "c:\test.dat"
        'Measuring_Pulse_Duration = "12u"
        'Baseline_Measuring_Interval = "100u"
        'Dirk_Measuring_Interval = "100u"
        'Recovery_Measuring_Interval = "1000u"
        'Dirk_Number_Points = 100
        'Baseline_Number_Points = 100
        'Recovery_Number_Points = 500
        'Actinic_Intensity = 0
        'Dirk_Actinic_Intensity = 16

        'Dim Number_Fluorescence_Traces As Integer


        'Dim Induction_Measuring_Pulse_Duration()
        'Dim Induction_Baseline_Measuring_Interval()
        'Dim Induction_Baseline_Measuring_Points()
        'Dim Induction_Measuring_Interval()

        'Dim Induction_Number_Points() As Integer


        'Dim Initial_Actinic_Intensity() As Integer

        'Dim Final_Acinic_Intensity() As Integer

        'Dim Induction_Gain() As Integer

        Dim Measuring_Mode() As String = {"520", "820", "fluor"}

        'fluor
        '520
        '820
        'If script_file = "" Then
        '    Call Do_Load_Script
        'End If
        ' see how long the script is

        'File_Name = "c:\test.dat"

        'script_file = Script_Label.Text


        If script_file = "" Then
            CommonDialog1Open.Title = "LOAD SCRIPT FILE"
            CommonDialog1Save.Title = "LOAD SCRIPT FILE"

            CommonDialog1Open.FileName = "*.txt"
            'CommonDialog1Save.FileName = "*.txt"
            'Else
            'CommonDialog1Open.FileName = script_file
            'End If

            CommonDialog1Open.ShowDialog()
            'CommonDialog1Save.FileName = CommonDialog1Open.FileName
            'UPGRADE_WARNING: Filter has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            'CommonDialog1Open.Filter = "Script Files|*.scr|Text Files|*.txt"
            'CommonDialog1Save.Filter = "Script Files|*.scr|Text Files|*.txt"
            CommonDialog1Open.Filter = "Text Files|*.txt"
            CommonDialog1Save.Filter = "Text Files|*.txt"

            script_file = CommonDialog1Open.FileName
            Script_Label.Text = script_file

            If CommonDialog1Open.CheckFileExists = False Then
                MessageBox.Show("no such file")

                Exit Sub
            End If
            If script_file = "*.*" Or script_file = "*.txt" Then
                Exit Sub
            End If

        End If

        FileOpen(1, script_file, OpenMode.Input)
        FileOpen(3, "c:\include_temp.txt", OpenMode.Output)
        Total_Script_Count = 0
        While EOF(1) = False
            Input(1, temp)
            temp = LCase(temp)
            If VB.Left(temp, 1) = ">" Then  'merge another file into this one
                temp = VB.Mid(temp, 2, (VB.Len(temp) - 1))
                FileOpen(2, temp, OpenMode.Input)
                While EOF(2) = False
                    Input(2, temp)
                    If LCase(temp) = ">" Then  'merge another file into this one
                        MessageBox.Show("Error: The program cannot handle recursive INCLUDE (>) statements", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        FileClose(1)
                        FileClose(2)
                        FileClose(3)
                        Exit Sub
                    End If
                    PrintLine(3, temp)
                End While
                FileClose(2)
            Else
                PrintLine(3, temp)
            End If
        End While
        FileClose(1)
        FileClose(3)

        FileOpen(1, "c:\include_temp.txt", OpenMode.Input)
        FileOpen(3, "c:\parse_temp.txt", OpenMode.Output)

        Total_Script_Count = 0
        While EOF(1) = False
            Input(1, temp)
            temp = Trim(temp)
            temp = LCase(temp)
            If temp = "" Then GoTo skipit
            If VB.Left(temp, 1) = "'" Then GoTo skipit
            word_hold = ""
            i = 1
            While i < VB.Len(temp) + 1  'in this section, we parse the script into nuggets, separated by CRLF, like the old script files

                If VB.Mid(temp, i, 1) = "," Or VB.Mid(temp, i, 1) = "(" Or VB.Mid(temp, i, 1) = ")" Or VB.Mid(temp, i, 1) = ";" Then  'ignore parentheses or commas
                    PrintLine(3, word_hold)
                    word_hold = ""
                ElseIf VB.Mid(temp, i, 1) = "+" Or VB.Mid(temp, i, 1) = "-" Or VB.Mid(temp, i, 1) = "*" Or VB.Mid(temp, i, 1) = "=" Or VB.Mid(temp, i, 1) = "/" Then  'this means we are doing math
                    PrintLine(3, word_hold)
                    PrintLine(3, VB.Mid(temp, i, 1))
                    i = i + 1
                    word_hold = VB.Mid(temp, i, 1)
                ElseIf VB.Mid(temp, i, 1) = "'" Then  'this means the rest of the line is a comment
                    GoTo skip_comment
                Else
                    word_hold = word_hold + VB.Mid(temp, i, 1)

                End If

                i = i + 1
            End While
skip_comment:
            If word_hold <> "" Then PrintLine(3, word_hold)
            'If VB.Left(temp, 1) = "@" Then PrintLine(3, "0") 'add another line to hold the value of a variable in the script
skipit:
        End While

        FileClose(3)
        FileClose(1)

        'Exit Sub

        '        temp = Trim(temp)
        '        If LCase(temp) = "include" Then  'merge another file into this one
        'Input(1, temp)
        'FileOpen(2, temp, OpenMode.Input)
        'While EOF(2) = False
        ' Input(2, temp)
        ' temp = Trim(temp)
        ' If VB.Left(temp, 1) <> "'" And temp <> "" Then ' ' designates a remark
        ' Total_Script_Count = Total_Script_Count + 1
        ' End If
        ' End While
        ' FileClose(2)
        ' End If
        ' If VB.Left(temp, 1) <> "'" And temp <> "" Then ' ' designates a remark
        ' Total_Script_Count = Total_Script_Count + 1
        ' End If
        '        End While
        ' FileClose(1)
        ' FileClose(3)

        'Exit Sub

        FileOpen(1, "c:\parse_temp.txt", OpenMode.Input)
        ' FileOpen(3, "c:\temp.txt", OpenMode.Output)

        Total_Script_Count = 0
        While EOF(1) = False
            Input(1, temp)
            temp = Trim(temp)
            If VB.Left(temp, 1) <> "'" And temp <> "" Then ' ' designates a remark
                Total_Script_Count = Total_Script_Count + 1
            End If
        End While
        FileClose(1)


        ReDim Script(Total_Script_Count)

        ' now input script into the array "SCRIPT"

        FileOpen(1, "c:\parse_temp.txt", OpenMode.Input)
        Script_Counter = 0
        While EOF(1) = False
            Input(1, temp)
            temp = Trim(temp)
            If VB.Left(temp, 1) <> "'" And temp <> "" Then ' ' designates a remark or empty line
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1
                Script(Script_Counter) = LCase(temp)
                If LCase(temp) = "include" Then  'include another file into this one
                    Input(1, temp)
                    FileOpen(2, temp, OpenMode.Input)
                    While EOF(2) = False
                        Input(2, temp)
                        temp = Trim(temp)
                        If VB.Left(temp, 1) <> "'" And temp <> "" Then ' ' designates a remark or empty line
                            Call Advance_Script()
                            'Script_Counter = Script_Counter + 1
                            Script(Script_Counter) = LCase(temp)
                        End If
                    End While
                    FileClose(2)
                End If
            End If
        End While
        FileClose(1)

        'Now run the script

        Loop_Number = 0
        Script_Counter = 0
        Sub_Return_Level = 0

        While Script_Counter < Total_Script_Count And Halt_Script = False
            Call Advance_Script()
            'Script_Counter = Script_Counter + 1
            'List1.AddItem(">" & Script(Script_Counter))

            If C_Script = "lb" Then 'designates the beginning of a loop
                ' this script command has the following form:
                'lb
                '1 (loop number, max of 20)
                '10 (number of loops to do)
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Loop_Number = Val(C_Script) 'set number of loops to do
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter

                L_Number(Loop_Number) = Val(C_Script) 'loop back at the next script_counter

                L_Beg(Loop_Number) = Script_Counter '+ 1 'loop back at the next script_counter

                '            End If

            ElseIf C_Script = "le" Then 'designates the beginning of a loop
                ' this script command has the following form:
                'lb
                '1 (loop number, max of 20)
                '10 (number of loops to do)
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Loop_Number = Val(C_Script) 'set number of loops to do

                L_Number(Loop_Number) = L_Number(Loop_Number) - 1 'decrement L_number()

                If ForReal Then List1.Items.Add("LOOP:" & Loop_Number & "TO GO:" & L_Number(Loop_Number))
                'Scrolling text box fix: make the box jump to the bottom after adding an item
                List1.TopIndex = List1.Items.Count - 1
                If L_Number(Loop_Number) > 0 Then
                    Script_Counter = L_Beg(Loop_Number) 'loop back at the next script_counter
                End If
                'End If



            ElseIf C_Script = "sub" Then 'designates a call to a subroutine
                ' this script command has the following form:
                'sub, name_of_subroutine

                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                sub_name = C_Script & "|" 'set number of loops to do
                Sub_Return_Level = Sub_Return_Level + 1
                Sub_Return_Pointer(Sub_Return_Level) = Script_Counter + 1


                'Label7.Text = ("not_found:" & sub_name)

                ' find the subroutine start point
                For iv = 1 To Total_Script_Count

                    If Script(iv) = sub_name Then
                        Script_Counter = iv
                        '       Label7.Text = ("found: " & sub_name)

                    End If
                Next iv

                'End If

            ElseIf C_Script = "return" Then 'designates the beginning of a loop
                Script_Counter = Sub_Return_Pointer(Sub_Return_Level) - 1
                Sub_Return_Level = Sub_Return_Level - 1
                'End If

            ElseIf C_Script = "end" Then 'designates the end of the script
                'Label7.Text = "end"
                Exit Sub
                'End If

            ElseIf VB.Left(C_Script, 1) = "!" Then 'designates that we are defining a variable

                'Script_Counter = Script_Counter + 1 'increment the script counter

                '                Script_Counter = Script_Counter + 1 'increment the script counter
                'If VB.Mid(Script(Script_Counter), 2, 1) <> "@" Then
                ' MessageBox.Show("Variables must begin with '@'", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ' Exit Sub
                'End If
                number_variables = number_variables + 1
                'iv = VB.Len(Script(Script_Counter)) - 1
                user_variable_name(number_variables) = VB.Mid(C_Script, 2, (VB.Len(C_Script) - 1))
                user_variable_index(number_variables) = 1
                If ForReal Then List1.Items.Add(user_variable_name(number_variables))
                'Scrolling text box fix: make the box jump to the bottom after adding an item
                List1.TopIndex = List1.Items.Count - 1
                'Call Advance_Script()
                'Script_Counter = Script_Counter + 1
                'i = Val(Script(Script_Counter))
                'List1.Items.Add(user_variable_name(number_variables))
                'End If

            ElseIf VB.Left(C_Script, 1) = "#" Then 'designates that we are doing arithmatic on a variable
                Dim j As Single
                Dim k As Integer
                Dim var As String
                var = VB.Right(C_Script, (Len(C_Script) - 1))
                'Label7.Text = var
                j = 0
                For i = 1 To number_variables
                    If var = user_variable_name(i) Then
                        j = i
                    End If
                Next i
                If j = 0 Then
                    MessageBox.Show("Error: I cannot find that variable", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter

                k = Val(C_Script)
                user_variable_index(j) = k

                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter

                Select Case C_Script
                    Case "+"
                        If ForReal Then List1.Items.Add("+")
                        'Scrolling text box fix: make the box jump to the bottom after adding an item
                        List1.TopIndex = List1.Items.Count - 1
                        Call Advance_Script()
                        'Script_Counter = Script_Counter + 1 'increment the script counter
                        user_variable(j, k) = user_variable(j, k) + Val(C_Script)
                    Case "-"
                        If ForReal Then List1.Items.Add("-")
                        'Scrolling text box fix: make the box jump to the bottom after adding an item
                        List1.TopIndex = List1.Items.Count - 1
                        Call Advance_Script()
                        'Script_Counter = Script_Counter + 1 'increment the script counter
                        user_variable(j, k) = user_variable(j, k) - Val(C_Script)

                    Case "*"
                        If ForReal Then List1.Items.Add("*")
                        'Scrolling text box fix: make the box jump to the bottom after adding an item
                        List1.TopIndex = List1.Items.Count - 1
                        Call Advance_Script()
                        'Script_Counter = Script_Counter + 1 'increment the script counter
                        user_variable(j, k) = user_variable(j, k) * Val(C_Script)

                    Case "/"
                        If ForReal Then List1.Items.Add("/")
                        'Scrolling text box fix: make the box jump to the bottom after adding an item
                        List1.TopIndex = List1.Items.Count - 1
                        Call Advance_Script()
                        'Script_Counter = Script_Counter + 1 'increment the script counter
                        user_variable(j, k) = user_variable(j, k) / Val(C_Script)

                    Case "="
                        If ForReal Then List1.Items.Add("=")
                        'Scrolling text box fix: make the box jump to the bottom after adding an item
                        List1.TopIndex = List1.Items.Count - 1
                        Call Advance_Script()
                        'Script_Counter = Script_Counter + 1 'increment the script counter
                        user_variable(j, k) = Val(C_Script)

                    Case Else
                        MessageBox.Show("Error: invalid operator", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                End Select
                If ForReal Then List1.Items.Add(user_variable_name(j) & ":" & k & " = " & user_variable(j, k))
                'Scrolling text box fix: make the box jump to the bottom after adding an item
                List1.TopIndex = List1.Items.Count - 1
                'End If
            ElseIf C_Script = "wait" Then 'designates the beginning of a loop
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If C_Script = "v" Then
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    Array_Number = Val(C_Script)

                    Wait_Time = Val(Array_Value(Array_Number, Array_Index(Array_Number)))
                Else
                    Wait_Time = Val(C_Script) 'set wait time
                End If
                If ForReal Then Call Hold_on_There(Wait_Time)
            ElseIf C_Script = "gain_slop" Then
                Call Advance_Script()
                gain_slop = Val(C_Script) '

            ElseIf C_Script = "set_base_file" Then
                If ForReal Then Call set_base_file_name()
            ElseIf C_Script = "note_query" Then
                If ForReal Then

                    Note_File_Name = Trim(Base_File_Name & "_notes.txt")
                    take_a_note.Show()
                    wait_for_notes = True
                    While wait_for_notes = True
                        System.Windows.Forms.Application.DoEvents()

                    End While
                End If
            ElseIf C_Script = "record_events" Then  'sets whether to adds events to the note file
                Call Advance_Script()
                If ForReal Then
                    If C_Script > 0 Then
                        take_notes = True
                    Else
                        take_notes = False
                    End If
                End If
            ElseIf C_Script = "record_script" Then  'adds text to the note file
                If ForReal Then
                    note_text = note_text & "*******************************************************************" & ControlChars.CrLf
                    note_text = note_text & "TEXT OF SCRIPT FILE USED:" & ControlChars.CrLf

                    FileOpen(1, script_file, OpenMode.Input)
                    While EOF(1) = False
                        temp = LineInput(1)
                        note_text = note_text & temp & ControlChars.CrLf

                    End While

                    note_text = note_text & ControlChars.CrLf & "*******************************************************************" & ControlChars.CrLf
                    FileClose(1)
                    FileOpen(7, Note_File_Name, OpenMode.Output)
                    Write(7, note_text)
                    FileClose(7)
                End If
            ElseIf C_Script = "make_note" Then  'adds text to the note file
                If ForReal Then note_text = note_text & ControlChars.CrLf & "*******************************************************************" & ControlChars.CrLf
                Call Advance_Script()
                If ForReal Then
                    note_text = note_text & Date.Now & C_Script & ControlChars.CrLf
                    note_text = note_text & ControlChars.CrLf & "*******************************************************************" & ControlChars.CrLf
                    FileOpen(7, Note_File_Name, OpenMode.Output)
                    Write(7, note_text)
                    FileClose(7)
                End If

            ElseIf C_Script = "shut_down" Then
                If ForReal Then Call Shut_Down()

            ElseIf C_Script = "append_base" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter

                Base_File_Name = Base_Base_File_Name + C_Script
                'List1.Items.Add("**" & Base_File_Name)

                For i = 1 To Number_Traces ' put in some default values
                    File_Name(i) = Trim(Base_File_Name & VB6.Format(i, "0000") & ".dat")
                    Times_Averaged(i) = 0
                Next i


            ElseIf C_Script = "link" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                script_file = C_Script

                Trace_Running = False
                run_another_script = True
                Exit Sub

            ElseIf C_Script = "measuring_pulse_duration" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Measuring_Pulse_Duration(Current_Protocol) = C_Script
                ' ERROR CATCH: If the pulse duration is greator than 100 us, then the LED will DIE!!!
                ' The following block will catch this problem
                ' ///// begin pulse duration catch /////
                Dim trimmed As String
                Dim factor As Double
                Dim duration As Double
                trimmed = Trim(Measuring_Pulse_Duration(Current_Protocol))
                If VB.Right(trimmed, 1) = "u" Then
                    factor = 0.000001
                ElseIf VB.Right(trimmed, 1) = "m" Then
                    factor = 0.001
                ElseIf VB.Right(trimmed, 1) = "n" Then
                    factor = 0.000000001
                Else
                    factor = 1
                End If
                duration = Val(trimmed) * factor
                If duration > (100 * 0.000001) Then
                    Dim keepgoing As Boolean
                    keepgoing = ShowScriptErrorMessage(Script_Counter, Script, "DANGER: measuring pulse length is too long and will probably cause permanent damage to the measuring LED.")
                    If keepgoing = False Then Exit Sub
                End If
                ' ///// end pulse duration catch /////
                'List1.AddItem "mpi" & Measuring_Pulse_Duration(Current_Protocol)

            ElseIf C_Script = "m_number_loops" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                M_Number_Loops(Current_Protocol) = Val(C_Script) '
                For iii = 1 To M_Number_Loops(Current_Protocol)  'set to take data on all loops; can be overridden with m_take_data
                    M_Take_Data(Current_Protocol, iii) = 1
                Next

            ElseIf C_Script = "baseline_start" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Baseline_Start(Current_Protocol) = Val(C_Script) '


            ElseIf C_Script = "baseline_end" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Baseline_End(Current_Protocol) = Val(C_Script) '

            ElseIf C_Script = "laser" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                laser = Val(C_Script) '

                'Dim M_Number_Loops As Integer

            ElseIf C_Script = "m_intensity" Then


                For M_Temp = 1 To M_Number_Loops(Current_Protocol)
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter

                    If C_Script = "v" Then
                        Call Advance_Script()
                        'Script_Counter = Script_Counter + 1 'increment the script counter
                        Array_Number = Val(C_Script)
                        M_Intensity(Current_Protocol, M_Temp) = Val(Array_Value(Array_Number, Array_Index(Array_Number)))
                    Else
                        M_Intensity(Current_Protocol, M_Temp) = Val(C_Script) 'set wait time
                    End If
                Next M_Temp

            ElseIf C_Script = "m_far_red" Then

                For M_Temp = 1 To M_Number_Loops(Current_Protocol)
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    M_Far_Red(Current_Protocol, M_Temp) = Val(C_Script) 'set wait time
                Next M_Temp

            ElseIf C_Script = "m_blue_actinic" Or C_Script = "m_flash" Then


                For M_Temp = 1 To M_Number_Loops(Current_Protocol)
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    M_Blue_Actinic(Current_Protocol, M_Temp) = Val(C_Script) 'set wait time

                Next M_Temp

            ElseIf C_Script = "m_leaf_actinic" Then


                For M_Temp = 1 To M_Number_Loops(Current_Protocol)
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    M_Blue_Actinic(Current_Protocol, M_Temp) = Val(C_Script) 'set wait time

                Next M_Temp

                'Dim M_Intensity() As Integer
            ElseIf C_Script = "xe_flash" Then

                For M_Temp = 1 To M_Number_Loops(Current_Protocol)
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    Xe_Flash(Current_Protocol, M_Temp) = CShort(C_Script)
                    If ForReal Then List1.Items.Add("xe_added" & Xe_Flash(Current_Protocol, M_Temp))
                    'Scrolling text box fix: make the box jump to the bottom after adding an item
                    List1.TopIndex = List1.Items.Count - 1
                Next M_Temp

            ElseIf C_Script = "q_switch" Then

                For M_Temp = 1 To M_Number_Loops(Current_Protocol)
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    q_switch(Current_Protocol, M_Temp) = CShort(C_Script)
                    'List1.Items.Add("xe_added" & Xe_Flash(Current_Protocol, M_Temp))
                Next M_Temp


            ElseIf C_Script = "m_pulse_set" Then

                For M_Temp = 1 To M_Number_Loops(Current_Protocol)
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    M_Number_Pulses(Current_Protocol, M_Temp) = Val(C_Script)
                Next M_Temp

                'Dim M_Number_Pulses() As Integer

            ElseIf C_Script = "m_number_measuring_lights" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                M_Number_Measuring_Lights(Current_Protocol) = Val(C_Script)

                'Dim M_Number_Measuring_Lights As Integer
            ElseIf C_Script = "m_take_data" Then
                Dim mm As Integer
                For mm = 1 To M_Number_Loops(Current_Protocol)
                    Call Advance_Script()

                    M_Take_Data(Current_Protocol, mm) = Val(C_Script)

                Next mm

            ElseIf C_Script = "m_measuring_light" Then

                For M_Temp = 1 To M_Number_Measuring_Lights(Current_Protocol)
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    M_Measuring_Light(Current_Protocol, M_Temp) = Val(C_Script)
                Next M_Temp

                'Dim M_Measuring_Light() As Integer

                '            ElseIf c_script = "m_measuring_interval" Then

                '                For M_Temp = 1 To M_Number_Measuring_Lights(Current_Protocol)
                ' Script_Counter = Script_Counter + 1 'increment the script counter
                ' M_Measuring_Interval(Current_Protocol, M_Temp) = (c_script)
                ' Next M_Temp

            ElseIf C_Script = "m_measuring_interval" Or C_Script = "l_measuring_interval" Then
                Dim mm As Integer
                For mm = 1 To M_Number_Loops(Current_Protocol)
                    For M_Temp = 1 To M_Number_Measuring_Lights(Current_Protocol)
                        Call Advance_Script()
                        'Script_Counter = Script_Counter + 1 'increment the script counter
                        L_Measuring_Interval(Current_Protocol, M_Temp, mm) = (C_Script)
                    Next M_Temp
                Next mm
            ElseIf C_Script = "m_pre_pulse_light" Or C_Script = "pre_pulse_light" Then
                Call Advance_Script()
                pre_pulse_light = Val(C_Script)

            ElseIf C_Script = "m_pre_pulse" Or C_Script = "pre_pulse" Then
                Dim mm As Integer
                For mm = 1 To M_Number_Loops(Current_Protocol)
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    ' Pre_Pulse(Current_Protocol, mm) = (C_Script)
                    'Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    If C_Script = "0" Then
                        Pre_Pulse(Current_Protocol, mm) = 0
                    Else
                        Pre_Pulse(Current_Protocol, mm) = 1
                        Pre_Pulse_Time(Current_Protocol, mm) = C_Script
                    End If

                Next mm

            ElseIf C_Script = "m_pre_delay" Or C_Script = "pre_delay" Then
                Dim mm As Integer
                For mm = 1 To M_Number_Loops(Current_Protocol)
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    Pre_Delay(Current_Protocol, mm) = C_Script
                Next mm

            ElseIf C_Script = "m_detector_gain" Then

                For M_Temp = 1 To M_Number_Measuring_Lights(Current_Protocol)
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    M_Primary_Gain(Current_Protocol, M_Temp) = CShort(C_Script)
                Next M_Temp




                '
            ElseIf C_Script = "m_reference_gain" Then

                For M_Temp = 1 To M_Number_Measuring_Lights(Current_Protocol)
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    M_Reference_Gain(Current_Protocol, M_Temp) = CShort(C_Script)
                Next M_Temp




            ElseIf C_Script = "m_trace" Or C_Script = "m_trace_r" Then
                If ForReal Then
                    'ulstat = cbDOut(BoardNum, AUXPORT, &HFFS)

                    System.Windows.Forms.Application.DoEvents()


                    'Call servofrm.SETTHESERVO(1, (Wheel(Wheel_Position(Current_Protocol))))

                    'Call servofrm.SETTHESERVO(1, (Wheel(Wheel_Position(Current_Protocol))))

                    'Call Hold_on_There(3)


                    Times_Averaged(Current_Trace) = Times_Averaged(Current_Trace) + 1

                    If Blocking_Filter(Current_Protocol) <> Last_Servo Then

                        If Blocking_Filter(Current_Protocol) = "blue" Then
                            SERVOset = Blue_Filter
                            'Call servofrm.SETTHESERVO(0, SERVOset%)
                            'Call Hold_on_There(3)
                        ElseIf Blocking_Filter(Current_Protocol) = "ir" Then
                            SERVOset = IR_Filter
                            'Call servofrm.SETTHESERVO(0, SERVOset%)
                            'Call Hold_on_There(3)
                        End If

                    End If
                    Last_Servo = Blocking_Filter(Current_Protocol)

                    Start_Time = VB.Timer() - Zero_Time 'save the start time of the trace

                    '                    Call Multi_Trace(Current_Protocol, M_Number_Loops, M_Intensity, M_Number_Pulses, M_Number_Measuring_Lights, M_Measuring_Light, M_Measuring_Interval, M_Primary_Gain, M_Reference_Gain, Measuring_Pulse_Duration, Gain(Current_Protocol), In_Channel(Current_Protocol), points, data_time)

                    Call Multi_Trace(Current_Protocol, M_Number_Loops, M_Intensity, M_Number_Pulses, M_Number_Measuring_Lights, M_Measuring_Light, L_Measuring_Interval, M_Primary_Gain, M_Reference_Gain, Measuring_Pulse_Duration, Gain(Current_Protocol), In_Channel(Current_Protocol), points, data_time, Xe_Flash, q_switch, M_Far_Red, M_Blue_Actinic, Pre_Pulse, Pre_Pulse_Time, Pre_Delay, M_Take_Data)


                    Call Dirk_Save(data_time, points, File_Name(Current_Trace), Save_Mode(Current_Trace), Times_Averaged(Current_Trace), Time_Mode(Current_Trace), Start_Time, M_Number_Measuring_Lights(Current_Protocol), Baseline_Start(Current_Protocol), Baseline_End(Current_Protocol), In_Channel(Current_Protocol))
                    Me.Text = "SAVED:" & File_Name(Current_Trace)
                End If

            ElseIf C_Script = "offset" Then

                'Call servofrm.SETTHESERVO(1, (Wheel(Wheel_Position(Current_Protocol))))

                'Times_Averaged(Current_Trace) = Times_Averaged(Current_Trace) + 1

                If Blocking_Filter(Current_Protocol) <> Last_Servo Then

                    If Blocking_Filter(Current_Protocol) = "blue" Then
                        SERVOset = Blue_Filter
                        'Call servofrm.SETTHESERVO(0, SERVOset%)
                        'Call Hold_on_There(3)
                    ElseIf Blocking_Filter(Current_Protocol) = "ir" Then
                        SERVOset = IR_Filter
                        'Call servofrm.SETTHESERVO(0, SERVOset%)
                        'Call Hold_on_There(3)
                    End If

                End If
                Last_Servo = Blocking_Filter(Current_Protocol)
                If ForReal Then
                    Start_Time = VB.Timer() - Zero_Time 'save the start time of the trace
                    M_Number_Loops(0) = 1
                    M_Intensity(0, 1) = M_Intensity(Current_Protocol, 1)
                    M_Number_Pulses(0, 1) = 100
                    M_Number_Measuring_Lights(0) = 1
                    M_Measuring_Light(0, 1) = 0
                    M_Take_Data(0, 1) = 1
                    M_Measuring_Interval(0, 1) = "10m"
                    L_Measuring_Interval(0, 1, 1) = "10m"
                    M_Primary_Gain(0, 1) = M_Primary_Gain(Current_Protocol, 1)

                    Measuring_Pulse_Duration(0) = Measuring_Pulse_Duration(Current_Protocol)
                    Gain(0) = Gain(Current_Protocol)
                    In_Channel(0) = In_Channel(Current_Protocol)

                    '                    Call Multi_Trace(0, M_Number_Loops, M_Intensity, M_Number_Pulses, M_Number_Measuring_Lights, M_Measuring_Light, M_Measuring_Interval, M_Primary_Gain, M_Reference_Gain, Measuring_Pulse_Duration, Gain(Current_Protocol), In_Channel(Current_Protocol), points, data_time)
                    Call Multi_Trace(0, M_Number_Loops, M_Intensity, M_Number_Pulses, M_Number_Measuring_Lights, M_Measuring_Light, L_Measuring_Interval, M_Primary_Gain, M_Reference_Gain, Measuring_Pulse_Duration, Gain(Current_Protocol), In_Channel(Current_Protocol), points, data_time, Xe_Flash, q_switch, M_Far_Red, M_Blue_Actinic, Pre_Pulse, Pre_Pulse_Time, Pre_Delay, M_Take_Data)
                    offset = 0

                    For i = 0 To points - 1
                        offset = offset + Data_Volts(i, In_Channel(Current_Protocol))
                    Next i

                    offset = offset / points
                    If ForReal Then List1.Items.Add("offset: " & offset)
                    'Scrolling text box fix: make the box jump to the bottom after adding an item
                    List1.TopIndex = List1.Items.Count - 1


                    '    Call Multi_Trace(0,
                    ', 1, M_Intensity(), 100, 1, 0, "10m", M_Primary_Gain(), Measuring_Pulse_Duration(), Gain(Current_Protocol), In_Channel(Current_Protocol), points&, data_time(), Data_Volts())


                    '    Call Dirk_Save(data_time(), Data_Volts(), points&, File_Name(Current_Trace), Save_Mode(Current_Trace), Times_Averaged(Current_Trace), Time_Mode(Current_Trace), Start_Time, M_Number_Measuring_Lights(Current_Protocol), Baseline_Start(Current_Protocol), Baseline_End(Current_Protocol))




                    'Dim M_Primary_Gain() As Integer
                End If
            ElseIf C_Script = "auto_gain" Then
                If ForReal Then
                    'Call servofrm.SETTHESERVO(1, (Wheel(Wheel_Position(Current_Protocol))))

                    'Times_Averaged(Current_Trace) = Times_Averaged(Current_Trace) + 1

                    Call do_auto_gain(Current_Protocol, M_Reference_Gain, L_Measuring_Interval, M_Measuring_Interval, M_Measuring_Light, M_Number_Measuring_Lights, M_Number_Loops, M_Intensity, M_Number_Pulses, M_Take_Data, Measuring_Pulse_Duration, Xe_Flash, Gain, In_Channel, M_Far_Red, M_Blue_Actinic, gain_slop, q_switch, Pre_Pulse, Pre_Pulse_Time, Pre_Delay)
                    'finis
                End If
            ElseIf C_Script = "in_channel" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                In_Channel(Current_Protocol) = Val(C_Script)

                'finis
            ElseIf C_Script = "servo_positions" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                current_servo = Val(C_Script)
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                number_servo_positions = Val(C_Script)
                For ii = 1 To number_servo_positions
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    servo_position(current_servo, ii) = Val(C_Script)
                Next ii


            ElseIf C_Script = "xe_intensity" Then


                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Xe_intensity_value = Val(C_Script)
                If ForReal Then List1.Items.Add("xe_intensity" & ":" & Xe_intensity_value)
                'Scrolling text box fix: make the box jump to the bottom after adding an item
                List1.TopIndex = List1.Items.Count - 1

                Call set_xe_intensity(Xe_intensity_value)
            ElseIf C_Script = "f_shutter" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If ForReal Then
                    Call fluorescence_shutter(C_Script)
                Else
                    If C_Script = "open" Or C_Script = "1" Or C_Script = "close" Or C_Script = "0" Then
                        ' input is fine
                    Else
                        'input is not fine
                        ShowScriptErrorMessage(Script_Counter, Script, "Value must be ""1"", ""open"", ""0"", or ""close"".")
                    End If
                End If
            ElseIf C_Script = "stir" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If ForReal Then Call stir_control(C_Script)

            ElseIf C_Script = "subtract" Then

                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                trace1 = Val(C_Script)
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                trace2 = Val(C_Script)
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                trace3 = Val(C_Script)
                If ForReal Then Call subtract_traces(File_Name(trace1), File_Name(trace2), File_Name(trace3))

            ElseIf C_Script = "dice_average" Then

                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                trace1 = Val(C_Script)
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                trace2 = Val(C_Script)
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                dice_length = Val(C_Script)
                If ForReal Then Call dice_average(File_Name(trace1), File_Name(trace2), dice_length)

            ElseIf C_Script = "delta_a" Then
                If ForReal Then
                    If ref_channel = False Then
                        Call Delta_A_no_ref(File_Name(Current_Trace), (Baseline_Start(Current_Protocol)), (Baseline_End(Current_Protocol)))
                    Else
                        Call Delta_A_WITH_ref(File_Name(Current_Trace), (Baseline_Start(Current_Protocol)), (Baseline_End(Current_Protocol)))
                    End If
                End If
            ElseIf C_Script = "zero_point" Then
                Call Advance_Script()
                If ForReal Then Call Zero_Point(File_Name(Current_Trace), Val(C_Script))

            ElseIf C_Script = "extract_points_delta" Then
                If ForReal Then
                    Dim extract_index, extract_file_number, extract_this_point, extract_from_this_number_of_traces As Integer
                    Dim extracted_value As Single
                    FileOpen(6, File_Name(Current_Trace), OpenMode.Output) 'open current trace file for output
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    extract_this_point = Val(C_Script)
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    extract_from_this_number_of_traces = Val(C_Script)
                    For extract_index = 1 To extract_from_this_number_of_traces
                        Call Advance_Script()
                        'Script_Counter = Script_Counter + 1
                        extracted_value = extract_point(File_Name(C_Script), extract_this_point)
                        extract_file_number = Val(C_Script)
                        If ForReal Then List1.Items.Add("extracted: " & extracted_value)
                        'Scrolling text box fix: make the box jump to the bottom after adding an item
                        List1.TopIndex = List1.Items.Count - 1
                        PrintLine(6, extracted_value)

                    Next extract_index
                    FileClose(6)
                End If
            ElseIf C_Script = "extract_points_raw" Then
                If ForReal Then
                    Dim extract_index, extract_file_number, extract_this_point, extract_from_this_number_of_traces As Integer
                    Dim extracted_value As Single
                    FileOpen(6, File_Name(Current_Trace), OpenMode.Output) 'open current trace file for output
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    extract_this_point = Val(C_Script)
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    extract_from_this_number_of_traces = Val(C_Script)
                    For extract_index = 1 To extract_from_this_number_of_traces
                        Call Advance_Script()
                        'Script_Counter = Script_Counter + 1
                        extracted_value = extract_point_raw(File_Name(C_Script), extract_this_point)
                        extract_file_number = Val(C_Script)
                        If ForReal Then List1.Items.Add("extracted: " & extracted_value)
                        'Scrolling text box fix: make the box jump to the bottom after adding an item
                        List1.TopIndex = List1.Items.Count - 1
                        PrintLine(6, extracted_value)

                    Next extract_index
                    FileClose(6)
                End If
            ElseIf C_Script = "load_file" Then  'load a trace into memory

                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter

                File_Name(Current_Trace) = C_Script


            ElseIf C_Script = "set_servo" Then

                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                current_servo = Val(C_Script)

                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                current_servo_position = Val(C_Script)

                'Script_Counter = Script_Counter + 1 'increment the script counter
                If ForReal Then Call set_servo(current_servo, servo_position(current_servo, current_servo_position))

            ElseIf C_Script = "simmer" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                simmer_number = Val(C_Script)
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                q_switch_time = Val(C_Script)
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                simmer_q_switch = Val(C_Script)
                If ForReal Then Call Simmer(simmer_number, (Str(q_switch_time) & "u"), simmer_q_switch)

            ElseIf C_Script = "number_traces" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Number_Traces = Val(C_Script) 'set wait time

                ReDim Times_Averaged(Number_Traces)
                ReDim Time_Mode(Number_Traces)
                ReDim trace_note(Number_Traces)
                ReDim smooth(Number_Traces)

                ReDim Save_Mode(Number_Traces)
                ReDim File_Name(Number_Traces)
                ReDim Trace_protocol(Number_Traces)
                '    m Baseline_Start(Number_Traces)
                '   ReDim Baseline_End(Number_Traces)

                For i = 1 To Number_Traces ' put in some default values
                    Save_Mode(i) = Average_Into
                    Times_Averaged(i) = 0
                    Trace_protocol(i) = -1
                    File_Name(i) = Trim(Base_File_Name & VB6.Format(i, "0000") & ".dat")
                    'List1.AddItem File_Name(i)
                    Time_Mode(i) = "from_zero"
                Next i

            ElseIf C_Script = "purge_file" Then
                If ForReal Then
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    'Recovery_Measuring_Interval(Current_Protocol) = c_script
                    FileOpen(6, File_Name(Current_Trace), OpenMode.Output)
                    PrintLine(6, "")
                    FileClose(6)
                End If
            ElseIf C_Script = "current_trace" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter

                If C_Script = "v" Then
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    Array_Number = Val(C_Script)
                    'UPGRADE_WARNING: Couldn't resolve default property of object Array_Value(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Current_Trace = Val(Array_Value(Array_Number, Array_Index(Array_Number)))
                Else
                    Current_Trace = Val(C_Script) 'set wait time
                End If
                If Trace_protocol(Current_Trace) >= 0 Then  'if we have set a trace_protocol, then set current_protocol to it
                    Current_Protocol = Trace_protocol(Current_Trace)
                End If
            ElseIf C_Script = "trace_protocol" Then
                Call Advance_Script()
                Trace_protocol(Current_Trace) = Val(C_Script) 'set wait time


            ElseIf C_Script = "time_mode" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Time_Mode(Current_Trace) = C_Script 'set wait time
                ' time_mode="from_zero", or ""sequential"
            ElseIf C_Script = "trace_note" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                trace_note(Current_Trace) = C_Script 'set wait time
                ' time_mode="from_zero", or ""sequential"

            ElseIf C_Script = "number_protocols" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Number_Protocols = Val(C_Script) 'set wait time


                ReDim M_Number_Loops(Number_Protocols)

                ReDim M_Intensity(Number_Protocols, Max_Number_Loops)
                ReDim M_Far_Red(Number_Protocols, Max_Number_Loops)
                ReDim M_Blue_Actinic(Number_Protocols, Max_Number_Loops)
                ReDim M_Number_Pulses(Number_Protocols, Max_Number_Loops)
                ReDim M_Number_Measuring_Lights(Number_Protocols)
                ReDim Pre_Pulse(Number_Protocols, Max_Number_Loops)
                ReDim Pre_Delay(Number_Protocols, Max_Number_Loops)
                ReDim Pre_Pulse_Time(Number_Protocols, Max_Number_Loops)
                ReDim M_Measuring_Light(Number_Protocols, 16)
                ReDim M_Take_Data(Number_Protocols, Max_Number_Loops)
                ReDim M_Measuring_Interval(Number_Protocols, 16)
                ReDim L_Measuring_Interval(Number_Protocols, 16, Max_Number_Loops)
                ReDim M_Primary_Gain(Number_Protocols, 16)
                ReDim Xe_Flash(Number_Protocols, Max_Number_Loops)
                ReDim q_switch(Number_Protocols, Max_Number_Loops)
                ReDim M_Reference_Gain(Number_Protocols, 16)
                ReDim Baseline_Start(Number_Protocols)
                ReDim Baseline_End(Number_Protocols)
                ReDim Detector_Gain(Number_Protocols)
                ReDim In_Channel(Number_Protocols)
                ReDim Wavelength(Number_Protocols)
                ReDim Gain(Number_Protocols)
                ReDim Blocking_Filter(Number_Protocols)
                ReDim Wheel_Position(Number_Protocols)
                ReDim Measuring_Pulse_Duration(Number_Protocols)
                ReDim Dirk_Measuring_Interval(Number_Protocols)
                ReDim Number_Dirk_Repeats(Number_Protocols)
                ReDim Baseline_Measuring_Interval(Number_Protocols)
                ReDim Recovery_Measuring_Interval(Number_Protocols)
                ReDim Baseline_Number_Points(Number_Protocols)
                ReDim Recovery_Number_Points(Number_Protocols)
                ReDim Dirk_Number_Points(Number_Protocols)
                Dim Actinic_Intensity(Number_Protocols) As Object
                ReDim Baseline_Actinic_Intensity(Number_Protocols)
                ReDim Recovery_Actinic_Intensity(Number_Protocols)
                ReDim Dirk_Actinic_Intensity(Number_Protocols)
                ReDim Measuring_Mode(Number_Protocols)
                ReDim Give_Flash(Number_Protocols)

                'set some default values
                For i = 1 To Number_Protocols
                    M_Number_Measuring_Lights(i) = 1
                    Detector_Gain(i) = 4
                    Wavelength(i) = "520"
                    Gain(i) = BIP10VOLTS
                    Blocking_Filter(i) = "blue"
                    Wheel_Position(i) = 2
                    In_Channel(i) = 0
                    Measuring_Pulse_Duration(i) = "20u"
                    'UPGRADE_WARNING: Couldn't resolve default property of object Actinic_Intensity(i). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Actinic_Intensity(i) = 7
                    Give_Flash(i) = False
                Next i

                Current_Protocol = 1

            ElseIf C_Script = "current_protocol" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Current_Protocol = Val(C_Script) 'set wait time

            ElseIf C_Script = "blocking_filter" Then
                Call Advance_Script()
                'Blocking_Filter(Current_Protocol) = C_Script "blocking_filter" used to be set as a servo property
                ' Now it is the same as f_shutter
                If ForReal Then
                    Call fluorescence_shutter(C_Script)
                Else
                    If C_Script = "open" Or C_Script = "1" Or C_Script = "close" Or C_Script = "0" Then
                        ' input is fine
                    Else
                        'input is not fine
                        ShowScriptErrorMessage(Script_Counter, Script, "Value must be ""1"", ""open"", ""0"", or ""close"".")
                    End If
                End If
            ElseIf C_Script = "home_wheel" Then
                If ForReal Then Call home_wheel()
            ElseIf C_Script = "wheel_position" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                temp_s = Val(C_Script)
                If ForReal Then Call set_wheel(temp_s)
            ElseIf C_Script = "give_flash" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If ForReal Then Give_Flash(Current_Protocol) = Val(C_Script)

            ElseIf C_Script = "measuring_mode" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Measuring_Mode(Current_Protocol) = C_Script 'set wait time
                If Measuring_Mode(Current_Protocol) = "520" Then
                    Wavelength(Current_Protocol) = "520"
                    Blocking_Filter(Current_Protocol) = "blue"
                ElseIf Measuring_Mode(Current_Protocol) = "l2" Then
                    Wavelength(Current_Protocol) = "???"
                    Blocking_Filter(Current_Protocol) = "blue"

                ElseIf Measuring_Mode(Current_Protocol) = "820" Then
                    Wavelength(Current_Protocol) = "820"
                    Blocking_Filter(Current_Protocol) = "ir"
                ElseIf Measuring_Mode(Current_Protocol) = "fluor" Then
                    Wavelength(Current_Protocol) = "520"
                    Blocking_Filter(Current_Protocol) = "ir"
                ElseIf Measuring_Mode(Current_Protocol) = "sat" Then
                    Wavelength(Current_Protocol) = "520"
                    Blocking_Filter(Current_Protocol) = "ir"
                ElseIf Measuring_Mode(Current_Protocol) = "satw" Then
                    Wavelength(Current_Protocol) = "520"
                    Blocking_Filter(Current_Protocol) = "ir"
                ElseIf Measuring_Mode(Current_Protocol) = "blue_artifact" Then
                    Wavelength(Current_Protocol) = "none"
                    Blocking_Filter(Current_Protocol) = "blue"
                ElseIf Measuring_Mode(Current_Protocol) = "actinic_intensity" Then
                    Wavelength(Current_Protocol) = "none"
                    Blocking_Filter(Current_Protocol) = "blue"
                    In_Channel(Current_Protocol) = 2
                End If

            ElseIf C_Script = "detector_gain" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Detector_Gain(Current_Protocol) = CShort(C_Script) 'set wait time

            ElseIf C_Script = "gain" Or C_Script = "adc_gain" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                temp = C_Script 'set wait time
                If temp = "bip10volts" Then
                    Gain(Current_Protocol) = BIP10VOLTS
                ElseIf temp = "bip5volts" Then
                    Gain(Current_Protocol) = BIP5VOLTS
                ElseIf temp = "bip2volts" Then
                    Gain(Current_Protocol) = BIP2VOLTS
                ElseIf temp = "bip1volts" Then
                    Gain(Current_Protocol) = BIP1VOLTS
                    'ElseIf temp = "bippt25volts" Then
                    '   Gain(Current_Protocol) = BIPPT25VOLTS

                Else
                    Gain(Current_Protocol) = BIP5VOLTS 'if something screws up, give +/5 5v
                End If


            ElseIf C_Script = "message" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                temp_string = C_Script
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter

                If C_Script = "v" Then
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    Array_Number = Val(C_Script)

                    temp_string = temp_string & Str(Array_Value(Array_Number, Array_Index(Array_Number)))
                Else
                    temp_string = temp_string & C_Script
                End If
                'Me.Text = temp_string


            ElseIf C_Script = "intensity" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter

                If C_Script = "v" Then
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    Array_Number = Val(C_Script)

                    Current_Intensity = Array_Value(Array_Number, Array_Index(Array_Number))
                Else
                    Current_Intensity = Val(C_Script) 'set current intensity
                End If
                If ForReal Then
                    Call Out_Intensity(Current_Intensity, Blue_Actinic, Far_Red, 0)

                    ProgressBar3.Value = Current_Intensity
                    actinic_label.Text = Current_Intensity
                End If
            ElseIf C_Script = "blue_actinic" Or C_Script = "flash" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Blue_Actinic = Val(C_Script) 'set current intensity
                If ForReal Then Call Out_Intensity(Current_Intensity, Blue_Actinic, Far_Red, 0)

            ElseIf C_Script = "leaf_actinic" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Blue_Actinic = Val(C_Script) 'set current intensity
                If ForReal Then Call Out_Intensity(Current_Intensity, Blue_Actinic, Far_Red, 0)
            ElseIf C_Script = "far_red" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Far_Red = Val(C_Script) 'set current intensity
                If ForReal Then Call Out_Intensity(Current_Intensity, Blue_Actinic, Far_Red, 0)

            ElseIf C_Script = "recovery_number_points" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If ForReal Then Recovery_Number_Points(Current_Protocol) = Val(C_Script) 'set wait time

            ElseIf C_Script = "baseline_actinic_intensity" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter

                If C_Script = "v" Then
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    Array_Number = Val(C_Script)

                    Baseline_Actinic_Intensity(Current_Protocol) = Array_Value(Array_Number, Array_Index(Array_Number))
                Else
                    Baseline_Actinic_Intensity(Current_Protocol) = Val(C_Script) 'set wait time
                End If
            ElseIf C_Script = "recovery_actinic_intensity" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If C_Script = "v" Then
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    Array_Number = Val(C_Script)
                    'UPGRADE_WARNING: Couldn't resolve default property of object Array_Value(Array_Number, Array_Index()). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Recovery_Actinic_Intensity(Current_Protocol) = Array_Value(Array_Number, Array_Index(Array_Number))
                Else
                    Recovery_Actinic_Intensity(Current_Protocol) = Val(C_Script)
                End If
            ElseIf C_Script = "dirk_actinic_intensity" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If C_Script = "v" Then
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    Array_Number = Val(C_Script)
                    'UPGRADE_WARNING: Couldn't resolve default property of object Array_Value(Array_Number, Array_Index()). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Dirk_Actinic_Intensity(Current_Protocol) = Array_Value(Array_Number, Array_Index(Array_Number))
                Else
                    Dirk_Actinic_Intensity(Current_Protocol) = Val(C_Script) 'set wait time
                End If
            ElseIf C_Script = "no_plot" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                No_Plot_Data = Val(C_Script)

            ElseIf C_Script = "smooth" Then
                ' not implemented
            ElseIf C_Script = "ref_channel" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter

                If Val(C_Script) = 1 Then
                    ref_channel = True
                Else
                    ref_channel = False
                End If

            ElseIf C_Script = "plot_file" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If ForReal Then Call Plot_File(File_Name(Current_Trace), (Val(C_Script)), (1), False, 0) ', Graph_Name)
            ElseIf C_Script = "plot_clear" Then
                For i = 1 To 6
                    'List1.Items.Add(Graph_Name(i))
                    If ForReal Then Call Plot_File("c:\test.dat", (i), (i And 1), False, 0)
                Next i

            ElseIf C_Script = "plot_raw" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If ForReal Then Call Plot_File(File_Name(Current_Trace), (Val(C_Script)), (0), False, 0) ', Graph_Name)

            ElseIf C_Script = "plot_delta" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If ForReal Then Call Plot_File(File_Name(Current_Trace), (Val(C_Script)), (1), False, 0) ', Graph_Name)
            ElseIf C_Script = "plot_delta_linear" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If ForReal Then Call Plot_File(File_Name(Current_Trace), (Val(C_Script)), (1), False, 1) ', Graph_Name)

            ElseIf C_Script = "plot_ref" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If ForReal Then Call Plot_File(File_Name(Current_Trace), (Val(C_Script)), (2), False, 0) ', Graph_Name)
            ElseIf C_Script = "add_plot_raw" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If ForReal Then Call Plot_File(File_Name(Current_Trace), (Val(C_Script)), (0), True, 0) ', Graph_Name)

            ElseIf C_Script = "add_plot_delta" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If ForReal Then Call Plot_File(File_Name(Current_Trace), (Val(C_Script)), (1), True, 0) ', Graph_Name)
            ElseIf C_Script = "add_plot_delta_linear" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If ForReal Then Call Plot_File(File_Name(Current_Trace), (Val(C_Script)), (1), True, 1) ', Graph_Name)

            ElseIf C_Script = "add_plot_ref" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If ForReal Then Call Plot_File(File_Name(Current_Trace), (Val(C_Script)), (2), True, 0) ', Graph_Name)


            ElseIf C_Script = "calc_delta" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                If ForReal Then Call Delta(File_Name(Current_Trace), File_Name(Val(C_Script)))

            ElseIf C_Script = "sub_var" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                i = (sub_var(C_Script, 1))

            ElseIf C_Script = "save_mode" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                temp = C_Script
                If temp = "file_replace" Or temp = "overwrite" Then
                    Save_Mode(Current_Trace) = File_Replace
                ElseIf temp = "average_into" Then
                    Save_Mode(Current_Trace) = Average_Into
                ElseIf temp = "file_append" Then
                    Save_Mode(Current_Trace) = File_Append
                End If

                'Global Const File_Replace = 0
                'Global Const Average_Into = 1
                'Global Const File_Append = 2
            ElseIf C_Script = "graph_labels" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter


                Graph_Name(1) = C_Script
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Graph_Name(2) = C_Script
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Graph_Name(3) = C_Script
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Graph_Name(4) = C_Script
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Graph_Name(5) = C_Script
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Graph_Name(6) = C_Script


            ElseIf C_Script = "array_index" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Array_Number = Val(C_Script)
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Array_Index(Array_Number) = Val(C_Script)

            ElseIf C_Script = "inc_array" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Array_Number = Val(C_Script) 'set wait time
                Array_Index(Array_Number) = Array_Index(Array_Number) + 1

            ElseIf C_Script = "dec_array" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter
                Array_Number = Val(C_Script) 'set wait time
                Array_Index(Array_Number) = Array_Index(Array_Number) - 1
            ElseIf VB.Right(C_Script, 1) = "|" Then
                ' indicates the start of a subroutine, ignore it
            ElseIf C_Script = "record_files" Then  'adds text to the note file
                Call Advance_Script()
                If Val(C_Script) > 0 Then
                    record_files = True
                Else
                    record_files = False
                End If

            ElseIf C_Script = "v_array" Then
                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter

                Array_Number = Val(C_Script)

                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter

                'UPGRADE_WARNING: Couldn't resolve default property of object Array_Max_Index(Array_Number). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Array_Max_Index(Array_Number) = Val(C_Script)

                'UPGRADE_WARNING: Couldn't resolve default property of object Array_Max_Index(Array_Number). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                For Array_Index(Array_Number) = 1 To Array_Max_Index(Array_Number)
                    Call Advance_Script()
                    'Script_Counter = Script_Counter + 1 'increment the script counter
                    'UPGRADE_WARNING: Couldn't resolve default property of object Array_Value(Array_Number, Array_Index()). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Array_Value(Array_Number, Array_Index(Array_Number)) = Val(C_Script)
                Next



            ElseIf C_Script = "att_sat" Then  ' change position of the fv/fv saturation pulse attenuation disk

                Call Advance_Script()
                'Script_Counter = Script_Counter + 1 'increment the script counter

                If Val(C_Script) = 1 Then
                    wt = 185
                Else
                    wt = 35
                End If

                'Call servofrm.SETTHESERVO(2, wt)

                'Else
                '    MessageBox.Show("Error:Command " & C_Script & " not recognized")
                '    Exit Sub
            Else ' script function not recognized

                Dim keepgoing As Boolean
                keepgoing = ShowScriptErrorMessage(Script_Counter, Script, "This command was not recognized.")
                If keepgoing = False Then Exit Sub
            End If

        End While
        If ForReal Then

            If record_files = True Then  'adds text to the note file")
                note_text = note_text & "*******************************************************************" & ControlChars.CrLf
                note_text = note_text & "ALL FILE FILE NAMES IN SCRIPT" & ControlChars.CrLf


                For i = 1 To Number_Traces
                    If Times_Averaged(i) > 0 Then
                        note_text = note_text & File_Name(i) & ControlChars.CrLf & " averaged " & Times_Averaged(i) & " times. " & ControlChars.CrLf
                        note_text = note_text & trace_note(i) & ControlChars.CrLf
                    End If
                Next i
                note_text = note_text & "*******************************************************************" & ControlChars.CrLf
                FileOpen(7, Note_File_Name, OpenMode.Output)
                Write(7, note_text)
                FileClose(7)
            End If
            If Halt_Script = True Then
                ulstat = cbStopBackground(BoardNum, AIFUNCTION)
                List1.Items.Add("HALT!")
                'Scrolling text box fix: make the box jump to the bottom after adding an item
                List1.TopIndex = List1.Items.Count - 1
            End If
spook:      Exit Sub

err_it:
            If Err.Number = 32755 Then
                Err.Clear()
                CommonDialog1Open.Reset()


                Resume spook
            Else

                ShowScriptFatalErrorMessage(Script_Counter, Script, "Error occured while reading script file: " & vbNewLine & Err.Number & ": " & Err.Description)
                'MessageBox.Show(Err.Number & " " & Err.Description & " Error in the script file around " & Script_Counter & " text = " & C_Script)
                Err.Clear()
                Close()

                Script_Counter = 0
                Resume spook
            End If
        Else ' end of test run
            List1.Items.Add("reached the end of the script file")
        End If

    End Sub


    Public Sub set_base_file_name()
        'On Error GoTo err_it3
        Dim prev As String = ""
        CommonDialog1Open.Title = "enter file name (& location) for date"
        CommonDialog1Save.Title = "enter file name (& location) for date"
        CommonDialog1Open.FileName = "*.dat"
        CommonDialog1Save.FileName = "*.dat"
        CommonDialog1Open.Title = "base file name to save"
        CommonDialog1Save.Title = "base file name to save"
        CommonDialog1Save.ShowDialog()
        CommonDialog1Open.FileName = CommonDialog1Save.FileName


        If CommonDialog1Open.FileName = "" Then
            Base_File_Name = "temp"
            Base_Base_File_Name = "temp"
        Else
            Base_Base_File_Name = CommonDialog1Open.FileName
            Base_File_Name = CommonDialog1Open.FileName
            List1.Items.Add("& " & Base_Base_File_Name)
            'Scrolling text box fix: make the box jump to the bottom after adding an item
            List1.TopIndex = List1.Items.Count - 1

        End If
        Exit Sub
err_it3:

        Err.Clear()
        script_file = prev
        Script_Label.Text = script_file
        Resume get_out
get_out:

    End Sub

    Public Sub Advance_Script()
        Script_Counter = Script_Counter + 1 'advance the script counter

        If VB.Left(Script(Script_Counter), 1) = "@" Then 'check to see if the next script line designates that we are inserting variable
            Dim j As Single
            Dim i, k As Integer
            Dim var As String

            var = VB.Right(Script(Script_Counter), (Len(Script(Script_Counter)) - 1))  'which variable are we inserting?
            'Label7.Text = var
            j = 0
            For i = 1 To number_variables
                If var = user_variable_name(i) Then
                    j = i
                End If
            Next i
            If j = 0 Then
                MessageBox.Show("Error:I cannot find that variable", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else
                Script_Counter = Script_Counter + 1
                If Script(Script_Counter) = "inc" Then
                    user_variable_index(j) = user_variable_index(i) + 1
                    k = user_variable_index(j)
                ElseIf Script(Script_Counter) = "dec" Then
                    user_variable_index(j) = user_variable_index(i) + 1
                    k = user_variable_index(j)
                Else
                    k = Val(Script(Script_Counter))
                    user_variable_index(j) = k
                End If
                'Script_Counter = Script_Counter + 1

                C_Script = Str$(user_variable(j, k)) 'insert the value of the variable into the script
            End If
        Else
            C_Script = Script(Script_Counter)
        End If

    End Sub
    Public Function sub_var(ByVal var As String, ByVal ii As Integer) As Single
        Dim j As Single
        Dim i As Integer
        If VB.Left(var, 1) = "@" Then
            For i = 1 To number_variables
                If var = user_variable_name(i) Then
                    j = user_variable(i, ii)
                End If
            Next i
        Else
            j = Val(var)
        End If
        List1.Items.Add(var & " " & j)
        'Scrolling text box fix: make the box jump to the bottom after adding an item
        List1.TopIndex = List1.Items.Count - 1
        Return (j)
    End Function

    Private Sub Script_Label_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Script_Label.Click
        On Error GoTo err_it2
        Dim prev As String
        prev = script_file
        'Dim script_file As String
        If prev <> "" Then
            CommonDialog1Open.FileName = prev
        Else
            CommonDialog1Open.FileName = "*.txt"
        End If
        'CommonDialog1Open.Filter = "Script Files|*.scr|Text Files|*.txt"
        CommonDialog1Open.Filter = "Text Files|*.txt"
        CommonDialog1Save.FileName = "*.txt"
        CommonDialog1Open.ShowDialog()
        '        CommonDialog1Save.FileName = CommonDialog1Open.FileName


        script_file = CommonDialog1Open.FileName
        Script_Label.Text = script_file
        Exit Sub
err_it2:
        Err.Clear()
        script_file = prev
        Script_Label.Text = script_file
        Resume get_out
get_out:
    End Sub

    Public Sub servoform_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'servofrm.Show()

    End Sub

    Public Sub Set_Actinic_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Set_Actinic.Click
        Intensity_Form.Show()
        Intensity_Form.BringToFront()

        'Intensity_done = False

        'While Intensity_done = False
        ' System.Windows.Forms.Application.DoEvents()
        ' End While

        'Call Out_Intensity(Intensity_Pass, blue_actinic, far_red)

    End Sub

    Public Sub skip_wait_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles skip_wait.Click
        Bug_Out = True

    End Sub

    Public Sub set_wheel(ByRef wp As Short)
        'Call servofrm.SETTHESERVO(1, wp)
    End Sub
    Private Sub ClearWindow(ByVal ThisGraph As Short)
        'this sub chooses the first picturebox as the default image for clearing
        'it changes all the pixels to black in the chosen box


    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        'Dim x As Double
        Dim arrData(30, 0) As Single

        'Dim i, j As Integer
        'Dim rowLabelCount, columnLabelCount, rowCount As Integer
        'Dim columnCount, labelIndex, Column, Row As Integer
        'MSChart1.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine


        'MSChart20Lib.VtChChartType.VtChChartType2dLine

        'VtChChartType2dCombination()
        'MSChart20Lib.VtChChartType.VtChChartType2dXY()


        For i = 1 To 30
            arrData(i, 0) = i * i
        Next


        '        MSChart1.ChartData = arrData

    End Sub


    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    'Private Sub MSChart1_ChartSelected(ByVal sender As System.Object, ByVal e As AxMSChart20Lib._DMSChartEvents_ChartSelectedEvent)

    'End Sub

    'Private Sub MSChart1_DblClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '   Call Dplot_Plot(s_Plot_File_Name(1), s_Plot_File_Type(1))

    'End Sub


    Private Sub Button1_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call Dplot_Plot("c:\test.dat", 0)
    End Sub
    Sub Dplot_Plot(ByRef Plot_File_name As String, ByRef Plot_Delta As Short)
        Dim d As DPLOT
        Dim i, ii, iii As Integer
        'Dim N As Integer
        Dim x() As Double
        Dim y() As Double
        Dim cmds As String
        Dim ret As Integer

        Dim Number_Measuring_Lights As Integer
        Dim total_points As Integer
        Dim j As String
        Dim Plot_Data_X() As Double
        Dim Plot_Data_Y() As Double
        Dim temp As Single = 0

        ' figure out how many columns


        FileOpen(1, Plot_File_name, OpenMode.Input)
        j = LineInput(1)
        FileClose(1)

        Number_Measuring_Lights = 1
        For i = 1 To Len(j)
            If Asc(Mid(j, i, 1)) = 9 Then  'counting the number of tabs
                Number_Measuring_Lights = Number_Measuring_Lights + 1
            End If
        Next i


        Number_Measuring_Lights = Number_Measuring_Lights / 4


        FileOpen(1, Plot_File_name, OpenMode.Input)


        i = 0

        While EOF(1) = False
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            Input(1, temp)
            i = i + 1
        End While  'coiunt the total number of points, each has 4 data

        FileClose(1)
        total_points = i

        ReDim Plot_Data_X(total_points)
        ReDim Plot_Data_Y(total_points)

        i = 0

        FileOpen(1, Plot_File_name, OpenMode.Input)
        If Plot_Delta = 1 Then

            While EOF(1) = False
                Input(1, Plot_Data_X(i))
                Input(1, temp)
                Input(1, temp)
                Input(1, Plot_Data_Y(i))
                i = i + 1
            End While

        Else

            While EOF(1) = False
                Input(1, Plot_Data_X(i))
                Input(1, Plot_Data_Y(i))
                Input(1, temp)
                Input(1, temp)
                i = i + 1
            End While

        End If
        FileClose(1)

        ' Plot sin(PI*x) and cos(PI*x) from x = 0 to 4.
        ' In this case we've used DATA_XYYY (one X array for one or more Y arrays).
        '
        ' Since the X values are evenly spaced we could also use
        ' DATA_DXY  - X has only 2 elements, DX and X0
        '
        ' And you can ALWAYS use
        ' DATA_XYXY - Each curve uses its own X array. This is the only
        '             option available if the curves have different X values or
        '             a different number of points.

        'N = total_points
        ReDim x(total_points)
        ReDim y(total_points)

        iii = 0
        For ii = 0 To Number_Measuring_Lights - 1

            For i = ii To (total_points - Number_Measuring_Lights) + ii Step Number_Measuring_Lights
                x(iii) = Plot_Data_X(i)
                y(iii) = Plot_Data_Y(i)
                iii = iii + 1
            Next i
        Next ii
        'For i = 0 To N - 1
        ' x(i) = Plot_Data_X(i) '(4.0# * i) / (N - 1)
        ' y(i) = Plot_Data_Y(i) 'System.Math.Sin(Math.PI * x(i))
        ' 'y(i + N) = Plot_Data_Y(i) * 2
        ' Next i

        d = New DPLOT
        d.Initialize()
        d.Version = DPLOT_DDE_VERSION
        d.DataFormat = DATA_XYXY
        d.MaxCurves = 16 ' Must be >= number of curves we plot
        d.MaxPoints = total_points * 2 ' Anything >= N will do
        d.NumCurves = Number_Measuring_Lights
        d.ScaleCode = SCALE_LINEARX_LINEARY
        d.LegendX = 0.05
        d.LegendY = 0.05
        For i = 0 To Number_Measuring_Lights - 1
            d.NP(i) = total_points / Number_Measuring_Lights
        Next i

        For i = 0 To Number_Measuring_Lights - 1
            d.LineType(i) = LINESTYLE_SOLID
            'd.NP(i) = total_points / Number_Measuring_Lights
        Next i

        '        d.LineType(0) = LINESTYLE_SOLID
        '        d.LineType(1) = LINESTYLE_LONGDASH
        d.Title1 = "Data sent to DPlot via DPLOTLIB.DLL"
        d.XAxis = "time (s)"
        d.YAxis = "y"

        ' The command string can be as complex as you like, limited to 32K characters.
        ' As an alternative, if your program will be producing many similar plots then
        ' you might prefer to create a preferences file, edit the file with a text
        ' editor to remove unwanted entries, and use the [GetPreferences("filename")]
        ' command.
        cmds = "" '"[ManualScale(0,-1.25,4,1.25)][TickInterval(1,0.5,0.25)]"
        cmds = cmds & "[Caption(" & Chr(34) & "DPLOTLIB XY Test" & Chr(34) & ")]"
        'cmds = cmds & "[Legend(1," & Chr(34) & "sin({\sp}x)" & Chr(34) & ")]"
        'cmds = cmds & "[Legend(2," & Chr(34) & "cos({\sp}x)" & Chr(34) & ")]"
        cmds = cmds & "[DocMaximize()][ClearEditFlag()]"
        'cmds = cmds & "Color(index,r,g,b)"

        '        ret = DPlot_Plot8(d, Plot_Data_X(0), Plot_Data_Y(0), cmds)
        ret = DPlot_Plot8(d, x(0), y(0), cmds)
        'ret = DPlot_AddData(d, Plot_Data_X(0), Plot_Data_Y(0), cmds)

        'List1.Items.Add(ret & " " & Number_Measuring_Lights & " " & total_points)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub MSChart2_DblClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call Dplot_Plot(s_Plot_File_Name(2), s_Plot_File_Type(2))
    End Sub

    Private Sub MSChart3_DblClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call Dplot_Plot(s_Plot_File_Name(3), s_Plot_File_Type(3))
    End Sub

    Private Sub MSChart4_DblClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call Dplot_Plot(s_Plot_File_Name(4), s_Plot_File_Type(4))
    End Sub

    Private Sub MSChart5_DblClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call Dplot_Plot(s_Plot_File_Name(5), s_Plot_File_Type(5))
    End Sub

    Private Sub MSChart6_DblClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call Dplot_Plot(s_Plot_File_Name(6), s_Plot_File_Type(6))
    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click

    End Sub

    Private Sub CreateGraph(ByVal zgc As ZedGraphControl, ByVal number_measuring_lights As Integer, ByVal total_points As Integer, ByVal plot_data_x As Object, ByVal plot_data_y As Object, ByVal plot_delta As Short, ByVal Graph_Name_l As Object, ByVal add_to As Integer, ByVal plot_window As Integer)
        Dim myPane As GraphPane = zgc.GraphPane
        Dim i, ii As Integer
        Dim x As Double
        Dim y As Double

        ' Set the titles and axis labels
        myPane.Title.Text = Graph_Name_l
        'Me.List1.Items.Add(Graph_Name_l)
        myPane.XAxis.Title.Text = "time (s)"
        If plot_delta = 1 Then
            myPane.YAxis.Title.Text = ".delta.A"
        Else
            myPane.YAxis.Title.Text = "V"
        End If

        If add_to = False Then
            myPane.CurveList.Clear()
            ccc(plot_window) = 0
        End If


        ' Make up some data points from the Sine function


        ', y As Double

        For ii = 1 To number_measuring_lights
            ccc(plot_window) = ccc(plot_window) + 1
            Dim list1 As New PointPairList()

            For i = 0 To total_points - 1
                x = plot_data_x(ii, i)
                y = plot_data_y(ii, i)
                list1.Add(x, y)
            Next i

            Select Case (ccc(plot_window) And 7)
                Case 1
                    Dim myCurve1 As LineItem = myPane.AddCurve("T:" & Str(Current_Trace), list1, Color.Red, SymbolType.None)
                Case 2
                    Dim myCurve1 As LineItem = myPane.AddCurve("T:" & Str(Current_Trace), list1, Color.Orange, SymbolType.None)
                Case 3
                    Dim myCurve1 As LineItem = myPane.AddCurve("T:" & Str(Current_Trace), list1, Color.Yellow, SymbolType.None)
                Case 4
                    Dim myCurve1 As LineItem = myPane.AddCurve("T:" & Str(Current_Trace), list1, Color.Green, SymbolType.None)
                Case 5
                    Dim myCurve1 As LineItem = myPane.AddCurve("T:" & Str(Current_Trace), list1, Color.Blue, SymbolType.None)
                Case 6
                    Dim myCurve1 As LineItem = myPane.AddCurve("T:" & Str(Current_Trace), list1, Color.Violet, SymbolType.None)
                Case 7
                    Dim myCurve1 As LineItem = myPane.AddCurve("T:" & Str(Current_Trace), list1, Color.White, SymbolType.None)

                    'myCurve1.Symbol.Fill = New Fill(Color.White)
            End Select
        Next
        ' Generate a blue curve with circle symbols, and "My Curve 2" in the legend

        ' Fill the axis background with a color gradient
        myPane.Chart.Fill = New Fill(Color.Black, Color.Green, 45.0F)

        ' Fill the pane background with a color gradient
        myPane.Fill = New Fill(Color.White, Color.FromArgb(255, 255, 155), 45.0F)

        ' Calculate the Axis Scale Ranges
        zgc.AxisChange()

        zgc.Refresh()

    End Sub

    Private Sub CreateGraph_old(ByVal zgc As ZedGraphControl, ByVal number_measuring_lights As Integer, ByVal total_points As Integer, ByVal plot_data_x As Object, ByVal plot_data_y As Object, ByVal plot_delta As Short, ByVal Graph_Name_l As Object)
        Dim myPane As GraphPane = zgc.GraphPane
        Dim i As Integer
        ' Set the titles and axis labels
        myPane.Title.Text = Graph_Name_l
        'Me.List1.Items.Add(Graph_Name_l)
        myPane.XAxis.Title.Text = "time (s)"
        If plot_delta = 1 Then
            myPane.YAxis.Title.Text = ".delta.A"
        Else
            myPane.YAxis.Title.Text = "V"
        End If

        myPane.CurveList.Clear()


        ' Make up some data points from the Sine function

        Dim x As Double
        Dim y As Double
        ', y As Double


        ' Generate a blue curve with circle symbols, and "My Curve 2" in the legend

        If number_measuring_lights > 0 Then
            Dim list1 As New PointPairList()
            'list1.Clear()
            For i = 0 To total_points - 1 Step number_measuring_lights
                x = plot_data_x(i)
                y = plot_data_y(i) 'Math.Sin(x * Math.PI / 15.0)
                list1.Add(x, y)

            Next i

            Dim myCurve1 As LineItem = myPane.AddCurve("Curve 1", list1, Color.Red, SymbolType.None)
            'myCurve1.Symbol.Fill = New Fill(Color.White)
        End If

        If number_measuring_lights > 1 Then
            Dim list2 As New PointPairList()
            For i = 1 To total_points - 1 Step number_measuring_lights
                x = plot_data_x(i)
                y = plot_data_y(i) 'Math.Sin(x * Math.PI / 15.0)
                list2.Add(x, y)
            Next i
            Dim myCurve2 As LineItem = myPane.AddCurve("Curve 2", list2, Color.Orange, SymbolType.None)
            'myCurve2.Symbol.Fill = New Fill(Color.White)
        End If

        If number_measuring_lights > 2 Then
            Dim list3 As New PointPairList()
            For i = 2 To total_points - 1 Step number_measuring_lights
                x = plot_data_x(i)
                y = plot_data_y(i) 'Math.Sin(x * Math.PI / 15.0)
                list3.Add(x, y)
            Next i
            Dim myCurve3 As LineItem = myPane.AddCurve("Curve 3", list3, Color.Yellow, SymbolType.None)
        End If

        If number_measuring_lights > 3 Then
            Dim list4 As New PointPairList()
            For i = 3 To total_points - 1 Step number_measuring_lights
                x = plot_data_x(i)
                y = plot_data_y(i) 'Math.Sin(x * Math.PI / 15.0)
                list4.Add(x, y)
            Next i
            Dim myCurve4 As LineItem = myPane.AddCurve("Curve 4", list4, Color.Green, SymbolType.None)
        End If

        If number_measuring_lights > 4 Then
            Dim list5 As New PointPairList()
            For i = 4 To total_points - 1 Step number_measuring_lights
                x = plot_data_x(i)
                y = plot_data_y(i) 'Math.Sin(x * Math.PI / 15.0)
                list5.Add(x, y)
            Next i
            Dim myCurve5 As LineItem = myPane.AddCurve("Curve 5", list5, Color.Aqua, SymbolType.None)
        End If

        If number_measuring_lights > 5 Then
            Dim list6 As New PointPairList()
            For i = 5 To total_points - 1 Step number_measuring_lights
                x = plot_data_x(i)
                y = plot_data_y(i) 'Math.Sin(x * Math.PI / 15.0)
                list6.Add(x, y)
            Next i
            Dim myCurve6 As LineItem = myPane.AddCurve("Curve 6", list6, Color.Blue, SymbolType.None)
        End If

        If number_measuring_lights > 6 Then
            Dim list7 As New PointPairList()
            For i = 6 To total_points - 1 Step number_measuring_lights
                x = plot_data_x(i)
                y = plot_data_y(i) 'Math.Sin(x * Math.PI / 15.0)
                list7.Add(x, y)
            Next i
            Dim myCurve7 As LineItem = myPane.AddCurve("Curve 7", list7, Color.Violet, SymbolType.None)
        End If

        If number_measuring_lights > 7 Then
            Dim list8 As New PointPairList()
            For i = 7 To total_points - 1 Step number_measuring_lights
                x = plot_data_x(i)
                y = plot_data_y(i) 'Math.Sin(x * Math.PI / 15.0)
                list8.Add(x, y)
            Next i
            Dim myCurve8 As LineItem = myPane.AddCurve("Curve 8", list8, Color.Black, SymbolType.None)
        End If

        If number_measuring_lights > 8 Then
            Dim list9 As New PointPairList()
            For i = 8 To total_points - 1 Step number_measuring_lights
                x = plot_data_x(i)
                y = plot_data_y(i) 'Math.Sin(x * Math.PI / 15.0)
                list9.Add(x, y)
            Next i
            Dim myCurve9 As LineItem = myPane.AddCurve("Curve 9", list9, Color.Brown, SymbolType.None)
        End If

        If number_measuring_lights > 9 Then
            Dim list10 As New PointPairList()
            For i = 9 To total_points - 1 Step number_measuring_lights
                x = plot_data_x(i)
                y = plot_data_y(i) 'Math.Sin(x * Math.PI / 15.0)
                list10.Add(x, y)
            Next i
            Dim myCurve10 As LineItem = myPane.AddCurve("Curve 10", list10, Color.BurlyWood, SymbolType.None)
        End If

        If number_measuring_lights > 10 Then
            Dim list11 As New PointPairList()
            For i = 10 To total_points - 1 Step number_measuring_lights
                x = plot_data_x(i)
                y = plot_data_y(i) 'Math.Sin(x * Math.PI / 15.0)
                list11.Add(x, y)
            Next i
            Dim myCurve11 As LineItem = myPane.AddCurve("Curve 11", list11, Color.AliceBlue, SymbolType.None)
        End If

        If number_measuring_lights > 11 Then
            Dim list12 As New PointPairList()
            For i = 11 To total_points - 1 Step number_measuring_lights
                x = plot_data_x(i)
                y = plot_data_y(i) 'Math.Sin(x * Math.PI / 15.0)
                list12.Add(x, y)
            Next i
            Dim myCurve12 As LineItem = myPane.AddCurve("Curve 12", list12, Color.Crimson, SymbolType.None)
        End If

        If number_measuring_lights > 12 Then
            Dim list13 As New PointPairList()
            For i = 12 To total_points - 1 Step number_measuring_lights
                x = plot_data_x(i)
                y = plot_data_y(i) 'Math.Sin(x * Math.PI / 15.0)
                list13.Add(x, y)
            Next i
            Dim myCurve13 As LineItem = myPane.AddCurve("Curve 13", list13, Color.Bisque, SymbolType.None)
        End If

        If number_measuring_lights > 13 Then
            Dim list14 As New PointPairList()
            For i = 13 To total_points - 1 Step number_measuring_lights
                x = plot_data_x(i)
                y = plot_data_y(i) 'Math.Sin(x * Math.PI / 15.0)
                list14.Add(x, y)
            Next i
            Dim myCurve14 As LineItem = myPane.AddCurve("Curve 14", list14, Color.DarkSeaGreen, SymbolType.None)
        End If

        If number_measuring_lights > 14 Then
            Dim list15 As New PointPairList()
            For i = 14 To total_points - 1 Step number_measuring_lights
                x = plot_data_x(i)
                y = plot_data_y(i) 'Math.Sin(x * Math.PI / 15.0)
                list15.Add(x, y)
            Next i
            Dim myCurve15 As LineItem = myPane.AddCurve("Curve 15", list15, Color.DarkGoldenrod, SymbolType.None)
        End If

        If number_measuring_lights > 15 Then
            Dim list16 As New PointPairList()
            For i = 15 To total_points - 1 Step number_measuring_lights
                x = plot_data_x(i)
                y = plot_data_y(i) 'Math.Sin(x * Math.PI / 15.0)
                list16.Add(x, y)
            Next i
            Dim myCurve16 As LineItem = myPane.AddCurve("Curve 16", list16, Color.DarkKhaki, SymbolType.None)
        End If


        'Color.Blue, SymbolType.Circle)
        '        Dim myCurve2 As LineItem = myPane.AddCurve("Curve 2", plot_data_x, Color.Red, SymbolType.Square)

        ' Fill the area under the curve with a white-red gradient at 45 degrees
        'myCurve.Line.Fill = New Fill(Color.White, Color.Red, 45.0F)
        ' Make the symbols opaque by filling them with white

        'myCurve2.Symbol.Fill = New Fill(Color.White)
        ' Fill the axis background with a color gradient
        myPane.Chart.Fill = New Fill(Color.Black, Color.Green, 45.0F)

        ' Fill the pane background with a color gradient
        myPane.Fill = New Fill(Color.White, Color.FromArgb(255, 255, 155), 45.0F)

        ' Calculate the Axis Scale Ranges
        zgc.AxisChange()

        zgc.Refresh()

    End Sub

    Private Sub zg1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles zg1.Load

    End Sub

    Private Sub Button1_Click_3(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call Dplot_Plot(s_Plot_File_Name(1), s_Plot_File_Type(1))
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call Dplot_Plot(s_Plot_File_Name(2), s_Plot_File_Type(2))
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call Dplot_Plot(s_Plot_File_Name(3), s_Plot_File_Type(3))
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Call Dplot_Plot(s_Plot_File_Name(4), s_Plot_File_Type(4))
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Call Dplot_Plot(s_Plot_File_Name(6), s_Plot_File_Type(6))
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Call Dplot_Plot(s_Plot_File_Name(6), s_Plot_File_Type(6))
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim ulstat As Integer
        Dim direction As Integer = 0
        Dim enable As Integer = 0
        Dim p_v As Short
        Dim p_o, P_O_A As Single
        Dim target As Single
        Dim i, ii As Integer
        target = Val(TextBox1.Text)
        p_o = 1
        P_O_A = 1

        ulstat = cbAIn(BoardNum, 7, BIP5VOLTS, p_v)
        ulstat = cbToEngUnits(BoardNum, BIP5VOLTS, p_v, p_o)
        Label1.Text = p_o

        Exit Sub
        Halt_Script = False


        If target > Current_Wheel_Position Then
            direction = 1
        Else
            direction = 0
        End If

        For i = 1 To Math.Abs(Current_Wheel_Position - target)
            enable = 1
            D_Out_Mask = 1 * enable + 2 * direction
            ulstat = cbDOut(BoardNum, AUXPORT, D_Out_Mask)
            enable = 0
            D_Out_Mask = 1 * enable + 2 * direction

            ulstat = cbDOut(BoardNum, AUXPORT, D_Out_Mask)

            ulstat = cbAIn(BoardNum, 7, BIP5VOLTS, p_v)
            ulstat = cbToEngUnits(BoardNum, BIP5VOLTS, p_v, p_o)

            'Application.DoEvents()
            For ii = 1 To 100000
            Next ii

        Next i
        Current_Wheel_Position = target
        Label1.Text = Current_Wheel_Position


        Exit Sub

        While P_O_A > target + 0.001 Or P_O_A < target - 0.001
            If Halt_Script = True Then Exit While
            If p_o > target Then
                direction = 1
            Else
                direction = 0
            End If

            enable = 1
            D_Out_Mask = 1 * enable + 2 * direction
            ulstat = cbDOut(BoardNum, AUXPORT, D_Out_Mask)
            enable = 0
            D_Out_Mask = 1 * enable + 2 * direction
            ulstat = cbDOut(BoardNum, AUXPORT, D_Out_Mask)
            'ulstat=
            P_O_A = 0
            For i = 1 To 8
                ulstat = cbAIn(BoardNum, 7, BIP5VOLTS, p_v)
                ulstat = cbToEngUnits(BoardNum, BIP5VOLTS, p_v, p_o)
                P_O_A = P_O_A + p_o
            Next i
            P_O_A = P_O_A / 8
            Label1.Text = P_O_A
            Application.DoEvents()

        End While

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
    Private Sub move_wheel(ByVal set_position As Single, ByVal av_it As Integer)
        Dim ulstat, direction, enable As Integer
        Dim p_v As Short
        Dim i As Integer
        Dim p_o, P_O_A As Single
        Dim target As Single
        target = set_position 'Val(TextBox1.Text)
        ulstat = cbAIn(BoardNum, 7, BIP5VOLTS, p_v)
        ulstat = cbToEngUnits(BoardNum, BIP5VOLTS, p_v, p_o)
        Label1.Text = p_o
        Halt_Script = False
        While P_O_A > target + 0.001 Or P_O_A < target - 0.001
            If Halt_Script = True Then Exit While
            If p_o > target Then
                direction = 1
            Else
                direction = 0
            End If

            enable = 1
            D_Out_Mask = 1 * enable + 2 * direction
            ulstat = cbDOut(BoardNum, AUXPORT, D_Out_Mask)
            enable = 0
            D_Out_Mask = 1 * enable + 2 * direction
            ulstat = cbDOut(BoardNum, AUXPORT, D_Out_Mask)
            'ulstat=
            P_O_A = 0
            For i = 1 To av_it
                ulstat = cbAIn(BoardNum, 7, BIP5VOLTS, p_v)
                ulstat = cbToEngUnits(BoardNum, BIP5VOLTS, p_v, p_o)
                P_O_A = P_O_A + p_o
            Next i
            P_O_A = P_O_A / av_it

            Application.DoEvents()

        End While
        Label1.Text = P_O_A


    End Sub

    Private Sub HomewheelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call move_wheel(1.51, 1)
        Call move_wheel(1.51, 32)
        Current_Wheel_Position = 0
    End Sub
    Private Sub set_wheel(ByVal target As Integer)
        'On Error Resume Next
        Dim receive_data As String = ""
        Label1.Text = "moving"
        System.Windows.Forms.Application.DoEvents()
        Using com1 As IO.Ports.SerialPort = My.Computer.Ports.OpenSerialPort("COM1")

            com1.BaudRate = 19200
            com1.DataBits = 8
            com1.StopBits = 1
            com1.Parity = IO.Ports.Parity.None
            com1.ReadTimeout = 20000

            com1.WriteLine("WGOTO" & target)


            'While receive_data = "" And Halt_Script = False
            receive_data = com1.ReadLine()
            System.Windows.Forms.Application.DoEvents()
            'End While
            Label1.Text = receive_data
            If receive_data = "*" Then
                Label1.Text = "OK"
            Else
                Label1.Text = "TIMED OUT"
            End If

        End Using

    End Sub
    Private Sub home_wheel()
        'On Error Resume Next
        Using com1 As IO.Ports.SerialPort = My.Computer.Ports.OpenSerialPort("COM1")
            Dim receive_data As String = ""
            com1.BaudRate = 19200
            com1.DataBits = 8
            com1.StopBits = 1
            com1.Parity = IO.Ports.Parity.None
            com1.ReadTimeout = 20000

            com1.WriteLine("WSMODE")


            'While receive_data = "" And Halt_Script = False
            receive_data = com1.ReadLine()
            System.Windows.Forms.Application.DoEvents()
            'End While
            Label1.Text = receive_data

            If receive_data <> "!" Then
                Label1.Text = "TIMED OUT"
                Exit Sub
            End If


            com1.WriteLine("WHOME")


            'While receive_data = "" And Halt_Script = False
            receive_data = com1.ReadLine()
            System.Windows.Forms.Application.DoEvents()
            'End While

            Label1.Text = receive_data


            com1.Close()
        End Using
    End Sub
    Private Sub Simmer(ByVal number_pulses As Integer, ByVal q_switch As String, ByVal simmer_q_switch As Short)
        'Dim ulstat As Short

        'ulstat = cbDOut(BoardNum, AUXPORT, 0)

        '        Trace_Running = True

        Dim Time_In_S As Single
        Dim PP_Line As Short = 0
        '       Dim i, j As Object
        Dim ii As Integer = 0
        Dim k As Short = 0
        Dim AD_Trigger As Short = 1

        Dim Loop_Return_Index As Short
        'Dim Points&
        '     Dim CBRate As Integer = 50000
        '     Dim CBCount As Integer
        '     Dim LowChan, HighChan As Short
        '     Dim Options As Integer
        'Dim ulstat%
        Dim ss As Short = 0
        '    Dim CurCount, CurIndex As Integer
        '     Dim Time_Accumulator As Single = 0
        '     Dim Measuring_Pulse_Number As Integer = 0
        '     Dim Timed_Out As Short
        '     Dim Time_Out_Time As Single = 1
        '     Dim Each_Measuring_Pulse_Interval() As Single
        '    Dim Current_Measuring_Point As Integer = 0
        '   Dim Give_Saturating_Pulse As Short
        '   Dim SH_State As Integer
        'Dim TTT1, TTT2 As Single
        '   Dim Raw_Data() As Short

        'ReDim Loop_Return_Index
        '  ReDim Each_Measuring_Pulse_Interval(Number_Measuring_Lights(Current_Protocol))

        ' set up pulseblaster

        Call outt(BaseAddress, 0)
        Call outt(BaseAddress + 2, &HAS)
        Call outt(BaseAddress + 3, 0)
        Call outt(BaseAddress + 4, 0) '&H55S)

        points = 0

        '  For i = 1 To Number_Loops(Current_Protocol)

        '        points = points + (Number_Measuring_Lights(Current_Protocol) * Number_Pulses(Current_Protocol, i))

        '       Next i

        '      ReDim data_time(points * 4 + 10)
        '     ReDim Data_Volts(points + 10, 4)
        '    ReDim Ref_Data_Volts(points * 4 + 10)
        '   ReDim Raw_Data(points * 4 + 10) ' dimension an array to hold the input values

        ' insert a 1 s delay
        '  SH_State = 0 'hold mode

        Call P_Blast(0, 0, 0, 0, 0, 0, 0, 0, 0, "100m", Time_In_S, 0, 0, 0, 0)

        PP_Line = PP_Line + 1


        Loop_Return_Index = PP_Line

        ' insert loop_begin code with


        Call P_Blast(0, 0, 0, 0, 0, 0, 0, 2, number_pulses, "5u", Time_In_S, 0, 1, 0, 0)

        'List1.Items.Add("l: " & Number_Pulses(Current_Protocol, i) - 1)
        PP_Line = PP_Line + 1
        'Xe_Flash(Current_Protocol, i)

        Call P_Blast(0, 0, 0, 0, 1, 0, 0, 0, 0, q_switch, Time_In_S, 0, 0, 0, 0)

        PP_Line = PP_Line + 1

        Call P_Blast(0, 0, 0, 0, 1, 0, 0, 0, 0, "10u", Time_In_S, 0, 1, simmer_q_switch, 0)

        PP_Line = PP_Line + 1

        ' insert loop end with:

        ' measuring_Pulse_Duration in delay count
        ' 3 in op code
        ' loop_return_index in data field
        ' intensity(i) and
        ' measuring_light(k)
        ' trigger
        ' in output field


        Call P_Blast(0, 0, 0, 0, 0, 0, 0, 3, (Loop_Return_Index), "100m", Time_In_S, 0, 0, 0, 0)


        PP_Line = PP_Line + 1



        ' insert STOP


        ' 1 in op code
        ' intensity(i) and


        Call P_Blast(0, 0, 0, 0, 0, 0, 0, 1, 0, "100n", Time_In_S, 0, 0, 0, 0)

        ' end of Pulse_Blaster program

        List1.Items.Add(" SIMMER")
        'Scrolling text box fix: make the box jump to the bottom after adding an item
        List1.TopIndex = List1.Items.Count - 1
        System.Windows.Forms.Application.DoEvents()



        'Call outt(BaseAddress + 6, 0)  'not needed for this version of PB?
        Call outt(BaseAddress + 7, 0) '7)

        'put the device in run mode
        Call outt(BaseAddress + 1, 0) '7)



    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Call Simmer(1000, (Str(TrackBar1.Value) & "u"), 0)
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        Label6.Text = TrackBar1.Value
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Call Simmer(1000, (Str(TrackBar1.Value) & "u"), 1)
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim x As String
        Dim i As Integer = 0
        'Dim mscomm1 as com


        x = Chr(255) & Chr(0) & Chr(100)

        'Using com1 As IO.Ports.SerialPort = My.Computer.Ports.OpenSerialPort("COM1")
        Using com1 As IO.Ports.SerialPort = My.Computer.Ports.OpenSerialPort("COM1")
            'My.Computer.Ports.OpenSerialPort("COM1")
            'com1.Encoding
            'com1.DtrEnable = True
            com1.Handshake = IO.Ports.Handshake.None
            com1.NewLine = 0

            com1.DtrEnable = False
            com1.Parity = IO.Ports.Parity.None
            'com1.

            com1.BaudRate = 2400
            com1.DataBits = 8
            com1.StopBits = IO.Ports.StopBits.One
            'com1.Encoding.ASCII(
            com1.WriteTimeout = 100000


            'com1.Write(x)
            'com1.Write (x)
            'com1.Write(Chr(255) & Chr(0) & Chr(22))
            'com1.
            'For i = 0 To 255
            'com1.Write(Chr(255) & Chr(0) & Chr(i))
            'Next i
            'Label1.Text = Asc((Chr(255))) & Asc(Chr(0)) & Asc(Chr(10))

            com1.Write(Chr(255))
            Call Hold_on_There(0.2)
            com1.Write(Chr(0))
            Call Hold_on_There(0.2)
            com1.Write(Chr(0))

            '           com1.WriteLine(Chr(255))
            '           com1.WriteLine(Chr(0))
            '          com1.WriteLine(Chr(25))

            'Call Hold_on_There(0.2)
            'Next

            '            Label1.Text = Asc((Chr(255))) & Asc(Chr(0)) & Asc(Chr(100))


            'com1.WriteLine(Chr(255))
            'com1.Write(Chr(255))
            'com1.Write(Chr(255))
            'com1.Write(Chr(255))
            'com1.Write(Chr(255))

            'com1.WriteLine(Chr(1))

            'com1.WriteLine(Chr(255))


        End Using

        Exit Sub

    End Sub

    Private Sub OnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub LaserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If view_laser = True Then
            Button8.Visible = False
            Button9.Visible = False
            TrackBar1.Visible = False
            Label6.Visible = False

            view_laser = False
        Else
            view_laser = True
            Button8.Visible = True
            Button9.Visible = True
            TrackBar1.Visible = True
            Label6.Visible = True
        End If

    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Call set_servo(1, Val(TextBox2.Text))
    End Sub
    Private Sub set_xe_intensity(ByVal inv As Single)
        If inv > 10 Then inv = 9.9
        If inv < 0 Then inv = 0


        Dim ttt As Long
        Dim outv, t As Short
        ttt = inv * (65535) / 10
        If ttt < 32768 Then
            outv = ttt
        Else
            outv = (ttt - 65535)

        End If
        'Label1.Text = "*" & outv
        '~~~

        t = cbAOut(1, 0, 1, outv)
        'System.Windows.Forms.Application.DoEvents()
    End Sub

    Sub set_servo(ByVal servo_num As Short, ByVal inv As Single)
        '        Label7.Text = (servo_num & " " & inv)


        'Dim inv As Single
        Dim ttt As Long
        Dim outv As Short = 0
        Dim t As Short = 0
        'inv = Val(TextBox1.Text)
        ttt = (1000 * inv / 180) + 250
        'inv * (65535) / 10
        'If ttt < 32768 Then
        ' outv = ttt
        ' Else
        ' outv = (ttt - 65535)

        'End If
        'Label2.Text = outv

        't = cbAOut(1, 0, 1, outv)


        t = cbC9513Init(1, 1, 2, FREQ2, CBDISABLED, CBDISABLED, CBDISABLED)

        t = cbC9513Config(1, servo_num, NOGATE, POSITIVEEDGE, FREQ2, CBDISABLED, LOADANDHOLDREG, RECYCLE, CBDISABLED, COUNTDOWN, TOGGLEONTC)
        t = cbCLoad(1, LOADREG1, 12500)
        't = cbCLoad(2, HOLDREG1, (1000 - (1000 * inv)))
        t = cbCLoad(1, HOLDREG1, (ttt))
        't = cbtimerout
    End Sub

    Private Sub FilterwheelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Filter_Wheel_Form.Show()
    End Sub

    Private Sub TakeFluorBaselineToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TakeFluorBaselineToolStripMenuItem.Click
        Dim ulstat As Integer
        ulstat = cbDConfigPort(BoardNum, AUXPORT, DIGITALOUT)
        If fluorescence_shutter_status = 0 Then
            Call fluorescence_shutter("open")

        Else
            Call fluorescence_shutter("close")

        End If

    End Sub

    Private Sub fluorescence_shutter(ByVal set_at As String)
        Dim ulstat As Integer
        ulstat = cbDConfigPort(BoardNum, AUXPORT, DIGITALOUT)
        If set_at = "open" Or set_at = "1" Then
            fluorescence_shutter_status = 1

            D_Out_Mask = (D_Out_Mask Or 2)
            ulstat = cbDOut(BoardNum, AUXPORT, D_Out_Mask)

            Call Hold_on_There(Shutter_delay)
            D_Out_Mask = (D_Out_Mask And &HFD)
            ulstat = cbDOut(BoardNum, AUXPORT, D_Out_Mask)
            List1.Items.Add(fluorescence_shutter_status)
            'Scrolling text box fix: make the box jump to the bottom after adding an item
            List1.TopIndex = List1.Items.Count - 1

        ElseIf set_at = "close" Or set_at = "0" Then
            fluorescence_shutter_status = 0
            D_Out_Mask = (D_Out_Mask Or 1)
            ulstat = cbDOut(BoardNum, AUXPORT, D_Out_Mask)

            Call Hold_on_There(Shutter_delay)
            D_Out_Mask = (D_Out_Mask And &HFE)
            ulstat = cbDOut(BoardNum, AUXPORT, D_Out_Mask)

            List1.Items.Add(fluorescence_shutter_status)
            'Scrolling text box fix: make the box jump to the bottom after adding an item
            List1.TopIndex = List1.Items.Count - 1

        End If
    End Sub

    Private Sub stir_control(ByVal on_off As Short)
        Dim ulstat As Integer
        ulstat = cbDConfigPort(BoardNum, AUXPORT, DIGITALOUT)
        If on_off > 0 Then
            stir_status = 1

            D_Out_Mask = (D_Out_Mask Or 4)
            ulstat = cbDOut(BoardNum, AUXPORT, D_Out_Mask)


        ElseIf on_off = 0 Then
            stir_status = 0
            D_Out_Mask = (D_Out_Mask And &HB)
            ulstat = cbDOut(BoardNum, AUXPORT, D_Out_Mask)


        End If
    End Sub

    Public Sub TakenoteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TakenoteToolStripMenuItem.Click
        Note_File_Name = Trim(Base_File_Name & "_notes.txt")
        take_a_note.Show()

    End Sub

    Private Sub NotesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotesToolStripMenuItem.Click

    End Sub

    Private Sub SetbasefileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetbasefileToolStripMenuItem.Click
        Call set_base_file_name()
    End Sub

    Private Sub RecordeventsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecordeventsToolStripMenuItem.Click

    End Sub

    Private Sub ONToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ONToolStripMenuItem.Click
        take_notes = True

    End Sub

    Private Sub OFFToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OFFToolStripMenuItem.Click
        take_notes = False
    End Sub
    Public Sub do_auto_gain(ByRef current_protocol As Integer, ByRef M_Reference_Gain(,) As Short, ByRef L_Measuring_Interval(,,) As String, ByRef M_Measuring_Interval(,) As String, ByRef M_Measuring_Light(,) As Short, ByRef m_number_measuring_lights() As Short, ByRef m_number_loops() As Short, ByRef M_Intensity(,) As Short, ByRef M_Number_Pulses(,) As Short, ByRef M_Take_Data(,) As Short, ByRef Measuring_Pulse_Duration() As String, ByRef Xe_Flash(,) As Short, ByRef Gain() As Integer, ByRef In_Channel() As Short, ByRef M_Far_Red(,) As Short, ByRef M_Blue_Actinic(,) As Short, ByRef gain_slop As Single, ByRef q_switch(,) As Short, ByRef Pre_Pulse(,) As Short, ByRef Pre_Pulse_Time(,) As String, ByRef Pre_Delay(,) As String)
        'do_auto_gain(M_Number_Measuring_Lights(Current_Protocol), 
        Dim ii, iiii As Integer
        Dim Max_Volts As Single
        Dim gain_temp, gain_set_temp As Single
        Dim gain_data(10) As Single
        'gain_data_list.Items.Clear()
        For iiii = 1 To m_number_measuring_lights(current_protocol)
            m_number_loops(0) = 1
            For ii = 1 To m_number_loops(0)
                M_Number_Pulses(0, ii) = 1
                M_Intensity(0, ii) = M_Intensity(current_protocol, 1)
            Next ii
            M_Take_Data(0, 1) = 1
            m_number_measuring_lights(0) = 8

            For ii = 1 To m_number_measuring_lights(0)
                M_Measuring_Light(0, ii) = M_Measuring_Light(current_protocol, iiii)  'set the measuring light to each of those used in the protocol
            Next ii

            For ii = 1 To m_number_measuring_lights(0)
                M_Measuring_Interval(0, ii) = "10m"
            Next ii
            For ii = 1 To m_number_measuring_lights(0)
                L_Measuring_Interval(0, ii, 1) = "10m"
            Next ii
            For ii = 1 To m_number_measuring_lights(0)
                M_Primary_Gain(0, ii) = ii - 1
            Next ii
            For ii = 1 To m_number_measuring_lights(0)
                M_Reference_Gain(0, ii) = ii - 1
            Next ii
            Measuring_Pulse_Duration(0) = Measuring_Pulse_Duration(current_protocol)
            Gain(0) = Gain(current_protocol)
            In_Channel(0) = In_Channel(current_protocol)
            'Xe_Flash, M_Far_Red, M_Blue_Actinic
            For ii = 1 To m_number_loops(0)
                Xe_Flash(0, ii) = Xe_Flash(current_protocol, 1)
                M_Far_Red(0, ii) = M_Far_Red(current_protocol, 1)
                M_Blue_Actinic(0, ii) = M_Blue_Actinic(current_protocol, 1)
            Next ii
            '                    Call Multi_Trace(0, M_Number_Loops, M_Intensity, M_Number_Pulses, M_Number_Measuring_Lights, M_Measuring_Light, M_Measuring_Interval, M_Primary_Gain, M_Reference_Gain, Measuring_Pulse_Duration, Gain(Current_Protocol), In_Channel(Current_Protocol), points, data_time)
            Call Multi_Trace(0, m_number_loops, M_Intensity, M_Number_Pulses, m_number_measuring_lights, M_Measuring_Light, L_Measuring_Interval, M_Primary_Gain, M_Reference_Gain, Measuring_Pulse_Duration, Gain(current_protocol), In_Channel(current_protocol), points, data_time, Xe_Flash, q_switch, M_Far_Red, M_Blue_Actinic, Pre_Pulse, Pre_Pulse_Time, Pre_Delay, M_Take_Data)
            'Call Dirk_Save(data_time, points, "c:\gain.dat", File_Replace, 1, 1, 0, m_number_measuring_lights(0), 1, 1, In_Channel(0))

            'Call Plot_File("c:\gain.dat", 1, (0), 0, 1)
            'Call Plot_File("c:\gain.dat", 2, (2), 0, 1)

            'For ii = 0 To m_number_measuring_lights(0) - 1
            'gain_data(ii) = Data_Volts(ii, In_Channel(current_protocol))
            'gain_data_list.Items.Add("gain data: " & M_Primary_Gain(0, ii + 1) & ": " & gain_data(ii))
            'Next ii

            'For ii = 0 To m_number_measuring_lights(0) - 1
            'gain_data(ii) = Data_Volts(ii, 2) 'reference channel
            'gain_data_list.Items.Add("gain ref: " & M_Reference_Gain(0, ii + 1) & ": " & gain_data(ii))
            ' Next ii
            'BIP10VOLTS, bip5volts, bip2pt5volts, bip1volts

            If Gain(0) = BIP10VOLTS Then
                Max_Volts = 10 * gain_slop
            ElseIf Gain(0) = BIP5VOLTS Then
                Max_Volts = 5 * gain_slop
            ElseIf Gain(0) = BIP2VOLTS Then
                Max_Volts = 2 * gain_slop
            ElseIf Gain(0) = BIP1VOLTS Then
                Max_Volts = 1 * gain_slop
            End If
            gain_temp = 0

            For ii = 0 To m_number_measuring_lights(0) - 1  'determine the best gain for the data channel
                gain_data(ii) = Data_Volts(ii, In_Channel(current_protocol))
                If Math.Abs(gain_data(ii)) > Math.Abs(gain_temp) And Math.Abs(gain_data(ii)) < Max_Volts Then
                    'If Math.Abs(gain_data(ii)) < Max_Volts Then

                    gain_temp = gain_data(ii)
                    gain_set_temp = ii
                End If
            Next ii

            'gain_data_list.Items.Add("best data: " & gain_temp & ": " & gain_set_temp)
            M_Primary_Gain(current_protocol, iiii) = gain_set_temp
            ProgressBar1.Value = M_Primary_Gain(current_protocol, iiii)
            sample_gain_label.Text = M_Primary_Gain(current_protocol, iiii)
            gain_temp = 0
            For ii = 0 To m_number_measuring_lights(0) - 1  'determine the best gain for the ref channel
                gain_data(ii) = Data_Volts(ii, 2)
                If Math.Abs(gain_data(ii)) > Math.Abs(gain_temp) And Math.Abs(gain_data(ii)) < Max_Volts Then
                    'If Math.Abs(gain_data(ii)) < Max_Volts Then
                    gain_temp = gain_data(ii)
                    gain_set_temp = ii
                End If
            Next ii

            'gain_data_list.Items.Add("best ref: " & gain_temp & ": " & gain_set_temp)
            M_Reference_Gain(current_protocol, iiii) = gain_set_temp
            ProgressBar2.Value = M_Reference_Gain(current_protocol, iiii)
            reference_gain_label.Text = M_Reference_Gain(current_protocol, iiii)

        Next iiii

    End Sub
    Public Sub do_auto_gain_ref(ByVal light As Integer)
        '        Call do_auto_gain(Current_Protocol, M_Reference_Gain, L_Measuring_Interval, M_Measuring_Interval, M_Measuring_Light, M_Number_Measuring_Lights, M_Number_Loops, M_Intensity, M_Number_Pulses, M_Take_Data, Measuring_Pulse_Duration, Xe_Flash, Gain, In_Channel, M_Far_Red, M_Blue_Actinic, gain_slop, q_switch, Pre_Pulse, Pre_Pulse_Time, Pre_Delay)
        script_file = "c:\gain_test.txt"
        'M_Measuring_Light(1, 1) = light
        Call run_the_script()
        script_file = ""
    End Sub

    Private Sub OnToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem1.Click
        Call stir_control(1)
    End Sub

    Private Sub OffToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem1.Click
        Call stir_control(0)
    End Sub

    Private Sub StirerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StirerToolStripMenuItem.Click

    End Sub
    ' This method is designed to produce more infomrative script-reading error messages
    Protected Function ShowScriptErrorMessage(ByVal linenumber As Integer, ByRef lines() As String, ByRef message As String) As Boolean
        Dim margin As Integer = 5
        Dim start As Integer = linenumber - margin
        Dim ans As DialogResult
        If start < 0 Then
            start = 0
        End If
        Dim code As String = vbNewLine
        While (start < (linenumber + margin) And start < lines.Length)
            code = code & lines(start) & vbNewLine
            start += 1
        End While

        ans = MessageBox.Show("Error reading script file:" & vbNewLine & "Command: """ & lines(linenumber) & """ (read #" & linenumber & ")" & vbNewLine & _
                              message & vbNewLine & "Error found in code block" & vbNewLine & code & vbNewLine & _
                              "Continue?", "READ ERROR", MessageBoxButtons.YesNo)
        If ans = Windows.Forms.DialogResult.No Then
            Return False
        Else
            Return True
        End If
    End Function
    Protected Function ShowScriptFatalErrorMessage(ByVal linenumber As Integer, ByRef lines() As String, ByRef message As String) As Boolean
        Dim margin As Integer = 5
        Dim start As Integer = linenumber - margin
        Dim ans As DialogResult
        If start < 0 Then
            start = 0
        End If
        Dim code As String = vbNewLine
        While (start < (linenumber + margin) And start < lines.Length)
            code = code & lines(start) & vbNewLine
            start += 1
        End While

        ans = MessageBox.Show("Error reading script file:" & vbNewLine & "Command: """ & lines(linenumber) & """ (read #" & linenumber & ")" & vbNewLine & _
                              message & vbNewLine & "Error found in code block" & vbNewLine & code & vbNewLine, _
                                "READ ERROR", MessageBoxButtons.OK)
        If ans = Windows.Forms.DialogResult.No Then
            Return False
        Else
            Return True
        End If
    End Function

    'This is the code generated for the Test_Script menu item
    Private Sub TestScriptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestScriptToolStripMenuItem.Click
        List1.Items.Add("TESTING SCRIPT FILE")
        List1.TopIndex = List1.Items.Count - 1
        Me.BackColor = Color.Yellow
        Call run_the_script(False)
        Me.BackColor = Color.Gray
        List1.Items.Add("TEST COMPLETE")
        List1.TopIndex = List1.Items.Count - 1
    End Sub

    Private Sub Button11_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        List1.Items.Add("TESTING SCRIPT FILE")
        List1.TopIndex = List1.Items.Count - 1
        Me.BackColor = Color.Yellow
        Call run_the_script(False)
        Me.BackColor = Color.Gray
        List1.Items.Add("TEST COMPLETE")
        List1.TopIndex = List1.Items.Count - 1
    End Sub
End Class