/*******************************************************************************************************************
 						    EXEMPLO Paineis Contexto - CI_Registo #1         
 
 Esta script exemplifica o processo de definição de um painel de contexto para a entidade Artigo,
 utilizando o componente CI_Registo.

 O Componente CI_Registo permite exibir uma lista de atributos de uma determinada entidade. 
 Está preparado para exibir atributos de vários tipos tais como: texto, valores numericos, datas, imagens, etc.
 É ainda possivel transformar um atributo num link de navegação (drilldown)
 
 Neste exemplo é criado um painel para exibir as principais caracteristicas de um Artigo.

 Esta script está dividida em 2 secções: 
	 
	PAINEIS:
	Esta secção define os parametros do Componente CI_Registo. Neste exemplo a origem da informação é uma query muito simples 
	centrada na tabela de Artigos.  
	Cada Componente está associado a um tipo de entidade através da Categoria correspondente. Neste caso a Categoria é 'Artigo'

	FORMS:
	A ultima secção destina-se a associar a definição do Componente a um ecra/Formulário. Um Componente pode estar associado 
	a multiplos ecras do ERP, embora neste exemplo apenas a ficha de manutenção do Artigo esteja contemplada.
	
********************************************************************************************************************/


 

/****************************************************************************/
/*						    PAINEIS  			    			            */
/****************************************************************************/
 
 DELETE [dbo].[ContextosInformacaoRelacionada] WHERE IdCategoria = '89E8B451-46DA-48C0-9714-698273F092A8'
GO

DELETE 	[dbo].[InformacaoRelacionada] WHERE Id = '89E8B451-46DA-48C0-9714-698273F092A8'
GO

	--------------------------------------- Painel Artigo - Info Artigo ----------------------------------------------

	
