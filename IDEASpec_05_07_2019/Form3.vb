Public Class Form3
    Dim connected As Boolean

    Dim comPORT As String
    Dim receivedData As String = ""
    Dim sendCom As String = ""
    Dim rec As Boolean = False
    Public moving As Boolean = False




    Private Sub lambdaController_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Timer1.Enabled = False
        Timer2.Enabled = False
        Timer1.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = False
        moveButton.Enabled = False
        engagedCheck.Enabled = True
        linPosToDo.Enabled = False
        LinMoveToIndexDo.Enabled = False
        linMoveRelDo.Enabled = False
        setLinIndexDo.Enabled = False

        comPORT = ""
        For Each sp As String In My.Computer.Ports.SerialPortNames
            comPort_ComboBox.Items.Add(sp)
        Next
    End Sub

    Private Sub comPort_ComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles comPort_ComboBox.SelectedIndexChanged
        If (comPort_ComboBox.SelectedItem <> "") Then
            comPORT = comPort_ComboBox.SelectedItem

            Timer1.Enabled = True
            Timer2.Enabled = True
            Button2.Enabled = True
            Button3.Enabled = True
            moveButton.Enabled = True
            engagedCheck.Enabled = True
            linPosToDo.Enabled = True
            LinMoveToIndexDo.Enabled = True
            linMoveRelDo.Enabled = True
            setLinIndexDo.Enabled = True

            Timer1.Enabled = True
            'System.Windows.Forms.MessageBox.Show("OK")

            'zero_mono()

        End If
    End Sub


    'Private Sub moveButton(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moveButton.Click    ', move.Click
    '    sendCommand("move:" + moveSteps.Text)

    'End Sub


    Sub sendCommand(ByVal data As String)

        data = Trim(data)
        Timer2.Enabled = False

        rec = False ' set to not received yet
        If data <> "" Then
            '    com.ReadTimeout = 1000
            '    com.WriteLine(data)
            '    Dim retdata As String = com.ReadLine()
            '    RichTextBox1.Text &= retdata
            '    com.Close()
            'End Using
            Try
                Using com As IO.Ports.SerialPort =
              My.Computer.Ports.OpenSerialPort(comPORT)

                    com.ReadTimeout = 200
                    com.WriteLine(data)

                    'If (1 = 0) Then

                    Dim retdata As String = com.ReadLine()
                    'RichTextBox1.Text &= retdata

                    'com.DiscardInBuffer()
                    com.Close()

                    If retdata = "" Then
                        'rec = False
                        'RichTextBox1.Text &= receivedData
                    Else
                        moving = False

                        rec = True
                        If sendCom = "p?" Then
                            posLabel.Text = retdata
                        ElseIf sendCom = "l?" Then
                            lambdaLabel.Text = retdata
                        ElseIf sendCom = "lp?" Then
                            linPosNow.Text = retdata
                        Else
                            'RichTextBox1.Text &= sendCom
                            'RichTextBox1.Text &= receivedData
                        End If
                    End If
                    'End If

                End Using
                'End if 
            Catch ex As Exception
                'MessageBox.Show(ex.Message)
            End Try
        End If

        data = ""
        'moveSteps.Text = Str(-1 * Val(moveSteps.Text))
        System.Windows.Forms.Application.DoEvents()
        Timer2.Enabled = True
    End Sub


    Function ReceiveSerialData() As String
        On Error Resume Next
        Dim Incoming As String
        'Try
        Using com As IO.Ports.SerialPort =
          My.Computer.Ports.OpenSerialPort(comPORT)
            com.ReadTimeout = 100

            Incoming = com.ReadExisting()
            If Incoming Is Nothing Then
                Return "" '& vbCrLf
            Else
                'rec = True
                Return Incoming
            End If
        End Using
        'Catch ex As TimeoutException
        '   Return "Error: Serial Port read timed out."

        'End Try

    End Function



    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        receivedData = ReceiveSerialData()
        If receivedData = "" Then
            If sendCom = "move" Then

                'RichTextBox1.Text &= "." 'receivedData
            End If
        Else
            If sendCom = "move" Then
                moving = False
            End If

            rec = True

            If sendCom = "p?" Then
                posLabel.Text = receivedData
            ElseIf sendCom = "l?" Then
                lambdaLabel.Text = receivedData
            ElseIf sendCom = "lp?" Then
                linPosNow.Text = receivedData
            Else
                'RichTextBox1.Text &= sendCom
                'RichTextBox1.Text &= receivedData
            End If

            sendCom = ""
        End If

    End Sub

    Private Sub moveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moveButton.Click

        moveIt()
        'sendCommand("move:" + moveSteps.Text)

        'Timer2.Enabled = False

        'While moving = True
        '    System.Windows.Forms.Application.DoEvents()
        'End While

        'While (rec = False)
        '    System.Windows.Forms.Application.DoEvents()
        'End While


        'If engagedCheck.CheckState = Windows.Forms.CheckState.Checked Then

        '    sendCom = "disengage"
        '    'RichTextBox1.Text &= "disengaged"

        '    sendCommand(sendCom)
        'End If


        'sendCom = "l?"
        'sendCommand(sendCom)

        'While (rec = False)
        '    System.Windows.Forms.Application.DoEvents()
        'End While


        'sendCom = "p?"
        'sendCommand(sendCom)

        ''While (rec = False)
        ''    System.Windows.Forms.Application.DoEvents()
        ''End While
        ''rec = False

        ''sendCom = "p?"
        ''sendCommand(sendCom)

        ''Timer1.Enabled = False
        ''moveSteps.Text = "0"
    End Sub

    Private Sub connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        sendCom = "l?"
        sendCommand(sendCom)

        'Timer1.Enabled = False

        'sendCommand("l?")
        'Timer1.Enabled = True

    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    sendCom = "p?"
    '    sendCommand(sendCom)
    'End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        moveIt()

        'sendCom = "move"

        'sendCommand("to:" + moveTo.Text)

        'While (rec = False)
        '    System.Windows.Forms.Application.DoEvents()
        'End While


        'sendCom = "l?"
        'sendCommand(sendCom)

        'While (rec = False)
        '    System.Windows.Forms.Application.DoEvents()
        'End While


        'sendCom = "p?"
        'sendCommand(sendCom)

    End Sub

    Public Sub moveIt()

        'sendCom = "move"

        If (Val(moveTo.Text) < 300) Or (Val(moveTo.Text > 900)) Then
            System.Windows.Forms.MessageBox.Show("wavelength range out of bounds (300<wl<900)")
            Exit Sub

        End If


        sendCom = "engage"
        sendCommand(sendCom)

        'End If

        While (rec = False)
            System.Windows.Forms.Application.DoEvents()
        End While

        System.Threading.Thread.Sleep(1000)
        

        moving = True

        sendCom = "move"

        sendCommand("to:" + moveTo.Text)

        While (rec = False)
            System.Windows.Forms.Application.DoEvents()
        End While

        'If engagedCheck.CheckState = Windows.Forms.CheckState.Checked Then
        sendCom = "disengage"
        sendCommand(sendCom)

        'End If

        While (rec = False)
            System.Windows.Forms.Application.DoEvents()
        End While

        sendCom = "l?"
        sendCommand(sendCom)

        While (rec = False)
            System.Windows.Forms.Application.DoEvents()
        End While


        sendCom = "p?"
        sendCommand(sendCom)
    End Sub



    Public Sub lMoveIt()

        'sendCom = "move"

        If ((Val(linPosTo.Text) < -4000) Or (Val(linPosTo.Text) > 4000)) Then
            System.Windows.Forms.MessageBox.Show("position range out of bounds")
            Exit Sub

        End If

        moving = True

        sendCom = "lengage"
        sendCommand(sendCom)

        'End If

        While (rec = False)
            System.Windows.Forms.Application.DoEvents()
        End While

        System.Threading.Thread.Sleep(1000)
        sendCom = "move"

        sendCommand("lto:" + linPosTo.Text)

        While (rec = False)
            System.Windows.Forms.Application.DoEvents()
        End While

        'If engagedCheck.CheckState = Windows.Forms.CheckState.Checked Then
        sendCom = "ldisengage"
        sendCommand(sendCom)

        'End If

        While (rec = False)
            System.Windows.Forms.Application.DoEvents()
        End While

        sendCom = "lp?"
        sendCommand(sendCom)

        While (rec = False)
            System.Windows.Forms.Application.DoEvents()
        End While

    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        zero_mono()

        'engagedCheck.CheckState = Windows.Forms.CheckState.Unchecked
        'sendCom = "disengage"
        'sendCommand(sendCom)

        ''While (rec = False)
        ''    System.Windows.Forms.Application.DoEvents()
        ''End While

        ''RichTextBox1.Text &= "disengaged" & vbCrLf

        'System.Windows.Forms.MessageBox.Show("move monochromator to exactly 500 nm, and hit OK")

        'sendCom = "setwl:500"
        'sendCommand(sendCom)

        'While (rec = False)
        '    System.Windows.Forms.Application.DoEvents()
        'End While

        'sendCom = "engage"
        'sendCommand(sendCom)
        ''RichTextBox1.Text &= "engaged" & vbCrLf

        'engagedCheck.CheckState = Windows.Forms.CheckState.Checked

        'While (rec = False)
        '    System.Windows.Forms.Application.DoEvents()
        'End While


        'sendCom = "l?"
        'sendCommand(sendCom)

        'While (rec = False)
        '    System.Windows.Forms.Application.DoEvents()
        'End While


        'sendCom = "p?"
        'sendCommand(sendCom)

    End Sub

    Private Sub engagedCheck_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles engagedCheck.CheckedChanged

        'If engagedCheck.CheckState = Windows.Forms.CheckState.Unchecked Then
        '    sendCom = "engage"
        '    'RichTextBox1.Text &= "engaged"
        'Else
        '    sendCom = "disengage"
        '    'RichTextBox1.Text &= "disengaged"
        'End If

        'sendCommand(sendCom)
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick

        sendCom = "l?"
        sendCommand(sendCom)

        While (rec = False)
            System.Windows.Forms.Application.DoEvents()
        End While


        sendCom = "p?"
        sendCommand(sendCom)

        While (rec = False)
            System.Windows.Forms.Application.DoEvents()
        End While

        sendCom = "lp?"
        sendCommand(sendCom)

        While (rec = False)
            System.Windows.Forms.Application.DoEvents()
        End While


    End Sub


    Private Sub zero_mono()
        'engagedCheck.CheckState = Windows.Forms.CheckState.Unchecked
        sendCom = "disengage"
        sendCommand(sendCom)

        'While (rec = False)
        '    System.Windows.Forms.Application.DoEvents()
        'End While

        'RichTextBox1.Text &= "disengaged" & vbCrLf

        'System.Windows.Forms.MessageBox.Show("move monochromator to exactly 500 nm, and hit OK")
        Dim set_pos_to As Integer = Val(set_mono_pos.Text)
        If (set_pos_to > 300) And (set_pos_to < 800) Then

            sendCom = "setwl:" + (Trim(Str(set_pos_to)))

            sendCommand(sendCom)

            While (rec = False)
                System.Windows.Forms.Application.DoEvents()
            End While

            ''If engagedCheck.CheckState = Windows.Forms.CheckState.Checked Then
            'sendCom = "disengage"
            'sendCommand(sendCom)

            ''End If
            'RichTextBox1.Text &= "engaged" & vbCrLf

            'engagedCheck.CheckState = Windows.Forms.CheckState.Checked

            While (rec = False)
                System.Windows.Forms.Application.DoEvents()
            End While


            sendCom = "l?"
            sendCommand(sendCom)

            While (rec = False)
                System.Windows.Forms.Application.DoEvents()
            End While


            sendCom = "p?"
            sendCommand(sendCom)
        End If

    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        sendCom = "disengage"
        sendCommand(sendCom)

        'While (rec = False)
        '    System.Windows.Forms.Application.DoEvents()
        'End While
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        sendCom = "engage"
        sendCommand(sendCom)

        'While (rec = False)
        '    System.Windows.Forms.Application.DoEvents()
        'End While
    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click

    End Sub

    Private Sub linPosToDo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linPosToDo.Click
        lMoveIt()

    End Sub

    Private Sub linMoveRelDo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linMoveRelDo.Click


        sendCommand("lmove:" + linMoveRel.Text)

        Timer2.Enabled = False

        While moving = True
            System.Windows.Forms.Application.DoEvents()
        End While

        While (rec = False)
            System.Windows.Forms.Application.DoEvents()
        End While


        If engagedCheck.CheckState = Windows.Forms.CheckState.Checked Then

            sendCom = "ldisengage"
            'RichTextBox1.Text &= "disengaged"

            sendCommand(sendCom)
        End If


        sendCom = "lp?"
        sendCommand(sendCom)

        While (rec = False)
            System.Windows.Forms.Application.DoEvents()
        End While

        sendCom = "lp?"
        sendCommand(sendCom)


    End Sub

    Private Sub setLinIndexDo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles setLinIndexDo.Click
        Dim setLinIndexVal As Integer = Int(Val(setLinIndex.Text()))
        If ((setLinIndexVal >= 0) And (setLinIndexVal < 20)) Then

            sendCom = "setlindexpos:" + Trim(Str(setLinIndexVal))
            TextBox1.Text = sendCom
            sendCommand(sendCom)

            While (rec = False)
                System.Windows.Forms.Application.DoEvents()
            End While

            sendCom = "lp?"
            sendCommand(sendCom)

            While (rec = False)
                System.Windows.Forms.Application.DoEvents()
            End While

            sendCom = "lp?"
            sendCommand(sendCom)

        End If
    End Sub



    Private Sub LinMoveToIndexDo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinMoveToIndexDo.Click
        linMoveToIndexf()

        'Dim setLinIndexVal As Integer = Int(Val(linMoveToIndexIndex.Text()))
        'If ((setLinIndexVal >= 0) And (setLinIndexVal < 20)) Then
        '    sendCom = "lton:" + Trim(Str(setLinIndexVal))
        '    TextBox1.Text = sendCom
        '    sendCommand(sendCom)
        '    While (rec = False)
        '        System.Windows.Forms.Application.DoEvents()
        '    End While

        '    sendCom = "lp?"
        '    sendCommand(sendCom)

        '    While (rec = False)
        '        System.Windows.Forms.Application.DoEvents()
        '    End While

        '    sendCom = "lp?"
        '    sendCommand(sendCom)

        'End If
    End Sub

    Public Sub linMoveToIndexf()
        Dim setLinIndexVal As Integer = Int(Val(linMoveToIndexIndex.Text()))
        If ((setLinIndexVal >= 0) And (setLinIndexVal < 20)) Then
            sendCom = "lton:" + Trim(Str(setLinIndexVal))
            TextBox1.Text = sendCom
            sendCommand(sendCom)
            While (rec = False)
                System.Windows.Forms.Application.DoEvents()
            End While

            sendCom = "lp?"
            sendCommand(sendCom)

            While (rec = False)
                System.Windows.Forms.Application.DoEvents()
            End While

            sendCom = "lp?"
            sendCommand(sendCom)

        End If
    End Sub
End Class