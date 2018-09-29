Attribute VB_Name = "Module1"
Private Sub main()
Dim objPainelContexto As UserForm1

Set objUserForm = New UserForm1
Call PSO.MDI.DockingManager.AbrePainel("UserForm", 200, 0, 0, , , objPainelContexto)

End Sub

