Public Class ChrisBlasterIO
    Const VERSION As Double = 0.05

    Public wasp As Integer = 0
    Public temp_sat_pulse As Integer
    ' this version uses the Digilent Nexys2 Board
    Public connected As Boolean ' useful for checking whether or not there is a connection befoe issuing commands
    Public DiagnosticMode As Boolean = True
    Public bitmasks_per_pulse As Integer = 128
    Public clockspeed_Hz As Double = 50000000 'Hz
    Public number_of_slow_outs As Integer = -1
    Public number_of_slow_ins As Integer = -1
    Public mpnum As Integer

    Public name As String ' name of the board to connect to 
    Public interface_handle As IntPtr ' handle for the connected board (multiple boards are allowed if each has a different name)

    'import functions from dll
    ' note: C++ ByVal integers are 32 bits and VB.net integers are 32 bits (VB.net ByVal integers are 64 bits)
    ' note: All functions listed below that return an integer actually return a boolean (except for 
    '   getClockFrequency()), but it is unsafe to use the boolean type in their declarations because C++ 
    '   defines booleans differently from VB
    Public Declare Sub getErrorMessage Lib "ChrisBlasterWindowsHost.dll" (ByVal msgbuff As String, ByVal length As Integer)
    Public Declare Function initDrivers Lib "ChrisBlasterWindowsHost.dll" () As Integer
    Public Declare Function openChrisBlasterConnection Lib "ChrisBlasterWindowsHost.dll" (ByVal name As String, ByRef devpointer As IntPtr) As Integer
    Public Declare Function closeChrisBlasterConnection Lib "ChrisBlasterWindowsHost.dll" (ByVal devpointer As IntPtr) As Integer

    Public Declare Function getClockFrequency Lib "ChrisBlasterWindowsHost.dll" (ByVal devpointer As IntPtr) As Integer
    Public Declare Function getSlowIOlimits Lib "ChrisBlasterWindowsHost.dll" (ByRef numouts As Integer, ByRef numins As Integer, ByVal devpointer As IntPtr) As Integer
    Public Declare Function getBoardStatus Lib "ChrisBlasterWindowsHost.dll" (ByRef bstat As Byte, ByVal devpointer As IntPtr) As Integer
    Public Declare Function castIntToByte Lib "ChrisBlasterWindowsHost.dll" (ByVal input As Integer) As Byte

    Public Declare Function allocateNewProtocol Lib "ChrisBlasterWindowsHost.dll" (ByVal pindex As Integer, ByVal numloops As Integer, ByVal numbitmasks As Integer, ByVal devpointer As IntPtr) As Integer
    Public Declare Function repeatLoop Lib "ChrisBlasterWindowsHost.dll" (ByVal pindex As Integer, ByVal lindex As Integer, ByVal numreps As Integer, ByVal devpointer As IntPtr) As Integer
    Public Declare Function addBitmask Lib "ChrisBlasterWindowsHost.dll" (ByVal pindex As Integer, ByVal lindex As Integer, ByVal bitmask As UInteger, ByVal cycles As UInteger, ByVal devpointer As IntPtr) As Integer
    Public Declare Function setTerminalBitmask Lib "ChrisBlasterWindowsHost.dll" (ByVal pindex As Integer, ByVal bitmask As UInteger, ByVal devpointer As IntPtr) As Integer
    Public Declare Function freeMemory Lib "ChrisBlasterWindowsHost.dll" (ByVal pindex As Integer, ByVal devpointer As IntPtr) As Integer
    Public Declare Function runProtocol Lib "ChrisBlasterWindowsHost.dll" (ByVal pindex As Integer, ByVal devpointer As IntPtr) As Integer
    Public Declare Function startProtocol Lib "ChrisBlasterWindowsHost.dll" (ByVal pindex As Integer, ByVal devpointer As IntPtr) As Integer
    Public Declare Function checkIfDone Lib "ChrisBlasterWindowsHost.dll" (ByVal devpointer As IntPtr) As Integer
    Public Declare Function killProtocol Lib "ChrisBlasterWindowsHost.dll" (ByVal devpointer As IntPtr) As Integer
    Public Declare Function changeOutputBitmask Lib "ChrisBlasterWindowsHost.dll" (ByVal bitmask As Integer, ByVal devpointer As IntPtr) As Integer
    Public Declare Function slowIOreadByte Lib "ChrisBlasterWindowsHost.dll" (ByVal address As Byte, ByRef getbyte As Byte, ByVal devpointer As IntPtr) As Integer
    Public Declare Function slowIOwriteByte Lib "ChrisBlasterWindowsHost.dll" (ByVal address As Byte, ByVal setbyte As Byte, ByVal devpointer As IntPtr) As Integer
    Public Declare Function refreshBoard Lib "ChrisBlasterWindowsHost.dll" (ByVal devpointer As IntPtr) As Integer

    Public Declare Function sendCommand Lib "ChrisBlasterWindowsHost.dll" (ByVal command As Byte, ByVal devpointer As IntPtr) As Integer
    Public Declare Function sendCommandWithParameters Lib "ChrisBlasterWindowsHost.dll" (ByVal command As Byte, ByVal p1 As Integer, ByVal p2 As Integer, ByVal p3 As Integer, ByVal p4 As Integer, ByVal devpointer As IntPtr) As Integer
    Public Declare Function writeInt32 Lib "ChrisBlasterWindowsHost.dll" (ByVal number As UInteger, ByVal startindex As Byte, ByVal devpointer As IntPtr) As Integer
    Public Declare Function readInt32 Lib "ChrisBlasterWindowsHost.dll" (ByRef number As UInteger, ByVal startindex As Byte, ByVal devpointer As IntPtr) As Integer

    'From http://www.eggheadcafe.com/software/aspnet/29978557/void-pointer-to-managed-o.aspx
    '"One of the functions used returns a void* which I need to cast into
    'a handle...Typically you can solve that problem with the IntPtr type. Of course C#
    'won't be able to interpret the meaning of IntPtr, but it can store its
    'value, and when you pass it back to a C++ function, you can get the
    'native pointer out of it. This is how handles (GDI or file handles) are
    'implemented in .NET -- they are simply stored as IntPtr members.
    '
    'Note that the value of an IntPtr is not managed, it just contains a
    'navite pointer address. The .NET framework doesn't know what to do with
    'that address, and doesn't keep track of native objects where those
    'IntPtrs point to. IntPtr is just as unsafe as a void*, because it
    'contains no type information, and it may point to a dead object."

    ' Windows OS imports
    Private Declare Sub Sleep Lib "kernel32" Alias "Sleep" (ByVal dwMilliseconds As Long)

    Private stringbuffer As String = "                                                                                                                                                                                                                                                                "
    Private bufferlength As Integer = 255

    Public Sub New(ByVal board_name As String)
        connected = False
        name = board_name
        Dim funcstat As Integer
        funcstat = initDrivers()
        If (funcstat = 0) Then 'failed to initialize drivers
            Windows.Forms.MessageBox.Show("Unable to initialize Adept drivers by Digilent Inc..")
            System.Windows.Forms.Application.DoEvents()
        Else
            If (board_name <> "") Then
                funcstat = openChrisBlasterConnection(name, interface_handle)
                If (funcstat <> 0) Then
                    connected = True
                Else
                    'unsuccessful connection!
                    connected = False
                    If (DiagnosticMode) Then
                        Windows.Forms.MessageBox.Show("Unable to establish connection to Digilent board named " + name + ".")
                        System.Windows.Forms.Application.DoEvents()
                    End If
                End If
            Else ' assume default name of "Nexys2"
                funcstat = openChrisBlasterConnection("Nexys2", interface_handle)
                If (funcstat <> 0) Then
                    connected = True
                Else
                    'unsuccessful connection!
                    connected = False
                    If (DiagnosticMode) Then
                        System.Windows.Forms.MessageBox.Show("Unable to establish connection to Digilent board named " + name + ".")
                        System.Windows.Forms.Application.DoEvents()
                    End If
                End If
            End If
        End If
        If (connected) Then
            Dim value As Integer
            value = getClockFrequency(interface_handle)
            If (value > 1) Then
                clockspeed_Hz = value
            End If
            Dim value2 As Integer
            If (getSlowIOlimits(value, value2, interface_handle) <> 0) Then
                number_of_slow_outs = value
                number_of_slow_ins = value2
            End If
        End If
    End Sub
    Protected Overrides Sub Finalize()
        If (connected) Then
            closeChrisBlasterConnection(interface_handle)
        End If
    End Sub

    Public Overridable Function ConnectToChrisBlasterBoard(ByVal board_name As String) As Boolean
        Dim connect_result As Integer
        If (connected) Then
            closeChrisBlasterConnection(interface_handle)
        End If
        If (board_name <> "") Then
            connect_result = openChrisBlasterConnection(name, interface_handle)
            If (connect_result <> 0) Then
                connected = True
            Else
                'unsuccessful connection!
                connected = False
                If (DiagnosticMode) Then
                    System.Windows.Forms.MessageBox.Show("Unable to establish connection to Digilent board named " + name + ".")
                    System.Windows.Forms.Application.DoEvents()
                End If
            End If
        Else ' assume default name of "Nexys2"
            connect_result = openChrisBlasterConnection("Nexys2", interface_handle)
            If (connect_result <> 0) Then
                connected = True
            Else
                'unsuccessful connection!
                connected = False
                If (DiagnosticMode) Then
                    System.Windows.Forms.MessageBox.Show("Unable to establish connection to Digilent board named " + name + ".")
                    System.Windows.Forms.Application.DoEvents()
                End If
            End If
        End If
        If (connect_result = 0) Then
            connected = False
            Return False
        Else
            connected = True
            Dim value As Integer
            value = getClockFrequency(interface_handle)
            If (value > 1) Then
                clockspeed_Hz = value
            End If
            Dim value2 As Integer
            If (getSlowIOlimits(value, value2, interface_handle) <> 0) Then
                number_of_slow_outs = value
                number_of_slow_ins = value2
            End If
            Return True
        End If
    End Function
    'CloseConnection closes the JTAG connection, but doesn't erase the data saved on the FPGA
    ' Thanks to the Finalize destructor, this sub does not need to be called at the end of the 
    ' program
    Public Overridable Sub CloseConnection()
        If (connected) Then
            closeChrisBlasterConnection(interface_handle)
            connected = False
        End If
    End Sub
    'CreateNewProtocol needs to be called before programming the protocols. Ideally, this is called whenever
    ' the user specifies a new protocol rather than waiting for the protocol to be used
    Public Overridable Function CreateNewProtocol(ByVal protocol_number As Integer, ByVal number_loops As Integer) As Boolean
        If (protocol_number > 255 Or protocol_number < 0) Then
            If (DiagnosticMode) Then
                System.Windows.Forms.MessageBox.Show("Protocol index " + protocol_number + " is too high")
                System.Windows.Forms.Application.DoEvents()
                Return False
            End If
        End If
        Dim result As Integer = allocateNewProtocol(protocol_number, number_loops, bitmasks_per_pulse, interface_handle)
        If (result = 0) Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Overridable Function CreateNewProtocol(ByVal protocol_number As Integer, ByVal number_loops As Integer, ByVal number_bitmasks As Integer) As Boolean
        If (protocol_number > 255 Or protocol_number < 0) Then
            If (DiagnosticMode) Then
                System.Windows.Forms.MessageBox.Show("Protocol index " + protocol_number + " is too high")
                System.Windows.Forms.Application.DoEvents()
                Return False
            End If
        End If
        Dim result As Integer = allocateNewProtocol(protocol_number, number_loops, number_bitmasks, interface_handle)
        If (result = 0) Then
            Return False
        Else
            Return True
        End If
    End Function
    'AddNextBitmask
    Public Overridable Function AddNextBitmask(ByVal protocol_number As Integer, ByVal loop_index As Integer, ByVal bitmask_as_int As UInteger, ByVal duration_in_seconds As Double) As Boolean
        Dim result As Integer = addBitmask(protocol_number, loop_index, bitmask_as_int, Convert.ToUInt32(duration_in_seconds * clockspeed_Hz), interface_handle)
        If (result = 0) Then
            Return False
        Else
            Return True
        End If
    End Function
    'SetPulseNumber Must be called for each loop of a protocol for it to run properly
    Public Overridable Function SetPulseNumber(ByVal protocol_number As Integer, ByVal loop_index As Integer, ByVal number_of_pulses As Integer) As Boolean
        Dim result As Integer = repeatLoop(protocol_number, loop_index, number_of_pulses, interface_handle)
        If (result = 0) Then
            Return False
        Else
            Return True
        End If
    End Function
    'SetPostProtocolBitmask tells the FPGA what the bitmask should look like after 
    ' the protocol finishes (e.g. actinic light setting). If not specified, the 
    ' resting bitmask will PROBABLY be all zeroes, but THIS FUNCTION SHOULD BE 
    ' CALLED EVERY TIME THE PROTOCOL IS CREATED TO BE SAFE BECAUSE THE FPGA DOES 
    ' NOT "REMEMBER" WHAT THE BITMASK WAS BEFRE RUNNING A PROTOCOL
    ' returns true if the operation was successful
    Public Overridable Function SetPostProtocolBitmask(ByVal protocol_number As Integer, ByVal bitmask_as_int As UInteger) As Boolean
        Dim result As Integer = setTerminalBitmask(protocol_number, bitmask_as_int, interface_handle)
        If (result = 0) Then
            Return False
        Else
            Return True
        End If
    End Function
    'ExecuteProtocol runs the protocol, waiting a maximum of timeout_time_in_s for it to execute. It 
    ' returns False on failed execution and True after a successful execution
    Public Overridable Function ExecuteProtocol(ByVal protocol_number As Integer, ByVal timeout_time_in_s As Double) As Boolean
        Dim result As Integer = startProtocol(protocol_number, interface_handle)
        If (result = 0) Then
            Return False
        End If
        Dim sleeptime As Double = 0
        Dim finished As Integer = 0
        While ((sleeptime < timeout_time_in_s) And (finished = 0))
            Sleep(100)
            sleeptime = sleeptime + 0.1
            finished = checkIfDone(interface_handle)
        End While
        If (finished = 0) Then
            Return False
        End If
        Return True
    End Function
    'StartRunningProtocol starts the execution of the indicated protocol number from the 
    ' FPGA board's memory. It returns false if the signal to start the protocol 
    ' failed to reach the board, true otherwise. THIS FUNCTION RETURNS BEFORE THE 
    ' PROTOCOL FINISHES EXECUTION
    Public Overridable Function StartRunningProtocol(ByVal protocol_number As Integer) As Boolean
        Dim result As Integer = StartProtocol(protocol_number, interface_handle)
        If (result = 0) Then
            Return False
        End If
        
        Return True
    End Function
    'ChangeCurrentBitmask changes the bitmask (the same pins used by protocols) to 
    ' a new value without running a protocol. This can be used for actions like 
    ' changing the actinic setting. THIS FUNCTION DOES NOT AUTOMATICALLY UPDATE 
    ' THE ENDING BITMASK OF PROTOCOLS, SO YOU NEED TO CALL SetPostProtocolBitmask 
    ' AS WELL IF YOU WANT THE CHANGE TO THE BITMASK TO PERSIST PAST THE NEXT 
    ' PROTOCOL
    ' returns true if the operation was successful
    Public Overridable Function ChangeCurrentBitmask(ByVal bitmask_as_int As UInteger) As Boolean
        Dim result As Integer = changeOutputBitmask(bitmask_as_int, interface_handle)
        If (result = 0) Then
            Return False
        Else
            Return True
        End If
    End Function
    'Slow I/O functions:
    'These functions read inputs or write to outputs as bytes referenced by index
    ' (first byte is byte zero)

    'ReadInputByte reads the slow input pins corresponding to the given byte 
    ' number. Note that the pins are not bidirectional (yet), so reading 
    ' byte #3 will affect different pins than writting to byte #3
    ' returns true if the operation was successful
    Public Overridable Function ReadInputByte(ByVal input_index As Integer, ByRef value As Byte) As Boolean
        Dim readi As Integer
        Dim result As Integer = slowIOreadByte(input_index, readi, interface_handle)
        If (result = 0) Then
            Return False
        Else
            If (readi >= 256) Then
                Return False
            Else
                value = readi
            End If
            Return True
        End If
    End Function
    'WriteOutputByte writes to the output pins corresponding to the 
    ' given byte number. Note that the pins are not bidirectional (yet), 
    ' so reading byte #3 will affect different pins than writting to 
    ' byte #3 
    ' returns true if the operation was successful
    Public Overridable Function WriteOutputByte(ByVal byte_address As Integer, ByVal byte_written As Integer) As Boolean
        Dim result As Integer = slowIOwriteByte(byte_address, byte_written, interface_handle)
        If (result = 0) Then
            Return False
        Else
            Return True
        End If
    End Function

    'Takes a Protocol object and programs the ChrisBlaster according to that object
    ' This function returns true on successful operation, false otherwise
    ' A standard measuring pulse has five parts: 
    '   (0. if there is a Xe flash, flash for 2 uS)
    '   1. measuring interval wait (measuring light off, trigger off, sample & hold off)
    '   2. measuring pulse (measuring light and sample&hold on for pulse duration)
    '   3. sample & hold settle time (measuring light on and sample&hold off for 10 uS)
    '   4. read time (measuring light on and trigger on for 5 uS)
    '   5. measuring light overhang to keep the ADC from reading at the same time the light pulse turns off (LEDs turn off faster than the ADC responce time and the sample&hold chip is not perfect)
    ' The time table for the data measurements are stored in time_map
    Public Overridable Function GenerateTimeMap(ByRef the_protocol As Protocol) As Double()
        'time_map is an array that corresponds to the timing of each measuring pulse. Each element in the array is the 
        ' time at which the corresponding measurement is taken
        Dim temp_time_map As Collections.Generic.List(Of Double) = New Collections.Generic.List(Of Double)(1024)
        Dim times As Double()
        Dim elapsed_time As Double = 0.0
        For nloop As Integer = 0 To the_protocol.Number_Loops - 1
            For npulse As Integer = 1 To the_protocol.Number_Pulses(nloop)
                If (the_protocol.Pre_Pulse(nloop) > 0) Then
                    If the_protocol.Pre_Delay(nloop) <> "" Then
                        elapsed_time += Protocol.TimeInSeconds(the_protocol.Pre_Delay(nloop))
                    End If
                    elapsed_time += Protocol.TimeInSeconds(the_protocol.Pre_Pulse_Time(nloop))
                End If
                If (the_protocol.Xe_Flash(nloop) > 0) Then
                    elapsed_time += Protocol.TimeInSeconds("3u")
                End If
                'bug:00001 (July 27, 2010)
                For nlight As Integer = 0 To the_protocol.Number_Measuring_Lights - 1
                    elapsed_time += Protocol.TimeInSeconds(the_protocol.Measuring_Interval(nlight, nloop)) + _
                                    Protocol.TimeInSeconds(the_protocol.Measuring_Pulse_Duration(nlight))
                    If the_protocol.Take_Data(nloop) > 0 Then
                        temp_time_map.Add(elapsed_time)
                    End If
                    elapsed_time += Protocol.TimeInSeconds("10u") + Protocol.TimeInSeconds("5u") + _
                                    Protocol.TimeInSeconds("2u")
                Next nlight
            Next npulse
        Next nloop
        ReDim times(temp_time_map.Count)
        times = temp_time_map.ToArray()
        Return times
    End Function
    Public Overridable Function ProgramProtocol(ByVal index As Integer, ByRef the_protocol As Protocol, _
                                                ByRef time_map() As Double) As Boolean

        Dim success As Boolean = True
        Dim intensity_holder As Integer
        Dim inject As Integer
        'im temp_gain As Integer
        inject = 1
        Dim test As Single
        'im takeoffset As Integer = False

        'new integrating detector has up to 4 gain settings 0-3, however detector sensitivity decreases with increasing bit value
        'to make the scripting more intuitive, scripted gain setting will be subtracted from gain_inverter
        'so that detector sensitivity will increase with the scripted gain

        Dim samp_det_gain As Integer = 0
        Dim ref_det_gain As Integer = 0
        Dim gain_inverter As Integer = 3


        'prepare the ChrisBlaster for a new protocol
        If (Not connected) Then
            ' not connected to the ChrisBlaster
            Return False
        End If                                             ' v-- was 10, but 10 might not have been enough

        'Note: not sure where index is defined. DMK
        ' I changed 50 * the_protocol.Number_Measuring_Lights ==> 20 * the_protocol.Number_Measuring_Lights
        CreateNewProtocol(index, the_protocol.Number_Loops, (20 * the_protocol.Number_Measuring_Lights)) ' 20 bitmasks per measuring light should be more than enough, as it should only need 3 + 5 * (number of lights)



        'time_map is an array that corresponds to the timing of each measuring pulse. Each element in the array is the 
        ' time at which the corresponding measurement is taken
        Dim temp_times As Collections.Generic.List(Of Double) = New Collections.Generic.List(Of Double)(1024)

        'Dim pulsepulse As Integer
        For p_loop As Integer = 0 To the_protocol.Number_Loops - 1 ' p_loop is loop number. ChrisBlaster variables are zero-indexed

            If (the_protocol.Xe_Flash(p_loop) > 0) Then
                'this is to set the infinite SH:
                success = success And AddNextBitmask(index, p_loop, _
                                       makeBitmask(intensity_holder, 0, 0, 1, _
                                                   1, the_protocol.Far_Red(p_loop), _
                                                   the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                                                0, 1), _
                                       Protocol.TimeInSeconds(the_protocol.Measuring_Interval(0, p_loop)))

                success = success And AddNextBitmask(index, p_loop, _
                                                       makeBitmask(intensity_holder, 0, the_protocol.Measuring_Light(0), _
                                                                   1, _
                                                                   1, the_protocol.Far_Red(p_loop), _
                                                                   the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                                                                1, 0), _
                                                                  Protocol.TimeInSeconds("1000u"))

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                success = success And AddNextBitmask(index, p_loop, _
                                       makeBitmask(intensity_holder, 0, 0, _
                                                   1, _
                                                  1, the_protocol.Far_Red(p_loop), _
                                                   the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                                                   0, 0), _
                                                 Protocol.TimeInSeconds("5u"))



                'success = success And AddNextBitmask(index, p_loop, _
                '                           makeBitmask(intensity_holder, 0, 0, _
                '                                       0, _
                '                                       0, the_protocol.Far_Red(p_loop), _
                '                                       the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                '                                       0, 0), _
                '                               Protocol.TimeInSeconds("2000u"))

                'success = success And AddNextBitmask(index, p_loop, _
                '                                           makeBitmask(intensity_holder, 0, 0, _
                '                                                       1, _
                '                                                       1, the_protocol.Far_Red(p_loop), _
                '                                                       the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                '                                                       0, 0), _
                '                                               Protocol.TimeInSeconds("20u"))


            End If

            ' pre-pulse (if set)
            ' Pre-pulse has two parts:
            '   1. pre-delay (normal light combination for predelay time)
            '   2. prepulse (has special light code on for prepulse time)





            'If (the_protocol.Pre_Pulse(p_loop) > 0) Then
            '    If the_protocol.Pre_Delay(p_loop) <> "" Then ' pre-delay
            '        success = success And AddNextBitmask(index, p_loop, _
            '                   makeBitmask(the_protocol.Intensity(p_loop), 0, 0, 0, 0, the_protocol.Far_Red(p_loop), _
            '                               the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
            '                               0, 0), _
            '                   Protocol.TimeInSeconds(the_protocol.Pre_Delay(p_loop)))

            '    End If
            '    'prepulse
            '    success = success And AddNextBitmask(index, p_loop, _
            '                   makeBitmask(the_protocol.Intensity(p_loop), 0, 0, 0, 0, ((the_protocol.pre_pulse_light And 4) / 4), _
            '                               ((the_protocol.pre_pulse_light And 2) / 2), (the_protocol.pre_pulse_light And 1), _
            '                               ((the_protocol.pre_pulse_light And 8) / 8), 0), _
            '                   Protocol.TimeInSeconds(the_protocol.Pre_Pulse_Time(p_loop)))

            'End If
            ' end of prepulse

            ' measuring pulse
            ' Xe flash, if present
            'If (the_protocol.Xe_Flash(p_loop) > 0) Then
            '    success = success And AddNextBitmask(index, p_loop, _
            '               makeBitmask(the_protocol.Intensity(p_loop), 0, 0, 0, _
            '                           0, the_protocol.Far_Red(p_loop), _
            '                           the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
            '                           the_protocol.Xe_Flash(p_loop), 0), _
            '               Protocol.TimeInSeconds("3u"))

            'End If




            'this is where the measuring pulses go &&&
            For p_light As Integer = 0 To the_protocol.Number_Measuring_Lights - 1  'goes through each measuing light 

                'added gain settings 0-3. The gains have been bit shifted since the first bit from previous versions was converted to an I/H bit
                'gain inverter is used so that the highest scripted gain (gain 3) will set the detector at the highest sensitivity.

                'The following ensures that the gains are <=3, which is critical becaue higher values will overwrite 
                ' other functional bits

                the_protocol.Primary_Gain(p_light) = the_protocol.Primary_Gain(p_light) And 3 'ensures that the gains are <=3
                the_protocol.Reference_Gain(p_light) = the_protocol.Reference_Gain(p_light) And 3 'ensures that the gains are <=3

                samp_det_gain = (gain_inverter - the_protocol.Primary_Gain(p_light)) * 2
                ref_det_gain = (gain_inverter - the_protocol.Reference_Gain(p_light)) * 2

                If the_protocol.Take_Data(p_loop) > 0 Then
                    If the_protocol.choke_actinic(p_light) = 1 Then
                        intensity_holder = 0
                    Else
                        intensity_holder = the_protocol.Intensity(p_loop)
                    End If
                    ' measuring interval


                    If inject = 1 Then  ' this is 
                        success = success And AddNextBitmask(index, p_loop, _
                                   makeBitmask(intensity_holder, 0, 0, samp_det_gain, _
                                               0, the_protocol.Far_Red(p_loop), _
                                               0, the_protocol.Blue_Actinic(p_loop), _
                                            0, 1), _
                                   Protocol.TimeInSeconds(the_protocol.Measuring_Interval(p_light, p_loop)))
                        Dim test2 As Single = Protocol.TimeInSeconds(the_protocol.Measuring_Interval(p_light, p_loop))
                        test = test2

                    Else
                        'success = success And AddNextBitmask(index, p_loop, _
                        '          makeBitmask(intensity_holder, 0, 0, samp_det_gain, _
                        '                      ref_det_gain, the_protocol.Far_Red(p_loop), _
                        '                      the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                        '                      0, 0), _
                        '          Protocol.TimeInSeconds(the_protocol.Measuring_Interval(p_light, p_loop)))

                    End If
                    test = Protocol.TimeInSeconds(the_protocol.Measuring_Pulse_Duration(p_light))
                    ' measuring pulse; note that the first bit of each gain is used for activating the integrate and hold.
                    ' for cleaner code, the integrate and holds for each detector should be given their own fields.

                    Dim temp_blue As Integer
                    If inject = 1 Then
                        If the_protocol.m_wasp(p_light) = 1 Then  ' this bit of code allows one to add WQASP pulses that occur simultaneously with the measuring pulse
                            temp_blue = 1
                        Else
                            temp_blue = the_protocol.Blue_Actinic(p_loop)
                        End If
                        success = success And AddNextBitmask(index, p_loop, _
                                               makeBitmask(intensity_holder, 0, the_protocol.Measuring_Light(p_light), _
                                                           samp_det_gain + 1, _
                                                           ref_det_gain + 1, the_protocol.Far_Red(p_loop), _
                                                           the_protocol.Saturating_Pulse(p_loop), temp_blue, _
                                                        0, 0), _
                                                         Protocol.TimeInSeconds(the_protocol.Measuring_Pulse_Duration(p_light)))

                        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        ' here, the measuring pulse goes away as well as switching off WASP pulses

                        success = success And AddNextBitmask(index, p_loop, _
                                               makeBitmask(intensity_holder, 0, 0, _
                                                           samp_det_gain + 1, _
                                                           ref_det_gain + 1, the_protocol.Far_Red(p_loop), _
                                                           the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                                                           0, 0), _
                                                         Protocol.TimeInSeconds("5u"))


                    Else

                        'success = success And AddNextBitmask(index, p_loop, _
                        '           makeBitmask(intensity_holder, 0, the_protocol.Measuring_Light(p_light), _
                        '                       samp_det_gain, _
                        '                       ref_det_gain, the_protocol.Far_Red(p_loop), _
                        '                       the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                        '                       0, 1), _
                        '           Protocol.TimeInSeconds(the_protocol.Measuring_Pulse_Duration(p_light)))
                    End If

                    ' sample & hold settle
                    If inject = 1 Then

                        success = success And AddNextBitmask(index, p_loop, _
                            makeBitmask(intensity_holder, 0, 0, _
                            samp_det_gain, _
                            ref_det_gain, the_protocol.Far_Red(p_loop), _
                            the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                            0, 0), _
                            Protocol.TimeInSeconds("10u")) 'changed from 30u to test for artifacts



                    Else

                        'success = success And AddNextBitmask(index, p_loop, _
                        '       makeBitmask(intensity_holder, 0, the_protocol.Measuring_Light(p_light), _
                        '                   samp_det_gain, _
                        '                   ref_det_gain, the_protocol.Far_Red(p_loop), _
                        '                   the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                        '                   0, 0), _
                        '       Protocol.TimeInSeconds("10u"))
                    End If

                    If inject = 1 Then
                        ' ADC read time
                        success = success And AddNextBitmask(index, p_loop, _
                                   makeBitmask(intensity_holder, 1, 0, _
                                               samp_det_gain, _
                                               ref_det_gain, the_protocol.Far_Red(p_loop), _
                                               the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                                            0, 0), _
                                   Protocol.TimeInSeconds("100u")) 'chagned from 2u to test for artifacts



                    Else
                        'success = success And AddNextBitmask(index, p_loop, _
                        'makeBitmask(intensity_holder, 1, the_protocol.Measuring_Light(p_light), _
                        '                       samp_det_gain, _
                        '                       ref_det_gain, the_protocol.Far_Red(p_loop), _
                        '                       the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                        '                       0, 0), _
                        '           Protocol.TimeInSeconds("50u"))
                    End If



                    ' measuring light overhang
                    If inject = 1 Then
                        success = success And AddNextBitmask(index, p_loop, _
                                   makeBitmask(intensity_holder, 0, 0, _
                                               samp_det_gain, _
                                               ref_det_gain, the_protocol.Far_Red(p_loop), _
                                               the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                                               0, 0), _
                                   Protocol.TimeInSeconds("50u"))
                        success = success And AddNextBitmask(index, p_loop, _
           makeBitmask(intensity_holder, 0, 0, _
                       samp_det_gain, _
                       ref_det_gain, the_protocol.Far_Red(p_loop), _
                       the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                       0, 1), _
           Protocol.TimeInSeconds("5u"))
                    Else
                        'the_protocol.Primary_Gain(p_light) = temp_gain
                        'success = success And AddNextBitmask(index, p_loop, _
                        '           makeBitmask(intensity_holder, 0, the_protocol.Measuring_Light(p_light), _
                        '                       samp_det_gain, _
                        '                       ref_det_gain, the_protocol.Far_Red(p_loop), _
                        '                       the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                        '                       0, 0), _
                        '           Protocol.TimeInSeconds("2u"))
                    End If

                Else ' not taking data, so the measuring light and trigger do not fire
                    ' measureing interval
                    success = success And AddNextBitmask(index, p_loop, _
                               makeBitmask(intensity_holder, 0, 0, samp_det_gain, _
                                           ref_det_gain, the_protocol.Far_Red(p_loop), _
                                           the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                                           0, 0), _
                               Protocol.TimeInSeconds(the_protocol.Measuring_Interval(p_light, p_loop)))

                    ' measuring pulse
                    success = success And AddNextBitmask(index, p_loop, _
                               makeBitmask(intensity_holder, 0, 0, _
                                           samp_det_gain, _
                                           ref_det_gain, the_protocol.Far_Red(p_loop), _
                                           the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                                           0, 0), _
                               Protocol.TimeInSeconds(the_protocol.Measuring_Pulse_Duration(p_light)))

                    ' sample & hold settle
                    success = success And AddNextBitmask(index, p_loop, _
                               makeBitmask(intensity_holder, 0, 0, _
                                           samp_det_gain, _
                                           ref_det_gain, the_protocol.Far_Red(p_loop), _
                                           the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                                           0, 0), _
                               Protocol.TimeInSeconds("10u"))

                    ' ADC read time
                    success = success And AddNextBitmask(index, p_loop, _
                               makeBitmask(intensity_holder, 0, 0, _
                                           samp_det_gain, _
                                           ref_det_gain, the_protocol.Far_Red(p_loop), _
                                           the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                                           0, 0), _
                               Protocol.TimeInSeconds("5u"))

                    ' measuring light overhang
                    success = success And AddNextBitmask(index, p_loop, _
                               makeBitmask(intensity_holder, 0, 0, _
                                           samp_det_gain, _
                                           ref_det_gain, the_protocol.Far_Red(p_loop), _
                                           the_protocol.Saturating_Pulse(p_loop), the_protocol.Blue_Actinic(p_loop), _
                                           0, 0), _
                               Protocol.TimeInSeconds("2u"))

                End If

            Next p_light
            ' set the number of times to repeat the loop
            success = success And SetPulseNumber(index, p_loop, the_protocol.Number_Pulses(p_loop))
        Next p_loop
        ' set the bitmask to be held when the protocol finishes (only actinic, farred, and blue are one, chosen from last loop
        success = success And SetPostProtocolBitmask(index, makeBitmask(the_protocol.Intensity(the_protocol.Number_Loops - 1), _
                                                  0, 0, 1, 1, the_protocol.Far_Red(the_protocol.Number_Loops - 1), _
                                                  0, the_protocol.Blue_Actinic(the_protocol.Number_Loops - 1), _
                                                  0, 0))

        '        'time_map is an array that corresponds to the timing of each measuring pulse. Each element in the array is the 
        '        ' time at which the corresponding measurement is taken
        '        Dim elapsed_time As Double = 0.0
        '        For nloop As Integer = 0 To the_protocol.Number_Loops - 1
        '            For npulse As Integer = 1 To the_protocol.Number_Pulses(nloop)
        '                If (the_protocol.Pre_Pulse(nloop) > 0) Then
        '                    If the_protocol.Pre_Delay(nloop) <> "" Then
        '                        elapsed_time += Protocol.TimeInSeconds(the_protocol.Pre_Delay(nloop))
        '                    End If
        '                    elapsed_time += Protocol.TimeInSeconds(the_protocol.Pre_Pulse_Time(nloop))
        '                End If
        '                If (the_protocol.Xe_Flash(nloop) > 0) Then
        '                    elapsed_time += Protocol.TimeInSeconds("3u")
        '                End If
        ''bug:00001 (July 27, 2010)
        '                For nlight As Integer = 0 To the_protocol.Number_Measuring_Lights - 1
        '                    elapsed_time += Protocol.TimeInSeconds(the_protocol.Measuring_Interval(nlight, nloop)) + _
        '                                    Protocol.TimeInSeconds(the_protocol.Measuring_Pulse_Duration)
        '                    If the_protocol.Take_Data(nloop) > 0 Then
        '                        temp_time_map.Add(elapsed_time)
        '                    End If
        '                    elapsed_time += Protocol.TimeInSeconds("10u") + Protocol.TimeInSeconds("5u") + _
        '                                    Protocol.TimeInSeconds("2u")
        '                Next nlight
        '            Next npulse
        '        Next nloop
        '        ReDim time_map(temp_time_map.Count)
        ReDim time_map(0)
        time_map = GenerateTimeMap(the_protocol)

        Return success
    End Function

    ' This utility function makes a bitmask for the ChrisBlaster out of a set of protocol properties
    Public Overridable Function makeBitmask(ByVal actinic_intensity As Short, ByVal ADC_trigger As Integer, _
                                            ByVal measuring_light As Short, ByVal sample_gain As Short, _
                                            ByVal reference_gain As Short, ByVal farred As Short, _
                                            ByVal saturation_pulse As Short, ByVal blue_actinic As Short, _
                                            ByVal flash As Short, ByVal sample_n_hold As Integer) As UInteger
        ' the bitmask is 32 bits, represented as 4 bytes
        Dim byte0 As Byte = 0
        Dim byte1 As Byte = 0
        Dim byte2 As Byte = 0
        Dim byte3 As Byte = 0
        ' byte0 is the actinic intensity
        byte0 = actinic_intensity Mod 256
        'byte1:
        '       1 ==> ADC trigger
        '       2,4,8,16 ==> measuring light
        '       32,64,128 ==> sample gain

        ' dddccccb (little endian) where b = adc trigger, c = measuring light, and d = sample gain
        byte1 = &H0
        If (ADC_trigger > 0) Then
            byte1 = byte1 + &H1 ' add the trigger
        End If
        byte1 = byte1 + ((measuring_light Mod 16) * 2) ' add the measuring light

        byte1 = byte1 + ((sample_gain Mod 8) * 32)
        'byte2: 1,2,4 ==> reference_gain
        '       8==> far_red
        '       16==> saturation_pulse
        '       32==> blue_actinic
        '       64==> flash
        '       128==> sample_n_hold

        'byte2 = mkhgfeee (little endian) e = reference gain, f = farred, g = saturation, h = blue, k = Xe flash/bit22, m = sample&hold
        byte2 = byte2 + (reference_gain Mod 8)
        If (farred > 0) Then
            byte2 = byte2 + 8
        End If
        If (saturation_pulse > 0 Or (actinic_intensity And 256) = 256) Then
            byte2 = byte2 + 16
        End If
        If (blue_actinic > 0) Then
            byte2 = byte2 + 32
        End If
        If (flash > 0) Then
            byte2 = byte2 + 64
        End If
        If (sample_n_hold > 0) Then
            byte2 = byte2 + 128
        End If
        ' byte3 is note yet used for anything
        Dim total As UInt32 = byte0 + (byte1 * &H100) + (byte2 * &H10000) + (byte3 * &H1000000)

        Return total
    End Function

End Class
