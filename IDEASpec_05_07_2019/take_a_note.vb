Public Class take_a_note
    'Dim text_changed
    'Dim note_text As String

    Public Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        On Error GoTo out
        note_text = TextBox1.Text()
        FileOpen(11, Note_File_Name, OpenMode.Output)
        'Base_File_Namxt
        Write(11, note_text)
out:
        FileClose(11)

        wait_for_notes = False
        Me.Hide()
    End Sub

    Public Sub take_a_note_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub
End Class