-- Este INSERT parametriza o componente CI_Registo de forma a mostrar os principais atributos de um Artigo
-- Os campos são:
--		[Id]			Identificador do Componente (será usado posteriormente para o associar a um ecra/formulário.
--		[Categoria]		Categoria que identifica o tipo de Entidade a que está associado o Componente
--		[Config]		Parametrização do Componente (neste caso CI_Registo)
--						Na configuração do CI_Registo há alguns elementos a destacar: 
--						<SqlQuery>		A query SQL que alimentará a lista de atributos.
--						<Record>		Definição da lista de atributos a apresentar 
--							<Type>		Normalmente não é necessário especificar o tipo de dados de um atributo já que ele é inferido da 
--										Base de Dados. Neste exemplo, ele é indispensável para o campo 'Imagem' pois o conteudo do atributo 
--										é um texto contendo o caminho para ficheiro da imagem. Assim, é necessário indicar que se trata de uma imagem.
--							<DrillDown> Este elemento é opcional e permite transformar o atributo num link de navegação (drilldown). 


DECLARE @SqlQuery   AS VARCHAR(MAX)
DECLARE @Config     AS VARCHAR(MAX)

SET @SqlQuery = '
		SELECT TOP 1 Imagem = ''ANEXOS\{'' + CONVERT(NVARCHAR(50),ANX.Id) + ''}'' + REVERSE(LEFT(REVERSE(ANX.FicheiroOrig),CHARINDEX(''.'',REVERSE(ANX.FicheiroOrig)))), 
				   A.Artigo,A.Descricao ArtigoDesc, A.TipoArtigo, TA.Descricao TipoArtigoDesc,
				   A.Familia, F.Descricao FamiliaDesc,SF.Descricao SubFamiliaDesc,
				   A.Marca, M.Descricao MarcaDesc,Mo.Descricao ModeloDesc,
				   A.UltimoTipoDoc +'' ''+A.UltimaSerieDoc +''/''+CAST(A.UltimoNumDoc as CHAR(9)) UltimoDoc, 
				   ''000'' UltimaFilial,A.UltimoTipoDoc,A.UltimaSerieDoc,A.UltimoNumDoc,
				   A.UltimoFornecedor, Fo.Nome UltimoFornecedorDesc,
				   A.STKActual,A.STKMinimo,A.STKMaximo,A.STKReposicao
			FROM  dbo.Artigo A
			INNER JOIN dbo.TiposArtigo TA ON TA.TipoArtigo = A.TipoArtigo 
			LEFT JOIN dbo.Familias F ON F.Familia = A.Familia 
			LEFT JOIN dbo.SubFamilias SF ON SF.SubFamilia = A.SubFamilia 
			LEFT JOIN dbo.Marcas M ON M.Marca  = A.Marca
			LEFT JOIN dbo.Modelos Mo ON Mo.Modelo = A.Modelo 
			LEFT JOIN dbo.Fornecedores Fo ON Fo.Fornecedor = A.UltimoFornecedor
			LEFT JOIN (SELECT TOP 1 * FROM Anexos WHERE Tabela = 4 AND Tipo = ''IPR'' AND Chave = ''@@Entidade@@'') ANX ON ANX.Chave = A.Artigo
			WHERE A.Artigo= ''@@Entidade@@''
			'

SET @Config = '
	<Config>
	  <General>
		<TypeClassName>PriCloudLibrary900.CI_Registo</TypeClassName>
		<Name>Exemplo: Informação do Artigo</Name>
		<Title>@RES_Titulo</Title>
		<Description>@RES_Titulo</Description>
		<ShowDescription>0</ShowDescription>
		<Height>6700</Height>
	  </General>
	  <List>
		<SqlQuery>' + @SqlQuery + '</SqlQuery>
	  </List>
	  <Record>
		<Columns>
		  <Column1>
			<Name>Imagem</Name>
			<Title>@RES_Imagem</Title>
			<Type>IMAGE</Type>
		  </Column1>
		  <Column2>
			<Name>ArtigoDesc</Name>
			<Title>@RES_Artigo</Title>
			 <DrillDown>
			   <Category>mnuTabArtigo</Category>
			   <KeyFields>Artigo</KeyFields>
			</DrillDown>
		  </Column2>
		  <Column3>
			<Name>TipoArtigoDesc</Name>
			<Title>@RES_Tipo</Title>
			 <DrillDown>
			  <Category>mnuTabTiposArtigo</Category>
			  <KeyFields>TipoArtigo</KeyFields>
			</DrillDown>
		  </Column3>
		  <Column4>
			<Name>FamiliaDesc</Name>
			<Title>@RES_Familia</Title>
			<DrillDown>
			  <Category>mnuTabFamilias</Category>
			  <KeyFields>Familia</KeyFields>
			</DrillDown>
		  </Column4>
		  <Column5>
			<Name>SubFamiliaDesc</Name>
			<Title>@RES_SubFamilia</Title>
			<DrillDown>
			  <Category>mnuTabFamilias</Category>
			  <KeyFields>Familia</KeyFields>
			</DrillDown>
		  </Column5>
		  <Column6>
			<Name>MarcaDesc</Name>
			<Title>@RES_Marca</Title>
			 <DrillDown>
			  <Category>mnuTabMarcas</Category>
			  <KeyFields>Marca</KeyFields>
			</DrillDown>
		  </Column6>
		  <Column7>
			<Name>ModeloDesc</Name>
			<Title>@RES_Modelo</Title>
			<DrillDown>
			  <Category>mnuTabMarcas</Category>
			  <KeyFields>Marca</KeyFields>
			</DrillDown>
		  </Column7>
		  <Column8>
			<Name>UltimoDoc</Name>
			<Title>@RES_UltimaEntrStk</Title>
			<DrillDown>
				<Event>GCP_EDITARDOCUMENTO</Event> 
				<Category>C</Category>
				<KeyFields>UltimaFilial,UltimoTipoDoc,UltimaSerieDoc,UltimoNumDoc</KeyFields>
			</DrillDown>
		  </Column8>
		   <Column9>
			<Name>UltimoFornecedorDesc</Name>
			<Title>@RES_UltimoFornecedor</Title>
			<DrillDown>
			  <Category>mnuTabFornecedores</Category>
			  <KeyFields>UltimoFornecedor</KeyFields>
			</DrillDown>
		  </Column9>
		  <Column10>
			<Name>STKActual</Name>
			<Title>@RES_StockActual</Title>
		  </Column10>
		  <Column11>
			<Name>STKMinimo</Name>
			<Title>@RES_StockMinimo</Title>
		  </Column11>
		  <Column12>
			<Name>STKMaximo</Name>
			<Title>@RES_StockMaximo</Title>
		  </Column12>
		  <Column13>
			<Name>STKReposicao</Name>
			<Title>@RES_StockReposicao</Title>
		  </Column13>
		</Columns>
	  </Record>
	</Config>
	'

	 
	SET @Config = REPLACE(@Config, '@RES_Titulo', 'Exemplo: Informação do Artigo')
	SET @Config = REPLACE(@Config, '@RES_Imagem', 'Imagem')
	SET @Config = REPLACE(@Config, '@RES_Artigo', 'Artigo')
	SET @Config = REPLACE(@Config, '@RES_Tipo', 'Tipo')
	SET @Config = REPLACE(@Config, '@RES_Familia', 'Família')
	SET @Config = REPLACE(@Config, '@RES_SubFamilia', 'SubFamília')
	SET @Config = REPLACE(@Config, '@RES_Marca', 'Marca')
	SET @Config = REPLACE(@Config, '@RES_Modelo', 'Modelo')
	SET @Config = REPLACE(@Config, '@RES_UltimaEntrStk', 'Última entrada em Stock')
	SET @Config = REPLACE(@Config, '@RES_UltimoFornecedor', 'Último fornecedor')
	SET @Config = REPLACE(@Config, '@RES_StockActual', 'Stock actual')
	SET @Config = REPLACE(@Config, '@RES_StockMinimo', 'Stock mínimo')
	SET @Config = REPLACE(@Config, '@RES_StockMaximo', 'Stock máximo')
	SET @Config = REPLACE(@Config, '@RES_StockReposicao', 'Stock reposição')
		 

	INSERT INTO [dbo].[InformacaoRelacionada]
			   ([Id]
			   ,[Categoria]
			   ,[Config])
		 VALUES
			   ('89E8B451-46DA-48C0-9714-698273F092A8',
				'Artigo',@Config)
GO
 
  
GO

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

-- Info Artigo
INSERT INTO [dbo].[ContextosInformacaoRelacionada]
           ([IdCategoria]
           ,[Contexto]
           ,[Ordem]
           ,[Params])
     VALUES
           ('89E8B451-46DA-48C0-9714-698273F092A8'
           ,'frmTabArtigos'
           ,1
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

 