Public Class Filter_Wheel_Form

    Dim receive_data As String = ""
    Dim bug_out As String = ""



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        Using com1 As IO.Ports.SerialPort = My.Computer.Ports.OpenSerialPort("COM1")

            com1.BaudRate = 19200
            com1.DataBits = 8
            com1.StopBits = 1
            com1.Parity = IO.Ports.Parity.None
            com1.ReadTimeout = 2000

            com1.WriteLine("WSMODE")

            'com1.WriteLine("WGOTO3")
            Timer1.Enabled = True


            System.Windows.Forms.Application.DoEvents()
            receive_data = com1.ReadLine()

            Label1.Text = receive_data

            If receive_data <> "!" Then
                Label1.Text = "TIMED OUT"
                Exit Sub
            End If


            com1.WriteLine("WHOME")
            Timer1.Enabled = False

            Timer1.Enabled = True
            While receive_data = "" And bug_out = ""
                System.Windows.Forms.Application.DoEvents()
                receive_data = com1.ReadLine()
            End While
            Label1.Text = receive_data
            Timer1.Enabled = False

            Timer1.Enabled = True

            com1.Close()
        End Using



    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        On Error Resume Next
        bug_out = ""
        Label1.Text = "moving"
        System.Windows.Forms.Application.DoEvents()
        Using com3 As IO.Ports.SerialPort = My.Computer.Ports.OpenSerialPort("COM3")

            com3.BaudRate = 9600
            com3.DataBits = 8
            com3.StopBits = 0
            com3.Parity = IO.Ports.Parity.None
            com3.ReadTimeout = 10000

            '            com1.WriteLine("WSMODE")


            com3.WriteLine(TextBox1.Text)

            System.Windows.Forms.Application.DoEvents()
            receive_data = com3.ReadLine()

            Label1.Text = receive_data
            If receive_data = "*" Then
                Label1.Text = "OK"
            Else
                Label1.Text = "TIMED OUT"
            End If

            'com1.WriteLine("WHOME")

            'While receive_data = ""
            ' receive_data = com1.ReadLine()
            ' End While
            'Label1.Text = receive_data
            'com1.Close()
        End Using

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        On Error Resume Next
        Dim x As Single
        bug_out = ""
        Label2.Text = "?"
        System.Windows.Forms.Application.DoEvents()
        Using com1 As IO.Ports.SerialPort = My.Computer.Ports.OpenSerialPort("COM1")

            com1.BaudRate = 19200
            com1.DataBits = 8
            com1.StopBits = 1
            com1.Parity = IO.Ports.Parity.None
            com1.ReadTimeout = 10000



            '            com1.WriteLine("WSMODE")
            '    Timer1.Enabled = True

            com1.WriteLine("WFILTR" & TextBox1.Text)




            While receive_data = "" And x < 100000
                x = x + 1
                System.Windows.Forms.Application.DoEvents()
                receive_data = com1.ReadLine()
            End While


            Label2.Text = receive_data

            'com1.WriteLine("WHOME")

            'While receive_data = ""
            ' receive_data = com1.ReadLine()
            ' End While
            'Label1.Text = receive_data
            'com1.Close()
        End Using

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        On Error Resume Next
        bug_out = ""
        Label1.Text = "moving"
        System.Windows.Forms.Application.DoEvents()
        Using com1 As IO.Ports.SerialPort = My.Computer.Ports.OpenSerialPort("COM1")

            com1.BaudRate = 19200
            com1.DataBits = 8
            com1.StopBits = 1
            com1.Parity = IO.Ports.Parity.None
            com1.ReadTimeout = 10000
            '            com1.WriteLine("WSMODE")


            com1.WriteLine("WEXITS" & TextBox1.Text)

            System.Windows.Forms.Application.DoEvents()
            receive_data = com1.ReadLine()



            Label1.Text = receive_data
            If receive_data = "END" Then Label1.Text = "BYE"
            'com1.WriteLine("WHOME")

            'While receive_data = ""
            ' receive_data = com1.ReadLine()
            ' End While
            'Label1.Text = receive_data
            'com1.Close()
        End Using

    End Sub

    Private Sub Filter_Wheel_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        bug_out = "X"
    End Sub
    
End Class