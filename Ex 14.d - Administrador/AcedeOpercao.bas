Attribute VB_Name = "Module4"
Sub AcedeOperacao()

If Aplicacao.Utilizador.AcedeOperacao("EXT", "mnuOperacao1") Then
    MsgBox "Com permisss�o.", vbInformation
Else
    MsgBox "Sem permisss�o.", vbInformation
End If

End Sub

Sub PodeAcederAtributo()

If Aplicacao.Utilizador.AcedeAtributo("Documento", "FA", "EXT", "CRIAR") Then
    MsgBox "Com permisss�o dinamica.", vbInformation
Else
    MsgBox "Sem permisss�o dinamica.", vbInformation
End If

End Sub
