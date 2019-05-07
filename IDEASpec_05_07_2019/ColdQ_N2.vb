Option Strict Off
Option Explicit On
'UPGRADE_NOTE: global was upgraded to global_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
Module global_Renamed
    Public Note_File_Name As String
    Public text_changed As Integer
    Public note_text As String
    Public offset As Single
    Public wait_for_notes As Boolean
    Public Trace_Running As Short
    'dmk global.
    Public Bug_Out As Short
    Public No_Plot_Data As Short
    Public Const File_Replace As Short = 0
    Public Const Average_Into As Short = 1

    Public Const Sequential As Short = 3
    Public Const File_Append As Short = 2
    Public script_file As String
    Public Intensity_Pass As Short
    Public Do_Intensity As Short
    Public Intensity_done As Short
    'Public Far_Red As Short
    'Public Blue_Actinic As Short

    Public Numer_of_loops As Short
    Public Data_Volts(,) As Single = {{2000, 8}}
    Public Ref_Data_Volts() As Single
    'Public Raw_Data() As Short
    Public data_time() As Single
    Public Const EndLoop As Short = 3
    Public Const BegLoop As Short = 2
    Public End_Loop_Code As Short
    Public Blue_Filter As Short
    Public IR_Filter As Short
    Public Time_Interval_in_Seconds As Single
    Public Start_Time As Single
    Public Zero_Time As Single
    Public First_Fluorescence_trace As Short
    Public ref_channel As Integer
    'Public Xe_flash As Integer

    Public MemHandle As Integer
    Public totres(3) As Single 'total resistance
    Public jp As Single 'op code
    Public SERVOset As Short 'SERVO VARIABLE
    Public INsecs() As Single 'keeps track of time
    Public zzz As Short
    Public itime As Short
    Public tnumm As Single 'total seconds of illumination
    Public numm As Single 'number of entered pulses
    Public minterval As String 'interval between measuring pulses
    Public LEDS() As Single 'bit configuration in hex

    Public TRIG As Short 'bit configuration for trigger
    Public MEAS As Short 'bit configuration for measuring pulses
    Public SATP As Short 'bit configuration for sat pulse only
    Public SATM As Short 'bit configuration for sat pulse + measuring pulse
    Public SATT As Short 'bit config for sat pulse + measuring pulse + trigger

    Public FileName As String 'name and dir of file
    Public FileNameBase As String 'name and dir of saved baseline file
    Public numsat As Short 'current number of dirk iterations
    Public SAVEemALL As Boolean 'directions for saving as one or multiple files
    Public dirknum As Short 'number of satpulses and dirk traces done during fluorescence
    Public OpenScript As String 'open notepad script file
    Public countt As Short 'keeps track of PB line numbers
    Public mnum_puls As Single 'entered # of measuring pulses
    Public wait_line(10) As Single
    Public act_line(10) As Single
    Public sat_line(10) As Single
    Public meas_line(10) As Single
    Public trig_line(10) As Single
    Public end_line(10) As Single
    Public txt As String 'line input variable when opening script files
    Public NumPoints As Integer ' Number of data points to collect
    Public baselinepoints, points, preillumpoints As Integer 'calculated num of points
    Public seconds As Single
    Public trigg As Single 'pause between measurement pulses
    Public NAV, MaxAV As Short 'averages of data
    Public parsinGTime As Single 'parses out the timing for the preillumination timing
    Public txxt As String 'redefines variables GETVARIBALES
    Public stoppall As Boolean 'stopps all datacollection

    Public firsttemp As String 'beginning temperature
    Public lasttemp As String 'final temperature
    Public plantcondition As String 'plant condition prior to experiment


    Public CURTIME As Object
    Public milliseconds As Object
    Public nuMcol As Short 'counts the number of columns needed for saving
    Public carrYpoint As Short 'holds last data point collected to next script
    Public dTIME As Single 'keeps track and saves the time of each data point
    'MsgBox "damn it, jim"

    Public BaseAddress As Short = 0
    Public Freq As Single
    Public pulselen As Single
    Public StdDelay As Single
    Public ptb0 As Short
    Public ptb1 As Short
    Public ptb2 As Short
    Public ptb3 As Short
    'Global np1 As Single
    'Global np0 As Single
    Public actcount As Short 'simply counts how many actinic intensities there are and files them in a matrix

    Public multiplier As Single
    Public cycles As Single
    Public act_int() As Integer

    Public pevery, variname, vari, hightime, resistance As String
    Public waitt() As String

    Public TAG520 As Boolean 'letting computer know your asking for 520 script
    Public TAG820 As Boolean ' "
    Public ORDER820 As String 'determining the order of wavelength collection
    Public ORDER520 As String ' "

    'DIRK variables
    Public DIRK As Boolean 'indicated the DIRK data will be collected
    Public NowDK As Boolean 'keeps track of which script is running
    Public satur, act_zero, act_one, measur As String 'variable in opening text script
    Public filterchange, act_on, intillum, baseground, illumat, act0, act_off, average, act_two As String 'script entered variables
    Public filterchange520, act_on520, intillum520, baseground520, illumat520, act0520, act_off520, average520, act_two520 As String 'script entered variables
    Public filterchange820, act_on820, intillum820, baseground820, illumat820, act0820, act_off820, average820, act_two820 As String 'script entered variables
    Public baseline As String
    Public recovery, illumint, darkint, averag As String 'PB script variables
    Public DnumPoints As Integer 'number of DIRK points to collect
    Public DKtime As Single 'time for each dirk period
    Public DKtAVG As Single 'total time incured thru total averages
    Public TTtime As Single 'total time in seconds
    Public ILtime As Single 'illumination time

    'fluorescence variables
    Public fluores As Boolean 'indicated the fluoresence data will be collected
    Public FBase As String 'dark interval, no sat, no actinic
    Public SatDur As String 'duration of sat pulse
    Public SatFreq As String 'sat pulse frequency
    Public ActDuration As String 'length of actinic period (with periodic saturating flashes every SatFreq
    Public FnumPoints, fpoints As Integer 'determine number of fluorescent points to collect
    Public fint As String 'separate measuring pulse interval for fluorescence
    Public SSDuration As String 'length of time that a saturating pulse occurs along with DIRK
    Public SSFluor As Boolean 'indicated that a steady state fluoro trace with DIRK wishes to be run
    Public NowFL As Boolean 'keeps track of which script is running
    Public NowSS As Boolean ' ""
    Public NowDI As Boolean 'dark interval
    Public spoints As Integer 'numpoints during steady state trace
    Public SatRecov As String 'recovery time after saturation pulse and before DIRK
    Public FLtime As Single 'time for first fluorescence period
    Public SStime As Single 'time for each fluorescence period after
    Public timethru As Short 'number of loops during steady state
    Public FmRecov As String 'recover after Fm signal
    Public dkpoints As Short 'number of points collected during dark interval
End Module