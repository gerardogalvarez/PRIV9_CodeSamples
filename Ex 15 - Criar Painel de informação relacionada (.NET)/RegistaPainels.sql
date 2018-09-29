
INSERT INTO InformacaoRelacionada ([Id],[Categoria],[Config]) 
Values(
	'58B09889-AE6F-42AA-AECC-27C2379B55B1',
	'Clientes',
	'<Config>
    <General>
        <TypeClassName>Primavera.InfoRelacionada.Pendentes</TypeClassName>
        <Name>Total pendente por cliente</Name>
        <Title>Valor em conta corrente por tipo conta.</Title>
        <Description>Exemplo.NET</Description>
        <ShowDescription>0</ShowDescription>
        <Height>0</Height>
    </General>
	</Config>'
)
GO


INSERT INTO ContextosInformacaoRelacionada([IdCategoria],[Contexto],[Ordem],[Params]) 
Values (
	'58B09889-AE6F-42AA-AECC-27C2379B55B1',
	'frmTabClientes',3,
	'<Config>
		<General>
	        <Expanded>1</Expanded>
	    </General>
	    <Params></Params>
	</Config>'
)
GO


/*
DELETE  from ContextosInformacaoRelacionada WHERE [IdCategoria]='58B09889-AE6F-42AA-AECC-27C2379B55B1'
DELETE  from InformacaoRelacionada WHERE [Id]='58B09889-AE6F-42AA-AECC-27C2379B55B1'
GO
*/