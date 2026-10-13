Public Interface ICommandPlugin
    ReadOnly Property Name As String

    Sub Initialize(CommandHelper As Object)

    Sub GetForm1(Form1Object As Object)

    Sub GetUI(UIObject As Object)

    Sub Execute()
End Interface
