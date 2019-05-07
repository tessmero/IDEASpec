Public Class SpecLoop
    Inherits SpecCommand

    Public loop_id As Integer
    Public count As Integer
    Public command_after_loop_end As SpecCommand

    Public Overrides Function getNextCommand() As SpecCommand
        If (count > 0) Then
            Return next_command
            count = count - 1
        Else
            Return command_after_loop_end
        End If
    End Function
End Class
