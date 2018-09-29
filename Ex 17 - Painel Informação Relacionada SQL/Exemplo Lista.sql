/*******************************************************************************************************************
 						    EXEMPLO Paineis Contexto - CI_Lista #1         
 
 Esta script exemplifica o processo de definição de um painel de contexto para a entidade Artigo,
 utilizando o componente CI_Lista.

 O Componente CI_Lista permite exibir uma tabela de dados na forma tabular ou gráfico. 
 Os dois modos de exibição são independentes e opcionais.
 
 Neste exemplo pretende-se mostrar uma grelha com a disponibilidade de stock para um determinado Artigo
 nos diversos Armazens.

 Esta script está dividida em 2 secções: 

	PAINEIS:
	Esta secção define os parametros do Componente CI_Lista. Neste exemplo a origem da informação é uma query sobre a tabela: ArtigoArmazem.
	Cada Componente está associado a um tipo de entidade através da Categoria correspondente. Neste caso a Categoria é 'Artigo'
 
	FORMS:
	A ultima secção destina-se a associar a definição do Componente a um ecra/Formulário. Um Componente pode estar associado 
	a multiplos ecras do ERP, embora neste exemplo apenas a ficha de manutenção do Artigo esteja contemplada.
	
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
-- Os campos são:
--		[Id]			Identificador do Componente (será usado posteriormente para o associar a um ecra/formulário.
--		[Categoria]		Categoria que identifica o tipo de Entidade a que está associado o Componente
--		[Config]		Parametrização do Componente (neste caso CI_Lista)
--						Os elementos principais desta configuração são:
--						<TypeClassName> Aqui deve ser colocado o nome do componente ActiveX que será incluido no Painel
--						<SqlQuery>		A query SQL que alimentará a grelha e respectivo gráfico.
--						<Grid>			Definição das colunas da grelha
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
			<Title>Armazém</Title>
		  </Column1>
		  <Column2>
			<Name>Localizacao</Name>
			<Title>Localização</Title>
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
			<Title>Em trânsito</Title>
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
-- Este INSERT associa o Painel/Componente definido anteriormente ao ecra/formulário: Ficha de Manutenção do Artigo
-- Os campos são:
--		[IdCategoria]	Identificador do Componente criado anteriomente
--		[Contexto]		O nome lógico do Form VB do ERP onde o Componente deve ser exibido
--		[Ordem]			Posição\Ordem em que o Componente deve aparecer caso existam outros para o mesmo Form e Categoria
--		[Params]		Lista de Parametros a transferir do Formulário para o Componente. Tipicamente existe apenas 
--						1 parametro: @CHAVE@ contendo a chave primária da entidade em causa e que será aplicado directamente 
--						na query SQL (ie: @CHAVE@ = @@Entidade@@ = Código do Artigo)
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
 