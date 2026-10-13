Imports System
Imports System.IO
Imports System.Security
Imports System.Reflection

Public Class CommandManager
    Public Shared Function LoadPlugins(folderPath As String) As List(Of ICommandHelper.ICommandPlugin)
        Dim plugins As New List(Of ICommandHelper.ICommandPlugin)()

        If Directory.Exists(folderPath) Then
            Dim dllFiles = Directory.GetFiles(folderPath, "*.dll")

            For Each dll In dllFiles
                Dim asm = Assembly.LoadFrom(dll)

                For Each type In asm.GetTypes()
                    If GetType(ICommandHelper.ICommandPlugin).IsAssignableFrom(type) AndAlso Not type.IsInterface AndAlso Not type.IsAbstract Then
                        Dim pluginInstance = CType(Activator.CreateInstance(type), ICommandHelper.ICommandPlugin)
                        plugins.Add(pluginInstance)
                    End If
                Next
            Next
        End If

        Return plugins
    End Function
End Class
