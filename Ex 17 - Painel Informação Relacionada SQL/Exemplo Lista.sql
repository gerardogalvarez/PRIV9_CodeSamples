/*******************************************************************************************************************
 						    EXEMPLO Paineis Contexto - CI_Lista #1         
 
 Esta script exemplifica o processo de defini��o de um painel de contexto para a entidade Artigo,
 utilizando o componente CI_Lista.

 O Componente CI_Lista permite exibir uma tabela de dados na forma tabular ou gr�fico. 
 Os dois modos de exibi��o s�o independentes e opcionais.
 
 Neste exemplo pretende-se mostrar uma grelha com a disponibilidade de stock para um determinado Artigo
 nos diversos Armazens.

 Esta script est� dividida em 2 sec��es: 

	PAINEIS:
	Esta sec��o define os parametros do Componente CI_Lista. Neste exemplo a origem da informa��o � uma query sobre a tabela: ArtigoArmazem.
	Cada Componente est� associado a um tipo de entidade atrav�s da Categoria correspondente. Neste caso a Categoria � 'Artigo'
 
	FORMS:
	A ultima sec��o destina-se a associar a defini��o do Componente a um ecra/Formul�rio. Um Componente pode estar associado 
	a multiplos ecras do ERP, embora neste exemplo apenas a ficha de manuten��o do Artigo esteja contemplada.
	
********************************************************************************************************************/


 


/****************************************************************************/
/*						    PAINEIS  			    			            */
/****************************************************************************/
 
DELETE [dbo].[ContextosInformacaoRelacionada] WHERE IdCategoria = '65336A27-A96B-4CD2-8ED7-70AD00539888'
GO

DELETE 	[dbo].[InformacaoRelacionada] WHERE Id = '65336A27-A96B-4CD2-8ED7-70AD00539888'
GO
	 
	--------------------------------------- Painel Artigo - Disponibilidades stock ----------------------------------------------

-- Este INSERT parametriza o componente CI_Lista de forma a mostrar o conteudo da tabela ArtigoArmazem
-- para o Artigo contextualizado.
-- Os campos s�o:
--		[Id]			Identificador do Componente (ser� usado posteriormente para o associar a um ecra/formul�rio.
--		[Categoria]		Categoria que identifica o tipo de Entidade a que est� associado o Componente
--		[Config]		Parametriza��o do Componente (neste caso CI_Lista)
--						Os elementos principais desta configura��o s�o:
--						<TypeClassName> Aqui deve ser colocado o nome do componente ActiveX que ser� incluido no Painel
--						<SqlQuery>		A query SQL que alimentar� a grelha e respectivo gr�fico.
--						<Grid>			Defini��o das colunas da grelha
	INSERT INTO [dbo].[InformacaoRelacionada]
			   ([Id]
			   ,[Categoria]
			   ,[Config])
		 VALUES
			   ('65336A27-A96B-4CD2-8ED7-70AD00539888',
				'Artigo',
	'<Config>
	  <General>
		<TypeClassName>PriCloudLibrary900.CI_Lista</TypeClassName>
		<Name>Exemplo: DisponibilidadesStock</Name>
		<Title>Exemplo: Disponibilidades de Stock</Title>
		<Description>Disponibilidades de Stock</Description>
		<ShowDescription>0</ShowDescription>
		<Height>2000</Height>
	  </General>
	  <List>
		<SqlQuery>
			  SELECT AA.Armazem,AA.Localizacao,AA.StkActual,AA.QtReservada,AA.QtTransito       
			  FROM dbo.ArtigoArmazem AA
			  WHERE AA.Artigo= ''@@Entidade@@'' AND AA.StkActual > 0
		</SqlQuery>
	  </List>
	  <Grid>
		<ShowGroupBox>0</ShowGroupBox>
		<ShowFooterRows>1</ShowFooterRows>
		<Visible>1</Visible>
		<Columns>
		  <Column1>
			<Name>Armazem</Name>
			<Title>Armaz�m</Title>
		  </Column1>
		  <Column2>
			<Name>Localizacao</Name>
			<Title>Localiza��o</Title>
		  </Column2>
		  <Column3>
			<Name>StkActual</Name>
			<Title>Stock actual</Title>
			<ShowTotal>1</ShowTotal>
		  </Column3>
		   <Column4>
			<Name>QtReservada</Name>
			<Title>Reservado</Title>
		  </Column4>
		  <Column5>
			<Name>QtTransito</Name>
			<Title>Em tr�nsito</Title>
		  </Column5>
		</Columns>
	  </Grid>
	  <Graph>
		<Visible>0</Visible>
	  </Graph>
	</Config>'
	           
	)
 
   
 
/****************************************************************************/
/*						FORMS												*/
/****************************************************************************/

  

 ------------------------------ frmTabArtigos ----------------------------------

-- Ficha do Artigo
-- Este INSERT associa o Painel/Componente definido anteriormente ao ecra/formul�rio: Ficha de Manuten��o do Artigo
-- Os campos s�o:
--		[IdCategoria]	Identificador do Componente criado anteriomente
--		[Contexto]		O nome l�gico do Form VB do ERP onde o Componente deve ser exibido
--		[Ordem]			Posi��o\Ordem em que o Componente deve aparecer caso existam outros para o mesmo Form e Categoria
--		[Params]		Lista de Parametros a transferir do Formul�rio para o Componente. Tipicamente existe apenas 
--						1 parametro: @CHAVE@ contendo a chave prim�ria da entidade em causa e que ser� aplicado directamente 
--						na query SQL (ie: @CHAVE@ = @@Entidade@@ = C�digo do Artigo)
INSERT INTO [dbo].[ContextosInformacaoRelacionada]
           ([IdCategoria]
           ,[Contexto]
           ,[Ordem]
           ,[Params])
     VALUES
           ('65336A27-A96B-4CD2-8ED7-70AD00539888'
           ,'frmTabArtigos'
           ,2
           ,
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
</Config>')
GO
 