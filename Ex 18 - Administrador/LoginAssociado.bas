Attribute VB_Name = "Module4"
Sub LoginAssociado()
Dim objLoginAssociado   As AdmBELoginAssociado
    
    'Edita o login associado
    Set objLoginAssociado = New AdmBELoginAssociado
    Set objLoginAssociado = BSO.DSO.Plat.Administrador.DaLoginAssociadoUtilizador("Sereno", "EXT", "Login_Apl_Externa")

End Sub

