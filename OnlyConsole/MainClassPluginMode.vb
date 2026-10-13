Imports TouchTest
Imports System.Windows.Forms
Imports System.Drawing

Public Class MainClassPluginMode
    Implements TouchTest.Q_U_U_U_Q_I


    Public Shared CustomConsole As Form1
    Public Shared UserIGotViaLogonScreen As TouchTest.UserManager

    Public Shared Form1 As TouchTest.Form1
    Public Shared UI As UI

    Public ReadOnly Property Name As String Implements Q_U_U_U_Q_I.Name
        Get
            Return "OnlyConsole"
        End Get
    End Property

    Public Sub ExecuteDebug(mainForm As TouchTest.Form1) Implements Q_U_U_U_Q_I.ExecuteDebug
        Form1 = mainForm

        If My.Computer.FileSystem.DirectoryExists(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config") Then
            If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & $"\Plugins\OnlyConsole_Config\Users\{mainForm.User.Username}\AutoRunCustomConsole.setting") Then
                Try
                    Dim value As Boolean = Convert.ToBoolean(My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & $"\Plugins\OnlyConsole_Config\Users\{mainForm.User.Username}\AutoRunCustomConsole.setting"))
                    If value = True Then
                        OnlyConsole.Form1.StartMainFormPlugin()
                    Else


                        Dim btn5 As Button

                        btn5 = GetVolumeButton(mainForm)

                        Dim NewButton As New Button
                        NewButton.Name = "NewWifiButton"

                        GetTimebarPanel(mainForm).Controls.Add(NewButton)

                        NewButton.Size = btn5.Size
                        NewButton.Location = btn5.Location
                        NewButton.FlatAppearance.BorderSize = 0
                        NewButton.FlatStyle = FlatStyle.Flat
                        NewButton.BackgroundImage = My.Resources._405007_terminal_v29b5395ed7
                        NewButton.BackgroundImageLayout = ImageLayout.Stretch
                        NewButton.BackColor = Color.Transparent
                        AddHandler NewButton.Click, AddressOf NewButton_Click
                        NewButton.Visible = True
                        NewButton.BringToFront()
                        btn5.Visible = True
                    End If
                Catch ex As Exception
                    If Environment.CommandLine.Contains("/DevMode") = True Then
                        Debug.WriteLine(ex.Message)
                    End If
                End Try
            Else
                OnlyConsole.Form1.StartMainFormPlugin()
            End If
        Else
            OnlyConsole.Form1.StartMainFormPlugin()
        End If
    End Sub

    Public Sub ExecuteUISubs(mainForm As UI) Implements Q_U_U_U_Q_I.ExecuteUISubs
        UI = mainForm

        If UI.LogonBool = True Then
            UserIGotViaLogonScreen = Form1.User
            AddHandler Form1.FormClosing, Sub()
                                              UI.LogonBool = False
                                          End Sub
        Else

        End If
    End Sub

    Private Sub NewButton_Click(sender As Object, e As EventArgs)
        OnlyConsole.Form1.StartMainFormPlugin()
    End Sub

    Public Function GetVolumeButton(form As Form)
        For Each control1 As Control In form.Controls
            If control1.Name = "Panel1" Then
                For Each control2 As Control In control1.Controls
                    If control2.Name = "TimebarPanel" Then
                        For Each control3 As Control In control2.Controls
                            If control3.Name = "VolumeButton" Then
                                Return control3
                            Else
                                Debug.WriteLine(control3.Name)
                            End If
                        Next
                    Else
                        Debug.WriteLine(control2.Name)
                    End If
                Next
            Else
                Debug.WriteLine(control1.Name)
            End If
        Next
        Return Nothing
    End Function

    Public Function GetTimebarPanel(form As Form) As Panel
        For Each control1 As Control In form.Controls
            If control1.Name = "Panel1" Then
                For Each control2 As Control In control1.Controls
                    If control2.Name = "TimebarPanel" Then
                        Return control2
                    Else
                        Debug.WriteLine(control2.Name)
                    End If
                Next
            Else
                Debug.WriteLine(control1.Name)
            End If
        Next
        Return Nothing
    End Function
End Class
