' Protocol class for housing all of the properties of a protocol in a single, convienient object
Public Class Protocol
    Public Number_Loops As Short ' number of loops in the protocol
    Public Number_Measuring_Lights As Short ' number of measuring lights
    Public Adc_Gain As Integer
    Public In_Channel As Short
    Public Ref_Channel As Boolean
    Public Measuring_Pulse_Duration(-1) As String

    Public Xe_intensity_value As Single
    Public laser As Integer
    Public Xe_Flash(-1) As Short ' bit 22 (Xe lamp) state for each loop

    Public Primary_Gain(-1) As Short ' array of sample channel gain settings (a gain setting for each measuring light)
    Public Reference_Gain(-1) As Short ' array of reference channel gain settings (a gain setting for each measuring light)


    Public Intensity(-1) As Short ' actinic intensity for each loop
    Public Saturating_Pulse(-1) As Short ' saturation pulse state for each loop
    Public Far_Red(-1) As Short ' farred state for each loop
    Public Blue_Actinic(-1) As Short ' blue actinic for each loop

    
    Public Pre_Pulse(-1) As Short ' prepulse for each loop
    Public Pre_Delay(-1) As String ' predelay state for each loop
    Public Pre_Pulse_Time(-1) As String  ' predelay duration  for each loop
    Public pre_pulse_light As Short ' 4-bit code for which combination of actinic lights to use during the prepulse (bit 1=blue, bit 2=saturating actinic, bit3=farred, bit 4=Xe flash)

    Public Number_Pulses(-1) As Short ' number of pulses in each loop
    Public Measuring_Light(-1) As Short ' array to hold the light number for each measuring light
    Public m_wasp(-1) As Short ' blue actinic for each loop
    Public choke_actinic(-1) As Short ' array to hold the lindicator of whether actinic light should be choked during measuring pulse
    Public Measuring_Interval(-1, -1) As String ' measuring interval for each light for each loop. Format: L_Measuring_Interval(light,loop) = duration

    Public Take_Data(-1) As Short ' whether or not to take a measurement on each loop


    'Public q_switch() As Short ' q_switch does nothing
    Public Baseline_Start As Short ' start of baseline correction
    Public Baseline_End As Short ' end of baseline correction

    ' constants
    Const Max_Number_Loops As Short = 100
    Const Max_Number_Lights As Short = 32
    'Private _m_wasp As Object

    ' Constructors 
    Public Sub New() ' default constructor fills variables with default values
        ReDim Primary_Gain(Max_Number_Lights)
        ReDim Reference_Gain(Max_Number_Lights)
        ReDim Intensity(Max_Number_Loops)
        ReDim Saturating_Pulse(Max_Number_Loops)
        ReDim Far_Red(Max_Number_Loops)
        ReDim Blue_Actinic(Max_Number_Loops)
        ReDim Pre_Pulse(Max_Number_Loops)
        ReDim Pre_Delay(Max_Number_Loops)
        ReDim Pre_Pulse_Time(Max_Number_Loops)
        ReDim Xe_Flash(Max_Number_Loops)
        ReDim Number_Pulses(Max_Number_Loops)
        ReDim Measuring_Light(Max_Number_Lights)
        ReDim m_wasp(Max_Number_Lights)
        ReDim choke_actinic(Max_Number_Lights)
        ReDim Measuring_Interval(Max_Number_Lights, Max_Number_Loops)
        ReDim Take_Data(Max_Number_Loops)

        ' set default values
        setDefaultValues()
    End Sub

    'Property m_wasp(ByVal loopindex As Integer) As Object
    '    Get
    '        Return _m_wasp
    '    End Get
    '    Set(ByVal value As Object)
    '        _m_wasp = value
    '    End Set
    'End Property

    Protected Overridable Sub setDefaultValues()
        Number_Measuring_Lights = 1
        Number_Loops = 3
        Adc_Gain = BIP10VOLTS
        In_Channel = 0
        Ref_Channel = False
        ReDim Measuring_Pulse_Duration(36)

        Xe_intensity_value = 0.0
        fillArrayWithShort(Xe_Flash, 0)
        laser = 0

        fillArrayWithShort(Primary_Gain, 0)
        fillArrayWithShort(Reference_Gain, 0)

        fillArrayWithShort(Intensity, 0)
        fillArrayWithShort(Saturating_Pulse, 0)
        fillArrayWithShort(Far_Red, 0)
        fillArrayWithShort(Blue_Actinic, 0)

        fillArrayWithShort(Pre_Pulse, 0)
        fillArrayWithString(Pre_Delay, "0")
        fillArrayWithString(Pre_Pulse_Time, "0")
        pre_pulse_light = 0

        fillArrayWithShort(Number_Pulses, 10)
        fillArrayWithShort(Measuring_Light, 0)
        For Each item As String In Measuring_Interval
            item = "10m"
        Next

        fillArrayWithShort(Take_Data, 1)

        Baseline_Start = 0
        Baseline_End = 0
    End Sub
    Private Sub fillArrayWithShort(ByRef short_array() As Short, ByVal val As Short)
        For Each item As Short In short_array
            item = val
        Next
    End Sub
    Private Sub fillArrayWithString(ByRef string_array() As String, ByRef val As String)
        For Each item As String In string_array
            item = val
        Next
    End Sub
    Public Shared Function TimeInSeconds(ByRef time As String) As Double
        If time = "" Then Return 0.0
        Dim multiplier As Double = 1
        Dim Count_Time_Num As String = Trim(time)

        If Count_Time_Num.Chars(Count_Time_Num.Length - 1) = "s" Then ' remove the s at the end, since it is assumed to be seconds
            Count_Time_Num.Remove(Count_Time_Num.LastIndexOf("s"), 1)
        End If
        If Count_Time_Num.Chars(Count_Time_Num.Length - 1) = "S" Then ' remove the s at the end, since it is assumed to be seconds
            Count_Time_Num.Remove(Count_Time_Num.LastIndexOf("S"), 1)
        End If

        If Count_Time_Num.Chars(Count_Time_Num.Length - 1) = "u" Then ' microseconds
            multiplier = 0.000001
        ElseIf Count_Time_Num.Chars(Count_Time_Num.Length - 1) = "m" Then ' milliseconds
            multiplier = 0.001
        ElseIf Count_Time_Num.Chars(Count_Time_Num.Length - 1) = "n" Then ' nanoseconds
            multiplier = 0.000000001
        Else
            multiplier = 1 ' seconds
        End If
        Return (Val(Count_Time_Num) * multiplier)
    End Function
End Class
