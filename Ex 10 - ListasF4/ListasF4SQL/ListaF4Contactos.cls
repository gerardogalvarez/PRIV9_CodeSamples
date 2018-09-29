VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsRibbon"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False


Public Property Set Plataforma(objPlat As StdBSInterfPub)
    
    Set m_objPlat = objPlat
    
End Property

Public Sub ListaContactos()
Dim strSql As StdBEStringBuilder

' Usar sempre o string builder
Set strSql = New StdBEStringBuilder

strSql.Append ("SELECT C.Contacto, C.PrimeiroNome, C.UltimoNome,")
strSql.Append (" C.Email as CEmail, C.Telefone as CTelefone, C.Telemovel as CTelemovel,")
strSql.Append (" L.TipoContacto, L.TipoEntidade, L.Entidade")
strSql.Append (" FROM Contactos C INNER JOIN LinhasContactoEntidades L ON (C.ID = L.IDContacto)")
strSql.Append (" WHERE ((L.Entidade = 'SOFRIO') AND (L.TipoEntidade = 'C')) ORDER BY L.Entidade")
 
PSO.Listas.GetF4SQL "A minha Lista Contactos", strSql.Value, "Contacto,PrimeiroNome,UltimoNome,CEmail,Telefone,CTelemovel,TipoContacto,TipoEntidade,Entidade"

End Sub




