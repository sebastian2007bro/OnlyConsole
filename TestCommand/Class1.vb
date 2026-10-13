Imports System
Imports ICommandHelper

Public Class Class1
    Implements ICommandPlugin

    Public ReadOnly Property Name As String Implements ICommandPlugin.Name
        Get
            Return "TestCommand"
        End Get
    End Property

    Public Sub Initialize(CommandHelper As Object) Implements ICommandPlugin.Initialize

    End Sub

    Public Sub GetForm1(Form1Object As Object) Implements ICommandPlugin.GetForm1

    End Sub

    Public Sub GetUI(UIObject As Object) Implements ICommandPlugin.GetUI

    End Sub

    Public Sub Execute() Implements ICommandPlugin.Execute
        Debug.WriteLine("asdhf")
    End Sub
End Class
