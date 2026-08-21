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
    End Sub

    Public Sub ExecuteUISubs(mainForm As UI) Implements Q_U_U_U_Q_I.ExecuteUISubs
        UI = mainForm

        If UI.LogonBool = True Then
            UserIGotViaLogonScreen = Form1.User
            AddHandler Form1.FormClosing, Sub()
                                              UI.LogonBool = False
                                          End Sub
        Else
            OnlyConsole.Form1.StartMainFormPlugin()
        End If


    End Sub
End Class
