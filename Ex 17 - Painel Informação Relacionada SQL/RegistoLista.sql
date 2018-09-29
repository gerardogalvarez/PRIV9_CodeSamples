
INSERT INTO InformacaoRelacionada ([Id],[Categoria],[Config]) 
Values(
	'B80F5924-BA0E-48A9-BA6E-14185C0D1135',
	'Clientes',
	'<Config>
	  <General>
		<TypeClassName>PriCloudLibrary900.CI_Lista</TypeClassName>
		<Name>Total pendente por cliente</Name>
		<Title>Valor em conta corrente por tipo conta.</Title>
		<Description>Exemplo Reutilização</Description>
		<ShowDescription>0</ShowDescription>
		<Height>0</Height>
	  </General>
	  <List>
		<SqlQuery>Select TipoConta,sum(ValorPendente) Pendente ,sum(Valortotal)as Total FROM Pendentes where entidade=''@@Entidade@@'' GROUP BY TipoConta</SqlQuery>
	  </List>
	  <Grid>
		<ShowGroupBox>0</ShowGroupBox>
		<ShowFooterRows>1</ShowFooterRows>
		<Visible>1</Visible>
		<Columns>
		  <Column1>
			<Name>TipoConta</Name>
			<Title>Tipo Conta</Title>
		  </Column1>
		  <Column2>
			<Name>Pendente</Name>
			<Title>Total Pendente</Title>
			<Alignment>2</Alignment>
			<DecimalPlaces>2</DecimalPlaces>
			<ShowTotal>1</ShowTotal>
		  </Column2>
		  <Column3>
			<Name>Total</Name>
			<Title>Valor Total</Title>
			<DecimalPlaces>2</DecimalPlaces>
			<Alignment>2</Alignment>
			<ShowTotal>1</ShowTotal>
		  </Column3>
		</Columns>
	  </Grid>
	  <Graph>
		<Visible>0</Visible>
		<Legend>
		  <Visible>1</Visible>
		</Legend>
		<Series>
		  <Serie1>
			<Name>Pendente</Name>
			<Legend>Pendente</Legend>
			<PointName>TipoConta</PointName>
			<SerieStyle>0</SerieStyle>
			<LegendVisible>1</LegendVisible>
			<LabelVisible>0</LabelVisible>
			<ColorEach>0</ColorEach>
			<Visible>1</Visible>
		  </Serie1>
		  <Serie2>
			<Name>Total</Name>
			<Legend>Total</Legend>
			<PointName>TipoConta</PointName>
			<SerieStyle>0</SerieStyle>
			<LegendVisible>1</LegendVisible>
			<LabelVisible>0</LabelVisible>
			<ColorEach>0</ColorEach>
			<Visible>1</Visible>
		  </Serie2>
		</Series>
	  </Graph>
	</Config>'
)
GO


INSERT INTO ContextosInformacaoRelacionada([IdCategoria],[Contexto],[Ordem],[Params]) 
Values (
	'B80F5924-BA0E-48A9-BA6E-14185C0D1135',
	'frmTabClientes',3,
	'<Config>
	  <General>
		<Expanded>1</Expanded>
	  </General>
	  <Params>
		<Param1>
		  <Name>Entidade</Name>
		  <Value>@CHAVE@</Value>
		</Param1>
	  </Params>
	</Config>'
)
GO


/*
DELETE  from ContextosInformacaoRelacionada WHERE [IdCategoria]='B80F5924-BA0E-48A9-BA6E-14185C0D1135'
DELETE  from InformacaoRelacionada WHERE [Id]='B80F5924-BA0E-48A9-BA6E-14185C0D1135'
GO
*/