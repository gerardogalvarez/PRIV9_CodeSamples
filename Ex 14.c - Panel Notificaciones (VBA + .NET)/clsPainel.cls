VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "clsPainel"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Private m_objPlat As StdBSInterfPub

Private WithEvents m_ObjNotificacoes As StdBSPainelNotificacoes
Attribute m_ObjNotificacoes.VB_VarHelpID = -1

Public Property Set Plataforma(objPlat As StdBSInterfPub)
    
    Set m_objPlat = objPlat
    Set m_ObjNotificacoes = objPlat.MDI.Notificacoes
    
End Property

Public Sub CriaNotificacao()

      m_objPlat.MDI.Notificacoes.MostraMensagem "Notificação", "Notificação do painel de informação.? <Hyperlink Click=""Markup_Notification"" Tag=""1"">Click aqui para detalhes.</Hyperlink>", False

End Sub
