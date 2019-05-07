Option Strict Off
Option Explicit On 
Imports System.Runtime.InteropServices
Module Module1
    '
    ' Standard Windows API stuff

    Public Structure BITMAPAPI
        Dim bmType As Integer
        Dim bmWidth As Integer
        Dim bmHeight As Integer
        Dim bmWidthBytes As Integer
        Dim bmPlanes As Short
        Dim bmBitsPixel As Short
        Dim bmBits As Integer
    End Structure
    Public Structure DOCINFO
        Dim cbSize As Integer
        Dim lpszDocName As String
        Dim lpszOutput As String
    End Structure
    Public Structure PRINTDLGSTRUCT
        Dim lStructSize As Integer
        Dim hwndOwner As Integer
        Dim hDevMode As Integer
        Dim hDevNames As Integer
        Dim hdc As Integer
        Dim flags As Integer
        Dim nFromPage As Short
        Dim nToPage As Short
        Dim nMinPage As Short
        Dim nMaxPage As Short
        Dim nCopies As Short
        Dim hInstance As Integer
        Dim lCustData As Integer
        Dim lpfnPrintHook As Integer
        Dim lpfnSetupHook As Integer
        Dim lpPrintTemplateName As String
        Dim lpSetupTemplateName As String
        Dim hPrintTemplate As Integer
        Dim hSetupTemplate As Integer
    End Structure
    Public Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure

    Public Const SRCCOPY As Integer = &HCC0020           ' (DWORD) dest = source

    '  Device Parameters for GetDeviceCaps()
    Public Const DRIVERVERSION As Integer = 0      '  Device driver version
    Public Const TECHNOLOGY As Integer = 2         '  Device classification
    Public Const HORZSIZE As Integer = 4           '  Horizontal size in millimeters
    Public Const VERTSIZE As Integer = 6           '  Vertical size in millimeters
    Public Const HORZRES As Integer = 8            '  Horizontal width in pixels
    Public Const VERTRES As Integer = 10           '  Vertical width in pixels
    Public Const BITSPIXEL As Integer = 12         '  Number of bits per pixel
    Public Const PLANES As Integer = 14            '  Number of planes
    Public Const NUMBRUSHES As Integer = 16        '  Number of brushes the device has
    Public Const NUMPENS As Integer = 18           '  Number of pens the device has
    Public Const NUMMARKERS As Integer = 20        '  Number of markers the device has
    Public Const NUMFONTS As Integer = 22          '  Number of fonts the device has
    Public Const NUMCOLORS As Integer = 24         '  Number of colors the device supports
    Public Const PDEVICESIZE As Integer = 26       '  Size required for device descriptor
    Public Const CURVECAPS As Integer = 28         '  Curve capabilities
    Public Const LINECAPS As Integer = 30          '  Line capabilities
    Public Const POLYGONALCAPS As Integer = 32     '  Polygonal capabilities
    Public Const TEXTCAPS As Integer = 34          '  Text capabilities
    Public Const CLIPCAPS As Integer = 36          '  Clipping capabilities
    Public Const RASTERCAPS As Integer = 38        '  Bitblt capabilities
    Public Const ASPECTX As Integer = 40           '  Length of the X leg
    Public Const ASPECTY As Integer = 42           '  Length of the Y leg
    Public Const ASPECTXY As Integer = 44          '  Length of the hypotenuse
    Public Const LOGPIXELSX As Integer = 88        '  Logical pixels/inch in X
    Public Const LOGPIXELSY As Integer = 90        '  Logical pixels/inch in Y
    Public Const SIZEPALETTE As Integer = 104      '  Number of entries in physical palette
    Public Const NUMRESERVED As Integer = 106      '  Number of reserved entries in palette
    Public Const COLORRES As Integer = 108         '  Actual color resolution

    '  Printing related DeviceCaps. These replace the appropriate Escapes
    Public Const PHYSICALWIDTH As Integer = 110     '  Physical Width in device units
    Public Const PHYSICALHEIGHT As Integer = 111    '  Physical Height in device units
    Public Const PHYSICALOFFSETX As Integer = 112   '  Physical Printable Area x margin
    Public Const PHYSICALOFFSETY As Integer = 113   '  Physical Printable Area y margin
    Public Const SCALINGFACTORX As Integer = 114    '  Scaling factor x
    Public Const SCALINGFACTORY As Integer = 115    '  Scaling factor y

    Public Const PD_ALLPAGES As Integer = &H0
    Public Const PD_SELECTION As Integer = &H1
    Public Const PD_PAGENUMS As Integer = &H2
    Public Const PD_NOSELECTION As Integer = &H4
    Public Const PD_NOPAGENUMS As Integer = &H8
    Public Const PD_COLLATE As Integer = &H10
    Public Const PD_PRINTTOFILE As Integer = &H20
    Public Const PD_PRINTSETUP As Integer = &H40
    Public Const PD_NOWARNING As Integer = &H80
    Public Const PD_RETURNDC As Integer = &H100
    Public Const PD_RETURNIC As Integer = &H200
    Public Const PD_RETURNDEFAULT As Integer = &H400
    Public Const PD_SHOWHELP As Integer = &H800
    Public Const PD_ENABLEPRINTHOOK As Integer = &H1000
    Public Const PD_ENABLESETUPHOOK As Integer = &H2000
    Public Const PD_ENABLEPRINTTEMPLATE As Integer = &H4000
    Public Const PD_ENABLESETUPTEMPLATE As Integer = &H8000
    Public Const PD_ENABLEPRINTTEMPLATEHANDLE As Integer = &H10000
    Public Const PD_ENABLESETUPTEMPLATEHANDLE As Integer = &H20000
    Public Const PD_USEDEVMODECOPIES As Integer = &H40000
    Public Const PD_USEDEVMODECOPIESANDCOLLATE As Integer = &H40000
    Public Const PD_DISABLEPRINTTOFILE As Integer = &H80000
    Public Const PD_HIDEPRINTTOFILE As Integer = &H100000
    Public Const PD_NONETWORKBUTTON As Integer = &H200000

    Declare Function BitBlt Lib "gdi32" (ByVal hDestDC As Integer, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hSrcDC As Integer, ByVal xSrc As Integer, ByVal ySrc As Integer, ByVal dwRop As Integer) As Integer
    Declare Function BringWindowToTop Lib "user32" (ByVal hwnd As Integer) As Integer
    Declare Function CloseClipboard Lib "user32" () As Integer
    Declare Function CopyEnhMetaFile Lib "gdi32" Alias "CopyEnhMetaFileA" (ByVal hemfSrc As Integer, ByVal lpszFile As String) As Integer
    Declare Function CreateCompatibleDC Lib "gdi32" (ByVal hdc As Integer) As Integer
    Declare Function DeleteDC Lib "gdi32" (ByVal hdc As Integer) As Integer
    Declare Function DeleteEnhMetaFile Lib "gdi32" (ByVal hemf As Integer) As Integer
    Declare Function DeleteObject Lib "gdi32" (ByVal hObject As Integer) As Integer
    Declare Function EmptyClipboard Lib "user32" () As Integer
    Declare Function EnableWindow Lib "user32" (ByVal hwnd As Integer, ByVal fEnable As Integer) As Integer
    Declare Function EndDoc Lib "gdi32" (ByVal hdc As Integer) As Integer
    Declare Function EndPage Lib "gdi32" (ByVal hdc As Integer) As Integer
    Declare Function FillRect Lib "user32" (ByVal hdc As Integer, ByRef rc As RECT, ByVal hBrush As Integer) As Integer
    Declare Function FreeLibrary Lib "kernel32" (ByVal hLib As IntPtr) As Long
    Declare Function GetClientRect Lib "user32" (ByVal hwnd As Integer, ByRef Rect As RECT) As Integer
    Declare Function GetDC Lib "user32" (ByVal hwnd As Integer) As Integer
    Declare Function GetDeviceCaps Lib "gdi32" (ByVal hdc As Integer, ByVal nIndex As Integer) As Integer
    Declare Function GetObjectBM Lib "gdi32" Alias "GetObjectA" (ByVal hObject As Integer, ByVal nCount As Integer, ByRef bm As BITMAPAPI) As Integer
    Declare Function GetStockObject Lib "gdi32" (ByVal nIndex As Integer) As Integer
    Declare Function GetWindowRect Lib "user32" (ByVal hwnd As Integer, ByRef rc As RECT) As Integer
    Declare Function GlobalFree Lib "kernel32" (ByVal hMem As Integer) As Integer
    Declare Function IsIconic Lib "user32" (ByVal hwnd As Integer) As Integer
    Declare Function IsZoomed Lib "user32" (ByVal hwnd As Integer) As Integer
    Declare Function IsWindow Lib "user32" (ByVal hwnd As Integer) As Integer
    Declare Function LoadLibrary Lib "kernel32" Alias "LoadLibraryA" (ByVal lpLibFileName As String) As IntPtr
    Declare Function MoveWindow Lib "user32" (ByVal hwnd As Integer, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal bRepaint As Integer) As Integer
    Declare Function OpenClipboard Lib "user32" (ByVal hwnd As Integer) As Integer
    Declare Function PlayEnhMetaFile Lib "gdi32" (ByVal hdc As Integer, ByVal hemf As Integer, ByRef rc As RECT) As Integer
    Declare Function PrintDlg Lib "comdlg32.dll" Alias "PrintDlgA" (ByRef pPrintdlg As PRINTDLGSTRUCT) As Integer
    Declare Function ReleaseDC Lib "user32" (ByVal hwnd As Integer, ByVal hdc As Integer) As Integer
    Declare Function SelectObject Lib "gdi32" (ByVal hdc As Integer, ByVal hObject As Integer) As Integer
    Declare Function SetClipboardData Lib "user32" (ByVal wFormat As Integer, ByVal hMem As Integer) As Integer
    Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Integer, ByVal hWndInsertAfter As Integer, ByVal x As Integer, ByVal y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
    Declare Function ShowWindow Lib "user32" (ByVal hwnd As Integer, ByVal nCmdShow As Integer) As Integer
    Declare Function StartDoc Lib "gdi32" Alias "StartDocA" (ByVal hdc As Integer, ByRef di As DOCINFO) As Integer
    Declare Function StartPage Lib "gdi32" (ByVal hdc As Integer) As Integer
    Declare Function ValidateRect Lib "user32" (ByVal hwnd As Integer, ByRef Rect As RECT) As Integer
    '
    ' DPlot-specific stuff
    '
    Public Const DPLOT_DDE_VERSION As Short = 3 ' Version 3 structure allows up to 100 data sets per plot

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> Structure DPLOT
        Dim Version As Integer      ' version number of this structure, should be set by caller to DPLOT_DDE_VERSION
        Dim hwnd As Integer         ' handle of client application window
        Dim DataFormat As Integer   ' XY pairs, DX and Y, etc. See DATA_ constants
        Dim MaxCurves As Integer    ' DataFormat    Description
        '                             DATA_XYXY     Max. number of curves, <= 100
        '                             DATA_DXY      ..
        '                             DATA_XYYY     ..
        '                             DATA_3D       Number of grid cells in X direction,
        '                                           = number of data columns-1
        '                             DATA_3DR      not used
        '                             DATA_1D       Max. number of groups, <= 100
        '                             DATA_3DS      Max. number of data sets, <= 100
        Dim MaxPoints As Integer    ' DATA_XYXY     Max. number of points/curve
        '                             DATA_DXY      ..
        '                             DATA_XYYY     ..
        '                             DATA_3D       Number of grid cells in Y direction,
        '                                           = number of data rows-1
        '                             DATA_3DR      Number of X,Y,Z triplets
        '                             DATA_1D       Max. number of points/group
        '                             DATA_3DS      Max. number of points/data set
        Dim NumCurves As Integer    ' Actual number of curves/groups, always 1 for
        '                             DATA_3D or DATA_3DR
        Dim ScaleCode As Integer    ' scaling code for XY plots (Linear, Log, etc.)
        Dim LegendX As Single       ' left coord of legend, expressed as a ratio
        '                             of plot size (0->1) (CAN be negative and/or > 1,
        '                             but exact placement is then a bit hard to predict)
        Dim LegendY As Single       ' top coord of legend, again expressed as a ratio
        '                             of plot size
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=100)> Public NP() As Integer
        '                             Actual number of points in each curve/group. Not used for DATA_3D.
        '                             Cannot exceed MaxPoints.
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=100)> Public LineType() As Integer     ' Line types (see codes below)
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=100)> Public SymbolType() As Integer   ' Symbol types (see codes below)
        Dim SizeofExtraInfo As Integer
        '                             Extra information following X,Y data. No need to
        '                             fill in this member for DPLOTLIB calls
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=8080)> Public Legend As String
        '                             Legend(0->79) is the caption for the legend
        '                             Legend(n*80->n*80+79) is the caption for the n'th curve
        '                             Ideally this would be dimensioned as Legend(0 to 100) as String*80,
        '                             but I don't see a way to do that. Same problem exists for the Label array (next).
        '                             Fortunately we can get around this mess with individual DPlot commands.
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4000)> Public Label As String
        '                             Strings displayed beside the last data point
        '                             in a curve, 40 characters per curve
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> Public Title1 As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> Public Title2 As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> Public Title3 As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> Public XAxis As String ' X Axis label.
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> Public YAxis As String ' Y Axis label.
        Public Sub Initialize()
            ReDim NP(99)
            ReDim LineType(99)
            ReDim SymbolType(99)
        End Sub
    End Structure
   
    Structure DPLOT_PLOTMETRICSEX
        Dim size As Integer ' size of this structure, should be filled in by the caller
        Dim hll As Integer ' horizontal and
        Dim vll As Integer '  vertical coordinates of the lower left corner of the plot, in pixels
        Dim hur As Integer ' horizontal and
        Dim vur As Integer '  vertical coordinates of the upper right corner of the plot, in pixels
        Dim xlo As Single ' value of x at the left plot extent
        Dim ylo As Single ' value of y at the bottom plot extent
        Dim xhi As Single ' value of x at the right plot extent
        Dim yhi As Single ' value of z at the top plot extent
        Dim stretch_x As Single
        Dim stretch_y As Single
        Dim stretch_z As Single
        Dim scale As Single
        Dim azimuth As Single
        Dim elevation As Single
    End Structure

    Declare Function DPlot_3DBorder Lib "dplotlib" (ByVal DocNum As Integer, ByVal NumPoints As Integer, ByRef Border As Double) As Integer
    Declare Function DPlot_AddData Lib "dplotlib" (ByVal DocNum As Integer, ByVal DataType As Integer, ByVal NumPts As Integer, ByVal Curve As Integer, ByRef x As Single, ByRef y As Single) As Integer
    Declare Function DPlot_Command Lib "dplotlib" (ByVal DocNum As Integer, ByVal Command_Renamed As String) As Integer
    Declare Function DPlot_GetBitmap Lib "dplotlib" (ByVal DocNum As Integer, ByVal cx As Integer, ByVal cy As Integer) As IntPtr
    Declare Function DPlot_GetBitmapEx Lib "dplotlib" (ByVal DocNum As Integer, ByVal cx As Integer, ByVal cy As Integer, ByRef DPM As DPLOT_PLOTMETRICSEX) As IntPtr
    Declare Function DPlot_GetEnhMetaFile Lib "dplotlib" (ByVal DocNum As Integer, ByVal cx As Single, ByVal cy As Single) As Integer
    Declare Function DPlot_GetVersion Lib "dplotlib" (ByVal Version As String) As Integer
    Declare Function DPlot_MinVersion Lib "dplotlib" (ByVal digit1 As Integer, ByVal digit2 As Integer, ByVal digit3 As Integer, ByVal digit4 As Integer) As Integer
    Declare Function DPlot_Plot Lib "dplotlib" (ByRef d As DPLOT, ByRef x As Single, ByRef y As Single, ByVal cmds As String) As Integer
    Declare Function DPlot_Plot8 Lib "dplotlib" (ByRef d As DPLOT, ByRef x As Double, ByRef y As Double, ByVal cmds As String) As Integer
    Declare Function DPlot_PlotBitmap Lib "dplotlib" (ByRef d As DPLOT, ByRef x As Single, ByRef y As Single, ByVal cmds As String, ByVal cx As Integer, ByVal cy As Integer) As Integer
    Declare Function DPlot_PlotBitmap8 Lib "dplotlib" (ByRef d As DPLOT, ByRef x As Double, ByRef y As Double, ByVal cmds As String, ByVal cx As Integer, ByVal cy As Integer) As Integer
    Declare Function DPlot_PlotToRect Lib "dplotlib" (ByRef d As DPLOT, ByRef x As Single, ByRef y As Single, ByVal cmds As String, ByVal hwnd As IntPtr, ByRef rect As RECT) As Integer
    Declare Function DPlot_PlotToRect8 Lib "dplotlib" (ByRef d As DPLOT, ByRef x As Double, ByRef y As Double, ByVal cmds As String, ByVal hwnd As IntPtr, ByRef rect As RECT) As Integer
    Declare Function DPlot_Request Lib "dplotlib" (ByVal DocNum As Integer, ByVal Item As String, ByVal Value As String, ByRef MaxCount As Integer) As Integer
    Declare Sub DPlot_SetErrorMethod Lib "dplotlib" (ByVal method As Integer)
    Declare Function DPlot_Start Lib "dplotlib" (ByVal Minimize As Integer, ByRef WasActive As Integer) As Integer
    Declare Sub DPlot_Stop Lib "dplotlib" ()
    Declare Function DPlot_ZFromXY Lib "dplotlib" (ByVal DocNum As Integer, ByVal X As Double, ByVal Y As Double, ByRef Z As Double) As Integer

    ' XY Scaling codes:

    Public Const SCALE_LINEARX_LINEARY As Short = 1
    Public Const SCALE_LINEARX_LOGY As Short = 2
    Public Const SCALE_LOGX_LINEARY As Short = 3
    Public Const SCALE_LOGX_LOGY As Short = 4
    Public Const SCALE_TRIPARTITE As Short = 5
    Public Const SCALE_LINEARX_PROBABILITY As Short = 6
    Public Const SCALE_GRAINSIZE_DIST As Short = 7
    Public Const SCALE_POLAR As Short = 8
    Public Const SCALE_BARCHART As Short = 9
    Public Const SCALE_LOGX_PROBABILITY As Short = 10
    Public Const SCALE_PROBX_LINEARY As Short = 11
    Public Const SCALE_PROBX_LOGY As Short = 12
    Public Const SCALE_PROBX_PROBY As Short = 13
    Public Const SCALE_TRIANGLE_PLOT As Short = 14
    Public Const SCALE_N185 As Short = 15

    ' Unit-specific scaling codes ... combine with above values using OR operator

    Public Const UNITS_DEFAULT As Short = 0
    Public Const UNITS_TRIPARTITE_INCHES As Short = UNITS_DEFAULT ' Velocity (Y) in inches/sec
    Public Const UNITS_TRIPARTITE_FEET As Short = &H100S '                 feet/sec
    Public Const UNITS_TRIPARTITE_MILLIMETERS As Short = &H200S '                 mm/sec
    Public Const UNITS_TRIPARTITE_CENTIMETERS As Short = &H300S '                 cm/sec
    Public Const UNITS_TRIPARTITE_METERS As Short = &H400S '                 meters/sec
    Public Const UNITS_GRAINSIZE_MILLIMETERS As Short = UNITS_DEFAULT ' Grain sizes (X) in mm
    Public Const UNITS_GRAINSIZE_INCHES As Short = &H100S '                 inches
    Public Const UNITS_POLAR_RADIANS As Short = UNITS_DEFAULT ' Rotation (X) in radians
    Public Const UNITS_POLAR_DEGREES As Short = &H100S '                 degrees
    Public Const UNITS_USERDEFINED As Short = &H7F00S

    ' Data organization following DPLOT structure:

    Public Const DATA_XYXY As Short = 0 ' One or more sets of X,Y data
    Public Const DATA_DXY As Short = 1 ' One or more X,Y curves. Constant spacing in X and same number of points in all curves.
    Public Const DATA_XYYY As Short = 2 ' One or more X,Y curves. All curves have the same X values.
    Public Const DATA_3D As Short = 3 ' Z values on a rectangular grid
    Public Const DATA_3DR As Short = 4 ' Random X,Y,Z values
    Public Const DATA_IMAGE As Short = 5 ' Used only by DPlot - there's no way to SEND DPlot an image
    Public Const DATA_1D As Short = 6 ' One or more groups of Y values.
    Public Const DATA_3DS As Short = 7 ' 3D scatter plot

    ' Line styles:
    Public Const LINESTYLE_NONE As Short = 0
    Public Const LINESTYLE_SOLID As Short = 1
    Public Const LINESTYLE_LONGDASH As Short = 2
    Public Const LINESTYLE_DOTTED As Short = 3
    Public Const LINESTYLE_DASHDOT As Short = 4
    Public Const LINESTYLE_MEDDASH As Short = 5
    Public Const LINESTYLE_DASHDOTDOT As Short = 6
    Public Const LINESTYLE_DASHDOTDOTDOT As Short = 7

    ' Symbol styles
    Public Const SYMBOLSTYLE_NONE As Short = 0
    Public Const SYMBOLSTYLE_DOT As Short = 1
    Public Const SYMBOLSTYLE_CROSS As Short = 2
    Public Const SYMBOLSTYLE_ASTERISK As Short = 3
    Public Const SYMBOLSTYLE_X As Short = 4
    Public Const SYMBOLSTYLE_SQUARE As Short = 5
    Public Const SYMBOLSTYLE_DIAMOND As Short = 6
    Public Const SYMBOLSTYLE_TRIANGLE As Short = 7
    Public Const SYMBOLSTYLE_OCTAGON As Short = 8
    Public Const SYMBOLSTYLE_ITRIANGLE As Short = 9
    Public Const SYMBOLSTYLE_HEXAGON As Short = 10
    Public Const SYMBOLSTYLE_PENTAGON As Short = 11
    Public Const SYMBOLSTYLE_STAR As Short = 12
    Public Const SYMBOLSTYLE_FILL As Short = &H100S ' May be combined with other styles

    Public Sub DPlot_FindDPLOTLIB()
        ' Find DPLOTLIB.DLL.
        Dim key As Microsoft.Win32.RegistryKey
        Dim path As String
        Dim hLib As IntPtr

        hLib = LoadLibrary("dplotlib.dll")
        If hLib <> 0 Then
            ' If LoadLibrary finds the DLL, then so will VB.NET, so there's
            ' no need to look in the registry or change the current
            ' directory
            FreeLibrary(hLib)
            Exit Sub
        End If

        key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\DPLOT\\DPLOTLIB\\Folder")
        path = key.GetValue("").ToString()
        key.Close()
        If Len(path) > 0 Then
            System.Environment.CurrentDirectory = path
        End If
    End Sub
End Module