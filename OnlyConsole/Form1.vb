Imports System.Windows.Forms

Public Class Form1
    Public IsInAppMode As Boolean = False

    Private IsInSetup As Boolean
    Private SetupStep As Int64 = 0

    Private UserMode As String = "Safe"

    Private SafeUser As TouchTest.UserManager
    Private UnSafeUser As TouchTest.UserManager

    Private Sub StartConsole(RunMode As RunningMode)
        If RunMode = RunningMode.OpenFrameworkApp Then
        ElseIf RunMode = RunningMode.Plugin Then
            If My.Computer.FileSystem.DirectoryExists(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config") Then

                If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\UsersMultiUsersConfigs.setting") Then
                    Dim IsMultiUsersConfigsOn As Boolean = False
                    Try
                        IsMultiUsersConfigsOn = Convert.ToBoolean(My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\UsersMultiUsersConfigs.setting"))
                    Catch ex As Exception
                        MainClassPluginMode.UI.ShowError(ex.Message)
                    End Try



                Else
                    MainClassPluginMode.UI.ShowError("""UsersMultiUsersConfigs.setting"" doesn't exist.")
                    Return
                End If
            End If

        ElseIf RunMode = RunningMode.CustomController Then
        End If
    End Sub

    Private Enum RunningMode
        OpenFrameworkApp = 1
        Plugin = 2
        CustomController = 3
    End Enum

    Public Sub RunCommand(Command As String, Form As Form, Optional User As TouchTest.UserManager = Nothing)
        Dim UseCustomUser As Boolean = False
        If User IsNot Nothing Then UseCustomUser = True

        Command = Command.Replace("Console>", "")
        Command = Command.Replace("CustomConsole>", "")

        If Command.StartsWith("exit") = True Then
            If Form IsNot Nothing Then
                Try
                    Form.Close()
                Catch ex As Exception
                    If IsInAppMode = True Then
                    Else
                        MainClassPluginMode.UI.ShowError(ex.Message, TouchTest.ErrorMSGBox.Alerts.Exclamation)
                    End If
                End Try
            End If
        End If
    End Sub

    Private Sub RichTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles RichTextBox1.KeyDown
        If IsInSetup = True Then
            If SetupStep = 1 Then
                'Asks if to use Unsafe User 
                If e.KeyCode = Keys.Y Or Keys.Enter Then
                    e.SuppressKeyPress = True
                    Setup_UseUnSafeUser = True
                    ClearConsole()
                    NextSetupStep(2)
                ElseIf e.KeyCode = Keys.N Then
                    e.SuppressKeyPress = True
                    Setup_UseUnSafeUser = False
                    ClearConsole()
                    NextSetupStep(2)
                End If
            ElseIf SetupStep = 2 Then
                If e.KeyCode = Keys.Y Or Keys.Enter Then
                    e.SuppressKeyPress = True
                    Setup_UseCustomConsoleCommands = True
                    ClearConsole()
                    NextSetupStep(3)
                ElseIf e.KeyCode = Keys.N Then
                    e.SuppressKeyPress = True
                    Setup_UseCustomConsoleCommands = False
                    ClearConsole()
                    NextSetupStep(3)
                End If
            ElseIf SetupStep = 3 Then
                If e.KeyCode = Keys.Y Or Keys.Enter Then
                    e.SuppressKeyPress = True
                    Setup_UseCustomConsoleInFullScreenMode = True
                    ClearConsole()
                    NextSetupStep(4)
                ElseIf e.KeyCode = Keys.N Then
                    e.SuppressKeyPress = True
                    Setup_UseCustomConsoleInFullScreenMode = False
                    ClearConsole()
                    NextSetupStep(4)
                End If
            ElseIf SetupStep = 4 Then
                If e.KeyCode = Keys.Y Or Keys.Enter Then
                    e.SuppressKeyPress = True
                    Setup_AutoRunCustomConsole = True
                    ClearConsole()
                    NextSetupStep(5)
                ElseIf e.KeyCode = Keys.N Then
                    e.SuppressKeyPress = True
                    Setup_AutoRunCustomConsole = False
                    ClearConsole()
                    NextSetupStep(5)
                End If
            ElseIf SetupStep = 5 Then
                If e.KeyCode = Keys.Y Or Keys.Enter Then
                    e.SuppressKeyPress = True
                    Setup_UseSameConfigForAllUsers = False
                    ClearConsole()
                    NextSetupStep(6)
                ElseIf e.KeyCode = Keys.N Then
                    e.SuppressKeyPress = True
                    Setup_UseSameConfigForAllUsers = True
                    ClearConsole()
                    NextSetupStep(6)
                End If
            End If
        Else

        End If
    End Sub

    Private Setup_UseUnSafeUser As Boolean = False
    Private Setup_UseCustomConsoleCommands As Boolean = False
    Private Setup_UseCustomConsoleInFullScreenMode As Boolean = False
    Private Setup_AutoRunCustomConsole As Boolean = False
    Private Setup_UseSameConfigForAllUsers As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IsInAppMode = True Then
            'Loads in App Mode (Doesn't load Unsafe User. Due to RunCommand() not allowing other then the current user to executes the command.)
            'WIP
        Else
            'Loads in Plugin Mode (Better Mode. But is less safe then App Mode)

            SafeUser = New TouchTest.UserManager(MainClassPluginMode.Form1.User.Username)

            If My.Computer.FileSystem.DirectoryExists(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config") Then
                IsInSetup = False

                UnSafeUser = MainClassPluginMode.UserIGotViaLogonScreen
            Else
                IsInSetup = True
            End If

            If IsInSetup = True Then
                SetupStep = 1
                WriteLine("Do you want to use the Unsafe User Commands?", True)
                SetReadOnly(True)
            Else
                StartConsole(RunningMode.Plugin)
            End If

        End If
    End Sub

    Public Shared Sub StartMainFormPlugin()
        MainClassPluginMode.CustomConsole = New Form1
        MainClassPluginMode.CustomConsole.IsInAppMode = False
        MainClassPluginMode.Form1.OpenChildForm(MainClassPluginMode.CustomConsole)
        MainClassPluginMode.Form1.EnableFullAppMode(True)
    End Sub

    Private Sub NextSetupStep(StepInt As Int64)
        If StepInt = 2 Then
            WriteLine("Do you want to use Custom Console Commands?", True)
            SetupStep = 2
        ElseIf StepInt = 3 Then
            WriteLine("Do you want to use the Console in FullScreen Mode.", True)
            SetupStep = 3
        ElseIf StepInt = 4 Then
            WriteLine("Do you want to autorun CustomConsole?", True)
            SetupStep = 4
        ElseIf StepInt = 5 Then
            If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\UsersMultiUsersConfigs.setting") Then
                Dim CheckBool As Boolean = Convert.ToBoolean(My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\UsersMultiUsersConfigs.setting"))
                If CheckBool = False Then
                    NextSetupStep(6)
                    Return
                End If
            End If
            WriteLine("Do you want to use the same config for all Users?", True)
            SetupStep = 5
        ElseIf StepInt = 6 Then
            SetupStep = 6

            'Checks if Main Config Folder exists
            If My.Computer.FileSystem.DirectoryExists(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config") = False Then
                My.Computer.FileSystem.CreateDirectory(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config")
            End If

            'Configs User Plugin Config
            If Setup_UseSameConfigForAllUsers = False Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\UsersMultiUsersConfigs.setting", "True", False)
                If My.Computer.FileSystem.DirectoryExists(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\Users") = False Then
                    My.Computer.FileSystem.CreateDirectory(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\Users")
                End If

                If My.Computer.FileSystem.DirectoryExists(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\Users\" & MainClassPluginMode.Form1.User.Username) = False Then
                    My.Computer.FileSystem.CreateDirectory(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\Users\" & MainClassPluginMode.Form1.User.Username)
                End If
            Else
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\UsersMultiUsersConfigs.setting", "False", False)
            End If

            'Setup_UseCustomConsoleCommands
            If Setup_UseSameConfigForAllUsers = False Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\Users\" & MainClassPluginMode.Form1.User.Username & "\UseCustomConsoleCommands.setting", Setup_UseCustomConsoleCommands.ToString, False)
            Else
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\UseCustomConsoleCommands.setting", Setup_UseCustomConsoleCommands.ToString, False)
            End If

            'Setup_UseUnSafeUser
            If Setup_UseSameConfigForAllUsers = False Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\Users\" & MainClassPluginMode.Form1.User.Username & "\UseUnSafeUser.setting", Setup_UseCustomConsoleCommands.ToString, False)
            Else
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\UseUnSafeUser.setting", Setup_UseCustomConsoleCommands.ToString, False)
            End If

            'Setup_UseCustomConsoleInFullScreenMode
            If Setup_UseSameConfigForAllUsers = False Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\Users\" & MainClassPluginMode.Form1.User.Username & "\UseCustomConsoleInFullScreenMode.setting", Setup_UseCustomConsoleCommands.ToString, False)
            Else
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\UseCustomConsoleInFullScreenMode.setting", Setup_UseCustomConsoleCommands.ToString, False)
            End If

            'Setup_AutoRunCustomConsole
            If Setup_UseSameConfigForAllUsers = False Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\Users\" & MainClassPluginMode.Form1.User.Username & "\AutoRunCustomConsole.setting", Setup_UseCustomConsoleCommands.ToString, False)
            Else
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\Plugins\OnlyConsole_Config\AutoRunCustomConsole.setting", Setup_UseCustomConsoleCommands.ToString, False)
            End If

            SetReadOnly(False)
            StartConsole(RunningMode.Plugin)
        End If
    End Sub

    Private Sub WriteLine(Text As String, Optional MakeSpace As Boolean = False)
        If MakeSpace = True Then
            RichTextBox1.AppendText(Text & vbCrLf)
        Else
            RichTextBox1.AppendText(Text)
        End If

    End Sub

    Private Sub ClearConsole()
        RichTextBox1.Clear()
    End Sub

    Private Sub SetReadOnly(Value As Boolean)
        RichTextBox1.ReadOnly = Value
    End Sub
End Class