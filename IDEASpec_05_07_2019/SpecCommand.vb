Public Class SpecCommand
    Public script As String
    Protected previous_command As SpecCommand
    Protected next_command As SpecCommand

    Protected command_type As String
    Protected command_inputs As System.Collections.Generic.List(Of String) ' a resizeable array of Strings

    Public Overridable Sub parseCommand(ByRef text As String)
        'extracts the command name and the parameters
        Dim text_break As Integer ' index of character in string that separates a command name from its parameters, such as '(' or a new-line character
        script = text.ToLower()
        text_break = 0
        Do
            text_break = text_break + 1
        Loop While (script.Chars(text_break) <> "(" And script.Chars(text_break) <> vbNewLine And text_break < script.Length)
        command_type = Trim(script.Substring(0, text_break))
        ' parse parameters into resizeable string array
        command_inputs = New System.Collections.Generic.List(Of String)
        If text_break < (script.Length - 1) Then
            Dim params As String() = script.Substring(text_break + 1).Split(",")
            For Each input As String In params
                command_inputs.Add(Trim(input.Trim(")"))) ' add a cleaned up version (no whitespaces or parenthesis) of the input
            Next
        End If

    End Sub

    Public Overridable Function getCommandName() As String
        Return command_type
    End Function

    Public Overridable Function getInputs() As String()
        Return command_inputs.ToArray()
    End Function

    Public Overridable Function getNextCommand() As SpecCommand
        Return next_command
    End Function
    Public Overridable Function getPreviousCommand() As SpecCommand
        Return previous_command
    End Function
End Class
