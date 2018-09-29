VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "clsRibbon"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Private m_objTab As Object
Private m_objGrupo As Object

Private Const cIDTAB As Long = 9000
Private Const cIDGRUPO As Long = 9001
Private Const cIBBOTAO1 As Long = 900101
Private Const cIBBOTAO2 As Long = 900102
Private Const cIBBOTAO3 As Long = 900103

Private m_objPlat As StdBSInterfPub
Private m_objNotificaNET As Notificacao.Painel

Private WithEvents m_objNotifica As StdBSPainelNotificacoes
Attribute m_objNotifica.VB_VarHelpID = -1
Private WithEvents m_objRibbon As StdBSPRibbon
Attribute m_objRibbon.VB_VarHelpID = -1

Public Property Set Plataforma(objPlat As StdBSInterfPub)
    Set m_objPlat = objPlat
End Property

Public Sub RegistaAddin()
    
    Set m_objRibbon = m_objPlat.Ribbon
    Set m_objNotifica = m_objPlat.MDI.Notificacoes
    
    CriaTab
    CriaGrupo "Grupo 1", cIDGRUPO
    CriaBotao16x16 "Configuracão", cIBBOTAO1, "C:\IconsStandard\16x16\Feramentas.ico"
    CriaBotao16x16 "Painel", cIBBOTAO3, "C:\IconsStandard\16x16\procurar.ico"
    CriaBotao32x32 "Notificação", cIBBOTAO2
    
    m_objPlat.Diagnosticos.Trace "teste"
    m_objPlat.Diagnosticos.TraceCompleto "teste completo"
    
End Sub

Private Sub CriaTab()
    
    Set m_objTab = m_objPlat.Ribbon.CriaRibbonTabERP("PRIMAVERA", cIDTAB, 9)
    
End Sub

Private Sub CriaGrupo(Nome As String, Id As Long)
    
    Set m_objGrupo = m_objPlat.Ribbon.CriaRibbonGroup(m_objTab, Nome, Id)
    
End Sub

Private Sub CriaBotao16x16(Nome As String, Id As Long, Optional Icon As String)
Dim objBotaoBE As StdBEBotaoRibbon
Dim objBotoesBE As StdBEBotoesRibbon

Set objBotoesBE = New StdBEBotoesRibbon
Set objBotaoBE = New StdBEBotaoRibbon

objBotaoBE.Descricao = Nome

If Len(Icon) > 0 Then
        
    Set objBotaoBE.Icon = m_objPlat.Branding.Imagens.DaIconExterno(Icon)

Else
    
    Set objBotaoBE.Icon = m_objPlat.Branding.Imagens.DaIconSistema(ico16x16, icoHardware_Display)
    
End If

objBotaoBE.Icon32 = False
objBotaoBE.Id = Id

objBotoesBE.Insere objBotaoBE

m_objPlat.Ribbon.AdicionaBotoes objBotoesBE, "Botoes", m_objGrupo.Id


End Sub


Private Sub CriaBotao32x32(Nome As String, Id As Long, Optional Icon As String)
Dim objBotaoBE As StdBEBotaoRibbon
Dim objBotoesBE As StdBEBotoesRibbon

Set objBotoesBE = New StdBEBotoesRibbon
Set objBotaoBE = New StdBEBotaoRibbon

objBotaoBE.Descricao = Nome

If Len(Icon) > 0 Then
        
    Set objBotaoBE.Icon = m_objPlat.Branding.Imagens.DaIconExterno(Icon)

Else
    
    Set objBotaoBE.Icon = m_objPlat.Branding.Imagens.DaIconSistema(ico32x32, icoHardware_Server)
    
End If

objBotaoBE.Icon32 = True
objBotaoBE.Id = Id

objBotoesBE.Insere objBotaoBE

m_objPlat.Ribbon.AdicionaBotoes objBotoesBE, "Botoes", m_objGrupo.Id


End Sub

Private Sub m_objNotifica_MarkupNotification(ByVal sender As Variant, ByVal args As Variant, ByVal params As Variant)
    
    Shell "C:\windows\System32\notepad.exe", vbMaximizedFocus
    
End Sub

Private Sub m_objRibbon_Executa(Id As String, Comando As String)
Dim objFrmConfig As New frmConfiguracao
Dim objPainel As clsPainel

    Select Case Id
        
        Case cIBBOTAO1
            'm_objPlat.Diagnosticos.Trace "Utilizador carregou na opção 1."
            m_objPlat.Diagnosticos.TraceFicheiro "C:\Omeulog.log", "Utilizador carregou na opção 1."
            objFrmConfig.Show
        
        Case cIBBOTAO2
            m_objPlat.MDI.Notificacoes.MostraInformacao "Notificação", "Notificação do painel de informação.? <Hyperlink Click=""Markup_Notification"" Tag=""1"">Click aqui para detalhes.</Hyperlink>", False
            
            Set m_objNotificaNET = New Notificacao.Painel
            Set m_objNotificaNET.Plataforma = m_objPlat
        
        Case cIBBOTAO3
            
            Set objPainel = New clsPainel
            Set objPainel.Plataforma = m_objPlat
            
            objPainel.CriaPainel
            
        
            
    End Select

End Sub
