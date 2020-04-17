using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interop.StdClasses900;

namespace EXTAudit900
{
    // Geral: Exemplo de implementação do audit para aplicações de parceiros.
    // Class: Implementar aqui as permissões da aplicação.
    // Nome classe: cls<AbtAPL>OperacoesAplicacao.
    //      AbtAPL: Abreviatura da aplicação e deve ter três caracteres.

    public class clsEXTOperacoesAplicacao: clsAplAudit
    {
        #region private properties

            private string Instancia { get; set; }
            private string Utilizador { get; set; }
            private string PassWord { get; set; }

        #endregion

        #region _clsAplAudit Members
            /// <summary>
            /// Adicionar aqui as permissões de acesso às operações da aplicação terceira.
            /// </summary>
            /// <param name="objParametros"></param>
            /// <returns>Operações associadas à aplicação.</returns>
            clsArvoreOperacoes _clsAplAudit.get_ArvOperacoes(ref clsParamOpsAplicacao objParametros)
            {
                clsArvoreOperacoes objOps = new clsArvoreOperacoes();
                clsOperacaoApl objOp;

                objOp = objOps.Add("mnuOperacao", "Operaciones", 0, "");
                objOp = objOps.Add("mnuOperacao1", "Operación 1", 0, "mnuOperacao");
                objOp = objOps.Add("mnuOperacao2", "Operación 2", 0, "mnuOperacao");

                return objOps;
            }
            
            /// <summary>
            /// Adicionar aqui as permissões dinamicas da aplicação. Este tipo de operações
            /// pode ser preenchido com base numa query SQL (ex: tipos de documentos).
            /// </summary>
            /// <param name="objParametros"></param>
            /// <returns></returns>
            clsPermissoesVar _clsAplAudit.get_PermissoesDinamicas(ref clsParamOpsAplicacao objParametros)
            {
                clsPermissoesVar objVars=new clsPermissoesVar();
                clsPermissaoVar objVar;
                
                

                objVar = objVars.Add("Documento", "Documento", "FA", "Fatura da Sorte.", objParametros.get_Empresa());
                objVar.OperacoesPossiveis.Add("CRIAR", "Crear");
                objVar.OperacoesPossiveis.Add ("MODIFICAR", "Modificar");
                objVar.OperacoesPossiveis.Add ("ANULAR", "Anular");
                objVar.OperacoesPossiveis.Add ("VISUALIZAR", "Visualizar");

                return objVars;
            }

        #endregion
    }
}
