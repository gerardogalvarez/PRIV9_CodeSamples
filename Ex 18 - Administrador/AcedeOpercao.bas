Attribute VB_Name = "Module4"
Sub AcedeOperacao()

If Aplicacao.Utilizador.AcedeOperacao("EXT", "mnuOperacao1") Then
    MsgBox "Com permisssão.", vbInformation
Else
    MsgBox "Sem permisssão.", vbInformation
End If

End Sub

Sub PodeAcederAtributo()

If Aplicacao.Utilizador.AcedeAtributo("Documento", "FA", "EXT", "CRIAR") Then
    MsgBox "Com permisssão dinamica.", vbInformation
Else
    MsgBox "Sem permisssão dinamica.", vbInformation
End If

End Sub
