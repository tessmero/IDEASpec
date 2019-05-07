' The Interpreter class parses and executes scripts, using the SpecCommand class to handle the scripts in a 
' more powerful way
' 
' 
Public Class Interpreter
    Public cblaster As ChrisBlasterIO

    Public Sub New()
        cblaster = New ChrisBlasterIO("Nexys2")
    End Sub
    ' This function runs a command, returning true if sucessful, false otherwise. The String errmessage is 
    ' given a brief description of the error for the user. The boolean ForReal is used to run in test mode 
    ' (if False) or with hardware control (if True)
    Public Overridable Function RunCommand(ByRef action As SpecCommand, ByRef errmessage As String, ByVal ForReal As Boolean) As Boolean


        errmessage = "No Errors"
        Return True
    End Function
End Class
