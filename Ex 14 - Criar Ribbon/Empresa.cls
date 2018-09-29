VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "Empresa"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = True

Private m_objMyRibbon As clsRibbon


Private Sub Empresa_DepoisDeAbrirEmpresa()

Set m_objMyRibbon = New clsRibbon

Set m_objMyRibbon.Plataforma = PSO
m_objMyRibbon.RegistaAddin

End Sub
