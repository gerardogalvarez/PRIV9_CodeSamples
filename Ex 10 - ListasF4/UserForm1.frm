VERSION 5.00
Begin {C62A69F0-16DC-11CE-9E98-00AA00574A4F} UserForm1 
   Caption         =   "Lista Artigos"
   ClientHeight    =   1560
   ClientLeft      =   45
   ClientTop       =   375
   ClientWidth     =   4710
   OleObjectBlob   =   "UserForm1.frx":0000
   StartUpPosition =   1  'CenterOwner
End
Attribute VB_Name = "UserForm1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False



Private Sub PriTextBoxF41_OLECompleteDrag(Effect As Long)

End Sub

Private Sub UserForm_KeyUp(ByVal KeyCode As MSForms.ReturnInteger, ByVal Shift As Integer)
PSO.AbreLista 1, CAT_Artigo, "Artigo", Me, PriTextBoxF41, MNU_TAB_ARTIGOS, blnModal:=True ', strClausulaWhere:="ARTIGO.Artigo='A0001'"
End Sub
