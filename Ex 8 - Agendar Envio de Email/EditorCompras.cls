VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "EditorCompras"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = True
Private Sub EditorCompras_DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Long)
Dim objmsg As StdBEMensagemEmail

Set objmsg = New StdBEMensagemEmail

objmsg.Assunto = "Compra " & Me.DocumentoCompra.TipoDoc & "\" & Me.DocumentoCompra.NumDoc
objmsg.Mensagem = "Foi registada uma compra pelo utilizador" & "\" & BSO.Contexto.UtilizadorActual
objmsg.EmailTo = "elevation.demo1@gamil.com"

PSO.Email.Actualiza objmsg

Set objmsg = Nothing

End Sub

